using System.ComponentModel.DataAnnotations;

namespace BuildManager.Models
{
    public class Whom
    {
        [Key]
        public int WhomId { get; set; }

        [Required, MaxLength(200)]
        public string WhomName { get; set; } = string.Empty;
    }
}
