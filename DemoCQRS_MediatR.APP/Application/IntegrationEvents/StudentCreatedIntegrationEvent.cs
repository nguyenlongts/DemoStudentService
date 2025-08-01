namespace DemoCQRS_MediatR.APP.Application.IntegrationEvents
{
    public class StudentCreatedIntegrationEvent
    {
        public int StudentId { get; set; }
        public string Name { get; set; } = null!;
        public DateTime DOB { get; set; }
        public int Gender { get; set; } 
        public int ClassId { get; set; }
    }

}
