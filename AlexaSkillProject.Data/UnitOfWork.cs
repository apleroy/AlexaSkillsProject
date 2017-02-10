using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexaSkillProject.Repository
{
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

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
