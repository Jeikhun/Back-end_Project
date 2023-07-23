namespace Back_end_Project.Models
{
    public class CourseTag:BaseModel
    {
		public int TagId { get; set; }

		public Tag? Tag { get; set; }
		public int CourseId { get; set; }
		public Course? Course { get; set; }

	}
}