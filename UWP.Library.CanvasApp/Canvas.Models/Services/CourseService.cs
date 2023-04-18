using Class.Library.Canvas.Models.Courses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class.Library.Canvas.Models.Services
{
    public class CourseService
    {
        private ObservableCollection<Course> CourseList;
        private static CourseService instance;
        private static readonly object _lock = new object();

        public static CourseService Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (_lock)
                    {
                        if (instance == null)
                        {
                            instance = new CourseService();
                        }
                    }

                    instance = new CourseService();
                }
                return instance;
            }
        }

        public CourseService()
        {
            CourseList = new ObservableCollection<Course>();
        }

        public void AddCourse(Course course)
        {
            CourseList.Add(course);
        }

        public ObservableCollection<Course> Courses
        {
            get
            {
                return CourseList;
            }
        }

        public IEnumerable<Course> Search(string query)
        {
            return Courses.Where(s => s.Name.ToUpper().Contains(query.ToUpper())
                || s.Description.ToUpper().Contains(query.ToUpper())
                || s.Code.ToUpper().Contains(query.ToUpper()));
        }

    }
}