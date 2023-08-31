using System.ComponentModel.DataAnnotations.Schema;

namespace MiParcialito.Models
{
    public partial class Inscripcion
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InscripcionId { get; set; }

        public int? CursoId { get; set; }
        public virtual Curso Curso { get; set; } = new Curso();

        public int? EstudianteId { get; set; }
        public virtual Estudiante? Estudiante { get; set; } = new Estudiante();
    }
}
