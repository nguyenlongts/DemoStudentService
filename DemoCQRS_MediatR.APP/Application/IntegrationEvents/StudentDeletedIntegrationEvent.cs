namespace DemoCQRS_MediatR.APP.Application.IntegrationEvents
{
    public class   StudentDeletedIntegrationEvent
    {
        public string Message { get; }
        public bool IsSuccess { get; }

        public StudentDeletedIntegrationEvent(string message, bool isSuccess)
        {
            Message = message;
            IsSuccess = isSuccess;
        }
    }

}
