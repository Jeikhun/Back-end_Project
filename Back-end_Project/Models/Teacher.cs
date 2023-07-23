using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Back_end_Project.Models
{
    public class Teacher:BaseModel
    {

        [StringLength(maximumLength: 50)]
        [Required]
        public string? Fullname { get; set; }

        [StringLength(maximumLength: 600)]
        [Required]
        public string? About { get; set; }
        [Required]

        [StringLength(maximumLength: 50)]

        public string? Degree { get; set; }

        [StringLength(maximumLength: 50)]
        [Required]


        public string? Experience { get; set; }

        [StringLength(maximumLength: 70)]
        [DataType(DataType.EmailAddress)]
        [Required]

        public string? Email { get; set; }

        [StringLength(maximumLength: 50)]
        [Required]

        public string? Phone { get; set; }

        [StringLength(maximumLength: 50)]

        public string? Skype { get; set; }

        [StringLength(maximumLength: 75)]
        [Required]

        public string? Faculty { get; set; }


        [StringLength(maximumLength: 70)]
        [Required]
        public string? Speciality { get; set; }

        [StringLength(maximumLength: 120)]


        [NotMapped]
        public IFormFile? FormFile { get; set; }


        public string? Image { get; set; }
        public List<TeacherHobbies>? TeacherHobbies { get; set; }

        [NotMapped]
        //[Required]
        public List<int>? HobbyIds { get; set; }
        public List<Networks>? Networks { get; set; }

    }
}
