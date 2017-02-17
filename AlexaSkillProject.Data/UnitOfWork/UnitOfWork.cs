using System;

namespace AlexaSkillProject.Repository
{
    /// <summary>
    /// Implementation of IUnitOfWork
    /// Responsible for maintaining handle on all repositories
    /// Implements the Dispose method to ensure contexts are deallocated between requests
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AlexaSkillProjectDataContext _context;

        public UnitOfWork(AlexaSkillProjectDataContext context)
        {
            _context = context;
            AlexaMembers = new AlexaMemberRepository(_context);
            AlexaRequests = new AlexaRequestRepository(_context);
            Words = new WordRepository(_context);
        }

        public IAlexaMemberRepository AlexaMembers { get; private set; }
        public IAlexaRequestRepository AlexaRequests { get; private set; }
        public IWordRepository Words { get; private set; }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this._context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
