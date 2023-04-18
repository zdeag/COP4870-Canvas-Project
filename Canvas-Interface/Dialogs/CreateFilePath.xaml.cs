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
    public sealed partial class CreateFilePath : ContentDialog
    {
        private IList<ContentItem> contentItems;
        private readonly ContentItem existingFilePath;
        public CreateFilePath(IList<ContentItem> contentItems, ContentItem existingFilePath = null)
        {
            this.InitializeComponent();
            this.contentItems = contentItems;

            if (existingFilePath != null)
            {
                this.existingFilePath = existingFilePath;
                Title = "Edit File Path";
                PrimaryButtonText = "Save";
                DataContext = existingFilePath;
            }
            else
            {
                Title = "Create FilePath";
                PrimaryButtonText = "Create";
                DataContext = new FileItem();
            }
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (existingFilePath == null)
            {
                contentItems.Add(DataContext as FileItem);
            }
        }


        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
