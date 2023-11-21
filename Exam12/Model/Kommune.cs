using System;
using System.ComponentModel;
using System.Linq;

namespace ContactsEditor_MVVM.Model
{
    public class Kommune : IDataErrorInfo, IComparable<Kommune>
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public Kommune()
        {
            Code = "";
            Name = "";
        }

        public Kommune(string code, string name)
        {
            Code = code;
            Name = name;
        }

        public override bool Equals(object obj)
        {
            return obj is Kommune k && Code.Equals(k.Code);
        }

        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", Code, Name);
        }

        // Implementerer ordning af objekter, så der alene sammenlignes på postnummer.
        public int CompareTo(Kommune kommune)
        {
            return Code.CompareTo(kommune.Code);
        }

        // Validering af objektet ud fra kriteriet at postnummeret skal bestå af 4 cifre, mens bynavnet ikke må være blankt.
        public bool IsValid
        {
            get { return Code != null && CodeOk(Code.Trim()) && Name != null && Name.Trim().Length > 0; }
        }

        string IDataErrorInfo.Error
        {
            get { return IsValid ? null : "Illegal model object"; }
        }

        string IDataErrorInfo.this[string propertyName]
        {
            get { return Validate(propertyName); }
        }

        private string Validate(string property)
        {
            if (property.Equals("Code")) return Code != null && CodeOk(Code.Trim()) ? null : "Illegal code";
            if (property.Equals("Name")) return Name != null && Name.Trim().Length > 0 ? null : "Illegal name";
            return null;
        }

        // Validering af postnummer som en streng bestående af 4 cifre.
        // Valideringen tillader, at 0 må forekomme på alle pladser og specielt på den første plads.
        private bool CodeOk(string code)
        {
            return code.Length == 3 && code.All(char.IsDigit);
        }
    }
}
