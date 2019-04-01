using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Course> Course { get; }

        IApplicationUserRepository ApplicationUser { get; }

        IRepository<Raitings> Raitings { get; }

        IRepository<UserCourse> UserCourse { get; }

        void Save();
    }
}
