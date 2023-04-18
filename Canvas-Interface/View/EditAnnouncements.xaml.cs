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
    public sealed partial class EditAnnouncements : Page
    {
        private InstructorViewModel instructorViewModel;

        public EditAnnouncements()
        {
            this.InitializeComponent();
            instructorViewModel = new InstructorViewModel();
            DataContext = instructorViewModel;
        }

        private async void CreateAnnouncement(object sender, RoutedEventArgs e)
        {
            if (instructorViewModel.SelectedAnnouncement == null)
            {
                var diag = new CreateAnnouncement(InstructorViewModel.SelectedCourse?.AnnouncementList);
                await diag.ShowAsync();
            }
            else
            {
                var diag = new CreateAnnouncement(InstructorViewModel.SelectedCourse.AnnouncementList, instructorViewModel.SelectedAnnouncement);
                await diag.ShowAsync();
            }
        }

        private void DeleteAnnouncement(object sender, RoutedEventArgs e)
        {
            (DataContext as InstructorViewModel).RemoveAnnouncement();
        }

        private void BackClicked(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CourseEdit));
        }

    }
}

