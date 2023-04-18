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
    public sealed partial class CreateAnnouncement : ContentDialog
    {
        private readonly IList<Announcement> announcementList;
        private readonly Announcement existingAnnouncement;

        public CreateAnnouncement(IList<Announcement> announcementList, Announcement existingAnnouncement = null)
        {
            this.InitializeComponent();
            this.announcementList = announcementList;

            if (existingAnnouncement != null)
            {
                this.existingAnnouncement = existingAnnouncement;
                Title = "Edit Announcement";
                PrimaryButtonText = "Save";
                DataContext = existingAnnouncement;
            }
            else
            {
                Title = "Create Announcement";
                PrimaryButtonText = "Create";
                DataContext = new Announcement();
            }
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (existingAnnouncement == null)
            {
                announcementList.Add(DataContext as Announcement);
            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}