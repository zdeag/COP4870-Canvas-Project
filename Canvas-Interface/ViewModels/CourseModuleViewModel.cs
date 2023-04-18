using Class.Library.Canvas.Models.Courses;
using Class.Library.Canvas.Models.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Canvas_Interface.ViewModels
{

    public class CourseModuleViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Course _selectedCourse { get; set; }
        public Module _selectedModule { get; set; }
        public ContentItem _selectedContentItem { get; set; }
        public AssignmentGroup _selectedAssignmentGroup { get; set; }
        public Assignment _selectedAssignment { get; set; }

        private PersonService personService;
        private CourseService courseService;

        public Course SelectedCourse
        {
            get { return _selectedCourse; }
            set
            {
                if (_selectedCourse != value)
                {
                    _selectedCourse = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedCourse)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ModuleList)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AssignmentGroups)));
                }
            }
        }

        public Module SelectedModule
        {
            get { return _selectedModule; }
            set
            {
                if (_selectedModule != value)
                {
                    _selectedModule = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedCourse)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ModuleList)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ContentItems)));
                }
            }
        }

        public ContentItem SelectedContentItem
        {
            get { return _selectedContentItem; }
            set
            {
                if (_selectedContentItem != value)
                {
                    _selectedContentItem = value;
                }
            }
        }

        public AssignmentGroup SelectedAssignmentGroup
        {
            get { return _selectedAssignmentGroup; }
            set
            {
                if (_selectedAssignmentGroup != value)
                {
                    _selectedAssignmentGroup = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Assignments)));
                }
            }
        }

        public Assignment SelectedAssignment
        {
            get { return _selectedAssignment; }
            set
            {
                if (_selectedAssignment!= value)
                {
                    _selectedAssignment = value;
                }
            }
        }

        public ObservableCollection<Course> CourseList
        {
            get { return courseService.Courses; }
        }

        public ObservableCollection<Module> ModuleList
        {
            get { return SelectedCourse?.Modules; }
        }

        public ObservableCollection<ContentItem> ContentItems
        {
            get { return SelectedModule?.ContentItems; }
        }

        public ObservableCollection<AssignmentGroup> AssignmentGroups
        {
            get { return SelectedCourse?.AssignmentGroups; }
        }

        public ObservableCollection<Assignment> Assignments
        {
            get { return SelectedAssignmentGroup?.Assignments; }
        }

        public CourseModuleViewModel()
        {
            this.personService = PersonService.Instance;
            this.courseService = CourseService.Instance;

            PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(SelectedCourse))
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ModuleList)));
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AssignmentGroups)));
                }

                if (args.PropertyName == nameof(SelectedModule))
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ContentItems)));
                }

                if (args.PropertyName == nameof(AssignmentGroups))
                {
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Assignments)));
                }
            };
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddAssignment()
        {
            if (SelectedModule != null && SelectedAssignmentGroup != null && SelectedAssignment != null)
            {
                SelectedModule.ContentItems.Add(new AssignmentItem { Assignment = SelectedAssignment });
            }
        }


        public void Remove()
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ContentItems)));
            }
        }
    }
}
