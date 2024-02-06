using AgendaExamHAB.Data;
using AgendaExamHAB.Models;
using AgendaExamHAB.Repository;
using AgendaExamHAB.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AgendaExamHAB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPeople _peopleRepository;
        private readonly IPersonContacts _personContactsRepository;

        public HomeController(ILogger<HomeController> logger, IPeople peopleRepository, IPersonContacts personContactsRepository)
        {
            _logger = logger;
            _peopleRepository = peopleRepository;
            _personContactsRepository = personContactsRepository;
        }

        public IActionResult Index()
        {
            var people = _peopleRepository.GetPeople();
            var personContacts = _personContactsRepository.GetPersonContacts();


            // Join LINQ
            var contactViewModels =
                from person in people
                join personContact in personContacts on person.Id equals personContact.PersonId
                select new ContactViewModel
                {
                    Person = new Person
                    {
                        Id = person.Id,
                        Name = person.Name,
                        LastName = person.LastName, 
                        Nationality = person.Nationality
                    },
                    PersonContact = new PersonContact
                    {
                        Type= personContact.Type,
                        ContactName = personContact.ContactName,
                        Value = personContact.Value
                    }
                };

            return View(contactViewModels.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
