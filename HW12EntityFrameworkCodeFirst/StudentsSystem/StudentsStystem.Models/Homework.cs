namespace StudentsStystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Homework
    {

        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Content { get; set; }

        public DateTime TimeSent { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }

        public int StudentId { get; set; }

        public Student Student { get; set; }
       
    }
}
