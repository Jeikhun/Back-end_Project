using Back_end_Project.Models;

namespace Back_end_Project.ViewModels
{
    public class ViewModel
    {
        public ICollection<Notice> notices { get; set; }
        public ICollection<Slide> slides { get; set; }
        public ICollection<Course> courses { get; set; }
    }
}
