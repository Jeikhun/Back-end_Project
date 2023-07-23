using Back_end_Project.Models;

namespace Back_end_Project.ViewModels
{
    public class TeacherVM
    {
        public Teacher teacher { get; set; }
        public ICollection<Networks>? networks { get; set; }
        public ICollection<Teacher>? teachers { get; set; }
        public List<Networks> network { get; set; }

    }
}
