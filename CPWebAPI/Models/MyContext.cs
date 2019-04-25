namespace CPWebAPI.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MyContext : DbContext
    {
        public MyContext()
            : base("name=MyContext")
        {
        }

        public virtual DbSet<Announcement> Announcement { get; set; }
        public virtual DbSet<Class> Class { get; set; }
        public virtual DbSet<Discontinuity> Discontinuity { get; set; }
        public virtual DbSet<Grade> Grade { get; set; }
        public virtual DbSet<Group> Group { get; set; }
        public virtual DbSet<Lesson> Lesson { get; set; }
        public virtual DbSet<Management> Management { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<Questions> Questions { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<Teacher> Teacher { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>()
                .HasMany(e => e.Student)
                .WithOptional(e => e.Class)
                .HasForeignKey(e => e.Class_Id);

            modelBuilder.Entity<Group>()
                .HasMany(e => e.Student)
                .WithOptional(e => e.Group)
                .HasForeignKey(e => e.Group_Id);

            modelBuilder.Entity<Lesson>()
                .HasMany(e => e.Teacher)
                .WithRequired(e => e.Lesson)
                .HasForeignKey(e => e.Lesson_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Discontinuity)
                .WithOptional(e => e.Student)
                .HasForeignKey(e => e.Student_Id);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Grade)
                .WithOptional(e => e.Student)
                .HasForeignKey(e => e.Student_Id);

            modelBuilder.Entity<Student>()
                .HasMany(e => e.Payment)
                .WithOptional(e => e.Student)
                .HasForeignKey(e => e.Student_Id);
        }
    }
}
