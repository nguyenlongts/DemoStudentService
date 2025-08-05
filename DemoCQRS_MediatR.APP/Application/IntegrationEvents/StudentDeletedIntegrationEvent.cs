namespace DemoCQRS_MediatR.APP.Application.IntegrationEvents
{
    public class   StudentDeletedIntegrationEvent
    {
        public string Message { get; }
        public bool IsSuccess { get; }
        public int ClassId { get; }
        public StudentDeletedIntegrationEvent(string message, bool isSuccess, int classId)
        {
            Message = message;
            IsSuccess = isSuccess;
            ClassId = classId;
        }
    }

}
