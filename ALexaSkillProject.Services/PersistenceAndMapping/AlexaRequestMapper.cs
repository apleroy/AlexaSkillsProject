using AlexaSkillProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AlexaSkillProject.Domain;

namespace AlexaSkillProject.Services
{
    public class AlexaRequestMapper : IAlexaRequestMapper
    {
        
        public AlexaRequest MapAlexaRequest(AlexaRequestInputModel alexaRequestInputModel)
        {
            AlexaRequest alexaRequest = new AlexaRequest
            {
                AlexaMemberId = (alexaRequestInputModel.Session.Attributes == null) ? 0 : alexaRequestInputModel.Session.Attributes.MemberId,
                Timestamp = alexaRequestInputModel.Request.Timestamp,
                Intent = (alexaRequestInputModel.Request.Intent == null) ? "" : alexaRequestInputModel.Request.Intent.Name,
                AppId = alexaRequestInputModel.Session.Application.ApplicationId,
                RequestId = alexaRequestInputModel.Request.RequestId,
                SessionId = alexaRequestInputModel.Session.SessionId,
                UserId = alexaRequestInputModel.Session.User.UserId,
                IsNew = alexaRequestInputModel.Session.New,
                Version = alexaRequestInputModel.Version,
                Type = alexaRequestInputModel.Request.Type,
                SlotsList = alexaRequestInputModel.Request.Intent.GetSlots(),
                DateCreated = DateTime.UtcNow
            };

            return alexaRequest;
        }
    }
}
