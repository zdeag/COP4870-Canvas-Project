using Class.Library.Canvas.Models.People;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class.Library.Canvas.Models.Courses
{
    public class Course
    {
        private SemesterClassfication semester;
        public string Code { get; set; }
        public string Name { get; set; }
        public string roomNumber { get; set; }
        public string Description { get; set; }
        public decimal AssignmentWeightLimit { get; set; }
        public int CreditHours { get; set; }
        public ObservableCollection<Person> Roster { get; set; }
        public ObservableCollection<Assignment> Assignments { get; set; }
        public ObservableCollection<CompletedAssignment> CompletedAssignments { get; set; }
        public ObservableCollection<Module> Modules { get; set; }
        public ObservableCollection<AssignmentGroup> AssignmentGroups { get; set; }
        public ObservableCollection<Announcement> AnnouncementList { get; set; }

        public SemesterClassfication Semester
        {
            get { return semester; }
            set
            {
                semester = value;
            }
        }


        public Course()
        {
            Code = string.Empty;
            Name = string.Empty;
            roomNumber = string.Empty;
            Description = string.Empty;
            AssignmentWeightLimit = 100;
            Roster = new ObservableCollection<Person>();
            Assignments = new ObservableCollection<Assignment>();
            CompletedAssignments = new ObservableCollection<CompletedAssignment>();
            AnnouncementList = new ObservableCollection<Announcement>();
            Modules = new ObservableCollection<Module>();
            AssignmentGroups = new ObservableCollection<AssignmentGroup>();
        }

        public override string ToString()
        {
            return $"[{Code}] [{roomNumber}] {Name}";
        }

        public string DetailDisplay
        {
            get
            {
                return $"{ToString()}\n{Description}\n\n" +
                    $"Roster:\n{string.Join("\n", Roster.Select(s => s.ToString()).ToArray())}\n\n" +
                    $"Assignments:\n{string.Join("\n", Assignments.Select(a => a.ToString()).ToArray())}";
            }
        }

    }

    public enum SemesterClassfication
    {
        Spring, Summer, Fall
    }

    public static class SemesterClassficationExtensions
    {
        public static IEnumerable<SemesterClassfication> GetValues()
        {
            return Enum.GetValues(typeof(SemesterClassfication)).Cast<SemesterClassfication>();
        }
    }

}
