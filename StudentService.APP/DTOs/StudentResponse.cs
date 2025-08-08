namespace StudentService.APP.DTOs
{
    public record StudentResponse
    {
        public string? Name { get; set; }

        public DateOnly DOB { get; set; }

        public string? Gender { get; set; }

        public int ClassId { get; set; }

        public string Message { get; set; }

    }
}
