using Class.Library.Canvas.Models.People;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class.Library.Canvas.Models.Services
{
    public class PersonService
    {
        private ObservableCollection<Person> PersonList;
        private ObservableCollection<Person> AdminList;

        private static PersonService instance;
        private static readonly object _lock = new object();

        public static PersonService Instance
        {
            get
            {
                if (instance == null)
                {
                    lock(_lock)
                    {
                        if (instance == null)
                        {
                            instance = new PersonService();
                        }
                    }

                    instance = new PersonService();
                }
                return instance;
            }
        }


        public PersonService()
        {
            PersonList = new ObservableCollection<Person>();
            AdminList = new ObservableCollection<Person>();
        }

        public void AddStudent(Person student)
        {
            PersonList.Add(student);
        }

        public void AddAdmin(Person Admin)
        {
            AdminList.Add(Admin);
        }

        public ObservableCollection<Person> Students
        {
            get
            {
                return PersonList;
            }
        }

        public ObservableCollection<Person> Admins
        {
            get
            {
                return AdminList;
            }
        }


        public IEnumerable<Person> Search(string query)
        {
            return PersonList.Where(s => s.Name.ToUpper().Contains(query.ToUpper()));
        }

    }
}
