using Class.Library.Canvas.Models.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class.Library.Canvas.Models.Services
{
    public class PersonService
    {
        private List<Person> personList;

        private static PersonService? _instance;

        private PersonService()
        {
            personList = new List<Person>();
        }

        public static PersonService Current
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PersonService();
                }

                return _instance;
            }
        }

        public void AddStudent(Person student)
        {
            personList.Add(student);
        }

        public List<Person> Students
        {
            get
            {
                return personList;
            }
        }


        public IEnumerable<Person> Search(string query)
        {
            return personList.Where(s => s.Name.ToUpper().Contains(query.ToUpper()));
        }

    }
}
