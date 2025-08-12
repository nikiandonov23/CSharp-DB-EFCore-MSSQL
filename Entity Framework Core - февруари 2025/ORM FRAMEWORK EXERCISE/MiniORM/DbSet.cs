// ReSharper disable PossibleMultipleEnumeration
namespace MiniORM
{
    using System.Collections;

    public class DbSet<TEntity> : ICollection<TEntity>
    where TEntity : class,new()
    {
        internal ICollection<TEntity> Entities { get; set; }
        internal ChangeTracker<TEntity> ChangeTracker { get; set; }


        public DbSet(IEnumerable<TEntity> entities)
        {
            this.Entities = entities.ToArray();
            this.ChangeTracker = new ChangeTracker<TEntity>(entities);
        }


        public void Add(TEntity item)
        {
        this.Entities.Add(item);
        this.ChangeTracker.Add(item);
        }

        public void Clear()
        {
            while (this.Entities.Any())
            {
                TEntity entityToRemove = this.Entities.First();
                this.Remove(entityToRemove);
            }
        }

        public bool Contains(TEntity item)
        {
            return this.Entities.Contains(item);
        }

        public void CopyTo(TEntity[] array, int arrayIndex)
        {
            this.Entities.CopyTo(array, arrayIndex);
        }

        public bool Remove(TEntity item)
        {
            if (item==null)
            {
                throw new ArgumentException(ErrorMessages.ElementCannotBeNull);
            }

            bool removedSuccessfully= this.Entities.Remove(item);
            if (removedSuccessfully)
            {
                ChangeTracker.Remove(item);
            }

            return removedSuccessfully;
        }

        public bool RemoveRange(IEnumerable<TEntity> range)
        {
            
            foreach (TEntity entityToRemove in range)
            {
                bool result = this.Remove(entityToRemove);
                if (!result)
                {
                    return false;
                }
            }

            return true;
        }

        public int Count => this.Entities.Count;

        public bool IsReadOnly => this.Entities.IsReadOnly;

        public IEnumerator<TEntity> GetEnumerator()
        {
            return this.Entities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}