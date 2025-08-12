using System.Data.Common;
using System.Reflection;

namespace MiniORM
{
    public class DbContext
    {
        private readonly DbConnection dbConnection;
        private readonly Dictionary<Type, PropertyInfo> dbSetProperties;

        internal static readonly Type[] allowedSqlTypes =
        {
            typeof(string),
            typeof(byte),
            typeof(byte?),
            typeof(sbyte),
            typeof(sbyte?),
            typeof(short),
            typeof(short?),
            typeof(ushort),
            typeof(ushort?),
            typeof(int),
            typeof(int?),
            typeof(uint),
            typeof(uint?),
            typeof(long),
            typeof(long?),
            typeof(ulong),
            typeof(ulong?),
            typeof(float),
            typeof(float?),
            typeof(double),
            typeof(double?),
            typeof(decimal),
            typeof(decimal?),
            typeof(bool),
            typeof(bool?),
            typeof(DateTime),
            typeof(DateTime?),
            typeof(Guid)

        };

        public DbContext(DbConnection dbConnection, Dictionary<Type, PropertyInfo> dbSetProperties)
        {
            this.dbConnection = dbConnection;
            this.dbSetProperties = dbSetProperties;
        }

        // TODO: Create your DbContext class here.
    }
}
