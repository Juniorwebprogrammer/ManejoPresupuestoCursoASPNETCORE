namespace ManejoPresupuesto.Services
{
    public interface IServiciosUsuarios
    {
        int ObtenerUsuarioId();
    }

    public class ServiciosUsuarios: IServiciosUsuarios
    {
        public int ObtenerUsuarioId()
        {
            return 1;
        }
    }
}
