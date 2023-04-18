using Canvas_Interface.Dialogs;
using Canvas_Interface.ViewModels;
using Class.Library.Canvas.Models.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
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
    public sealed partial class InstructorView : Page
    {
        private InstructorViewModel instructorViewModel;
        private StudentViewModel studentViewModel;

        public InstructorView()
        {
            this.InitializeComponent();
            instructorViewModel = new InstructorViewModel();
            studentViewModel = new StudentViewModel();
            DataContext = instructorViewModel;
        }

        private void SearchQuery(object sender, RoutedEventArgs e)
        {
            (DataContext as InstructorViewModel).SearchQuery = SearchBox.Text;
            (DataContext as InstructorViewModel).SearchCourse();
        }

        private async void CreateCourse(object sender, RoutedEventArgs e)
        {
            var diag = new CourseCreate((DataContext as InstructorViewModel).CourseList);
            await diag.ShowAsync();
        }

        private async void CreateInstructor(object sender, RoutedEventArgs e)
        {
            var diag = new CreateInstructor(instructorViewModel.AdminList);
            await diag.ShowAsync();
        }

        private async void CreateTA(object sender, RoutedEventArgs e)
        {
            var diag = new CreateTA(instructorViewModel.AdminList);
            await diag.ShowAsync();
        }

        private void EditClicked(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CourseEdit));
        }

        private void DeleteCourse(object sender, RoutedEventArgs e)
        {
            (DataContext as InstructorViewModel).Remove();
        }

        private void BackClicked(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Canvas_Interface.MainPage));
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
