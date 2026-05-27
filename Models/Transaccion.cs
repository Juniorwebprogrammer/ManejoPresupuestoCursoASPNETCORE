using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ManejoPresupuesto.Models
{
    public class Transaccion
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        [Display(Name = "Fecha Transacción")]
        [DataType(DataType.Date)]
        public DateTime FechaTransaccion { get; set; } = DateTime.Today;
        public decimal Monto { get; set; }
        [Range(1, maximum: int.MaxValue, ErrorMessage = "La categoría es obligatoria.")]
        [DisplayName("Categoría")]
        public int CategoriaId { get; set; }
        [StringLength(maximumLength:1000, ErrorMessage = "La nota no puede exceder los 1000 caracteres.")]
        public string Notas { get; set; }
        [Range(1, maximum: int.MaxValue, ErrorMessage = "La Cuenta es obligatoria.")]
        [DisplayName("Cuenta")]
        public int CuentaId { get; set; }
        [DisplayName("Tipo Operación")]
        public TipoOperacion TipoOperacionId { get; set; } = TipoOperacion.Ingreso;
        public string Cuenta { get; set; }
        public string Categoria { get; set; }
    }
}
