using DAL.Context;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;

        public UnitOfWork()
        {
            GenerateContext generateContext = new GenerateContext();
            string[] param = new string[0];
            _context = generateContext.CreateDbContext(param);
        }

        private IRepository<Course> _course;
        public IRepository<Course> Course
        {
            get
            {
                if (_course == null)
                    _course = new EntityBaseRepository<Course>(_context);
                return _course;
            }
        }

        private IApplicationUserRepository _applicationUser;
        public IApplicationUserRepository ApplicationUser
        {
            get
            {
                if (_applicationUser == null)
                    _applicationUser = new ApplicationUserRepository(_context);
                return _applicationUser;
            }
        }

        private IRepository<Raitings> _raitings;
        public IRepository<Raitings> Raitings
        {
            get
            {
                if (_raitings == null)
                    _raitings = new EntityBaseRepository<Raitings>(_context);
                return _raitings;
            }
        }

        private IRepository<UserCourse> _userCourse;
        public IRepository<UserCourse> UserCourse
        {
            get
            {
                if (_userCourse == null)
                    _userCourse = new EntityBaseRepository<UserCourse>(_context);
                return _userCourse;
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if(!this.disposed)
            {
                if(disposing)
                {
                    _context?.Dispose();
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            _context?.SaveChanges();
        }
    }
}
