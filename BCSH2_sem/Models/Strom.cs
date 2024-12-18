using System;

namespace StromApp.Models
{
    public class Strom
    {
        public int ID { get; set; }
        public DruhyStromuTyp DruhStromu { get; set; }
        public int SpravceID { get; set; }
        public DateTime DatumZasazeni { get; set; }
        public DateTime DatumPridaniZaznamu { get; set; }
        public string Lokace { get; set; }
        public double Vyska { get; set; }
        public double PrumerKmeni { get; set; }
        public string TypKury { get; set; }

        // Tato vlastnost vrátí jméno a příjmení spravce
        public string Spravce { get; set; }
    }
}
