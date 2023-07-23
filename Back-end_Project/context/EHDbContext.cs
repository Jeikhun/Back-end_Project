﻿using Back_end_Project.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Back_end_Project.context
{
    public class EHDbContext : IdentityDbContext<User>
    {
        public EHDbContext(DbContextOptions<EHDbContext> options, ModelBuilder modelBuilder) : base(options)
        {
            modelBuilder.Entity<CourseTag>()
            .HasOne(p => p.Course)
            .WithMany(b => b.courseTags);
        }
        public DbSet<Slide>? slides { get; set; }
        public DbSet<Notice>? notices { get; set; }
        public DbSet<Information>? information { get; set; }
        public DbSet<Person>? people { get; set; }
        public DbSet<Hobby>? hobbies { get; set; }
        public DbSet<Teacher>? teachers { get; set; }
        public DbSet<TeacherHobbies>? teacherHobbies { get; set; }
        public DbSet<Language>? Languages { get; set; }
        public DbSet<Course>? courses { get; set; }
        public DbSet<CourseCategory>? courseCategories { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<CourseTag>? courseTags { get; set; }
        public DbSet<Tag>? Tags { get; set; }
        public DbSet<Networks>? networks { get; set; }
        public DbSet<CAssets> cAssets { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Course>()
        //        .HasMany(e => e.Tags)
        //        .WithMany(e => e.courses)
        //        .UsingEntity<CourseTag>(
        //            l => l.HasOne<Tag>().WithMany(e => e.courseTag),
        //            r => r.HasOne<Course>().WithMany(e => e.courseTags));
        //}
    }
}
