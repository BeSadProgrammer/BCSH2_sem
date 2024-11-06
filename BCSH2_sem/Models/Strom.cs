using System;

namespace BCSH2.Models
{
    [Serializable]
    public class Strom
    {
        private static int _nextID = 1;

        public DruhyStromuTyp StromTyp { get; set; }
        public Spravce Spravce { get; set; }
        public DateTime DatumZasazeni { get; set; }
        public DateTime DatumPridaniZaznamu { get; set; }
        public string Lokace { get; set; }
        public double Vyska { get; set; }
        public double PrumerKmene { get; set; }
        public string TypKury { get; set; }
        public int ID { get; set; }

        public Strom(DruhyStromuTyp stromTyp, Spravce spravce, DateTime datumZasazeni, DateTime datumPridaniZaznamu,
                     string lokace, double vyska, double prumerKmene, string typKury)
        {
            StromTyp = stromTyp;
            Spravce = spravce;
            DatumZasazeni = datumZasazeni;
            DatumPridaniZaznamu = datumPridaniZaznamu;
            Lokace = lokace;
            Vyska = vyska;
            PrumerKmene = prumerKmene;
            TypKury = typKury;
            ID = _nextID++;
        }

        public static void SetNextID(int nextID)
        {
            _nextID = nextID;
        }
    }
}
