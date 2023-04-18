using Canvas_Interface.View;
using Canvas_Interface.ViewModels;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Canvas_Interface
{
    public sealed partial class MainPage : Page
    {
        public MainViewModel mainViewModel;

        public MainPage()
        {
            this.InitializeComponent();

            mainViewModel = new MainViewModel();
            DataContext = mainViewModel;
        }

        private void StudentClicked(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Canvas_Interface.View.NewStudentView));
        }

        private void InstructorClicked(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(InstructorView));
        }
    }
}

