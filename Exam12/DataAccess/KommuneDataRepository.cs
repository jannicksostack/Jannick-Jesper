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

    public void Search(string OldAgeGrp, string MidAgeGrp, string YoungAgeGrp, string zipcode, string city)
    {
      try
      {
        SqlCommand command = new SqlCommand("SELECT dbo.KommuneData.age_0_17, dbo.KommuneData.age_17_64, dbo.KommuneData.age_65, dbo.Kommune.Code, dbo.Kommune.Name " +
            "FROM dbo.KommuneData INNER JOIN dbo.Kommune ON dbo.KommuneData.KommuneCode = dbo.Kommune.Code " +
            "WHERE dbo.KommuneData.age_65 LIKE @OldAgeGrp AND dbo.KommuneData.age_17_64 LIKE @MidAgeGrp AND dbo.KommuneData.age_0_17 LIKE @YoungAgeGrp AND dbo.Kommune.Code LIKE @zipcode AND dbo.Kommune.Name LIKE @City", connection);
        command.Parameters.Add(CreateParam("@OldAgeGrp", OldAgeGrp + "%", SqlDbType.NVarChar));
        command.Parameters.Add(CreateParam("@MidAgeGrp", MidAgeGrp + "%", SqlDbType.NVarChar));
        command.Parameters.Add(CreateParam("@YoungAgeGrp", YoungAgeGrp + "%", SqlDbType.NVarChar));
        command.Parameters.Add(CreateParam("@zipcode", zipcode + "%", SqlDbType.NVarChar));
        command.Parameters.Add(CreateParam("@City", city + "%", SqlDbType.NVarChar));
        connection.Open();
        SqlDataReader reader = command.ExecuteReader();
        list.Clear();
        while (reader.Read()) list.Add(new KommuneData(reader[2].ToString(), reader[1].ToString(), reader[0].ToString(), reader[3].ToString(), reader[4].ToString()));
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

    //public void Add(string phone, string fname, string lname, string addr, string code, string email, string title)
    //{
    //  string error = "";
    //  Contact contact = new Contact(phone, fname, lname, addr, code, "", email, title);
    //  if (contact.IsValid)
    //  {
    //    try
    //    {
    //      SqlCommand command = new SqlCommand("INSERT INTO Addresses (phone, lname, fname, addr, code, email, title) VALUES (@Phone, @Lname, @Fname, @Addr, @Code, @Mail, @Title)", connection);
    //      command.Parameters.Add(CreateParam("@Phone", phone, SqlDbType.NVarChar));
    //      command.Parameters.Add(CreateParam("@Lname", lname, SqlDbType.NVarChar));
    //      command.Parameters.Add(CreateParam("@Fname", fname, SqlDbType.NVarChar));
    //      command.Parameters.Add(CreateParam("@Addr", addr, SqlDbType.NVarChar));
    //      command.Parameters.Add(CreateParam("@Code", code, SqlDbType.NVarChar));
    //      command.Parameters.Add(CreateParam("@Mail", email, SqlDbType.NVarChar));
    //      command.Parameters.Add(CreateParam("@Title", title, SqlDbType.NVarChar));
    //      connection.Open();
    //      if (command.ExecuteNonQuery() == 1)
    //      {
    //        contact.City = ZipcodeRepository.GetCity(code);
    //        list.Add(contact);
    //        list.Sort();
    //        OnChanged(DbOperation.INSERT, DbModeltype.Contact);
    //        return;
    //      }
    //      error = string.Format("Address could not be inserted in the database");
    //    }
    //    catch (Exception ex)
    //    {
    //      error = ex.Message;
    //    }
    //    finally
    //    {
    //      if (connection != null && connection.State == ConnectionState.Open) connection.Close();
    //    }
    //  }
    //  else error = "Illegal value for address";
    //  throw new DbException("Error in Contact repositiory: " + error);
    //}

    public void Add(KommuneData contact)
    {
      string error = "";
      if (contact.IsValid)
      {
        try
        {
                    contact.OldAgeGrp = contact.OldAgeGrp.Replace(',','.');
                    contact.MidAgeGrp = contact.MidAgeGrp.Replace(',', '.');
                    contact.YoungAgeGrp = contact.YoungAgeGrp.Replace(',', '.');
                    SqlCommand command = new SqlCommand("INSERT INTO KommuneData (age_0_17, age_17_64, age_65, KommuneCode) VALUES (@YoungAgeGrp, @MidAgeGrp, @OldAgeGrp, @Zipcode)", connection);
          command.Parameters.Add(CreateParam("@YoungAgeGrp", contact.YoungAgeGrp, SqlDbType.NVarChar));
          command.Parameters.Add(CreateParam("@MidAgeGrp", contact.MidAgeGrp, SqlDbType.NVarChar));
          command.Parameters.Add(CreateParam("@OldAgeGrp", contact.OldAgeGrp, SqlDbType.NVarChar));
          command.Parameters.Add(CreateParam("@Zipcode", contact.Zipcode, SqlDbType.NVarChar));
          connection.Open();
          if (command.ExecuteNonQuery() == 1)
          {
            contact.City = KommuneRepository.GetName(contact.Zipcode);
            list.Add(contact);
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
          SqlCommand command = new SqlCommand("UPDATE Addresses SET Lastname = @Lname, Firstname = @Fname, Address = @Addr, Zipcode = @Code, Email = @Mail, Title = @Title WHERE Phone = @Phone", connection);
                    /*
          command.Parameters.Add(CreateParam("@Phone", contact.Phone, SqlDbType.NVarChar));
          command.Parameters.Add(CreateParam("@Lname", contact.Lastname, SqlDbType.NVarChar));
          command.Parameters.Add(CreateParam("@Fname", contact.Firstname, SqlDbType.NVarChar));
          command.Parameters.Add(CreateParam("@Addr", contact.Address, SqlDbType.NVarChar));
          command.Parameters.Add(CreateParam("@Code", contact.Zipcode, SqlDbType.NVarChar));
          command.Parameters.Add(CreateParam("@Mail", contact.Email, SqlDbType.NVarChar));
          command.Parameters.Add(CreateParam("@Title", contact.Title, SqlDbType.NVarChar));
                    */
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

    private void UpdateList(KommuneData contact)
    {
      for (int i = 0; i < list.Count; ++i)
        if (list[i].Zipcode.Equals(contact.Zipcode))
        {
          list[i].YoungAgeGrp = contact.YoungAgeGrp;
          list[i].MidAgeGrp = contact.MidAgeGrp;
          list[i].OldAgeGrp = contact.OldAgeGrp;
          list[i].Zipcode = contact.Zipcode;
          list[i].City = ZipcodeRepository.GetCity(contact.Zipcode);
          break;
        }

    }

    public void Remove(string phone)
    {
      string error = "";
      try
      {
        SqlCommand command = new SqlCommand("DELETE FROM Addresses WHERE Phone = @Phone", connection);
        command.Parameters.Add(CreateParam("@Phone", phone, SqlDbType.NVarChar));
        connection.Open();
        if (command.ExecuteNonQuery() == 1)
        {
          list.Remove(new KommuneData(phone, "", "", "", ""));
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
