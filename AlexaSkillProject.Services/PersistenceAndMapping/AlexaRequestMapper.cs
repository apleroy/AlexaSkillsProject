using AlexaSkillProject.Domain;
using System;

namespace AlexaSkillProject.Services
{
    /// <summary>
    /// Maps an AlexaRequestPayload object into SQL savable object
    /// </summary>
    public class AlexaRequestMapper : IAlexaRequestMapper
    {
        
        public AlexaRequest MapAlexaRequest(AlexaRequestPayload alexaRequestInputModel)
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
