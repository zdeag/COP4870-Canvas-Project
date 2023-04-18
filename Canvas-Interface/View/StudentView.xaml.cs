using Canvas_Interface.Dialogs;
using Canvas_Interface.ViewModels;
using Class.Library.Canvas.Models.People;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
    public sealed partial class NewStudentView : Page
    {
        private StudentViewModel studentViewModel;

        public NewStudentView()
        {
            this.InitializeComponent();
            studentViewModel = new StudentViewModel();
            DataContext = studentViewModel;
        }

        private async void Create_Student(object sender, RoutedEventArgs e)
        {
            var diag = new StudentCreate((DataContext as StudentViewModel).StudentList);
            await diag.ShowAsync();
        }

        private void UserLogIn(object sender, RoutedEventArgs e)
        {
            if (StudentViewModel.SelectedStudent != null)
            {
                Frame.Navigate(typeof(StudentLoggedIn));
            }
        }

        private void SearchQuery(object sender, RoutedEventArgs e)
        {
            (DataContext as StudentViewModel).SearchQuery = SearchBox.Text;
            (DataContext as StudentViewModel).SearchStudents();
        }

        private void BackClicked(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Canvas_Interface.MainPage));
        }
    }
}
