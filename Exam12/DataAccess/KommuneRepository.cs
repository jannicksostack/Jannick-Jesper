using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using ContactsEditor_MVVM.Model;

namespace ContactsEditor_MVVM.DataAccess
{
    public class KommuneRepository : Repository, IEnumerable<Kommune>
    {
        private List<Kommune> list = new List<Kommune>();

        public IEnumerator<Kommune> GetEnumerator()
        {
            return list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Search(string code, string name)
        {
            try
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Kommune WHERE Code LIKE @Code AND Name LIKE @Name", connection);
                command.Parameters.Add(CreateParam("@Code", code + "%", SqlDbType.NVarChar));
                command.Parameters.Add(CreateParam("@Name", name + "%", SqlDbType.NVarChar));
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                list.Clear();
                while (reader.Read()) list.Add(new Kommune(reader[0].ToString(), reader[1].ToString()));
                OnChanged(DbOperation.SELECT, DbModeltype.Kommune);
            }
            catch (Exception ex)
            {
                throw new DbException("Error in Kommune repositiory: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open) connection.Close();
            }
        }

        public void Add(string code, string name)
        {
            string error = "";
            name = name.Trim();
            Kommune kommune = new Kommune(code, name);
            if (kommune.IsValid)
            {
                try
                {
                    SqlCommand command = new SqlCommand("INSERT INTO Kommune (code, name) VALUES (@Code, @Name)", connection);
                    command.Parameters.Add(CreateParam("@Code", code, SqlDbType.NVarChar));
                    command.Parameters.Add(CreateParam("@Name", name, SqlDbType.NVarChar));
                    connection.Open();
                    if (command.ExecuteNonQuery() == 1)
                    {
                        list.Add(kommune);
                        list.Sort();
                        OnChanged(DbOperation.INSERT, DbModeltype.Kommune);
                        return;
                    }
                    error = string.Format("{0} {1} could not be inserted in the database", code, name);
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                }
                finally
                {
                    if (connection != null && connection.State == ConnectionState.Open) connection.Close();
                }
            }
            else error = "Illegal value for code or name";
            throw new DbException("Error in Kommune repositiory: " + error);
        }

        public void Update(string code, string name)
        {
            string error = "";
            name = name.Trim();
            if (name.Length > 0)
            {
                try
                {
                    SqlCommand command = new SqlCommand("UPDATE Kommune SET Name = @Name WHERE code = @Code", connection);
                    command.Parameters.Add(CreateParam("@Code", code, SqlDbType.NVarChar));
                    command.Parameters.Add(CreateParam("@Name", name, SqlDbType.NVarChar));
                    connection.Open();
                    if (command.ExecuteNonQuery() == 1)
                    {
                        for (int i = 0; i < list.Count; ++i)
                            if (list[i].Code.Equals(code))
                            {
                                list[i].Name = name;
                                break;
                            }
                        OnChanged(DbOperation.UPDATE, DbModeltype.Kommune);
                        return;
                    }
                    error = string.Format("Kommune {0} could not be updated", code);
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                }
                finally
                {
                    if (connection != null && connection.State == ConnectionState.Open) connection.Close();
                }
            }
            else error = "Illegal value for name";
            throw new DbException("Error in Kommune repositiory: " + error);
        }

        public void Remove(string code)
        {
            string error = "";
            try
            {
                SqlCommand command = new SqlCommand("DELETE FROM Kommune WHERE Code = @Code", connection);
                command.Parameters.Add(CreateParam("@Code", code, SqlDbType.NVarChar));
                connection.Open();
                if (command.ExecuteNonQuery() == 1)
                {
                    list.Remove(new Kommune(code, ""));
                    OnChanged(DbOperation.DELETE, DbModeltype.Kommune);
                    return;
                }
                error = string.Format("Kommune {0} could not be deleted", code);
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open) connection.Close();
            }
            throw new DbException("Error in Kommune repositiory: " + error);
        }

        public static string GetName(string code)
        {
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(ConfigurationManager.ConnectionStrings["post"].ConnectionString);
                SqlCommand command = new SqlCommand("SELECT Name FROM Kommune WHERE Code = @Code", connection);
                SqlParameter param = new SqlParameter("@Code", SqlDbType.NVarChar);
                param.Value = code;
                command.Parameters.Add(param);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read()) return reader[0].ToString();
            }
            catch
            {
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open) connection.Close();
            }
            return "";
        }
    }
}
