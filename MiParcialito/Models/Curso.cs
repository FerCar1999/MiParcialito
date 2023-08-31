using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiParcialito.Models;

public partial class Curso
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CursoId { get; set; }

    public string? NombreCurso { get; set; }

    public int? UserId { get; set; }
    public virtual User? User { get; } = new User();

    public virtual ICollection<Inscripcion> Inscripciones { get;} = new List<Inscripcion>();
}
