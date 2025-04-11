using System.ComponentModel.DataAnnotations;

namespace DBZ.Infraestructure.Models
{
    public class Character
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(25)]
        public string Name { get; set; }

        [StringLength(35)]
        public string Ki { get; set; }

        [StringLength(25)]
        public string Race { get; set; }

        [StringLength(20)]
        public string Gender { get; set; }

        public string Description { get; set; }

        [StringLength(35)]
        public string Affiliation { get; set; }

        public ICollection<Transformation> Transformations { get; set; }
    }
}
