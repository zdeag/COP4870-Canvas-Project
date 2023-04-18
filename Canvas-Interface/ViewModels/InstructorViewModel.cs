using Canvas_Interface.Dialogs;
using Class.Library.Canvas.Models.Courses;
using Class.Library.Canvas.Models.People;
using Class.Library.Canvas.Models.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Canvas_Interface.ViewModels
{
    public class InstructorViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private static Course selectedCourse;
        
        private Module selectedModule;
        private ContentItem selectedContentItem;
        private AssignmentGroup selectedAssignmentGroup;
        private Assignment selectedAssignment;
        private Announcement selectedAnnouncement;
        private Student selectedStudent;
        private CompletedAssignment selectedCompletedAssignment;
        private ObservableCollection<Course> _filteredCourseList;
        private string _searchQuery;

        private PersonService personService;
        private CourseService courseService;

        public InstructorViewModel()
        {
            this.personService = PersonService.Instance;
            this.courseService = CourseService.Instance;

            FilteredCourseList = CourseList;
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

        public ObservableCollection<Person> AdminList
        {
            get
            {
                return personService.Admins;
            }
        }

        public ObservableCollection<Announcement> AnnouncementList
        {
            get
            {
                return selectedCourse?.AnnouncementList;
            }
        }

        public ObservableCollection<Person> Roster
        {
            get { return selectedCourse?.Roster; }
        }

        public ObservableCollection<Module> Modules
        {
            get { return selectedCourse?.Modules; }
        }

        public ObservableCollection<AssignmentGroup> AssignmentGroups
        {
            get { return selectedCourse?.AssignmentGroups; }
        }

        public ObservableCollection<Assignment> Assignments
        { 
            get { return selectedAssignmentGroup?.Assignments; }
        }

        public ObservableCollection<ContentItem> Contents
        {
            get { return selectedModule?.ContentItems; }
        }

        public ObservableCollection<CompletedAssignment> CompletedAssignmentList
        {
            get
            {
                var filteredList = SelectedCourse.CompletedAssignments.Where(s => s.IsGraded == false).ToList();
                var observableCollection = new ObservableCollection<CompletedAssignment>(filteredList);
                return observableCollection;
            }
        }

        public static Course SelectedCourse
        {
            get { return selectedCourse; }
            set { selectedCourse = value; }
        }

        public Module SelectedModule
        {
            get { return selectedModule; }
            set
            {
                if (selectedModule != value)
                {
                    selectedModule = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedCourse)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Modules)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Contents)));
                }
            }
        }

        public ContentItem SelectedContentItem
        {
            get { return selectedContentItem; }
            set
            {
                if (selectedContentItem != value)
                {
                    selectedContentItem = value;
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

        public Announcement SelectedAnnouncement
        {
            get { return selectedAnnouncement; }
            set
            {
                if (selectedAnnouncement != value)
                {
                    selectedAnnouncement = value;
                }
            }
        }

        public Student SelectedStudent
        {
            get { return selectedStudent; }
            set
            {
                if (selectedStudent != value)
                {
                    selectedStudent = value;
                }
            }
        }

        public CompletedAssignment SelectedCompletedAssignment
        {
            get { return selectedCompletedAssignment; }
            set { if (selectedCompletedAssignment != value)
                {
                    selectedCompletedAssignment = value;
                } 
            }
        }

        public ObservableCollection<Course> FilteredCourseList
        {
            get
            {
                return _filteredCourseList;
            } 
            set
            {
                _filteredCourseList = value;

            }

        }

        public string SearchQuery
        {
            set
            {
                _searchQuery = value;
            }
        }

        public void SearchCourse()
        {


            var filteredCourses = (CourseList.Where(s => s.Name.IndexOf(_searchQuery, StringComparison.OrdinalIgnoreCase) >= 0
                      || s.Code.IndexOf(_searchQuery, StringComparison.OrdinalIgnoreCase) >= 0)).ToList();

            FilteredCourseList = new ObservableCollection<Course>(filteredCourses);

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FilteredCourseList)));
        }

        public async void EditCourseInformation()
        {
            var diag = new CourseCreate(CourseList, SelectedCourse);
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CourseList)));
            await diag.ShowAsync();

        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public async void GradeAssignment()
        {
            var diag = new GradeAssignment(CompletedAssignmentList, SelectedCompletedAssignment);
            NotifyPropertyChanged("SelectedAssignmentGroup");
            await diag.ShowAsync();
        }


        public void Remove()
        {
            courseService.Courses.Remove(SelectedCourse);
            CourseList.Remove(SelectedCourse);
        }



        public void RemoveModule()
        {
            if (SelectedCourse != null)
            {
                SelectedCourse.Modules.Remove(SelectedModule);
            }
        }

        public void RemoveContent()
        {
            if (SelectedModule != null && SelectedContentItem != null)
            {
                SelectedModule.ContentItems.Remove(SelectedContentItem);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Contents)));
            }
        }

        public void RemoveAnnouncement()
        {
            if (SelectedCourse != null)
            {
                SelectedCourse.AnnouncementList.Remove(SelectedAnnouncement);
            }
        }

        public void RemoveAssignmentGroup()
        {
            if (SelectedCourse != null)
            {
                SelectedCourse.AssignmentGroups.Remove(SelectedAssignmentGroup);
            }
        }

        public void RemoveAssignment()
        {
            if (SelectedCourse != null)
            {
                if (SelectedAssignmentGroup != null && SelectedAssignment != null)
                {
                    SelectedAssignmentGroup.Assignments.Remove(SelectedAssignment);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Assignments)));
                }
            }
        }
    }
}
