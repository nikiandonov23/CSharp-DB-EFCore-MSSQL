using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace MiniORM
{
    public class ChangeTracker<T>
    where T : class,new()
    {
        private readonly ICollection<T> allEntities;
        private readonly ICollection<T> added;
        private readonly ICollection<T> removed;

        public IReadOnlyCollection<T> AllEntities => this.allEntities.ToList().AsReadOnly();
        public IReadOnlyCollection<T> Added => this.added.ToList().AsReadOnly(); 
        public IReadOnlyCollection<T> Removeed => this.removed.ToList().AsReadOnly();


        public void Add(T entity)
        {
            this.added.Add(entity);
        }

        public void Remove(T entity)
        {
            this.removed.Add(entity);
        }

        public IEnumerable<T> GetModifiedEntities(DbSet<T> dbSet)
        {
            ICollection<T> modifiedEntities = new HashSet<T>();
            PropertyInfo[] primaryKeys = typeof(T)
                .GetProperties()
                .Where(pi => pi.HasAttribute<KeyAttribute>())
                .ToArray();

            foreach (var proxyEntity in this.AllEntities)
            {
                IEnumerable<object> proxyPrimaryKeys = GetPrimaryKeyValues(primaryKeys, proxyEntity);


                T dbSetEntity = dbSet
                    .Entities
                    .Single(e => GetPrimaryKeyValues(primaryKeys, e).SequenceEqual(proxyPrimaryKeys));

                bool isEntityModified = this.isModified(proxyEntity, dbSetEntity);
                if (isEntityModified)
                {
                    modifiedEntities.Add(dbSetEntity);
                }
            }

            return modifiedEntities;
            


        }

        private bool isModified(T proxyEntity, T dbSetEntity)
        {
            PropertyInfo[] monitoredEntities = typeof(T)
                .GetProperties()
                .Where(pi => DbContext.allowedSqlTypes.Contains(pi.PropertyType))
                .ToArray();
            foreach (PropertyInfo pi in monitoredEntities)
            {
                object? proxyEntityValue = pi.GetValue(proxyEntity);
                object? dbSetEntityValue = pi.GetValue(dbSetEntity);
                if (proxyEntityValue==null&&dbSetEntityValue==null)
                {
                    continue;
                }

                if (!proxyEntityValue!.Equals(dbSetEntityValue))
                {
                    return true;
                }
            }

            return false;
        }

        private static IEnumerable<object> GetPrimaryKeyValues(IEnumerable<PropertyInfo> primaryKeys, T entity)
        {
            ICollection<object> primaryKeyValues = new HashSet<object>();
            foreach (PropertyInfo primaryKeyInfo in primaryKeys)
            {
                object? primaryKeyValue = primaryKeyInfo.GetValue(entity);
                if (primaryKeyValue == null)
                {
                    throw new ArgumentNullException(primaryKeyInfo.Name,ErrorMessages.PrimaryKeyErrorMessage);
                }

                primaryKeyValues.Add(primaryKeyValue);
            }

            return primaryKeyValues;
        }



        public ChangeTracker(IEnumerable<T>entities)
        {
            this.allEntities = this.cloneEntities(entities);
            this.added = new HashSet<T>();
            this.removed = new HashSet<T>();
        }

      

        private ICollection<T> cloneEntities(IEnumerable<T> entities)
        {
            ICollection<T> clonedEntities = new HashSet<T>();
            PropertyInfo[] properetiesToClone = typeof(T)
                .GetProperties()
                .Where(pi => DbContext.allowedSqlTypes.Contains(pi.PropertyType))
                .ToArray();

            foreach (T entity in entities)
            {
                T entityClone = Activator.CreateInstance<T>();

                foreach (PropertyInfo propertyInfo  in properetiesToClone)
                {
                    object? originalEntityValue = propertyInfo.GetValue(entity);
                    propertyInfo.SetValue(entityClone,originalEntityValue);
                }
                clonedEntities.Add(entityClone);
            }

            return clonedEntities;
        }

       

    }
}