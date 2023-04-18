using Class.Library.Canvas.Models.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class.Library.Canvas.Models.People
{
    public class Student : Person
    {
        public Dictionary<int, double> Grades { get; set; }

        private PersonClassification classification;

        public PersonClassification Classification
        {
            get { return classification; }
            set
            {
                classification = value;
            }
        }

        public Student()
        {
            Grades = new Dictionary<int, double>();
            Classification = PersonClassification.Freshman;
        }

        public override string ToString()
        {
            return $"[{ID}] {Classification} - {Name}";
        }
    }



    public enum PersonClassification
    {
        Freshman, Sophomore, Junior, Senior
    }
}