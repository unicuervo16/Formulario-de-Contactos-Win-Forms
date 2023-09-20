using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulario_de_Contactos_Win_Forms
{
    internal class CapaDeAccesoBD
    {
        public SqlConnection conn = new SqlConnection("Server=J-GARBAGNATE\\MSSQLSERVER01;Database=ContactosWinForms;Integrated Security=True;");

        public void InsertarContacto(Contacto contacto)
        {
            try
            {
                conn.Open();
                string query = @"
                INSERT INTO Contactos (Nombre, Apellido, Telefono, Direccion, FechaInscripcion)
                VALUES (@Nombre, @Apellido, @Telefono, @Direccion, @FechaInscripcion)";

                SqlParameter Nombre = new SqlParameter("@Nombre", contacto.Nombre);
                SqlParameter Apellido = new SqlParameter("@Apellido", contacto.Apellido);
                SqlParameter Telefono = new SqlParameter("@Telefono", contacto.Telefono);
                SqlParameter Direccion = new SqlParameter("@Direccion", contacto.Direccion);
                SqlParameter FechaInscripcion = new SqlParameter("@FechaInscripcion", contacto.FechaInscripcion);

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.Add(Nombre);
                command.Parameters.Add(Apellido);
                command.Parameters.Add(Telefono);
                command.Parameters.Add(Direccion);
                command.Parameters.Add(FechaInscripcion);

                command.ExecuteNonQuery();

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }
        }
        public void ActualizarContacto(Contacto contacto)
        {

            try
            {
                conn.Open();
                string query = @"UPDATE Contactos
                                SET Nombre = @Nombre,
                                 Apellido = @Apellido,
                                 Telefono = @Telefono,
                                 Direccion = @Direccion,
                                 FechaInscripcion = @FechaInscripcion
                                WHERE Id = @Id ";

                SqlParameter Id = new SqlParameter("@Id", contacto.Id);
                SqlParameter Nombre = new SqlParameter("@Nombre", contacto.Nombre);
                SqlParameter Apellido = new SqlParameter("@Apellido", contacto.Apellido);
                SqlParameter Telefono = new SqlParameter("@Telefono", contacto.Telefono);
                SqlParameter Direccion = new SqlParameter("@Direccion", contacto.Direccion);
                SqlParameter FechaInscripcion = new SqlParameter("@FechaInscripcion", contacto.FechaInscripcion);

                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.Add(Id);
                command.Parameters.Add(Nombre);
                command.Parameters.Add(Apellido);
                command.Parameters.Add(Telefono);
                command.Parameters.Add(Direccion);
                command.Parameters.Add(FechaInscripcion);
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally { conn.Close(); }
        }

        public void EliminarContacto(int id)
        {
            try
            {
                conn.Open();
                string query = @"DELETE FROM Contactos WHERE Id = @Id";
                SqlCommand command = new SqlCommand(@query, conn);
                command.Parameters.Add(new SqlParameter("@Id", id));
                command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally { conn.Close(); }
        }

        public List<Contacto> TraerContactos(string textoBusqueda = null)
        {
            List<Contacto> contactos = new List<Contacto>();
            try
            {
                conn.Open();
                string query = @"SELECT Id, Nombre, Apellido, Telefono, Direccion, FechaInscripcion FROM Contactos";

                SqlCommand command = new SqlCommand();

                if (!string.IsNullOrEmpty(textoBusqueda))
                {
                    query += @" WHERE Nombre LIKE @textoBusqueda 
                OR Apellido LIKE @textoBusqueda 
                OR Telefono LIKE @textoBusqueda 
                OR Direccion LIKE @textoBusqueda 
                OR CONVERT(NVARCHAR(10), FechaInscripcion, 120) LIKE @textoBusqueda";
                    command.Parameters.Add(new SqlParameter("@textoBusqueda", $"%{textoBusqueda}%"));
                }

                command.CommandText = query;
                command.Connection = conn;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    contactos.Add(new Contacto()
                    {
                        Id = int.Parse(reader["Id"].ToString()),
                        Nombre = reader["Nombre"].ToString(),
                        Apellido = reader["Apellido"].ToString(),
                        Telefono = reader["Telefono"].ToString(),
                        Direccion = reader["Direccion"].ToString(),
                        FechaInscripcion = DateTime.Parse(reader["FechaInscripcion"].ToString())
                    });
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally { conn.Close(); }
            return contactos;
        }
    }

}
