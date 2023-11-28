using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using ContactsEditor_MVVM.Model;
using ContactsEditor_MVVM.DataAccess;
using System.Diagnostics;

namespace ContactsEditor_MVVM.ViewModel
{
    public class KommuneViewModel : ViewModelBase, IDataErrorInfo
    {
        public RelayCommand SelectCommand { get; private set; }
        public RelayCommand RemoveCommand { get; private set; }
        public RelayCommand InsertCommand { get; private set; }
        public RelayCommand ClearCommand { get; private set; }
        public RelayCommand UpdateCommand { get; private set; }
        private Kommune model = new Kommune();
        private KommuneRepository repository = new KommuneRepository();
        private ObservableCollection<Kommune> data;

        public KommuneViewModel()
        {
            repository.RepositoryChanged += ModelChanged;
            Search();
            UpdateCommand = new RelayCommand(p => Update(), p => CanUpdate());
            SelectCommand = new RelayCommand(p => Search());
            ClearCommand = new RelayCommand(p => Clear());
            InsertCommand = new RelayCommand(p => Add(), p => CanAdd());
            RemoveCommand = new RelayCommand(p => Remove(), p => CanRemove());
        }

        public ObservableCollection<Kommune> Data
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
            Data = new ObservableCollection<Kommune>(repository);
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

        public string Name
        {
            get { return model.Name; }
            set
            {
                if (!model.Name.Equals(value))
                {
                    model.Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public Kommune SelectedModelTargetNullValue => new Kommune();

        public Kommune SelectedModel
        {
            get { return model; }
            set
            {
                if(value == null)
                {
                    return;
                }
                else
                {
                    model = value;
                    OnPropertyChanged("Code");
                    OnPropertyChanged("Name");
                    OnPropertyChanged("SelectedModel");
                }
                
            }
        }

        private void Clear()
        {
            SelectedModel = new Kommune();
        }

        public void Search()
        {
            repository.Search(Code, Name);
        }

        public void Update()
        {
            try
            {
                repository.Update(Code, Name);
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
                repository.Add(Code, Name);
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