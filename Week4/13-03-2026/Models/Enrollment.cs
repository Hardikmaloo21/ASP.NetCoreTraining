namespace CourseManagementAPI.Models
{
    public class Enrollment
    {
        public int EnrollmentId { get; set; }

        public int CourseId { get; set; }

        public string? StudentName { get; set; }   // CHANGE HERE

        public DateTime EnrollmentDate { get; set; }

        public DateTime? DropDate { get; set; }
    }
}