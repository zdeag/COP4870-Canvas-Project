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
    public sealed partial class CourseAssignmentView : Page
    {
        private InstructorViewModel instructorViewModel;

        public CourseAssignmentView()
        {
            this.InitializeComponent();
            instructorViewModel = new InstructorViewModel();
            DataContext = instructorViewModel;
        }

        private async void CreateAssignmentGroup(object sender, RoutedEventArgs e)
        {
            if ((DataContext as InstructorViewModel).SelectedAssignmentGroup == null)
            {
                var diag = new CreateAssignmentGroup(InstructorViewModel.SelectedCourse?.AssignmentGroups);
                await diag.ShowAsync();
            } else
            {
                var selectedAssignmentGroup = (DataContext as InstructorViewModel).SelectedAssignmentGroup;
                var diag = new CreateAssignmentGroup(InstructorViewModel.SelectedCourse?.AssignmentGroups)
                {
                    DataContext = selectedAssignmentGroup
                };
                await diag.ShowAsync();
            }
        }

        private async void CreateAssignment(object sender, RoutedEventArgs e)
        {
            if (instructorViewModel.SelectedAssignment == null)
            {
                var diag = new CreateAssignment((DataContext as InstructorViewModel).SelectedAssignmentGroup.Assignments);
                await diag.ShowAsync();
            }
            else
            {
                var selectedAssignment = (DataContext as InstructorViewModel).SelectedAssignment;
                var diag = new CreateAssignment((DataContext as InstructorViewModel).SelectedAssignmentGroup.Assignments)
                {
                    DataContext = selectedAssignment
                };
                await diag.ShowAsync();
            }
        }


        private void DeleteAssignmentGroup(object sender, RoutedEventArgs e)
        {
            (DataContext as InstructorViewModel).RemoveAssignmentGroup();
        }

        private void DeleteAssignment(object sender, RoutedEventArgs e)
        {
            (DataContext as InstructorViewModel).RemoveAssignment();
        }

        private void BackClicked(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CourseEdit));
        }

    }
}
