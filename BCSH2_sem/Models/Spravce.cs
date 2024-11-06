using System;

namespace BCSH2.Models
{
    [Serializable]
    public class Spravce
    {
        private static int _nextID = 1;

        public int SpravceID { get; }
        public string Jmeno { get; set; }
        public string Prijmeni { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public Region Region { get; set; }

        public Spravce(string jmeno, string prijmeni, string email, string telefon, Region region)
        {
            Jmeno = jmeno;
            Prijmeni = prijmeni;
            Email = email;
            Telefon = telefon;
            Region = region;
            SpravceID = _nextID++;
        }

        public static void SetNextID(int nextID)
        {
            _nextID = nextID;
        }
    }
}
