namespace StudentAttendanceManagementSystem.model
{
    public class Attendance
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public DateTime date { get; set; }
        public bool isPresent { get; set; }
    }
}



