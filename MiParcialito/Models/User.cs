using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiParcialito.Models;

public partial class User
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string? Email { get; set; }

    public int? Edad { get; set; }
    public string? Password { get; set; }

    public int UserTypeId { get; set; }
    public virtual UserType? UserType { get; set; } = new UserType(); 

    public virtual ICollection<Curso> Cursos { get; set; } = new List<Curso>();
}
