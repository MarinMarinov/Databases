namespace StudentsSystem.Data
{
    using System.Data.Entity;

    using StudentsStystem.Models;

    public class StudentsDbContext : DbContext
    {
        public StudentsDbContext()
            : base("StudentsSystem")
        {
        }

        public virtual IDbSet<Student> Students { get; set; }
        public virtual IDbSet<Course> Courses { get; set; }
        public virtual IDbSet<Homework> Homeworks { get; set; }
    }
}
