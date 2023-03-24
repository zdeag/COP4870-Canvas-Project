using Class.Library.Canvas.Models.Courses;
using Class.Library.Canvas.Models.People;
using Class.Library.Canvas.Models.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace CanvasApplication.Helpers
{
    public class CourseHelper
    {
        private CourseService courseService;
        private PersonService personService;
        private ListNavigator<Course> courseNavigator;

        public CourseHelper()
        {
            personService = PersonService.Current;
            courseService = CourseService.Current;
            courseNavigator = new ListNavigator<Course>(courseService.Courses, 2);
        }

        private Course? FindCourse(string? input = null)
        {
            if (input == null)
            {
                Console.WriteLine();
                Console.WriteLine("====> Course List <====");
                courseService.Courses.ForEach(Console.WriteLine);

                Console.WriteLine("Input Course Code for Course Locator:");
                input = Console.ReadLine() ?? String.Empty;
            }

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(input, StringComparison.InvariantCultureIgnoreCase));

            return selectedCourse ?? null;
        }

        private bool VerifyCode(string code)
        {
            foreach (var obj in courseService.Courses)
            {
                if (obj.Code == code)
                {
                    return false;
                }
            }

            return true;
        }

        public void CreateCourse(Course? selectedCourse = null)
        {
            bool isNewCourse = false;

            // Creates a new course if parameter is null
            if (selectedCourse == null)
            {
                isNewCourse = true;
                selectedCourse = new Course();
            }

            var choice = "Y";
            if (!isNewCourse)
            {
                Console.WriteLine("Do you want to edit the Course Code?");
                choice = Console.ReadLine() ?? "N";
            }

            if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.Write("Input Course Code:");
                var input = Console.ReadLine() ?? string.Empty;

                while (VerifyCode(input) != true)
                {
                    Console.WriteLine("Invalid Course Code. Input Course Code:");
                    input = Console.ReadLine() ?? string.Empty;
                }

                selectedCourse.Code = input;
            }

            if (!isNewCourse)
            {
                Console.WriteLine("Do you want to edit the Course Name?");
                choice = Console.ReadLine() ?? "N";
            }

            if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.Write("Input Course Name:");
                selectedCourse.Name = Console.ReadLine() ?? string.Empty;
            }

            if (!isNewCourse)
            {
                Console.WriteLine("Do you want to edit the Course Description?");
                choice = Console.ReadLine() ?? "N";
            }

            if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("Input Course Description:");
                selectedCourse.Description = Console.ReadLine() ?? string.Empty;
            }

            if (!isNewCourse)
            {
                Console.WriteLine("Do you want to edit the Course Credit Hours?");
                choice = Console.ReadLine() ?? "N";
            }

            if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("Input Course Credit Hours:");
                var userInput = Console.ReadLine() ?? string.Empty;
                selectedCourse.CreditHours = int.Parse(userInput);
            }

            if (isNewCourse)
            {

                SetupRoster(selectedCourse);
                SetupAssignmentGroups(selectedCourse);
                SetupAssignments(selectedCourse);
                SetupModules(selectedCourse);
            }

            if (isNewCourse)
            {
                courseService.AddCourse(selectedCourse);
            }
        }

        public void UpdateCourse()
        {
            Console.WriteLine("Enter the Code for the Course to update:");
            courseService.Courses.ForEach(Console.WriteLine);
            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                CreateCourse(selectedCourse);
            }
        }

        public void ListCourses(string? query = null)
        {
            ListNavigator<Course>? currentNavigator = null;
            if (query == null)
            {
                currentNavigator = courseNavigator;
            }
            else
            {
                currentNavigator = new ListNavigator<Course>(courseService.Search(query).ToList(), 2);
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

                Console.WriteLine("Input Course Code or navigate:");
                var selectionStr = Console.ReadLine();

                if ((selectionStr?.Equals("P", StringComparison.InvariantCultureIgnoreCase) ?? false) || (selectionStr?.Equals("N", StringComparison.InvariantCultureIgnoreCase) ?? false))
                {
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
                    var selectedCourse = FindCourse(selectionStr);

                    if (selectedCourse != null)
                    {
                        Console.WriteLine(selectedCourse.DetailDisplay);
                    }
                    
                    keepPaging = false;
                }
            }
        }

        public void SearchCourses()
        {
            Console.WriteLine("Enter a query:");
            var query = Console.ReadLine() ?? string.Empty;

            ListCourses(query);
        }

        public void UpdateStudentMenu()
        {

            Console.WriteLine("Enter the code of the Course you want to Utilize:");
            courseService.Courses.ForEach(Console.WriteLine);
            var selection = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));

            if (selectedCourse != null)
            {
                Console.WriteLine("\n1. Add a Student");
                Console.WriteLine("2. Remove a Student");
                Console.WriteLine("Please Select an Option:");

                var input = Console.ReadLine();
                if (int.TryParse(input, out int result))
                {
                    if (result == 1)
                    {
                        AddStudent(selectedCourse);
                    }
                    else if (result == 2)
                    {
                        RemoveStudent(selectedCourse);
                    }
                }
            }
        }

        public void AddStudent(Course c)
        {
            if (c != null)
            {
                personService.Students.ForEach(Console.WriteLine);
                var userInput = Console.ReadLine() ?? String.Empty;
                var selectedId = int.Parse(userInput);
                var selectedStudent = personService.Students.FirstOrDefault(s => s.ID == selectedId);
                if (selectedStudent != null)
                {
                    c.Roster.Add(selectedStudent);
                }
            }
        }
        public void RemoveStudent(Course c)
        {
            var selection = "";

            if (c != null)
            {
                c.Roster.ForEach(Console.WriteLine);
                if (c.Roster.Any())
                {
                    selection = Console.ReadLine() ?? string.Empty;
                }
                else
                {
                    selection = null;
                }

                if (selection != null)
                {
                    var selectedId = int.Parse(selection);
                    var selectedStudent = personService.Students.FirstOrDefault(s => s.ID == selectedId);
                    if (selectedStudent != null)
                    {
                        c.Roster.Remove(selectedStudent);
                    }
                }

            }
        }

        private CompletedAssignment AddCompletedAssignment(Course c)
        {
            c.Assignments.ForEach(Console.WriteLine);

            Console.WriteLine("Please input the ID of the assignment you want to turn in: ");
            var choice = Console.ReadLine() ?? string.Empty;

            var selectionInt = int.Parse(choice);

            var selectedAssignment = c.Assignments.FirstOrDefault(s => s.Id.Equals(selectionInt));

            Console.WriteLine(selectedAssignment);

            if (selectedAssignment != null)
            {
                c.Roster.ForEach(Console.WriteLine);

                Console.WriteLine("Please input your Student ID:");
                var userInput = Console.ReadLine() ?? string.Empty;

                var selectionUserInt = int.Parse(userInput);

                var selectedStudent = c.Roster.FirstOrDefault(s => s.ID.Equals(selectionUserInt));

                if (selectedStudent != null)
                {

                    var matchingAssignmentGroup = c.AssignmentGroups.FirstOrDefault(ag =>
                    ag.Assignments.Any(a => a.Id == a.Id)
                    );

                    return new CompletedAssignment
                    {
                        Id = selectedAssignment.Id,
                        StudentID = selectedStudent.ID,
                        Name = selectedAssignment.Name,
                        Description = selectedAssignment.Description,
                        AvailablePoints = selectedAssignment.TotalAvailablePoints,
                        totalPointsGiven = 0,
                        Weight = matchingAssignmentGroup.AssignmentWeight,
                        IsGraded = false
                    };
                } else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public void UpdateAssignmentGroupMenu()
        {
            var selectedCourse = FindCourse();

            if (selectedCourse != null)
            {
                Console.WriteLine("1. Create an Assignment Group");
                Console.WriteLine("2. Edit an Assignment Group");
                Console.WriteLine("3. Remove an Assignment Group");
                Console.WriteLine("4. Add an Assignment to Group");
                Console.WriteLine("5. Remove an Assigment from Group");
                Console.WriteLine("6. List all Assignment Groups");

                var input = Console.ReadLine();
                if (int.TryParse(input, out int result))
                {
                    if (result == 1)
                    {
                        AddAssigmentGroup(selectedCourse);
                    }
                    else if (result > 1 && result < 6)
                    {
                        Console.WriteLine($"Please Enter the name of the desired Assignment Group found in {selectedCourse.Name}");
                        selectedCourse.AssignmentGroups.ForEach(Console.WriteLine);
                        var userInput = Console.ReadLine();

                        var selectedAssignmentGroup = selectedCourse.AssignmentGroups.FirstOrDefault(s => s.Name.Equals(userInput, StringComparison.InvariantCultureIgnoreCase));

                        if (selectedAssignmentGroup != null)
                        {
                            if (result == 2)
                            {
                                EditAssignmentGroup(selectedCourse, selectedAssignmentGroup);
                            }
                            else if (result == 3)
                            {
                                RemoveAssignmentGroup(selectedCourse, selectedAssignmentGroup);
                            }
                            else if (result == 4 || result == 5)
                            {
                                selectedCourse.Assignments.ForEach(Console.WriteLine);
                                Console.WriteLine("Please input the ID of the Assignment you want to add/remove:");
                                var choice = Console.ReadLine() ?? string.Empty;

                                var index = int.Parse(choice);

                                var selectedAssignment = selectedCourse.Assignments.FirstOrDefault(s => s.Id.Equals(index));

                                if (selectedAssignment != null)
                                {
                                    if (result == 4)
                                    {
                                        AddAssignmentGrouping(selectedAssignmentGroup, selectedAssignment);
                                    }
                                    else if (result == 5)
                                    {
                                        RemoveAssignmentGrouping(selectedAssignmentGroup, selectedAssignment);
                                    }
                                }
                            }
                        }
                    }
                    else if (result == 6)
                    {
                        if (selectedCourse != null)
                        {
                            selectedCourse.AssignmentGroups.ForEach(Console.WriteLine);
                        }
                    }
                }
            }
        }

        private void SetupAssignmentGroups(Course c)
        {
            Console.WriteLine("Would you like to add an Assignment Group? (Y/N)");
            var assignResponse = Console.ReadLine() ?? "N";
            bool continueAdding;
            if (assignResponse.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                continueAdding = true;
                while (continueAdding)
                {
                    AddAssigmentGroup(c);
                    Console.WriteLine("Add more Assignment Groups? (Y/N)");
                    assignResponse = Console.ReadLine() ?? "N";
                    if (assignResponse.Equals("N", StringComparison.InvariantCultureIgnoreCase))
                    {
                        continueAdding = false;
                    }
                }
            }
        }

        public void AddAssigmentGroup(Course c)
        {
            if (c != null)
            {
                c.AssignmentGroups.Add(CreateAssignmentGroup(c));
            }
        }

        // Creates Assignment Group with Course Param
        private AssignmentGroup CreateAssignmentGroup(Course c)
        {
            Console.WriteLine("Please input Assigment Group Name:");
            var assignmentGroupName = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Please input Assigment Group Description:");
            var assignmentGroupDesc = Console.ReadLine() ?? string.Empty;

            Console.WriteLine("Please input Assignment Group Weightage:");

            var assignmentGroupWeight = Console.ReadLine() ?? string.Empty;
            int weightage = int.Parse(assignmentGroupWeight);

            while (CheckAssignmentWeightLimit(c, weightage) != true)
            {
                Console.WriteLine("[ERROR] AssignmentWeightLimit has been exceeded.");
                Console.WriteLine("Enter Assignment Groups Weightage:");
                assignmentGroupWeight = Console.ReadLine() ?? string.Empty;
                weightage = int.Parse(assignmentGroupWeight);
            }

            c.AssignmentWeightLimit = c.AssignmentWeightLimit - weightage;

            return new AssignmentGroup
            {
                Name = assignmentGroupName,
                Description = assignmentGroupDesc,
                AssignmentWeight = weightage
            };
        }

        // Returns TRUE/FALSE value if AssignmentWeightLimit has been exceeded
        private bool CheckAssignmentWeightLimit(Course c, int ask)
        {
            var temporaryVal = c.AssignmentWeightLimit;

            if ((temporaryVal - ask) < 0)
            {
                return false;
            }
            else return true;
        }

        private void EditAssignmentGroup(Course c, AssignmentGroup a)
        {
            Console.WriteLine("Do you want to edit the Assignment Group Name?");
            var choice = Console.ReadLine() ?? string.Empty;

            if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.Write("Input Assignment Group Name:");
                a.Name = Console.ReadLine() ?? string.Empty;
            }

            Console.WriteLine("Do you want to edit the Assignment Group Description?");
            choice = Console.ReadLine() ?? string.Empty;


            if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.Write("Input Assignment Group Description:");
                a.Description = Console.ReadLine() ?? string.Empty;
            }

            Console.WriteLine("Do you want to edit the Assignment Group's weight?");
            choice = Console.ReadLine() ?? string.Empty;

            if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                c.AssignmentWeightLimit -= a.AssignmentWeight;
                Console.WriteLine("Input Assignment Group Weight:");
                var input = Console.ReadLine() ?? string.Empty;

                a.AssignmentWeight = int.Parse(input);

                c.AssignmentWeightLimit += int.Parse(input);
            }
        }

        private void RemoveAssignmentGroup(Course c, AssignmentGroup a)
        {
            if (c != null && a != null)
            {
                c.AssignmentGroups.Remove(a);
            }
        }

        private void AddAssignmentGrouping(AssignmentGroup a, Assignment hw)
        {
            a.Assignments.Add(hw);
        }

        private void RemoveAssignmentGrouping(AssignmentGroup a, Assignment hw)
        {
            a.Assignments.Remove(hw);
        }


        public void UpdateAssignmentMenu()
        {
            Console.WriteLine("Enter the code for the course to add the assignment to:");
            courseService.Courses.ForEach(Console.WriteLine);
            var userInput = Console.ReadLine();

            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(userInput, StringComparison.InvariantCultureIgnoreCase));

            if (selectedCourse != null)
            {
                Console.WriteLine("\n1. Add an Assignment");
                Console.WriteLine("2. Remove an Assignment");
                Console.WriteLine("3. Update an Assignment");
                Console.WriteLine("Please Select an Option:");

                var input = Console.ReadLine();
                if (int.TryParse(input, out int result))
                {
                    if (result == 1)
                    {
                        AddAssignment(selectedCourse);
                    }
                    else if (result == 2)
                    {
                        RemoveAssignment(selectedCourse);
                    }
                    else if (result == 3)
                    {
                        UpdateAssignment(selectedCourse);
                    }
                }
            }
        }

        public void AddAssignment(Course c)
        {
            var assignment = CreateAssignment(c);

            if (assignment != null)
            {
                c.Assignments.Add(assignment);
            }
            else Console.WriteLine("[ERROR] Assignment was not created.");
        }

        public void UpdateAssignment(Course c)
        {
                Console.WriteLine("Choose an assignment to update:");
                c.Assignments.ForEach(Console.WriteLine);
                var selectionStr = Console.ReadLine() ?? string.Empty;
                var selectionInt = int.Parse(selectionStr);
                var selectedAssignment = c.Assignments.FirstOrDefault(a => a.Id == selectionInt);
                if (selectedAssignment != null)
                {
                    var index = c.Assignments.IndexOf(selectedAssignment);
                    c.Assignments.RemoveAt(index);
                    c.Assignments.Insert(index, CreateAssignment(c));
                }          
        }

        public void RemoveAssignment(Course c)
        {
                Console.WriteLine("Choose an assignment to delete:");
                c.Assignments.ForEach(Console.WriteLine);
                var selectionStr = Console.ReadLine() ?? string.Empty;
                var selectionInt = int.Parse(selectionStr);
                var selectedAssignment = c.Assignments.FirstOrDefault(a => a.Id == selectionInt);
                if (selectedAssignment != null)
                {
                    c.Assignments.Remove(selectedAssignment);
                }
        }

        private void SetupRoster(Course c)
        {
            Console.WriteLine("Which students should be enrolled in this course? ('Q' to quit)");
            bool continueAdding = true;
            while (continueAdding)
            {
                personService.Students.Where(s => !c.Roster.Any(s2 => s2.ID == s.ID)).ToList().ForEach(Console.WriteLine);
                var selection = "Q";
                if (personService.Students.Any(s => !c.Roster.Any(s2 => s2.ID == s.ID)))
                {
                    selection = Console.ReadLine() ?? string.Empty;
                }

                if (selection.Equals("Q", StringComparison.InvariantCultureIgnoreCase))
                {
                    continueAdding = false;
                }
                else
                {
                    var selectedId = int.Parse(selection);
                    var selectedStudent = personService.Students.FirstOrDefault(s => s.ID == selectedId);

                    if (selectedStudent != null)
                    {
                        c.Roster.Add(selectedStudent);
                    }
                }


            }
        }

        private void SetupAssignments(Course c)
        {
            Console.WriteLine("Would you like to add assignments? (Y/N)");
            var assignResponse = Console.ReadLine() ?? "N";
            bool continueAdding;
            if (assignResponse.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                continueAdding = true;
                while (continueAdding)
                {
                    var createdAssignment = CreateAssignment(c);

                    if (createdAssignment != null)
                    {
                        c.Assignments.Add(createdAssignment);
                    }
                    else Console.WriteLine("Invalid Response from Assignment Creator.");
                    
                    
                    Console.WriteLine("Add more assignments? (Y/N)");
                    assignResponse = Console.ReadLine() ?? "N";
                    if (assignResponse.Equals("N", StringComparison.InvariantCultureIgnoreCase))
                    {
                        continueAdding = false;
                    }
                }
            }

        }

        private Assignment? CreateAssignment(Course c)
        {
            //Name
            Console.WriteLine("Name:");
            var assignmentName = Console.ReadLine() ?? string.Empty;
            //Description
            Console.WriteLine("Description:");
            var assignmentDescription = Console.ReadLine() ?? string.Empty;
            //TotalPoints
            Console.WriteLine("TotalPoints:");
            var totalPoints = decimal.Parse(Console.ReadLine() ?? "100");
            //DueDate
            Console.WriteLine("DueDate:");
            var dueDate = DateTime.Parse(Console.ReadLine() ?? "01/01/1900");


            Console.WriteLine("");
            c.AssignmentGroups.ForEach(Console.WriteLine);
            Console.WriteLine("Please Select an Assignment Group to add this Assignment to:");
            var userInput = Console.ReadLine() ?? string.Empty;

            var selectedAssignmentGroup = c.AssignmentGroups.FirstOrDefault(s => s.Name.Equals(userInput, StringComparison.InvariantCultureIgnoreCase));

            if (selectedAssignmentGroup != null)
            {
                var createdAssignment = new Assignment
                {
                    Name = assignmentName,
                    Description = assignmentDescription,
                    TotalAvailablePoints = totalPoints,
                    DueDate = dueDate
                };

                selectedAssignmentGroup.Assignments.Add(createdAssignment);

                return createdAssignment;
            }
            else return null;
        }

        public void UpdateModuleMenu()
        {
            var selectedCourse = FindCourse();

            if (selectedCourse != null)
            {
                Console.WriteLine("\n1. Add a Module");
                Console.WriteLine("2. Remove a Module");
                Console.WriteLine("3. Update Content");
                Console.WriteLine("4. Print Modules");
                Console.WriteLine("Please Select an Option:");

                var input = Console.ReadLine();
                if (int.TryParse(input, out int result))
                {
                    if (result == 1)
                    {
                        CreateModule(selectedCourse);
                    }
                    else if (result == 2 || result == 3)
                    {
                        selectedCourse.Modules.ForEach(Console.WriteLine);
                        Console.WriteLine("Please input Module Name:");

                        var choice = Console.ReadLine() ?? string.Empty;
                        var selectedModule = selectedCourse.Modules.FirstOrDefault(m => m.Name.Equals(choice, StringComparison.InvariantCultureIgnoreCase));

                        if (selectedModule != null)
                        {
                            if (result == 2)
                            {
                                RemoveModule(selectedCourse, selectedModule);
                            } else if (result == 3)
                            {
                                CreateModule(selectedCourse, selectedModule);
                            }
                        }

                    } else if (result == 4)
                    {
                        ReadModules(selectedCourse);
                    }
                }
            }
        }

        private void SetupModules(Course c)
        {
            Console.WriteLine("Would you like to add Modules? (Y/N)");
            var input = Console.ReadLine() ?? "N";
            bool continueAdding;
            if (input.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                continueAdding = true;
                while (continueAdding)
                {
                    CreateModule(c);
                    Console.WriteLine("Do you want to add more Modules? (Y/N)");
                    input = Console.ReadLine() ?? "N";
                    if (input.Equals("N", StringComparison.InvariantCultureIgnoreCase))
                    {
                        continueAdding = false;
                    }
                }
            }

        }

        public void CreateModule(Course c, Module? selectedModule = null)
        {
            bool isNewModule = false;

            // Creates a new course if parameter is null
            if (selectedModule == null)
            {
                isNewModule = true;
                selectedModule = new Module();
            }

            var choice = "Y";
            if (!isNewModule)
            {
                Console.WriteLine("Do you want to add/change the Module Name?");
                choice = Console.ReadLine() ?? "N";
            }

            if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.Write("Input Module Name:");
                selectedModule.Name = Console.ReadLine() ?? string.Empty;
            }

            if (!isNewModule)
            {
                Console.WriteLine("Do you want to add/change the Module Description?");
                choice = Console.ReadLine() ?? "N";
            }

            if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("Input Module Description:");
                selectedModule.Description = Console.ReadLine() ?? string.Empty;
            }

            if (!isNewModule)
            {
                Console.WriteLine("Do you want to add/change any Module Content?");
                choice = Console.ReadLine() ?? string.Empty;
            }

            if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                CreateContentItemMenu(c, selectedModule);
            }

            if (isNewModule)
            {
                c.Modules.Add(selectedModule);
            }

        }

        private void RemoveModule(Course c, Module m)
        {
            c.Modules.Remove(m);
        }

        public void ReadModules(Course c)
        {
            Console.WriteLine($"====> Modules associated with {c.Name} <====");
            c.Modules.ForEach(Console.WriteLine);

        }

        private void CreateContentItemMenu(Course c, Module? selectedModule = null)
        {
            if (selectedModule == null)
            {
                Console.WriteLine("Enter the Code for the Course to update:");
                c.Modules.ForEach(Console.WriteLine);
                var selection = Console.ReadLine();

                var module = c.Modules.FirstOrDefault(s => s.Name.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            }

            if (selectedModule != null)
            {
                var choice = "Y";

                Console.WriteLine("Do you want to add/edit Page Item? (Y/N)");
                choice = Console.ReadLine() ?? string.Empty;

                if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                {
                    CreatePageItem(selectedModule);
                }

                Console.WriteLine("Do you want to add/edit Assignment Item? (Y/N)");
                choice = Console.ReadLine() ?? string.Empty;

                if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                {
                    ModuleAssignmentManagement(c, selectedModule);
                }
                Console.WriteLine("Do you want to add/edit File Item? (Y/N)");
                choice = Console.ReadLine() ?? string.Empty;

                if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                {
                    CreateFileItem(selectedModule);
                }
            }
        }

        private void CreatePageItem(Module s)
        {
            Console.WriteLine("1. Add Page Item");
            Console.WriteLine("2. Edit Page Item");
            Console.WriteLine("3. Remove Page Item");
            Console.WriteLine("4. Exit");
            Console.WriteLine("Input Selection:");

            var input = Console.ReadLine();
            if (int.TryParse(input, out int result))
            {
                if (result == 1)
                {
                    Console.WriteLine("Input Page HTML:");
                    var htmlCode = Console.ReadLine() ?? string.Empty;


                    s.ContentItems.Add(new PageItem { HtmlBody = htmlCode });
                }
                else if (result == 2 || result == 3)
                {

                    Console.WriteLine("Input Content ID:");
                    if (int.TryParse(Console.ReadLine(), out int output))
                    {
                        var selectedContent = s.ContentItems.OfType<PageItem>().FirstOrDefault(s => s.ID.Equals(output));

                        if (selectedContent != null)
                        {
                            if (result == 1)
                            {

                                Console.WriteLine("Do you want to edit the HTML Code:");

                                var choice = Console.ReadLine() ?? string.Empty;

                                if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    Console.WriteLine("Input HTML Code:");
                                    selectedContent.HtmlBody = Console.ReadLine();
                                }
                            }
                            else if (result == 2)
                            {
                                s.ContentItems.Remove(selectedContent);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input.");
                    }
                }
            }
        }

        private void CreateFileItem(Module s)
        {
            Console.WriteLine("1. Add File Item");
            Console.WriteLine("2. Edit File Item");
            Console.WriteLine("3. Remove File Item");
            Console.WriteLine("4. Exit");
            Console.WriteLine("Input Selection:");

            var input = Console.ReadLine();
            if (int.TryParse(input, out int result))
            {
                if (result == 1)
                {
                    Console.WriteLine("Input File Path:");
                    var filePath = Console.ReadLine() ?? string.Empty;


                    s.ContentItems.Add(new FileItem { Path = filePath });
                }
                else if (result == 2 || result == 3)
                {

                    Console.WriteLine("Input Content ID:");
                    if (int.TryParse(Console.ReadLine(), out int output))
                    {
                        var selectedContent = s.ContentItems.OfType<FileItem>().FirstOrDefault(s => s.ID.Equals(output));

                        if (selectedContent != null)
                        {
                            if (result == 1)
                            {

                                Console.WriteLine("Do you want to edit the File Path:");

                                var choice = Console.ReadLine() ?? string.Empty;

                                if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    Console.WriteLine("Input HTML Code:");
                                    selectedContent.Path = Console.ReadLine();
                                }
                            }
                            else if (result == 2)
                            {
                                s.ContentItems.Remove(selectedContent);
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input.");
                    }
                }
            }
        }

        private AssignmentItem CreateAssignmentItem(Assignment a)
        {

            return new AssignmentItem
            {
                Assignment = a
            };
        }

        private void RemoveAssignmentItem(Module m, Assignment a)
        {

            var selectedAssignmentItem = m.ContentItems.OfType<AssignmentItem>().FirstOrDefault(s => a.Name.Equals(s.Name, StringComparison.CurrentCultureIgnoreCase));

            if (selectedAssignmentItem != null)
            {
                m.ContentItems.Remove(selectedAssignmentItem);
            }
        }

        private void ModuleAssignmentManagement(Course c, Module m)
        {
            Console.WriteLine("1. Add Assignment to Module");
            Console.WriteLine("2. Remove Assignment from Module");

            if (int.TryParse(Console.ReadLine(), out int output))
            {
                Console.WriteLine("Choose an assignment to update:");
                c.Assignments.ForEach(Console.WriteLine);
                var selectionStr = Console.ReadLine() ?? string.Empty;
                var selectionInt = int.Parse(selectionStr);
                var selectedAssignment = c.Assignments.FirstOrDefault(a => a.Id == selectionInt);

                if (selectedAssignment != null)
                {

                    if (output == 1)
                    {
                        var here = CreateAssignmentItem(selectedAssignment);

                        Console.WriteLine(here);

                        m.ContentItems.Add(CreateAssignmentItem(selectedAssignment));
                    }
                    else if (output == 2)
                    {
                        RemoveAssignmentItem(m, selectedAssignment);
                    }
                }
            }
        }

        public void ManageAnnouncements()
        {
            var selectedCourse = FindCourse();

            Console.WriteLine("===> Course Announcement Menu <===");
            Console.WriteLine("1. Add Announcement");
            Console.WriteLine("2. Remove Announcement");
            Console.WriteLine("3. Update Announcement");
            Console.WriteLine("4. View Announcements");
            Console.WriteLine("5. Exit");

            var userInput = Console.ReadLine() ?? string.Empty;

            var result = int.Parse(userInput);

            if (selectedCourse != null)
            {
                if (result == 1)
                {
                    CreateAnnouncement();
                }
                else if (result == 2 || result == 3)
                {
                    Console.WriteLine("===> Course Announcements <===");
                    selectedCourse.AnnouncementList.ForEach(Console.WriteLine);

                    Console.WriteLine("Input Announcement ID:");
                    var input = Console.ReadLine() ?? string.Empty;

                    var output = int.Parse(input);

                    var selectedAnnouncement = selectedCourse.AnnouncementList.FirstOrDefault(x => x.Id.Equals(output));

                    if (selectedAnnouncement != null)
                    {
                        if (result == 2)
                        {
                            selectedCourse.AnnouncementList.Remove(selectedAnnouncement);
                        }
                        else if (result == 3)
                        {
                            CreateAnnouncement(selectedCourse, selectedAnnouncement);
                        }
                    }
                }
                else if (result == 4)
                {
                    Console.WriteLine("===> Course Announcements <===");
                    selectedCourse.AnnouncementList.ForEach(Console.WriteLine);
                }
            }
        }

        private void CreateAnnouncement(Course? c = null, Announcement? a = null)
        {
            bool isNewAnnouncement = false;

            if (c == null)
            {
                c = FindCourse();
            }

            if (a == null)
            {
                a = new Announcement();
                isNewAnnouncement = true;
            }

            var choice = "Y";
            if (!isNewAnnouncement)
            {
                Console.WriteLine("Do you want to edit the Announcement?");
                choice = Console.ReadLine() ?? "N";
            }

            if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.Write("Input Announcement Content:");
                a.announcementContent = Console.ReadLine() ?? string.Empty;
            }

            if (isNewAnnouncement)
            {
                c.AnnouncementList.Add(a);
            }
        }

        public void TurnInAssignment()
        {

            var selectedCourse = FindCourse();

            if (selectedCourse != null)
            {
                selectedCourse.CompletedAssignments.Add(AddCompletedAssignment(selectedCourse));
            }
        }

        public void GradeAssignment()
        {
            var selectedCourse = FindCourse();

            if(selectedCourse != null)
            {
                foreach (var assignment in selectedCourse.CompletedAssignments.Where(s => (s.IsGraded == false)))
               {
                    Console.WriteLine(assignment);
               }

                Console.WriteLine("Please input the Assignment ID you want to Grade:");

                var choice = Console.ReadLine() ?? string.Empty;

                var selectedInt = int.Parse(choice);

                var selectedAssignment = selectedCourse.CompletedAssignments.FirstOrDefault(s => s.Id.Equals(selectedInt));


                if (selectedAssignment != null)
                {
                    Console.WriteLine($"{selectedAssignment.Name} - Total Possible Points: {selectedAssignment.AvailablePoints}");
                    Console.WriteLine("Please Input Grade:");
                    var grade = Console.ReadLine() ?? string.Empty;

                    var gradeInt = int.Parse(grade);

                    selectedAssignment.totalPointsGiven = gradeInt;
                    selectedAssignment.IsGraded = true;

                }
            }
        }

        public decimal GetAverageGrade(Course? c = null , Person? s = null)
        {

            if (c == null)
            {
                c = FindCourse();
            }

            if (s == null)
            {
                Console.WriteLine("Please input your Student ID:");
                var userInput = Console.ReadLine() ?? string.Empty;

                var selectionUserInt = int.Parse(userInput);

                s = c.Roster.FirstOrDefault(a => a.ID.Equals(selectionUserInt));
            }

            decimal grade = 0;

            foreach (var assignmentGroup in c.AssignmentGroups)
            {
                decimal totalPointsReceived = 0;
                decimal totalPointsPossible = 0;

                foreach (var assignment in assignmentGroup.Assignments)
                {
                    foreach (var completedAssignment in c.CompletedAssignments)
                    {
                        if (assignment.Id == completedAssignment.Id &&
                            s.ID == completedAssignment.StudentID &&
                            completedAssignment.IsGraded == true)
                        {
                            totalPointsReceived += completedAssignment.totalPointsGiven;
                            totalPointsPossible += completedAssignment.AvailablePoints;
                        }
                    }
                }

                if (totalPointsPossible > 0)
                {
                    decimal assignmentGroupGrade = ((decimal)totalPointsReceived / (decimal)totalPointsPossible) *
                                                    ((decimal)assignmentGroup.AssignmentWeight / 100);
                    grade += assignmentGroupGrade;
                }
            }




            return grade * 100;
        }

        public decimal GetGPA()
        {

            Console.WriteLine("Please input your Student ID:");
            var userInput = Console.ReadLine() ?? string.Empty;

            var selectionUserInt = int.Parse(userInput);

            int creditHours = 0;
            int totalPoints = 0;
            
            foreach (var selectedCourse in courseService.Courses)
            {
                foreach (var student in selectedCourse.Roster)
                {
                    if (selectionUserInt == student.ID)
                    {

                        decimal tempGrade = GetAverageGrade(selectedCourse, student);

                        if (tempGrade <= 100 && tempGrade >= 90)
                        {
                            totalPoints += 4 * selectedCourse.CreditHours;
                        } else if (tempGrade < 90 && tempGrade >= 80)
                        {
                            totalPoints += 3 * selectedCourse.CreditHours;
                        } else if (tempGrade < 80 && tempGrade >= 70)
                        {
                            totalPoints += 2 * selectedCourse.CreditHours ;
                        } else if (tempGrade < 70 && tempGrade >= 60) {
                            totalPoints += 1 * selectedCourse.CreditHours;
                        }
                    }

                    creditHours += selectedCourse.CreditHours;
                }


            }

            return (decimal)(totalPoints / creditHours);
        }
    }
}


