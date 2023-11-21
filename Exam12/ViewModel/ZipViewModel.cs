using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using ContactsEditor_MVVM.Model;
using ContactsEditor_MVVM.DataAccess;

namespace ContactsEditor_MVVM.ViewModel
{
  public class ZipViewModel : ViewModelBase, IDataErrorInfo
  {
    public RelayCommand SelectCommand { get; private set; }
    public RelayCommand RemoveCommand { get; private set; }
    public RelayCommand InsertCommand { get; private set; }
    public RelayCommand ClearCommand { get; private set; }
    public RelayCommand UpdateCommand { get; private set; }
    private Zipcode model = new Zipcode();
    private ZipcodeRepository repository = new ZipcodeRepository();
    private ObservableCollection<Zipcode> data;

    public ZipViewModel()
    {
      repository.RepositoryChanged += ModelChanged;
      Search();
      UpdateCommand = new RelayCommand(p => Update(), p => CanUpdate());
      SelectCommand = new RelayCommand(p => Search());
      ClearCommand = new RelayCommand(p => Clear());
      InsertCommand = new RelayCommand(p => Add(), p => CanAdd());
      RemoveCommand = new RelayCommand(p => Remove(), p => CanRemove());
    }

    public ObservableCollection<Zipcode> Data
    {
      get { return data; }
      set
      {
        if (data != value)
        {
          data = value;
          OnPropertyChanged("Data");
        }
      }
    }

    public void ModelChanged(object sender, DbEventArgs e)
    {
      if (e.Operation != DbOperation.SELECT)
      {
        Clear();
      }
      Data = new ObservableCollection<Zipcode>(repository);
    }

    public string Code
    {
      get { return model.Code; }
      set
      {
        if (!model.Code.Equals(value))
        {
          model.Code = value;
          OnPropertyChanged("Code");
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

    public Zipcode SelectedModel
    {
      get { return model; }
      set
      {
        model = value;
        OnPropertyChanged("Code");
        OnPropertyChanged("City");
        OnPropertyChanged("SelectedModel");
      }
    }

    private void Clear()
    {
      SelectedModel = new Zipcode();
    }

    public void Search()
    {
      repository.Search(Code, City);
    }

    public void Update()
    {
      try
      {
        repository.Update(Code, City);
      }
      catch (Exception ex)
      {
        OnWarning(ex.Message);
      }
    }

    private bool CanUpdate()
    {
      return model.IsValid;
    }

    public void Add()
    {
      try
      {
        repository.Add(Code, City);
      }
      catch (Exception ex)
      {
        OnWarning(ex.Message);
      }
    }

    public bool CanAdd()
    {
      return model.IsValid;
    }

    public void Remove()
    {
      try
      {
        repository.Remove(Code);
      }
      catch (Exception ex)
      {
        OnWarning(ex.Message);
      }
    }

    private bool CanRemove()
    {
      return Code != null && Code.Length > 0;
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
  }
}