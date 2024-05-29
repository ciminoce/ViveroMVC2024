﻿using Microsoft.EntityFrameworkCore.Storage;

namespace ViveroEF2024.Datos
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ViveroDbContext _context;
        private IDbContextTransaction? _transaction;

        public UnitOfWork(ViveroDbContext context)
        {
            _context = context;
        }

        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                SaveChanges();
                _transaction?.Commit();
            }
            catch (Exception)
            {
                Rollback();
                throw;
            }
        }

        public void Rollback()
        {
            _transaction?.Rollback();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
