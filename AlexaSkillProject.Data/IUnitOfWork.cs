using AlexaSkillProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlexaSkillProject.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        
        IAlexaMemberRepository AlexaMembers { get; }
        IAlexaRequestRepository AlexaRequests { get; }
        IWordRepository Words { get; }
        int Complete();
    }
}
