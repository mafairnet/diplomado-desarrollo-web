using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using Catalogo.Dto;
using System.Text;

namespace Catalogo.Dao
{
    public class DAOCalificaciones
    {
        public DataSet GetCalificion(DTOCalificacion dto)
        {

            try
            {
                DataSet ds = new DataSet();

                StringBuilder SqlString = new StringBuilder();
                StringBuilder Fields = new StringBuilder();
                ArrayList Parameters = new ArrayList();

                SqlString.Append("Select id, id_usuario, calificacion from calificaciones ");

                if (dto == null)
                {
                    throw new NullReferenceException("DAOCalificacion.GetCalificacion(dto)");
                }

                if (dto.Id != null)
                {
                    Fields.Append("id = @Id AND");
                    Parameters.Add(new SqlParameter("@Id", (object)dto.Id));

                }

                if (dto.IdUsuario != null)
                {
                    Fields.Append("id_usuario = @IdUsuario AND");
                    Parameters.Add(new SqlParameter("@IdUsuario", (object)dto.IdUsuario));
                }

                if (dto.Calificacion != null)
                {
                    Fields.Append("calificacion = @Calificacion AND");
                    Parameters.Add(new SqlParameter("@Calificacion", (object)dto.Calificacion));
                }

                
                if (Fields.Length > 0)
                {
                    SqlString.Append(" WHERE ");
                    SqlString.Append(Fields.ToString().Substring(0, Fields.ToString().Length - 4));
                }

                SqlCommand newQuery = SqlOperation(SqlString.ToString(), Parameters);
                SqlDataAdapter da = new SqlDataAdapter(newQuery);

                da.Fill(ds);

                return (ds);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw (ex);
            }
        }
        private SqlCommand SqlOperation(string SqlQuery, ArrayList Parameters)
        {
            try
            {
                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbServer"].ConnectionString);
                SqlCommand newQuery = new SqlCommand(SqlQuery, connection);
                newQuery.CommandType = CommandType.Text;

                foreach (SqlParameter p in Parameters)
                {
                    newQuery.Parameters.Add(p);
                }
                return newQuery;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw (ex);
            }
        }
    }
}