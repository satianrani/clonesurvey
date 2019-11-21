using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace CopySurveyMeta.DataAccess
{
    public class RepositoryBase<T>
    {
        protected IDbConnection _connect = null;
        protected IDbTransaction _transaction = null;
        protected static string prefix = "dbo";

        public RepositoryBase(DbConnect connection)
        {
            _connect = connection.GetConnection();
        }

        public RepositoryBase(IDbConnection connection, IDbTransaction transaction)
        {
            _connect = connection;
            _transaction = transaction;
        }

        public virtual T Insert(T entity)
        {
            var columns = GetColumns();
            var query = $"insert into {GetTableName()} ({StringBucketSysbolInColumns(columns)}) values ({StringAssignSysbolInColumns(columns)}); select * from {GetTableName()}  where {GetPrimaryKey()} = (SELECT CAST(SCOPE_IDENTITY() AS BIGINT))";
            if (_transaction != null)
            {
                return _connect.Query<T>(query, entity, _transaction).Single();
            }
            else
            {
                return _connect.Query<T>(query, entity).Single();
            }
        }

        public virtual void InsertALL(IEnumerable<T> entity)
        {
            var columns = GetColumns();
            var query = $"insert into {GetTableName()} ({StringBucketSysbolInColumns(columns)}) values ({StringAssignSysbolInColumns(columns)})";
            if (_transaction != null)
            {
                _connect.Execute(query, entity, _transaction);
            }
            else
            {
                _connect.Execute(query, entity);
            }
        }

        public virtual void Delete(object entity, string where = null)
        {
            var query = $"delete from {GetTableName()} ";
            if (where != null)
            {
                query +=  where;
            }
            else
            {
                throw new Exception("delete must where conditions");
            }

            _connect.Execute(query, entity, _transaction);
        }

        public virtual void Update(T entity)
        {
            var columns = GetColumns();
            var query = $"update {GetTableName()} set {StringBucketSysbolInColumns(columns)} where Id = @Id";
            if (_transaction != null)
            {
                _connect.Execute(query, entity, _transaction);
            }
            else
            {
                _connect.Execute(query, entity);
            }
        }

        public virtual IEnumerable<T> Query(string where = null)
        {
            var query = $"select * from {GetTableName()} ";

            if (!string.IsNullOrWhiteSpace(where))
                query += where;
            if (_transaction != null)
            {
                return _connect.Query<T>(query.Trim(), new { }, _transaction);
            }
            else
            {
                return _connect.Query<T>(query.Trim(), new { });
            }
        }

        public virtual IEnumerable<T> Query(object entity, string where = null)
        {
            var query = $"select * from {GetTableName()} ";
            if (!string.IsNullOrWhiteSpace(where))
                query += where;
            if (_transaction != null)
            {
                return _connect.Query<T>(query.Trim(), entity, _transaction);
            }
            else
            {
                return _connect.Query<T>(query.Trim(), entity);
            }
        }

        public virtual T QueryFirst(object entity, string where = null)
        {
            var query = $"select * from {GetTableName()} ";

            if (!string.IsNullOrWhiteSpace(where))
                query += where;
            if (_transaction != null)
            {
                return _connect.QueryFirst<T>(query.Trim(), entity, _transaction);
            }
            else
            {
                return _connect.QueryFirst<T>(query.Trim(), entity);
            }
        }

        private IEnumerable<string> GetColumns()
        {
            string primaryKeyName = GetPrimaryKey();
            var column = typeof(T)
                    .GetProperties()
                    .Where(e => e.Name.ToUpper() != primaryKeyName && !e.PropertyType.GetTypeInfo().IsGenericType)
                    .Select(e => e.Name).ToList();
            return ChangeColumnMapName(column);
        }

        private string StringBucketSysbolInColumns(IEnumerable<string> columns)
        {
            return string.Join(", ", columns.Select(x => $"[{x}]"));
        }

        private string StringAssignSysbolInColumns(IEnumerable<string> columns)
        {
            return string.Join(", ", columns.Select(x => $"@{x}"));
        }

        private IEnumerable<string> GetColumnsObject(Type entity)
        {
            string primaryKeyName = GetPrimaryKey();
            var column = entity
                    .GetProperties()
                    .Where(e => e.Name != "ID" && !e.PropertyType.GetTypeInfo().IsGenericType)
                    .Select(e => e.Name).ToList();
            return ChangeColumnMapName(column);
        }

        private string GetTableName()
        {
            IEnumerable<CustomAttributeData> table = typeof(T).CustomAttributes;
            string tableName = typeof(T).Name;
            if (table.Any(x => x.AttributeType.Name == "TableAttribute"))
            {
                tableName = table.SelectMany(x => x.ConstructorArguments.Select(c => c.Value.ToString())).FirstOrDefault();
            }
            return $"[{prefix}].[{tableName}]";
        }

        private string GetPrimaryKey()
        {
            string primaryKeyName = "ID";
            var props = typeof(T).GetProperties();
            int countProps = props.Length;
            for (int i = 0; i < countProps; i++)
            {
                if (props[i].CustomAttributes.Any(x => x.AttributeType.Name == "KeyAttribute"))
                {
                    primaryKeyName = props[i].Name;
                    i = countProps;
                }
            }

            return primaryKeyName.ToUpper();
        }

        private IEnumerable<string> ChangeColumnMapName(List<string> columnsNames)
        {
            var props = typeof(T).GetProperties();
            int countProps = props.Length;
            for (int i = 0; i < countProps; i++)
            {
                var test = props[i];
                if (test.CustomAttributes.Any(x => x.AttributeType.Name == "ColumnAttribute"))
                {
                    var data = props[i].CustomAttributes.Where(x => x.AttributeType.Name == "ColumnAttribute").FirstOrDefault().ConstructorArguments.FirstOrDefault().Value.ToString();
                    if (!string.IsNullOrEmpty(data))
                    {
                        columnsNames[columnsNames.IndexOf(test.Name)] = data;
                    }
                }
            }
            return columnsNames;
        }
    }
}