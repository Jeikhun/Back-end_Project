using NuGet.ContentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_end_Project.Models
{
	public class Course : BaseModel
	{
		[Required]
		public string Name { get; set; }
		[Required]
		public string Description { get; set; }
		[Required]
		public string AboutCourse { get; set; }
		[Required]
		public string Apply { get; set; }
		[Required]
		public string Certificiation { get; set; }
		[Required]
		public DateTime StartDate { get; set; }
		[Required]
		public DateTime EndDate { get; set; }
        [Required]
        public string CourseDuration { get; set; }
        public double ClassDuration { get; set; }
		[Required]
		public string SkillLevel { get; set; }
		[Required]
		public int? CLanguageId { get; set; }
		public Language? Language { get; set; }
		[Required]
		public int StudentCount { get; set; }
		[Required]
		public double CourseFee { get; set; }
		[Required]
		public int? CAssetsId { get; set; }
		public CAssets? CAssets { get; set; }
		public List<CourseCategory>? courseCategories { get; set; }
		[NotMapped]
		public List<int> CategoryIds { get; set; }
		public string? Image { get; set; }
		[NotMapped]
		public IFormFile? FormFile { get; set; }//yox day hechne ishinde ol
	}
}
