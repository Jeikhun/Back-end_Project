namespace Back_end_Project.Models
{
    public class CourseCategory:BaseModel
    {
        public Course? Course { get; set; }
        public int CourseId { get; set; }
        public Category? Category { get; set; }
        public int CategoryId { get; set; }
    }
}
