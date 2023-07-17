namespace Back_end_Project.Models
{
    public class Networks:BaseModel
    {
        public string Icon { get; set; }
        public string Link { get; set; }
        public int TeacherId { get; set; }
        public Teacher? Teacher { get; set; }

    }
}
