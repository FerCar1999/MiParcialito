using System.ComponentModel.DataAnnotations.Schema;

namespace MiParcialito.Models
{
    public partial class UserType
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? Nombre { get; set; }

        public virtual ICollection<User> Users { get; } = new List<User>();
    }
}
