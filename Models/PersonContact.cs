using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AgendaExamHAB.Models
{
    public class PersonContact : Audit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContactId { get; set; }

        [Required]
        public int PersonId { get; set; }

        public string? Type { get; set; }
       
        public string? ContactName { get; set; }

        public string? Value { get; set; }

        // Relación con DimPerson
        [ForeignKey(nameof(PersonId))]
        public virtual Person? Person { get; set; }
    }
}
