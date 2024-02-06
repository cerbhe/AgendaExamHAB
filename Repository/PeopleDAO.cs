using AgendaExamHAB.Data;
using AgendaExamHAB.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;

namespace AgendaExamHAB.Repository
{
    public class PeopleDAO : IPeople
    {
        private readonly ApplicationDbContext _context;

        public PeopleDAO(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Person> GetPeople()
        {
            return _context.People.ToList();
        }
    }
}
