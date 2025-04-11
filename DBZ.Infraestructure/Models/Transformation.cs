using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DBZ.Infraestructure.Models
{
    public class Transformation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(25)]
        public string Name { get; set; }

        [StringLength(35)]
        public string Ki { get; set; }

        public int CharacterId { get; set; }

        [ForeignKey("CharacterId")]
        public Character Character { get; set; }
    }
}
