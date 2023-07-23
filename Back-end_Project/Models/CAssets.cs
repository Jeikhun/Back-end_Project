using System.ComponentModel.DataAnnotations;

namespace Back_end_Project.Models
{
	public class CAssets : BaseModel
	{
		[Required]
		public string? Name { get; set; }
		public List<Course>? Courses { get; set; }
	}
}
