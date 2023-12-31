﻿using System;
using System.Windows.Input;
using System.ComponentModel;
using ContactsEditor_MVVM.Model;
using ContactsEditor_MVVM.DataAccess;

namespace ContactsEditor_MVVM.ViewModel
{
    public class KommuneDataViewModel : ViewModelBase, IDataErrorInfo
    {
        public RelayCommand OkCommand { get; private set; }
        public RelayCommand ModCommand { get; private set; }
        public RelayCommand RemCommand { get; private set; }
        protected KommuneData model;
        protected KommuneDataRepository repository;

        public KommuneDataViewModel(KommuneData model, KommuneDataRepository repository)
        {
            this.model = model;
            this.repository = repository;
            OkCommand = new RelayCommand(p => Add(), p => CanUpdate());
            ModCommand = new RelayCommand(p => Update(), p => CanUpdate());
            RemCommand = new RelayCommand(p => Remove());
        }

        public KommuneData Model
        {
            get { return model; }
        }


        public string Year
        {
            get { return model.Year; }
            set
            {
                if (!model.Year.Equals(value))
                {
                    model.Year = value;
                    OnPropertyChanged("Year");
                }
            }
        }

        public string MidAgeGrp
        {
            get { return model.MidAgeGrp; }
            set
            {
                if (!model.MidAgeGrp.Equals(value))
                {
                    model.MidAgeGrp = value;
                    OnPropertyChanged("MidAgeGrp");
                }
            }
        }

        public string YoungAgeGrp
        {
            get { return model.YoungAgeGrp; }
            set
            {
                if (!model.YoungAgeGrp.Equals(value))
                {
                    model.YoungAgeGrp = value;
                    OnPropertyChanged("YoungAgeGrp");
                }
            }
        }

        public string OldAgeGrp
        {
            get { return model.OldAgeGrp; }
            set
            {
                if (!model.OldAgeGrp.Equals(value))
                {
                    model.OldAgeGrp = value;
                    OnPropertyChanged("OldAgeGrp");
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



        public void Clear()
        {
            model = new KommuneData();
            OnPropertyChanged("YoungAgeGrp");
            OnPropertyChanged("MidAgeGrp");
            OnPropertyChanged("OldAgeGrp");
            OnPropertyChanged("Zipcode");
            OnPropertyChanged("City");

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
                repository.Remove(model.Zipcode);
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
                    CloseCommand.Execute(null);
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
