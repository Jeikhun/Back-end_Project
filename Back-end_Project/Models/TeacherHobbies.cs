﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back_end_Project.Models
{
    public class TeacherHobbies:BaseModel
    {

        public int TeacherId { get; set; }

        public int HobbyId { get; set; }
        public Teacher? Teacher { get; set; }

        public Hobby? Hobby { get; set; }


    }
}
