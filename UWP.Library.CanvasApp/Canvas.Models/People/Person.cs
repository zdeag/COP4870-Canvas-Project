using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class.Library.Canvas.Models.People
{
    public class Person
    {
        public string Name { get; set; }

        private static int lastId = 0;
        private int id = 0;
        public int ID
        {
            get
            {
                if (id == 0)
                {
                    id = ++lastId;
                }
                return id;
            }
        }

        public Person()
        {
            Name = string.Empty;
        }

        public override string ToString()
        {
            return $"[{GetType().Name}] [{ID}] {Name}";
        }
    }
}