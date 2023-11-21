using System;
using System.ComponentModel;
using System.Diagnostics;

namespace ContactsEditor_MVVM.Model
{
    // Modelklasse for et postnummer.
    // Zipcode implementerer datavalidering, så postnummeret skal være en streng på 4 cifre, og bynavnet må ikke være tomt.
    // Objekterne ordnes alene efter postnummer svarende til, at postnummeret opfattes som unikt.
    public class Zipcode : IDataErrorInfo, IComparable<Zipcode>
    {
        public string Code { get; set; }    // repræsenterer postnummeret
        public string City { get; set; }    // repræsenterer bynavnet

        // Opretter et tomt objekt, hvor både postnummer og bynavn er blanke.
        public Zipcode()
        {
            Code = "";
            City = "";
        }

        // Opretter et objekt initialiseret med værdier for både postnummer og bynavn.
        public Zipcode(string code, string city)
        {
            Code = code;
            City = city;
        }

        // Implementerer sammenligning af objekter, så der alene sammenlignes på postnummer.
        public override bool Equals(object obj)
        {
            return obj is Zipcode zip && Code.Equals(zip.Code);
        }

        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", Code, City);
        }

        // Implementerer ordning af objekter, så der alene sammenlignes på postnummer.
        public int CompareTo(Zipcode zipcode)
        {
            return Code.CompareTo(zipcode.Code);
        }

        // Validering af objektet ud fra kriteriet at postnummeret skal bestå af 4 cifre, mens bynavnet ikke må være blankt.
        public bool IsValid
        {
            get { return Code != null && CodeOk(Code.Trim()) && City != null && City.Trim().Length > 0; }
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
            if (property.Equals("City")) return City != null && City.Trim().Length > 0 ? null : "Illegal city";
            return null;
        }

        // Validering af postnummer som en streng bestående af 4 cifre.
        // Valideringen tillader, at 0 må forekomme på alle pladser og specielt på den første plads.
        private bool CodeOk(string code)
        {
            if (code.Length != 4) return false;
            foreach (char c in code) if (c < '0' || c > '9') return false;
            return true;
        }
    }
}
