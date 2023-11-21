using System;
using System.Collections.ObjectModel;
using ContactsEditor_MVVM.Model;
using ContactsEditor_MVVM.DataAccess;
using ContactsEditor_MVVM.View;

namespace ContactsEditor_MVVM.ViewModel
{
  public class MainViewModel : ViewModelBase
  {
    public RelayCommand SearchCommand { get; private set; }
    public RelayCommand CreateCommand { get; private set; }
    public RelayCommand ZipCommand { get; private set; }
    public RelayCommand KommuneCommand { get; private set; }
    public RelayCommand ClearCommand { get; private set; }
    public static ContactRepository repository = new ContactRepository();
    private ObservableCollection<Contact> contacts;
    private string phone = "";
    private string fname = "";
    private string lname = "";
    private string addr = "";
    private string code = "";
    private string city = "";
    private string title = "";

    public MainViewModel()
    {
      SearchCommand = new RelayCommand(p => Search(), p => CanSearch());
      CreateCommand = new RelayCommand(p => (new CreateWindow()).ShowDialog());
      ZipCommand = new RelayCommand(p => (new ZipWindow()).ShowDialog());
      KommuneCommand = new RelayCommand(p => new KommuneWindow().ShowDialog());
      ClearCommand = new RelayCommand(p => Clear());
      contacts = new ObservableCollection<Contact>(repository);
      repository.RepositoryChanged += Refresh;
    }

    private void Refresh(object sender, DbEventArgs e)
    {
      Contacts = new ObservableCollection<Contact>(repository);
    }

    public ObservableCollection<Contact> Contacts
    {
      get { return contacts; }
      set
      {
        if (!contacts.Equals(value))
        {
          contacts = value;
          OnPropertyChanged("Contacts");
        }
      }
    }

    public string Phone
    {
      get { return phone; }
      set
      {
        if (!phone.Equals(value))
        {
          phone = value;
          OnPropertyChanged("Phone");
        }
      }
    }

    public string Fname
    {
      get { return fname; }
      set
      {
        if (!fname.Equals(value))
        {
          fname = value;
          OnPropertyChanged("Fname");
        }
      }
    }

    public string Lname
    {
      get { return lname; }
      set
      {
        if (!lname.Equals(value))
        {
          lname = value;
          OnPropertyChanged("Lname");
        }
      }
    }

    public string Addr
    {
      get { return addr; }
      set
      {
        if (!addr.Equals(value))
        {
          addr = value;
          OnPropertyChanged("Addr");
        }
      }
    }

    public string Code
    {
      get { return code; }
      set
      {
        if (!code.Equals(value))
        {
          code = value;
          OnPropertyChanged("Code");
        }
      }
    }

    public string City
    {
      get { return city; }
      set
      {
        if (!city.Equals(value))
        {
          city = value;
          OnPropertyChanged("City");
        }
      }
    }

    public string Title
    {
      get { return title; }
      set
      {
        if (!title.Equals(value))
        {
          title = value;
          OnPropertyChanged("Title");
        }
      }
    }

    private void Clear()
    {
      Phone = "";
      Fname = "";
      Lname = "";
      Addr = "";
      Code = "";
      City = "";
      Title = "";
    }

    private void Search()
    {
      try
      {
        repository.Search(phone, fname, lname, addr, code, city, title);
        Contacts = new ObservableCollection<Contact>(repository);
      }
      catch (Exception ex)
      {
        OnWarning(ex.Message);
      }
    }

    public void UpdateContact(Contact contact)
    {
      UpdateWindow dlg = new UpdateWindow(contact);
      dlg.ShowDialog();
    }

    private bool CanSearch()
    {
      return phone.Length > 0 || fname.Length > 0 || 
                lname.Length > 0 || addr.Length > 0 || 
                code.Length > 0 || city.Length > 0 || 
                title.Length > 0;
    }    
  }
}