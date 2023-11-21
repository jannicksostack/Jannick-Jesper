using System;
using System.Windows.Input;
using System.ComponentModel;
using ContactsEditor_MVVM.Model;
using ContactsEditor_MVVM.DataAccess;

namespace ContactsEditor_MVVM.ViewModel
{
  public class ContactViewModel : ViewModelBase, IDataErrorInfo
  {
    public RelayCommand OkCommand { get; private set; }
    public RelayCommand ModCommand { get; private set; }
    public RelayCommand RemCommand { get; private set; }
    protected Contact model;
    protected ContactRepository repository;

    public ContactViewModel(Contact model, ContactRepository repository)
    {
      this.model = model;
      this.repository = repository;
      OkCommand = new RelayCommand(p => Add(), p => CanUpdate());
      ModCommand = new RelayCommand(p => Update(), p => CanUpdate());
      RemCommand = new RelayCommand(p => Remove());
    }

    public Contact Model
    {
      get { return model; }
    }

    public string Phone
    {
      get { return model.Phone; }
      set
      {
        if (!model.Phone.Equals(value))
        {
          model.Phone = value;
          OnPropertyChanged("Phone");
        }
      }
    }

    public string Firstname
    {
      get { return model.Firstname; }
      set
      {
        if (!model.Firstname.Equals(value))
        {
          model.Firstname = value;
          OnPropertyChanged("Firstname");
        }
      }
    }

    public string Lastname
    {
      get { return model.Lastname; }
      set
      {
        if (!model.Lastname.Equals(value))
        {
          model.Lastname = value;
          OnPropertyChanged("Lastname");
        }
      }
    }

    public string Address
    {
      get { return model.Address; }
      set
      {
        if (!model.Address.Equals(value))
        {
          model.Address = value;
          OnPropertyChanged("Address");
        }
      }
    }

    public string Zipcode
    {
      get { return model.Zipcode; }
      set
      {
        if (!model.Zipcode.Equals(value))
        {
          model.Zipcode = value;
          OnPropertyChanged("Zipcode");
        }
      }
    }

    public string City
    {
      get { return model.City; }
      set
      {
        if (!model.City.Equals(value))
        {
          model.City = value;
          OnPropertyChanged("City");
        }
      }
    }

    public string Email
    {
      get { return model.Email; }
      set
      {
        if (!model.Email.Equals(value))
        {
          model.Email = value;
          OnPropertyChanged("Email");
        }
      }
    }

    public string Title
    {
      get { return model.Title; }
      set
      {
        if (!model.Title.Equals(value))
        {
          model.Title = value;
          OnPropertyChanged("Title");
        }
      }
    }

    public void Clear()
    {
      model = new Contact();
      OnPropertyChanged("Phone");
      OnPropertyChanged("Lastname");
      OnPropertyChanged("Firstname");
      OnPropertyChanged("Address");
      OnPropertyChanged("Zipcode");
      OnPropertyChanged("City");
      OnPropertyChanged("Email");
      OnPropertyChanged("Title");
    }

    public void Update()
    {
      if (IsValid)
      {
        try
        {
          repository.Update(model);
          OnClose();
        }
        catch (Exception ex)
        {
          OnWarning(ex.Message);
        }
      }
    }

    public void Remove()
    {
      try
      {
        repository.Remove(model.Phone);
        OnClose();
      }
      catch (Exception ex)
      {
        OnWarning(ex.Message);
      }
    }

    public void Add()
    {
      if (IsValid)
      {
        try
        {
          repository.Add(model);
          Clear();
        }
        catch (Exception ex)
        {
          OnWarning(ex.Message);
        }
      }
    }

    string IDataErrorInfo.Error
    {
      get { return (model as IDataErrorInfo).Error; }
    }

    string IDataErrorInfo.this[string propertyName]
    {
      get
      {
        string error = null;
        try
        {
          error = (model as IDataErrorInfo)[propertyName];
        }
        catch
        {
        }
        CommandManager.InvalidateRequerySuggested();
        return error;
      }
    }

    private bool IsValid
    {
      get { return model.IsValid; }
    }

    private bool CanUpdate()
    {
      return IsValid;
    }
  }
}
