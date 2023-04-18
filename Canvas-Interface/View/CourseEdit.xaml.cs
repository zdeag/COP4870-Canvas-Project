using Canvas_Interface.Dialogs;
using Canvas_Interface.ViewModels;
using Class.Library.Canvas.Models.Services;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Canvas_Interface.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CourseEdit : Page
    {
        private InstructorViewModel instructorViewModel;

        public CourseEdit()
        {
            this.InitializeComponent();
            instructorViewModel = new InstructorViewModel();
            DataContext = instructorViewModel;
        }

        private async void CreateCourse(object sender, RoutedEventArgs e)
        {
            var diag = new CourseCreate((DataContext as InstructorViewModel).CourseList);
            await diag.ShowAsync();
        }

        private async void EditCourse(object sender, RoutedEventArgs e)
        {
            (DataContext as InstructorViewModel).EditCourseInformation();
        }

        private void AnnouncementEditClicked(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EditAnnouncements));
        }

        private void ModuleEditClicked(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EditCourseModuleViewModule));
        }

        private void AssignmentEditClicked(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CourseAssignmentView));
        }

        private void RosterEdit(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CourseStudentView));
        }

        private void GradeAssignments(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AssignmentGrading));
        }

        private void BackClicked(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(InstructorView));
        }


    }
}
