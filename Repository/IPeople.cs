using AgendaExamHAB.Models;
using System.Diagnostics.Contracts;

namespace AgendaExamHAB.Repository
{
    public interface IPeople
    {
        List<Person> GetPeople();
    }
}
