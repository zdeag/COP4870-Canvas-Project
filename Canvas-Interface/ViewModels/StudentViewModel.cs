using Canvas_Interface.Dialogs;
using Class.Library.Canvas.Models.Courses;
using Class.Library.Canvas.Models.People;
using Class.Library.Canvas.Models.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace Canvas_Interface.ViewModels
{
    public class StudentViewModel : INotifyPropertyChanged
    {
        private PersonService personService;
        private CourseService courseService;
        private static Student selectedStudent;
        private static Course selectedCourse;
        private AssignmentGroup selectedAssignmentGroup;
        private Module selectedModule;
        private Assignment selectedAssignment;
        private ObservableCollection<Course> _filteredCourseList;
        private ObservableCollection<Person> _filteredStudentList;
        private decimal _GPA;
        private string _searchQuery;

        private bool _isSummerSelected;
        private bool _isSpringSelected;
        private bool _isFallSelected;

        public event PropertyChangedEventHandler PropertyChanged;

        public StudentViewModel()
        {
            this.personService = PersonService.Instance;
            this.courseService = CourseService.Instance;

            _filteredCourseList = StudentCourses;
            _filteredStudentList = StudentList;

        }

        public string SearchQuery
        {
            set
            {
                _searchQuery = value;
            }
        }

        public ObservableCollection<Person> FilteredStudentList
        {
            get { return _filteredStudentList; }
            set
            {
                _filteredStudentList = value;
            }
        }

        public ObservableCollection<Course> CourseList
        {
            get
            {
                return courseService.Courses;
            }
        }

        public ObservableCollection<Person> StudentList
        {
            get
            {
                return personService.Students;
            }
        }

        public ObservableCollection<Course> StudentCourses
        {
            get
            {
                var enrolledCourses = courseService.Courses
                    .Where(course => course.Roster.Any(s => s.Name == selectedStudent?.Name))
                    .ToList();

                return new ObservableCollection<Course>(enrolledCourses);
            }
        }

        public ObservableCollection<Course> FilteredStudentCourses
        {
            get { return _filteredCourseList; }
            set
            {
                _filteredCourseList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(StudentCourses)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FilteredGradedCourses)));
            }
        }

        public ObservableCollection<string> FilteredGradedCourses
        {
            get
            {
                var gradedCourses = new ObservableCollection<string>();
                if (SelectedStudent != null)
                {
                    foreach (var course in FilteredStudentCourses)
                    {
                        if (course != null)
                        {
                            gradedCourses.Add($"[{course.Code}] {course.Name} - {GetAverageGrade(course)}%");
                        }
                    }
                }

                GPA = GetGPA();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(GPA)));

                return gradedCourses;
            }
        }

        public decimal GPA
        {
            get { return _GPA; }
            set
            {
                _GPA = value;
            }
        }

        public ObservableCollection<Announcement> Announcements
        {
            get
            {
                return selectedCourse?.AnnouncementList;
            }
        }

        public ObservableCollection<AssignmentGroup> AssignmentGroups
        {
            get
            {
                return selectedCourse?.AssignmentGroups;
            }
        }

        public ObservableCollection<Module> Modules
        {
            get
            { return selectedCourse?.Modules; }
        }

        public ObservableCollection<Assignment> Assignments
        {
            get
            {
                var observableAssignments = new ObservableCollection<Assignment>();

                if (selectedCourse != null)
                {
                    foreach (var assignmentGroup in selectedCourse.AssignmentGroups)
                    {
                        foreach (var assignment in assignmentGroup.Assignments)
                        {
                            observableAssignments.Add(assignment);
                        }
                    }
                }

                return observableAssignments;
            }
        }


        public ObservableCollection<ContentItem> Contents
        {
            get { return selectedModule?.ContentItems; }
        }


        public Module SelectedModule
        {
            get { return selectedModule; }
            set
            {
                if (selectedModule != value)
                {
                    selectedModule = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Contents)));
                }
            }
        }

        public AssignmentGroup SelectedAssignmentGroup
        {
            get { return selectedAssignmentGroup; }
            set
            {
                if (selectedAssignmentGroup != value)
                {
                    selectedAssignmentGroup = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Assignments)));
                }
            }
        }

        public Assignment SelectedAssignment
        {
            get { return selectedAssignment; }
            set
            {
                if (selectedAssignment != value)
                {
                    selectedAssignment = value;
                }
            }
        }

        public static Student SelectedStudent
        {
            get { return selectedStudent; }
            set { selectedStudent = value; }
        }

        public static Course SelectedCourse
        {
            get { return selectedCourse; }
            set { selectedCourse = value; }
        }


        public bool IsSummerSelected
        {
            get { return _isSummerSelected; }
            set
            {
                _isSummerSelected = value;
                FilterCourses();
            }
        }

        public bool IsSpringSelected
        {
            get { return _isSpringSelected; }
            set
            {
                _isSpringSelected = value;
                FilterCourses();
            }
        }

        public bool IsFallSelected
        {
            get { return _isFallSelected; }
            set
            {
                _isFallSelected = value;
                FilterCourses();
            }
        }

        private void FilterCourses()
        {
            if (IsSummerSelected)
            {
                FilteredStudentCourses = new ObservableCollection<Course>(StudentCourses.Where(c => c.Semester == SemesterClassfication.Summer));
            }
            else if (IsSpringSelected)
            {
                FilteredStudentCourses = new ObservableCollection<Course>(StudentCourses.Where(c => c.Semester == SemesterClassfication.Spring));
            }
            else if (IsFallSelected)
            {
                FilteredStudentCourses = new ObservableCollection<Course>(StudentCourses.Where(c => c.Semester == SemesterClassfication.Fall));

            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FilteredStudentCourses)));
        }

        public void SearchStudents()
        {
            if (int.TryParse(_searchQuery, out int result)) {

                FilteredStudentList = new ObservableCollection<Person>(
                StudentList.Where(s => s.ID.Equals(result)));
            } else
            {
                FilteredStudentList = new ObservableCollection<Person>(
                StudentList.Where(s => s.Name.IndexOf(_searchQuery, StringComparison.OrdinalIgnoreCase) >= 0).ToList());
            }
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FilteredStudentList)));
        }


        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SubmitAssignment()
        {
            var matchingAssignmentGroup = StudentViewModel.SelectedCourse.AssignmentGroups.FirstOrDefault(ag =>
                    ag.Assignments.Any(a => a.Id == a.Id)
                    );

            StudentViewModel.SelectedCourse.CompletedAssignments.Add(new CompletedAssignment
            {
                Id = SelectedAssignment.Id,
                StudentID = SelectedStudent.ID,
                Name = SelectedAssignment.Name,
                Description = SelectedAssignment.Description,
                AvailablePoints = SelectedAssignment.TotalAvailablePoints,
                totalPointsGiven = 0,
                Weight = matchingAssignmentGroup.AssignmentWeight,
                IsGraded = false
            });
        }

        public decimal GetAverageGrade(Course inputCourse)
        {
            decimal grade = 0;

            foreach(var assignmentGroup in inputCourse.AssignmentGroups)
            {
                decimal totalPointsReceived = 0;
                decimal totalPointsPossible = 0;

                foreach (var assignment in assignmentGroup.Assignments)
                {
                    foreach (var completedAssignment in inputCourse.CompletedAssignments)
                    {
                        if (assignment.Id == completedAssignment.Id &&
                            SelectedStudent.ID == completedAssignment.StudentID &&
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
            int creditHours = 0;
            int totalPoints = 0;

            foreach (var course in FilteredStudentCourses)
            {
                decimal tempGrade = GetAverageGrade(selectedCourse);

                if (tempGrade <= 100 && tempGrade >= 90)
                {
                    totalPoints += 4 * course.CreditHours;
                }
                else if (tempGrade < 90 && tempGrade >= 80)
                {
                    totalPoints += 3 * course.CreditHours;
                }
                else if (tempGrade < 80 && tempGrade >= 70)
                {
                    totalPoints += 2 * course.CreditHours;
                }
                else if (tempGrade < 70 && tempGrade >= 60)
                {
                    totalPoints += 1 * course.CreditHours;
                }

                creditHours += course.CreditHours;
            }

            if (creditHours > 0)
            {
                return (decimal)(totalPoints / creditHours);
            } return 0;
        }
    }
}