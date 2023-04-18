using Class.Library.Canvas.Models.Services;

namespace Canvas_Interface.ViewModels
{
    public class MainViewModel
    {
        public PersonService personService;
        public CourseService courseService;
        public MainViewModel()
        {
            courseService = CourseService.Instance;
            personService = PersonService.Instance;
        }
    }

}
