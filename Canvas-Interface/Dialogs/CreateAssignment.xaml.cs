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

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Canvas_Interface.Dialogs
{
    public sealed partial class CreateAssignment : ContentDialog
    {
        private IList<Assignment> assignments;


        public CreateAssignment(IList<Assignment> assignments)
        {
            this.InitializeComponent();
            DataContext = new Assignment();
            this.assignments = assignments;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var selectedAssignment = assignments.FirstOrDefault(a => a.Id == (DataContext as Assignment)?.Id);
            if (selectedAssignment != null)
            {
                selectedAssignment.Name = (DataContext as Assignment)?.Name;
                selectedAssignment.Description = (DataContext as Assignment)?.Description;
                selectedAssignment.TotalAvailablePoints = (DataContext as Assignment)?.TotalAvailablePoints ?? 0;
                selectedAssignment.DueDate = (DataContext as Assignment)?.DueDate as DateTime? ?? DateTime.MinValue;
            }
            else
            {
                assignments.Add(DataContext as Assignment);
            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
