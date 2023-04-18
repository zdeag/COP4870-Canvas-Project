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
    public sealed partial class CreateInstructor : ContentDialog
    {

        private IList<Person> personList;

        public CreateInstructor(IList<Person> personList)
        {
            this.InitializeComponent();
            this.personList = personList;
            DataContext = new Instructor();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            personList.Add(DataContext as Instructor);
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
