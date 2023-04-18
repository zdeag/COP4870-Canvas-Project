using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class.Library.Canvas.Models.Courses
{
    public class Module
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ObservableCollection<ContentItem> ContentItems { get; set; }

        public Module() 
        {
            ContentItems = new ObservableCollection<ContentItem>();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Module Name: {Name}");
            sb.AppendLine($"Description: {Description}");
            return sb.ToString();
        }
    }
}
