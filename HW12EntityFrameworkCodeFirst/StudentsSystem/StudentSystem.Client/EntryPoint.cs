namespace StudentSystem.Client
{
    using System;
    using System.Data.Entity;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using StudentsSystem.Data;
    using StudentsSystem.Data.Migrations;
    using StudentsStystem.Models;

    public class EntryPoint
    {
        public static void Main(string[] args)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<StudentsDbContext, Configuration>());

            var dbContext = new StudentsDbContext();
            SeedData(dbContext);

            var studentNames = dbContext.Students.Select(s => s.Name).ToList();

            foreach (var studentName in studentNames)
            {
                Console.WriteLine(studentName);
            }

        }

        private static void SeedData(StudentsDbContext dbContext)
        {
            var generator = new DataGenerator();

            var students = generator.GenerateStudents(10);

            var courses = generator.GenerateCourses(10);

            var index = 0;
            foreach (var student in students)
            {
                student.Courses = courses.Skip(index).ToList();
                dbContext.Students.Add(student);
            }

            dbContext.SaveChanges();
            dbContext.Dispose();

            dbContext = new StudentsDbContext();

            var homeworks = generator.GenerateHomeworks(5);
            var actualStudents = dbContext.Students.ToList();
            var actualCourses = dbContext.Courses.ToList();

            var studentsToSkip = 0;
            foreach (var homework in homeworks)
            {
                homework.Student = actualStudents.Skip(studentsToSkip).FirstOrDefault();
                homework.Course = actualCourses.Skip(studentsToSkip).FirstOrDefault();
                dbContext.Homeworks.Add(homework);
            }

            dbContext.SaveChanges();
        }
    }
}
