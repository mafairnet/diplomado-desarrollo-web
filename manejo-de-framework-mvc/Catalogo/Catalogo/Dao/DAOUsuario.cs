using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.SqlClient;

using System.Text;
using System.Collections;

using System.Configuration;

using Catalogo.Dto;
using Catalogo.Models;
using System.Web.Configuration;

namespace Catalogo.Dao
{
    public class DAOUsuario {

        public bool DeleteUsuario(DTOUsuario dto)
        {
            var result = false;

            try
            {
                StringBuilder SqlString = new StringBuilder();
                StringBuilder Fields = new StringBuilder();
                ArrayList Parameters = new ArrayList();

                SqlString.Append("delete from users ");

                if (dto == null)
                {
                    throw new NullReferenceException("DAOUsuario.DeleteUsuario(dto)");
                }


                
                
                if (dto.Id > 0)
                {
                    SqlString.Append(" WHERE id = " + dto.Id);
                }

                SqlCommand newQuery = SqlEditOperation(SqlString.ToString());
                //SqlDataAdapter da = new SqlDataAdapter(newQuery);
                //var cantEditados = newQuery.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw (ex);
            }
            return result;
        }
        public bool EditUsuario(DTOUsuario dto)
        {
            var result = false;

            try
            {
                StringBuilder SqlString = new StringBuilder();
                StringBuilder Fields = new StringBuilder();
                ArrayList Parameters = new ArrayList();

                SqlString.Append("update users set ");

                if (dto == null)
                {
                    throw new NullReferenceException("DAOUsuario.EditUsuario(dto)");
                }

                
                if (dto.FirstName != null)
                {
                    Fields.Append("first_name = '"+dto.FirstName+"' , ");
                }

                if (dto.SecondName != null)
                {
                    Fields.Append("second_name = '"+dto.SecondName+"' , ");
                }

                if (dto.UserName != null)
                {
                    Fields.Append("username = '"+dto.UserName+"' , ");
                }

                if (dto.Password != null)
                { 
                    Fields.Append("password = '"+dto.Password+"' , ");
                }

                if (Fields.Length > 0)
                {
                    SqlString.Append(Fields.ToString().Substring(0, Fields.ToString().Length - 2));
                    SqlString.Append(" WHERE id = " + dto.Id);
                }

                SqlCommand newQuery = SqlEditOperation(SqlString.ToString() );
                //SqlDataAdapter da = new SqlDataAdapter(newQuery);
                //var cantEditados = newQuery.ExecuteNonQuery();

            }
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                throw (ex);
            }
            return result;
        }

        public bool AddUsuario(DTOUsuario dto)
        {
            var result = false;

            try
            {
                StringBuilder SqlString = new StringBuilder();
                StringBuilder Fields = new StringBuilder();
                ArrayList Parameters = new ArrayList();

                SqlString.Append("insert into users (first_name, second_name, username, password) values  ");

                if (dto == null)
                {
                    throw new NullReferenceException("DAOUsuario.EditUsuario(dto)");
                }


                if (dto.FirstName == null)
                {
                    dto.FirstName = "";
                }

                if (dto.SecondName == null)
                {
                    dto.SecondName = "";
                }

                if (dto.UserName == null)
                {
                    dto.UserName = "";
                }

                if (dto.Password == null)
                {
                    dto.Password = "";
                }

                SqlString.Append("('" + dto.FirstName + "', '" + dto.SecondName + "', '" + dto.UserName + "', '" +dto.Password + "')");


                SqlCommand newQuery = SqlEditOperation(SqlString.ToString());
                //SqlDataAdapter da = new SqlDataAdapter(newQuery);
                //var cantEditados = newQuery.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw (ex);
            }
            return result;
        }
        public List<Usuario> GetUsuarios(DTOUsuario dto)
        {
            var usuarios = new List<Usuario>();

            try
            {
                DataSet ds = new DataSet();

                StringBuilder SqlString = new StringBuilder();
                StringBuilder Fields = new StringBuilder();
                ArrayList Parameters = new ArrayList();

                SqlString.Append("Select id, first_name, second_name, username, password from users ");

                if (dto == null)
                {
                    throw new NullReferenceException("DAOUaruio.GetUsuario(dto)");
                }

                if (dto.Id != null)
                {
                    Fields.Append("id = @Id AND");
                    Parameters.Add(new SqlParameter("@Id", (object)dto.Id));

                }

                if (dto.FirstName != null)
                {
                    Fields.Append("first_name = @FirstName AND");
                    Parameters.Add(new SqlParameter("@FirstName", (object)dto.FirstName));
                }

                if (dto.SecondName != null)
                {
                    Fields.Append("second_name = @SecondName AND");
                    Parameters.Add(new SqlParameter("@SecondName", (object)dto.SecondName));
                }

                if (dto.UserName != null)
                {
                    Fields.Append("username = @UserName AND");
                    Parameters.Add(new SqlParameter("@UserName", (object)dto.UserName));
                }

                if (dto.Password != null)
                {
                    Fields.Append("password = @Password AND");
                    Parameters.Add(new SqlParameter("@Password", (object)dto.Password));
                }

                if (Fields.Length > 0)
                {
                    SqlString.Append(" WHERE ");
                    SqlString.Append(Fields.ToString().Substring(0, Fields.ToString().Length - 4));
                }

                SqlCommand newQuery = SqlOperation(SqlString.ToString(), Parameters);
                SqlDataAdapter da = new SqlDataAdapter(newQuery);

                da.Fill(ds);

                if (ds != null)
                {
                    foreach (DataTable table in ds.Tables)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            var usuarioActual = new Usuario { 
                                ID = int.Parse(row["id"].ToString()),
                                FirstName= row["first_name"].ToString(),
                                SecondName = row["second_name"].ToString(),
                                Username = row["username"].ToString(),
                                Password = row["password"].ToString()
                            };
                            /*System.Diagnostics.Debug.WriteLine(row["id"].ToString());
                            System.Diagnostics.Debug.WriteLine(row["first_name"].ToString());
                            System.Diagnostics.Debug.WriteLine(row["second_name"].ToString());
                            System.Diagnostics.Debug.WriteLine(row["username"].ToString());
                            System.Diagnostics.Debug.WriteLine(row["password"].ToString());*/
                            //usuarioActual = row["id"].ToString() + "," + row["first_name"].ToString() + "," + row["second_name"].ToString() + "," + row["username"].ToString() + "," + row["password"].ToString();
                            //usuariosActuales += usuarioActual;
                            //usuariosActuales.Add(usuarioActual);
                            usuarios.Add(usuarioActual);
                        }

                    }
                }

                //return (ds);
                return (usuarios);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw (ex);
            }
        }

        public DataSet GetUsuario(DTOUsuario dto) {

            try
            {
                DataSet ds = new DataSet();

                StringBuilder SqlString = new StringBuilder();
                StringBuilder Fields = new StringBuilder();
                ArrayList Parameters = new ArrayList();

                SqlString.Append("Select id, first_name, second_name, username, password from users ");

                if (dto == null)
                {
                    throw new NullReferenceException("DAOUaruio.GetUsuario(dto)");
                }

                if (dto.Id != null)
                {
                    Fields.Append("id = @Id AND");
                    Parameters.Add(new SqlParameter("@Id", (object)dto.Id));

                }

                if (dto.FirstName != null)
                {
                    Fields.Append("first_name = @FirstName AND");
                    Parameters.Add(new SqlParameter("@FirstName", (object)dto.FirstName));
                }

                if (dto.SecondName != null)
                {
                    Fields.Append("second_name = @SecondName AND");
                    Parameters.Add(new SqlParameter("@SecondName", (object)dto.SecondName));
                }

                if (dto.UserName != null)
                {
                    Fields.Append("username = @UserName AND");
                    Parameters.Add(new SqlParameter("@UserName", (object)dto.UserName));
                }

                if (dto.Password != null)
                {
                    Fields.Append("password = @Password AND");
                    Parameters.Add(new SqlParameter("@Password", (object)dto.Password));
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
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                throw (ex);
            }
        }

        private SqlCommand SqlOperation(string SqlQuery, ArrayList Parameters) {
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
            catch (Exception ex) {
                Console.WriteLine(ex.ToString());
                throw (ex);
            }
        }

        private SqlCommand SqlEditOperation(string SqlQuery )
        {
            try
            {
                SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbServer"].ConnectionString);
                SqlCommand newQuery = new SqlCommand(SqlQuery, connection);
                newQuery.CommandType = CommandType.Text;

                connection.Open();

                var cantEditados = newQuery.ExecuteNonQuery();

                connection.Close();

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