using Hospital.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories.Implementation
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        private bool disposed = false;
        //dispose manually call korle ati cleanup calay
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                //if dispose is true then context database connection release 
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
            //this check if one time is dispose then it will not dispose again
        }
        //T is either model or class 
        public IGenericRepository<T> GetRepository<T>() where T : class
        {
            return new GenericRepository<T>(_context);
        }
        //ai repository joto kicu change hoice sob save kora holo ai save ar kaj
        public void save()
        {
            _context.SaveChanges();
        }

       
    }
}
