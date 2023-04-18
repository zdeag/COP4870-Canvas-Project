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
    public sealed partial class StudentCreate : ContentDialog
    {
        private IList<Person> studentList;
        private readonly Student existingStudent;

        public StudentCreate(IList<Person> studentList, Student existingStudent = null)
        {
            this.InitializeComponent();
            this.studentList = studentList;

            if (existingStudent != null)
            {
                this.existingStudent = existingStudent;
                Title = "Edit Student";
                PrimaryButtonText = "Save";
                DataContext = existingStudent;
            } else
            {
                Title = "Create Student";
                PrimaryButtonText = "Create";
                DataContext = new Student();
            }
        }

        public enum PersonClassification
        {
            Freshman,
            Sophomore,
            Junior,
            Senior
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var selectedComboBoxItem = cmbClassification.SelectedItem as ComboBoxItem;
            if (selectedComboBoxItem != null)
            {
                var selectedClassificationString = selectedComboBoxItem.Content as string;
                if (selectedClassificationString != null)
                {
                    var selectedClassification = (PersonClassification)Enum.Parse(typeof(PersonClassification), selectedClassificationString);
                    (DataContext as Student).Classification = (Class.Library.Canvas.Models.People.PersonClassification)selectedClassification;

                    if (existingStudent == null)
                    {
                        studentList.Add(DataContext as Student);
                    }
                }
            }
        }



        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
