using System;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Linq;
using System.Diagnostics;

namespace ContactsEditor_MVVM.Model
{
    // Modelklasse for en adresse.
    // Contact implementerer datavalidering, så en adresse skal opfylde:
    //   Phone      skal være en streng bestående af 8 cifre
    //   Firstname  må ikke være null
    //   Lastname   må ikke være blank
    //   Address    må ikke være null
    //   Zipcode    skal være en streng bestående af 4 cifre
    //   Email      skal være blank eller også en streng, som kan være en lovlig email adresse
    //   Title      må ikke være null
    // Telefonnummer opfattes som nøgle, og sammenligning og ordning sker alene ud fra værdien af Phone.
    public class KommuneData : IDataErrorInfo, IComparable<KommuneData>
    {
        public string OldAgeGrp { get; set; }         // telefonnummer
        public string MidAgeGrp { get; set; }     // fornavn
        public string YoungAgeGrp { get; set; }      // efternavn
        public string Zipcode { get; set; }       // postnummer
        public string City { get; set; }          // bynavn

        public string Year { get; set; }


        // Opretter et objekt ved at sætte alle felter til blank.
        public KommuneData()
        {
            OldAgeGrp = "";
            MidAgeGrp = "";
            YoungAgeGrp = "";
            Zipcode = "";
            City = "";
            Year = "";
        }

        // Opretter et objekt, hvor alle felter initialiseres med parametre.
        // Konstruktøren garanterer ikke, at objektet er lovligt.
        public KommuneData(string OldAgeGrp, string MidAgeGrp, string YoungAgeGrp, string zipcode, string city, string year)
        {
            this.OldAgeGrp = OldAgeGrp;
            this.MidAgeGrp = MidAgeGrp;
            this.YoungAgeGrp = YoungAgeGrp;
            Zipcode = zipcode;
            City = city;
            Year = year;
        }

        // Implementerer sammenligning alene på telefonnummer.
        public override bool Equals(object obj)
        {
            KommuneData kommuneData = obj as KommuneData;

            if (kommuneData is null)
            {
                return false;
            } else
            {
                return Zipcode.Equals(kommuneData.Zipcode) && Year.Equals(kommuneData.Year);
            }
        }

        public override int GetHashCode()
        {
            // TODO
            return OldAgeGrp.GetHashCode();
        }

        // Implementerer ordning af adresse alene ud fra telefonnummeret
        public int CompareTo(KommuneData contact)
        {
            // TODO
            return Zipcode.CompareTo(contact.Zipcode);
        }

        // Validering af objektet.
        // Arrayet angiver hvilke properties, der skal valideres.
        private static readonly string[] validatedProperties = { "OldAgeGrp", "MidAgeGrp", "YoungAgeGrp", "Zipcode" };

        public bool IsValid
        {
            get
            {
                foreach (string property in validatedProperties) if (GetError(property) != null) return false;
                return true;
            }
        }

        string IDataErrorInfo.Error
        {
            get { return IsValid ? null : "Illegal Contact object"; }
        }

        string IDataErrorInfo.this[string propertyName]
        {
            get { return Validate(propertyName); }
        }

        private string GetError(string name)
        {
            foreach (string property in validatedProperties) if (property.Equals(name)) return Validate(name);
            return null;
        }

        private string Validate(string name)
        {
            switch (name)
            {
                case "OldAgeGrp": return ValidateAgeGrp(OldAgeGrp);
                case "MidAgeGrp": return ValidateAgeGrp(MidAgeGrp);
                case "YoungAgeGrp": return ValidateAgeGrp(YoungAgeGrp);
                case "Zipcode": return ValidateZipcode();
            }
            return null;
        }


        // Valideringsmetoder til de enkelte properties.
        private string ValidateAgeGrp(string age)
        {
            /*
            if (OldAgeGrp.Length != 8) return "Phone must be a number of 8 digits";
            foreach (char c in OldAgeGrp) if (c < '0' || c > '9') return "Phone must be a number of 8 digits";
            return null;
            */
            if (float.TryParse(age.Replace('.', ','), out float temp))
            {
                return null;
            }
            else
            {
                return "Needs to be float";
            }

        }


        private string ValidateZipcode()
        {
            if (Zipcode.Length != 3) return "Zipcode must be a number of 3 digits";
            foreach (char c in Zipcode) if (c < '0' || c > '9') return "Zipcode must be a number of 3 digits";
            return null;
        }
    }
}
