using System;
using System.Data.Common;
using System.Collections.Generic;
using Exam.Services;

namespace Exam.Data
{
    public abstract class DbDataAccess<T> : IDisposable
    {
        protected int _rows;

        protected readonly int _step;
        protected readonly DbProviderFactory factory;
        protected readonly DbConnection connection;

        public DbDataAccess()
        {
            _rows = 10;

            factory = DbProviderFactories.GetFactory("SqlProvider");

            connection = factory.CreateConnection();
            connection.ConnectionString = ConfigurationService.Configuration["ConnectionString"];
            connection.Open();
        }

        public void Dispose()
        {
            connection.Close();
        }

        public abstract void Update(T entity);
        public abstract void Insert(T entity);
        public abstract void Delete(T entity);
        public abstract ICollection<T> Select();
    }
}