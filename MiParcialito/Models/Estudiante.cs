using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiParcialito.Models;

public partial class Estudiante
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int EstudianteId { get; set; }

    public string? Nombre { get; set; }

    public string? Password { get; set; }

    public string? Correo { get; set; }

    [DataType(DataType.Date)]
    public DateTime? FechaNacimiento { get; set; }
    
    public virtual ICollection<Inscripcion> Inscripciones { get; } = new List<Inscripcion>();

}
