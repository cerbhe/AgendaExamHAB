using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AgendaExamHAB.Models
{
    public class Person : Audit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? LastName { get; set; }

        public string? Nationality { get; set; }

        // Relationship with UdtPersonContact
        public virtual ICollection<PersonContact>? Contacts { get; set; }
    }
}
