namespace StudentService.APP.DTOs
{
    public record GetStudentResponse
    {
        public string? Name { get; set; }

        public DateOnly DOB { get; set; }

        public string? Gender { get; set; }

        public int ClassId { get; set; }
        public List<MarkDTO> Marks { get; set; } = new();

    }
}
