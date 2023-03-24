using Class.Library.Canvas.Models.Courses;
using Class.Library.Canvas.Models.People;
using Class.Library.Canvas.Models.Services;
using CanvasApplication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CanvasApplication.Helpers
{
    internal class PersonHelper
    {
        private PersonService personService;
        private CourseService courseService;
        private ListNavigator<Person> personNavigator;

        public PersonHelper()
        {
            personService = PersonService.Current;
            courseService = CourseService.Current;
            personNavigator = new ListNavigator<Person>(personService.Students, 2);
        }


        private bool VerifyID(int id)
        {
            foreach (var obj in personService.Students)
            {
                if (obj.ID == id)
                {
                    return false;
                }
            }

            return true;
        }

        public void CreatePersonRecord(Person? selectedPerson = null)
        {
            bool isCreate = false;
            if (selectedPerson == null)
            {
                isCreate = true;
                Console.WriteLine("What type of person would you like to add?");
                Console.WriteLine("(S)tudent");
                Console.WriteLine("(T)eachingAssistant");
                Console.WriteLine("(I)nstructor");
                var choice = Console.ReadLine() ?? string.Empty;
                if (string.IsNullOrEmpty(choice))
                {
                    return;
                }
                if (choice.Equals("S", StringComparison.InvariantCultureIgnoreCase))
                {
                    selectedPerson = new Student();
                }
                else if (choice.Equals("T", StringComparison.InvariantCultureIgnoreCase))
                {
                    selectedPerson = new TeachingAssistant();
                }
                else if (choice.Equals("I", StringComparison.InvariantCultureIgnoreCase))
                {
                    selectedPerson = new Instructor();
                }
            }

            Console.WriteLine("Input the ID for the Student (INT VALUE):");

            var id = Console.ReadLine() ?? string.Empty;
            var intOutput = int.Parse(id);

            while (VerifyID(intOutput) != true)
            {
                Console.WriteLine("Invalid ID. Please Input another ID:");
                id = Console.ReadLine() ?? string.Empty;
                intOutput = int.Parse(id);
            }
            Console.WriteLine("What is the Name of the Person?");
            var name = Console.ReadLine();
            if (selectedPerson is Student)
            {
                Console.WriteLine("What is the classification of the student? [(F)reshman, S(O)phomore, (J)unior, (S)enior]");
                var classification = Console.ReadLine() ?? string.Empty;
                PersonClassification classEnum = PersonClassification.Freshman;

                if (classification.Equals("O", StringComparison.InvariantCultureIgnoreCase))
                {
                    classEnum = PersonClassification.Sophomore;
                }
                else if (classification.Equals("J", StringComparison.InvariantCultureIgnoreCase))
                {
                    classEnum = PersonClassification.Junior;
                }
                else if (classification.Equals("S", StringComparison.InvariantCultureIgnoreCase))
                {
                    classEnum = PersonClassification.Senior;
                }
                var studentRecord = selectedPerson as Student;
                if (studentRecord != null)
                {
                    studentRecord.Classification = classEnum;
                    studentRecord.ID = intOutput;
                    studentRecord.Name = name ?? string.Empty;

                    if (isCreate)
                    {
                        personService.AddStudent(selectedPerson);
                    }
                }
            }
            else
            {
                if (selectedPerson != null)
                {
                    selectedPerson.ID = int.Parse(id ?? "0");
                    selectedPerson.Name = name ?? string.Empty;
                    if (isCreate)
                    {
                        personService.AddStudent(selectedPerson);
                    }
                }
            }
        }

        public void UpdateStudentRecord()
        {
            Console.WriteLine("Select a person to update:");
            personService.Students.ForEach(Console.WriteLine);

            var selectionStr = Console.ReadLine();

            if (int.TryParse(selectionStr, out int selectionInt))
            {
                var selectedStudent = personService.Students.FirstOrDefault(s => s.ID == selectionInt);
                if (selectedStudent != null)
                {
                    CreatePersonRecord(selectedStudent);
                }
            }
        }

        private void NavigateStudents(string? query = null)
        {
            ListNavigator<Person>? currentNavigator = null;
            if (query == null)
            {
                currentNavigator = personNavigator;
            }
            else
            {
                currentNavigator = new ListNavigator<Person>(personService.Search(query).ToList(), 2);
            }

            bool keepPaging = true;
            while (keepPaging)
            {
                foreach (var pair in currentNavigator.GetCurrentPage())
                {
                    Console.WriteLine($"{pair.Key}. {pair.Value}");
                }

                if (currentNavigator.HasPreviousPage)
                {
                    Console.WriteLine("P. Previous Page");
                }

                if (currentNavigator.HasNextPage)
                {
                    Console.WriteLine("N. Next Page");
                }

                Console.WriteLine("Make a selection:");
                var selectionStr = Console.ReadLine();

                if ((selectionStr?.Equals("P", StringComparison.InvariantCultureIgnoreCase) ?? false)
                    || (selectionStr?.Equals("N", StringComparison.InvariantCultureIgnoreCase) ?? false))
                {
                    //Navigate through pages
                    if (selectionStr.Equals("P", StringComparison.InvariantCultureIgnoreCase))
                    {
                        currentNavigator.GoBackward();
                    }
                    else if (selectionStr.Equals("N", StringComparison.InvariantCultureIgnoreCase))
                    {
                        currentNavigator.GoForward();
                    }
                }
                else
                {
                    var selectionInt = int.Parse(selectionStr ?? "0");

                    Console.WriteLine("Student Course List:");
                    courseService.Courses.Where(c => c.Roster.Any(s => s.ID == selectionInt)).ToList().ForEach(Console.WriteLine);
                    keepPaging = false;
                }
            }
        }

        public void ListStudents()
        {
            NavigateStudents();
        }

        public void SearchStudents()
        {
            Console.WriteLine("Enter a query:");
            var query = Console.ReadLine() ?? string.Empty;

            NavigateStudents(query);
        }
    }
}