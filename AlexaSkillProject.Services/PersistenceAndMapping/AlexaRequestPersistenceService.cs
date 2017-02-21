using AlexaSkillProject.Domain;
using AlexaSkillProject.Repository;
using System;
using System.Linq;

namespace AlexaSkillProject.Services
{
    /// <summary>
    /// Saves the AlexaRequest and Upserts AlexaMember information with the request
    /// </summary>
    public class AlexaRequestPersistenceService : IAlexaRequestPersistenceService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AlexaRequestPersistenceService (IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void PersistAlexaRequestAndMember(AlexaRequest alexaRequest)
        {
            // save request
            AlexaMember alexaMember = _unitOfWork.AlexaMembers.Find(m => m.AlexaUserId == alexaRequest.UserId).FirstOrDefault();

            if (alexaMember == null)
            {
                alexaRequest.AlexaMember = new AlexaMember()
                {
                    AlexaUserId = alexaRequest.UserId,
                    CreatedDate = DateTime.UtcNow,
                    LastRequestDate = DateTime.UtcNow,
                    RequestCount = 1
                };

                _unitOfWork.AlexaRequests.Add(alexaRequest);
            }
            else
            {
                alexaMember.AlexaRequests.Add(alexaRequest);
            }

            _unitOfWork.Complete();
        }
    }
}
