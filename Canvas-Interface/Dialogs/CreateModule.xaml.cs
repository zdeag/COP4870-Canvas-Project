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
    public sealed partial class CreateModule : ContentDialog
    {
        private readonly IList<Module> moduleList;
        private readonly Module existingModule;

        public CreateModule(IList<Module> moduleList, Module existingModule = null)
        {
            this.InitializeComponent();
            this.moduleList = moduleList;

            if (existingModule != null)
            {
                this.existingModule = existingModule;
                Title = "Edit Module";
                PrimaryButtonText = "Save";
                DataContext = new Module
                {
                    Name = existingModule.Name,
                    Description = existingModule.Description
                };
            }
            else
            {
                Title = "Create Module";
                PrimaryButtonText = "Create";
                DataContext = new Module();
            }
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            if (existingModule == null)
            {
                moduleList.Add(DataContext as Module);
            }
            else
            {
                existingModule.Name = (DataContext as Module).Name;
                existingModule.Description = (DataContext as Module).Description;
            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
