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
    public sealed partial class AddAssignmentToModule : Page
    {
        private CourseModuleViewModel courseModuleViewModel;

        public AddAssignmentToModule()
        {
            this.InitializeComponent();
            courseModuleViewModel = new CourseModuleViewModel();
            DataContext = courseModuleViewModel;
        }

        private void AddAssignment(object sender, RoutedEventArgs e)
        {
            (DataContext as CourseModuleViewModel).AddAssignment();
        }

        private void DeleteContent(object sender, RoutedEventArgs e)
        {
            (DataContext as CourseModuleViewModel).RemoveContent();
        }

        private void BackClicked(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EditCourseModuleViewModule));
        }

    }
}
