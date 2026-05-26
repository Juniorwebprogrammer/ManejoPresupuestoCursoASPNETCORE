using Dapper;
using ManejoPresupuesto.Models;
using Microsoft.Data.SqlClient;

namespace ManejoPresupuesto.Services
{
    public interface IRepositorioCategorias
    {
        Task Actualizar(Categoría categoría);
        Task Crear(Categoría categoría);
        Task Eliminar(int id);
        Task<IEnumerable<Categoría>> Obtener(int usuarioId);
        Task<Categoría> ObtenerPorId(int id, int usuarioId);
    }
    public class RepositorioCategorias : IRepositorioCategorias
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

        public async Task<Categoría> ObtenerPorId(int id, int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);

            return await connection.QueryFirstOrDefaultAsync<Categoría>(@"
                SELECT *
                FROM Categorias
                WHERE Id = @Id AND UsuarioId = @UsuarioId
            ", new { Id = id, UsuarioId = usuarioId });
        }

        public async Task Actualizar(Categoría categoría)
        {
            using var connection = new SqlConnection(connectionString);

            await connection.ExecuteAsync(@"
                UPDATE Categorias
                SET Nombre = @Nombre, TipoOperacionId = @TipoOperacionId
                WHERE Id = @Id AND UsuarioId = @UsuarioId
            ", categoría);
        }

        public async Task Eliminar(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"
                DELETE FROM Categorias
                WHERE Id = @Id
            ", new { Id = id });
        }
    }
}
