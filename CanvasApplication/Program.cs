using Class.Library.Canvas.Models.Courses;
using Class.Library.Canvas.Models.People;
using System.Globalization;
﻿using CanvasApplication.Helpers;

namespace CanvasApplication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var personHelper = new PersonHelper();
            var courseHelper = new CourseHelper();
            bool cont = true;

            while (cont)
            {
                Console.WriteLine("1. Access Persons");
                Console.WriteLine("2. Access Courses");
                Console.WriteLine("3. Exit");
                var input = Console.ReadLine();
                if (int.TryParse(input, out int result))
                {
                    if (result == 1)
                    {
                        ShowPersonMenu(personHelper, courseHelper);
                    }
                    else if (result == 2)
                    {
                        ShowCourseMenu(courseHelper);
                    }
                    else if (result == 3)
                    {
                        cont = false;
                    }
                }

            }

        }

        static void ShowPersonMenu(PersonHelper personHelper, CourseHelper courseHelper )
        {
            Console.WriteLine("Choose an action:");
            Console.WriteLine("1. Add a New Person");
            Console.WriteLine("2. Update a Person");
            Console.WriteLine("3. Retrieve Grade");
            Console.WriteLine("4. Retrieve GPA");
            Console.WriteLine("5. List all People");
            Console.WriteLine("6. Search for a Person");

            var input = Console.ReadLine();
            if (int.TryParse(input, out int result))
            {
                if (result == 1)
                {
                    personHelper.CreatePersonRecord();
                }
                else if (result == 2)
                {
                    personHelper.UpdateStudentRecord();
                } 
                else if (result == 3)
                {
                    Console.WriteLine($"Recieved Grade: {courseHelper.GetAverageGrade()}%");
                }
                else if (result == 4)
                {
                    Console.WriteLine($"Student's GPA: {courseHelper.GetGPA()}");
                }
                else if (result == 5)
                {
                    personHelper.ListStudents();
                }
                else if (result == 6)
                {
                    personHelper.SearchStudents();
                }
            }
        }

        static void ShowCourseMenu(CourseHelper courseHelper)
        {
            Console.WriteLine("1. Create Course");
            Console.WriteLine("2. Update Course");
            Console.WriteLine("3. Update Student on Course");
            Console.WriteLine("4. Update Assignments on Course");
            Console.WriteLine("5. Manage Assignment Groups");
            Console.WriteLine("6. Manage Modules");
            Console.WriteLine("7. Manage Announcements");
            Console.WriteLine("8. Turn In Assignment");
            Console.WriteLine("9. Grade an Assignment");
            Console.WriteLine("10. List all courses");
            Console.WriteLine("11. Search for a course");
            Console.WriteLine("12. Exit");


            var input = Console.ReadLine();
            if (int.TryParse(input, out int result))
            {
                if (result == 1)
                {
                    courseHelper.CreateCourse();
                }
                else if (result == 2)
                {
                    courseHelper.UpdateCourse();
                }
                else if (result == 3)
                {
                    courseHelper.UpdateStudentMenu();
                }
                else if (result == 4)
                {
                    courseHelper.UpdateAssignmentMenu();
                }
                else if (result == 5)
                {
                    courseHelper.UpdateAssignmentGroupMenu();
                }
                else if (result == 6)
                {
                    courseHelper.UpdateModuleMenu();
                }
                else if (result == 7)
                {
                    courseHelper.ManageAnnouncements();
                }
                else if (result == 8)
                {
                    courseHelper.TurnInAssignment();
                }
                else if (result == 9)
                {
                    courseHelper.GradeAssignment();
                }
                else if (result == 10)
                {
                    courseHelper.ListCourses();
                }
                else if (result == 11)
                {
                    courseHelper.SearchCourses();
                }
            }
        }
    }
}