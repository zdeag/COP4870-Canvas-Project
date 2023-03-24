using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class.Library.Canvas.Models.Courses
{
    public class AssignmentItem : ContentItem
    {
        public Assignment? Assignment { get; set; }

        public override string ToString()
        {
            return $"Assignment: {Assignment?.ToString() ?? "N/A"}";
        }
    }
}
