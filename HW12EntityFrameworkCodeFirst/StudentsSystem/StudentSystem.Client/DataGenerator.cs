namespace StudentSystem.Client
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using StudentsStystem.Models;

    public class DataGenerator
    {
        public DataGenerator()
        {
        }

        public List<Student> GenerateStudents(int studentsCount)
        {
            var students = new List<Student>();

            for (int i = 0; i < studentsCount; i++)
            {
                var student = new Student();
                student.Name = "Student #" + i;
                student.FacultyNumber = "123113" + i + "" + (i + 1);
                students.Add(student);
            }

            return students;
        }

        public List<Course> GenerateCourses(int coursesCount)
        {
            var courses = new List<Course>();

            for (int i = 0; i < coursesCount; i++)
            {
                var course = new Course();
                course.Name = "Course #" + i;
                course.Description = "Description #" + i;
                course.Materials = "Materials #" + i;
                courses.Add(course);
            }

            return courses;
        }

        public List<Homework> GenerateHomeworks(int homeworksCount)
        {
            var homeworks = new List<Homework>();

            for (int i = 1; i <= homeworksCount; i++)
            {
                var homework = new Homework();
                homework.Content = "Content #" + i;
                homework.TimeSent = DateTime.Now;
                homeworks.Add(homework);
            }

            return homeworks;
        }
    }
}
