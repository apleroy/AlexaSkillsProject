using System;

namespace AlexaSkillProject.Repository
{
    /// <summary>
    /// Defines the UnitOfWork interface to implement IDisposable
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        
        IAlexaMemberRepository AlexaMembers { get; }
        IAlexaRequestRepository AlexaRequests { get; }
        IWordRepository Words { get; }

        int Complete();
    }
}
