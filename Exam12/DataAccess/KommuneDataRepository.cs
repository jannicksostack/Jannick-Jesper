using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using ContactsEditor_MVVM.Model;

namespace ContactsEditor_MVVM.DataAccess
{
  public class KommuneDataRepository : Repository, IEnumerable<Contact>
  {
    private List<Contact> list = new List<Contact>();

    public IEnumerator<Contact> GetEnumerator()
    {
      return list.GetEnumerator();
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return GetEnumerator();
    }

    public void Search(string phone, string fname, string lname, string addr, string code, string city, string title)
    {
      try
      {
        SqlCommand command = new SqlCommand("SELECT Phone, Lastname, Firstname, Address, Zipcode, City, Email, Title FROM Zipcodes, Addresses WHERE Zipcode = Code AND Phone LIKE @Phone AND Lastname LIKE @Lname AND Firstname LIKE @Fname AND Address LIKE @Addr AND Zipcode LIKE @Code AND Title LIKE @Title AND City LIKE @City", connection);
        command.Parameters.Add(CreateParam("@Phone", phone + "%", SqlDbType.NVarChar));
        command.Parameters.Add(CreateParam("@Lname", lname + "%", SqlDbType.NVarChar));
        command.Parameters.Add(CreateParam("@Fname", fname + "%", SqlDbType.NVarChar));
        command.Parameters.Add(CreateParam("@Addr", addr + "%", SqlDbType.NVarChar));
        command.Parameters.Add(CreateParam("@Code", code + "%", SqlDbType.NVarChar));
        command.Parameters.Add(CreateParam("@Title", title + "%", SqlDbType.NVarChar));
        command.Parameters.Add(CreateParam("@City", city + "%", SqlDbType.NVarChar));
        connection.Open();
        SqlDataReader reader = command.ExecuteReader();
        list.Clear();
        while (reader.Read()) list.Add(new Contact(reader[0].ToString(), reader[2].ToString(), reader[1].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString()));
        OnChanged(DbOperation.SELECT, DbModeltype.Contact);
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

    public void Add(Contact contact)
    {
      string error = "";
      if (contact.IsValid)
      {
        try
        {
          SqlCommand command = new SqlCommand("INSERT INTO Addresses (Phone, Lastname, Firstname, Address, Zipcode, Email, Title) VALUES (@Phone, @Lname, @Fname, @Addr, @Code, @Mail, @Title)", connection);
          command.Parameters.Add(CreateParam("@Phone", contact.Phone, SqlDbType.NVarChar));
          command.Parameters.Add(CreateParam("@Lname", contact.Lastname, SqlDbType.NVarChar));
          command.Parameters.Add(CreateParam("@Fname", contact.Firstname, SqlDbType.NVarChar));
          command.Parameters.Add(CreateParam("@Addr", contact.Address, SqlDbType.NVarChar));
          command.Parameters.Add(CreateParam("@Code", contact.Zipcode, SqlDbType.NVarChar));
          command.Parameters.Add(CreateParam("@Mail", contact.Email, SqlDbType.NVarChar));
          command.Parameters.Add(CreateParam("@Title", contact.Title, SqlDbType.NVarChar));
          connection.Open();
          if (command.ExecuteNonQuery() == 1)
          {
            contact.City = ZipcodeRepository.GetCity(contact.Zipcode);
            list.Add(contact);
            list.Sort();
            OnChanged(DbOperation.INSERT, DbModeltype.Contact);
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

    public void Update(string phone, string fname, string lname, string addr, string code, string email, string title)
    {
      Update(new Contact(phone, fname, lname, addr, code, "", email, title));
    }

    public void Update(Contact contact)
    {
      string error = "";
      if (contact.IsValid)
      {
        try
        {
          SqlCommand command = new SqlCommand("UPDATE Addresses SET Lastname = @Lname, Firstname = @Fname, Address = @Addr, Zipcode = @Code, Email = @Mail, Title = @Title WHERE Phone = @Phone", connection);
          command.Parameters.Add(CreateParam("@Phone", contact.Phone, SqlDbType.NVarChar));
          command.Parameters.Add(CreateParam("@Lname", contact.Lastname, SqlDbType.NVarChar));
          command.Parameters.Add(CreateParam("@Fname", contact.Firstname, SqlDbType.NVarChar));
          command.Parameters.Add(CreateParam("@Addr", contact.Address, SqlDbType.NVarChar));
          command.Parameters.Add(CreateParam("@Code", contact.Zipcode, SqlDbType.NVarChar));
          command.Parameters.Add(CreateParam("@Mail", contact.Email, SqlDbType.NVarChar));
          command.Parameters.Add(CreateParam("@Title", contact.Title, SqlDbType.NVarChar));
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

    private void UpdateList(Contact contact)
    {
      for (int i = 0; i < list.Count; ++i)
        if (list[i].Phone.Equals(contact.Phone))
        {
          list[i].Firstname = contact.Firstname;
          list[i].Lastname = contact.Lastname;
          list[i].Address = contact.Address;
          list[i].Zipcode = contact.Zipcode;
          list[i].City = ZipcodeRepository.GetCity(contact.Zipcode);
          list[i].Email = contact.Email;
          list[i].Title = contact.Title;
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
          list.Remove(new Contact(phone, "", "", "", "", "", "", ""));
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
