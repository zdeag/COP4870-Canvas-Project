using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class.Library.Canvas.Models.Courses
{
    public class FileItem : ContentItem
    {
        public string Path { get; set; }
        public override string ToString()
        {
            return $"[{Name}] [Description: {Description}] File Path: {Path}";
        }
    }
}
