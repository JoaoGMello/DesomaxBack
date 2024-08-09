using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DesomaxBack.Models
{
    public class ModelBase
    {
        [Key, Column(TypeName = "UniqueIdentifier")]
        public Guid Id { get; set; } = Guid.NewGuid();
        public bool Excluded { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? ChangeDate { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? InclusionDate { get; set; }
    }
}
