using AgendaExamHAB.Data;
using AgendaExamHAB.Models;

namespace AgendaExamHAB.Repository
{
    public class PersonContactsDAO : IPersonContacts
    {
        private readonly ApplicationDbContext _context;

        public PersonContactsDAO(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<PersonContact> GetPersonContacts()
        {
            return _context.PersonContacts.ToList();
        }
    }
}
