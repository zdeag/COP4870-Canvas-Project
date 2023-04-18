using Canvas_Interface.ViewModels;
using Class.Library.Canvas.Models.Courses;
using Class.Library.Canvas.Models.People;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Canvas_Interface.Dialogs
{
    public sealed partial class CourseCreate : ContentDialog
    {
        private IList<Course> courseList;

        public CourseCreate(IList<Course> courseList)
        {
            InitializeComponent();
            DataContext = new Course();
            this.courseList = courseList;
        }

        public CourseCreate(IList<Course> courseList, Course SelectedCourse)
        {
            this.InitializeComponent();
            DataContext = SelectedCourse;
            this.courseList = courseList;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {


            var selectedComboBoxItem = cmbClassification.SelectedItem as ComboBoxItem;
            if (selectedComboBoxItem != null)
            {
                var selectedClassificationString = selectedComboBoxItem.Content as string;
                if (selectedClassificationString != null)
                {
                    var selectedClassification = (PersonClassification)Enum.Parse(typeof(SemesterClassfication), selectedClassificationString);
                    (DataContext as Course).Semester = (Class.Library.Canvas.Models.Courses.SemesterClassfication)selectedClassification;
                }
            }

            var selectedCourse = courseList.FirstOrDefault(a => a.Code == (DataContext as Course)?.Code);

            if (selectedCourse == null)
            {
                courseList.Add(DataContext as Course);
            }
            else
            {
                courseList.Remove(selectedCourse);
                courseList.Add(DataContext as Course);
            };
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

        }

        public enum SemesterClassfication
        {
            Spring, Summer, Fall
        }
    }
}
