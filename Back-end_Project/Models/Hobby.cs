using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Back_end_Project.Models
{
    public class Hobby:BaseModel
    {

        public int Id { get; set; }

        [StringLength(maximumLength: 50)]

        public string? Name { get; set; }

        public List<TeacherHobbies>? TeacherHobbies { get; set; }
    }
}
