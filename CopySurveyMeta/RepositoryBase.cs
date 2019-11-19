using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace CopySurveyMeta
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
            var stringOfColumns = string.Join(", ", columns.Where(x => x.ToUpper() != "ID").Select(x => $"[{x}]"));
            var stringOfParameters = string.Join(", ", columns.Where(x => x.ToUpper() != "ID").Select(e => "@" + e));
            var query = $"insert into [{prefix}].[{typeof(T).Name}] ({stringOfColumns}) values ({stringOfParameters}); select * from [{prefix}].[{typeof(T).Name}]  where ID = (SELECT CAST(SCOPE_IDENTITY() AS BIGINT))";
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
            var stringOfColumns = string.Join(", ", columns.Where(x => x.ToUpper() != "ID").Select(x => $"[{x}]"));
            var stringOfParameters = string.Join(", ", columns.Where(x => x.ToUpper() != "ID").Select(e => "@" + e));
            var query = $"insert into [{prefix}].[{typeof(T).Name}] ({stringOfColumns}) values ({stringOfParameters})";
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
            var query = $"delete from [{prefix}].[{typeof(T).Name}] ";
            if (where != null) {
                query += query + where;
            }
            if (_transaction != null)
            {
                _connect.Execute(query, entity, _transaction);
            }
            else
            {
                _connect.Execute(query, entity);
            }
        }
        public virtual void Update(T entity)
        {
            var columns = GetColumns();
            var stringOfColumns = string.Join(", ", columns.Select(e => $"{e} = @{e}"));
            var query = $"update [{prefix}].[{typeof(T).Name}] set {stringOfColumns} where Id = @Id";
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
            var query = $"select * from [{prefix}].[{typeof(T).Name}] ";

            if (!string.IsNullOrWhiteSpace(where))
                query += where;
            if (_transaction != null)
            {
                return _connect.Query<T>(query.Trim(),new { }, _transaction);
            }
            else
            {
                return _connect.Query<T>(query.Trim(), new { });
            }
        }
        public virtual IEnumerable<T> Query(object entity, string where = null)
        {
            var query = $"select * from [{prefix}].[{typeof(T).Name}] ";
            // var a = GetColumnsObject(entity.GetType());
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
            var query = $"select * from [{prefix}].[{typeof(T).Name}] ";

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
            return typeof(T)
                    .GetProperties()
                    .Where(e => e.Name != "Id" && !e.PropertyType.GetTypeInfo().IsGenericType)
                    .Select(e => e.Name);
        }
        private IEnumerable<string> GetColumnsObject(Type entity)
        {
            return entity
                    .GetProperties()
                    .Where(e => e.Name != "Id" && !e.PropertyType.GetTypeInfo().IsGenericType)
                    .Select(e => e.Name);
        }
    }
}