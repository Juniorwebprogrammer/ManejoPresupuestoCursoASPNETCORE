using Dapper;
using ManejoPresupuesto.Models;
using Microsoft.Data.SqlClient;

namespace ManejoPresupuesto.Services
{
    public interface IRepositorioCategorias
    {
        Task Crear(Categoría categoría);
        Task<IEnumerable<Categoría>> Obtener(int usuarioId);
    }
    public class RepositorioCategorias: IRepositorioCategorias
    {
        private readonly string connectionString;
        public RepositorioCategorias(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(Categoría categoría)
        {
            using var connection = new SqlConnection(connectionString);

            var id = await connection.QuerySingleAsync<int>(@"
                INSERT INTO Categorias (Nombre, TipoOperacionId, UsuarioId)
                VALUES (@Nombre, @TipoOperacionId, @UsuarioId);
                SELECT SCOPE_IDENTITY();
            ", categoría);

            categoría.Id = id;
        }
    
        public async Task<IEnumerable<Categoría>> Obtener(int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);

            return await connection.QueryAsync<Categoría>(@"
                SELECT *
                FROM Categorias
                WHERE UsuarioId = @UsuarioId
            ", new { UsuarioId = usuarioId });
        }
    }
}
