using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using ContactsEditor_MVVM.Model;

namespace ContactsEditor_MVVM.DataAccess
{
  public class KommuneDataRepository : Repository, IEnumerable<KommuneData>
  {
    private List<KommuneData> list = new List<KommuneData>();

    public IEnumerator<KommuneData> GetEnumerator()
    {
      return list.GetEnumerator();
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

        public void Search(string zipcode, string city, string year)
        {
            try
            {
                SqlCommand command = new SqlCommand("SELECT age_65, age_17_64, age_0_17, dbo.Kommune.Code, Name, Year " +
                    "FROM KommuneData INNER JOIN Kommune ON KommuneData.KommuneCode = Kommune.Code " +
                    "WHERE Kommune.Code LIKE @zipcode AND Name LIKE @City AND Year LIKE @Year", connection);
                command.Parameters.Add(CreateParam("@zipcode", zipcode + "%", SqlDbType.NVarChar));
                command.Parameters.Add(CreateParam("@City", city + "%", SqlDbType.NVarChar));
                command.Parameters.Add(CreateParam("@Year", year + "%", SqlDbType.NVarChar));
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                list.Clear();
                while (reader.Read())
                {
                    KommuneData data = new KommuneData(reader["age_65"].ToString(), reader["age_17_64"].ToString(), reader["age_0_17"].ToString(), reader["Code"].ToString(), reader["Name"].ToString(), reader["Year"].ToString()); ;
                    list.Add(data);
                }
                OnChanged(DbOperation.SELECT, DbModeltype.Kommune);
            }
            catch (Exception ex)
            {
                throw new DbException("Error in Contact repositiory: " + ex.Message);
            }
            finally
            {
                if (connection != null && connection.State == ConnectionState.Open) connection.Close();
            }
        }


        public void Add(KommuneData kommuneData)
        {
            string error = "";
            if (kommuneData.IsValid)
            {
                try
                {
                    kommuneData.OldAgeGrp = kommuneData.OldAgeGrp.Replace(',', '.');
                    kommuneData.MidAgeGrp = kommuneData.MidAgeGrp.Replace(',', '.');
                    kommuneData.YoungAgeGrp = kommuneData.YoungAgeGrp.Replace(',', '.');
                    SqlCommand command = new SqlCommand("INSERT INTO KommuneData (age_0_17, age_17_64, age_65, KommuneCode, Year) VALUES (@YoungAgeGrp, @MidAgeGrp, @OldAgeGrp, @Zipcode, @Year)", connection);
                    command.Parameters.Add(CreateParam("@YoungAgeGrp", kommuneData.YoungAgeGrp, SqlDbType.NVarChar));
                    command.Parameters.Add(CreateParam("@MidAgeGrp", kommuneData.MidAgeGrp, SqlDbType.NVarChar));
                    command.Parameters.Add(CreateParam("@OldAgeGrp", kommuneData.OldAgeGrp, SqlDbType.NVarChar));
                    command.Parameters.Add(CreateParam("@Zipcode", kommuneData.Zipcode, SqlDbType.NVarChar));
                    command.Parameters.Add(CreateParam("@Year", kommuneData.Year, SqlDbType.NVarChar));
                    connection.Open();
                    if (command.ExecuteNonQuery() == 1)
                    {
                        kommuneData.City = KommuneRepository.GetName(kommuneData.Zipcode);
                        list.Add(kommuneData);
                        list.Sort();
                        OnChanged(DbOperation.INSERT, DbModeltype.Kommune);
                        return;
                    }
                    error = string.Format("Address could not be inserted in the database");
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
            else error = "Illegal value for address";
            throw new DbException("Error in Contact repositiory: " + error);
        }

    public void Update(string OldAgeGrp, string MidAgeGrp, string YoungAgeGrp, string zipcode, string city)
    {
      Update(new KommuneData(OldAgeGrp, MidAgeGrp, YoungAgeGrp, zipcode, city));
    }

    public void Update(KommuneData contact)
    {
      string error = "";
      if (contact.IsValid)
      {
        try
        {
          SqlCommand command = new SqlCommand("UPDATE KommuneData SET age_0_17 = @YoungAgeGrp, age_17_64 = @MidAgeGrp, age_65 = @OldAgeGrp WHERE KommuneCode = @Code", connection);
                    
          command.Parameters.Add(CreateParam("@YoungAgeGrp", contact.YoungAgeGrp, SqlDbType.NVarChar));
          command.Parameters.Add(CreateParam("@MidAgeGrp", contact.MidAgeGrp, SqlDbType.NVarChar));
          command.Parameters.Add(CreateParam("@OldAgeGrp", contact.OldAgeGrp, SqlDbType.NVarChar));
          command.Parameters.Add(CreateParam("@Code", contact.Zipcode, SqlDbType.NVarChar));
                    
          connection.Open();
          if (command.ExecuteNonQuery() == 1)
          {
            UpdateList(contact);
            OnChanged(DbOperation.UPDATE, DbModeltype.Contact);
            return;
          }
          error = string.Format("Contact could not be updated");
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
      else error = "Illegal value for contact";
      throw new DbException("Error in Contact repositiory: " + error);
    }

        private void UpdateList(KommuneData kommuneData)
        {
            for (int i = 0; i < list.Count; ++i)
                if (list[i].Equals(kommuneData))
                {
                    list[i].YoungAgeGrp = kommuneData.YoungAgeGrp;
                    list[i].MidAgeGrp = kommuneData.MidAgeGrp;
                    list[i].OldAgeGrp = kommuneData.OldAgeGrp;
                    list[i].Zipcode = kommuneData.Zipcode;
                    list[i].Year = kommuneData.Year;
                    list[i].City = ZipcodeRepository.GetCity(kommuneData.Zipcode);
                    break;
                }

        }

    public void Remove(string code)
    {
      string error = "";
      try
      {
        SqlCommand command = new SqlCommand("DELETE FROM KommuneData WHERE KommuneCode = @Code", connection);
        command.Parameters.Add(CreateParam("@Code", code, SqlDbType.NVarChar));
        connection.Open();
        if (command.ExecuteNonQuery() == 1)
        {
          list.Remove(new KommuneData(code, "", "", "", ""));
          OnChanged(DbOperation.DELETE, DbModeltype.Contact);
          return;
        }
        error = string.Format("Contact could not be deleted");
      }
      catch (Exception ex)
      {
        error = ex.Message;
      }
      finally
      {
        if (connection != null && connection.State == ConnectionState.Open) connection.Close();
      }
      throw new DbException("Error in Contact repositiory: " + error);
    }
  }
}
>>>>>>> master
