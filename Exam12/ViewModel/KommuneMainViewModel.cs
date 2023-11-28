using System;
using System.Collections.ObjectModel;
using ContactsEditor_MVVM.Model;
using ContactsEditor_MVVM.DataAccess;
using ContactsEditor_MVVM.View;
using Exam12.View;
using System.Data.OleDb;
using System.Linq;
using System.Windows.Controls.Primitives;

namespace ContactsEditor_MVVM.ViewModel
{
    public class KommuneMainViewModel : ViewModelBase
    {
        public RelayCommand SearchCommand { get; private set; }
        public RelayCommand CreateCommand { get; private set; }
        public RelayCommand ZipCommand { get; private set; }
        public RelayCommand KommuneCommand { get; private set; }
        public RelayCommand UpdateCommand { get; private set; }
        public RelayCommand ClearCommand { get; private set; }
        public static KommuneDataRepository repository = new KommuneDataRepository();
        private ObservableCollection<KommuneData> contacts;

        private string oldAgeGrp = "";
        private string midAgeGrp = "";
        private string youngAgeGrp = "";
        private string code = "";
        private string city = "";
        private string year = "";

        public KommuneMainViewModel()
        {
            SearchCommand = new RelayCommand(p => Search(), p => CanSearch());
            CreateCommand = new RelayCommand(p => (new CreateKommuneWindow()).ShowDialog());
            ZipCommand = new RelayCommand(p => (new ZipWindow()).ShowDialog());
            UpdateCommand = new RelayCommand(p => new UpdateKommuneWindow(Selected).ShowDialog());
            KommuneCommand = new RelayCommand(p => new KommuneWindow().ShowDialog());
            ClearCommand = new RelayCommand(p => Clear());
            contacts = new ObservableCollection<KommuneData>(repository);
            repository.RepositoryChanged += Refresh;
            Search();
        }

        private void Refresh(object sender, DbEventArgs e)
        {
            Contacts = new ObservableCollection<KommuneData>(repository);
        }

        public ObservableCollection<KommuneData> Contacts
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

        private KommuneData selected = new KommuneData();
        public KommuneData Selected
        {
            get { return selected; }
            set
            {
                if (!selected.Equals(value))
                {
                    selected = value;
                    OnPropertyChanged("Selected");
                }
            }
        }
        public string OldAgeGrp
        {
            get { return oldAgeGrp; }
            set
            {
                if (!oldAgeGrp.Equals(value))
                {
                    oldAgeGrp = value;
                    OnPropertyChanged("OldAgeGrp");
                }
            }
        }

        public string MidAgeGrp
        {
            get { return midAgeGrp; }
            set
            {
                if (!midAgeGrp.Equals(value))
                {
                    midAgeGrp = value;
                    OnPropertyChanged("MidAgeGrp");
                }
            }
        }

        public string YoungAgeGrp
        {
            get { return youngAgeGrp; }
            set
            {
                if (!youngAgeGrp.Equals(value))
                {
                    youngAgeGrp = value;
                    OnPropertyChanged("YoungAgeGrp");
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

        public string Year
        {
            get { return year; }
            set
            {
                if (!year.Equals(value))
                {
                    year = value;
                    OnPropertyChanged("Year");
                }
            }
        }
        private void Clear()
        {
            OldAgeGrp = "";
            MidAgeGrp = "";
            YoungAgeGrp = "";
            Code = "";
            City = "";
            Year = "";
        }

        private void Search()
        {
            try
            {
                repository.Search(code, city, year);
                Contacts = new ObservableCollection<KommuneData>(repository);
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
            return true;
            /*
            return oldAgeGrp.Length > 0 || midAgeGrp.Length > 0 ||
                      youngAgeGrp.Length > 0 ||
                      code.Length > 0 || city.Length > 0 || year.Length > 0;
            */
        }
    }
}
