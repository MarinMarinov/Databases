namespace StudentsStystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Course
    {
        private ICollection<Student> students;
        private ICollection<Homework> homeworks;

        public Course()
        {
            this.students = new HashSet<Student>();
            this.homeworks = new HashSet<Homework>();
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(45)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [MaxLength(500)]
        public string Materials { get; set; }

        public virtual ICollection<Student> Students
        {
            get { return this.students; }

            set { this.students = value; }
        }

        public virtual ICollection<Homework> Homeworks
        {
            get { return this.homeworks; }

            set { this.homeworks = value; }
        }
    }
}
