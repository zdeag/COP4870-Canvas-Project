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
    public sealed partial class CreatePageItem : ContentDialog
    {
        private IList<ContentItem> contentItems;
        private readonly ContentItem existingPageItem;

        public CreatePageItem(IList<ContentItem> contentItems, ContentItem existingPageItem = null)
        {
            this.InitializeComponent();
            this.contentItems = contentItems;

            if (existingPageItem != null )
            {
                this.existingPageItem = existingPageItem;
                Title = "Edit Page Item";
                PrimaryButtonText = "Save";
                DataContext = existingPageItem;
            } else
            {
                Title = "Create Page Item";
                PrimaryButtonText = "Create";
                DataContext = new PageItem();
            }
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

            if (existingPageItem == null)
            {
                contentItems.Add(DataContext as PageItem);
            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
