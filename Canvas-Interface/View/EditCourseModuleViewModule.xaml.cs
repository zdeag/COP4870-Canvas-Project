using Canvas_Interface.Dialogs;
using Canvas_Interface.ViewModels;
using Class.Library.Canvas.Models.Courses;
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
    public sealed partial class EditCourseModuleViewModule : Page
    {
        private InstructorViewModel instructorViewModel;

        public EditCourseModuleViewModule()
        {
            this.InitializeComponent();
            instructorViewModel = new InstructorViewModel();
            DataContext = instructorViewModel;
        }

        private async void CreateModule(object sender, RoutedEventArgs e)
        {
            var diag = new CreateModule(InstructorViewModel.SelectedCourse.Modules);
            await diag.ShowAsync();
        }

        private async void EditModule(object sender, RoutedEventArgs e)
        {
            var diag = new CreateModule(InstructorViewModel.SelectedCourse.Modules, (DataContext as InstructorViewModel).SelectedModule);
            await diag.ShowAsync();
        }

        private async void CreateFilePath(object sender, RoutedEventArgs e)
        {
            if ((DataContext as InstructorViewModel).SelectedModule != null)
            {
                   var diag = new CreateFilePath((DataContext as InstructorViewModel).SelectedModule.ContentItems);
                   await diag.ShowAsync();
            };
        }

        private async void EditFilePath(object sender, RoutedEventArgs e)
        {
            if ((DataContext as InstructorViewModel).SelectedModule != null)
            {
                    var diag = new CreateFilePath((DataContext as InstructorViewModel).SelectedModule.ContentItems, instructorViewModel.SelectedContentItem);
                    await diag.ShowAsync();
            };
        }

        private async void CreatePageItem(object sender, RoutedEventArgs e)
        {
            if ((DataContext as InstructorViewModel).SelectedModule != null)
            {
                var diag = new CreatePageItem((DataContext as InstructorViewModel).SelectedModule.ContentItems);
                await diag.ShowAsync();
            }
        }

        private async void EditPageItem(object sender, RoutedEventArgs e)
        {
            if ((DataContext as InstructorViewModel).SelectedModule != null)
            {
                var diag = new CreatePageItem((DataContext as InstructorViewModel).SelectedModule.ContentItems, instructorViewModel.SelectedContentItem);
                await diag.ShowAsync();
            }
        }

        private void DeleteModule(object sender, RoutedEventArgs e)
        {
            (DataContext as InstructorViewModel).RemoveModule();
        }

        private void DeleteContent(object sender, RoutedEventArgs e)
        {
            (DataContext as InstructorViewModel).RemoveContent();
        }

        private void GoToAssignmentAdd(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddAssignmentToModule));
        }

        private void BackClicked(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CourseEdit));
        }
    }
}
