using Canvas_Interface.Dialogs;
using Canvas_Interface.ViewModels;
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
    public sealed partial class StudentLoggedIn : Page
    {
        private StudentViewModel studentViewModel;
        public StudentLoggedIn()
        {
            this.InitializeComponent();
            studentViewModel = new StudentViewModel();
            DataContext = studentViewModel;
        }

        private async void Create_Student(object sender, RoutedEventArgs e)
        {
            var diag = new StudentCreate((DataContext as StudentViewModel).StudentList, StudentViewModel.SelectedStudent);
            await diag.ShowAsync();
        }

        private void ViewCourse(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(StudentViewCourses));
        }

        private void ViewGrades(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(StudentGradeView));
        }

        private void BackClicked(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NewStudentView));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
