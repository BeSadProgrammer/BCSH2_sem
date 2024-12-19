namespace StromApp.Models
{
    public class Spravce
    {
        public int ID { get; set; }
        public string Jmeno { get; set; }
        public string Prijmeni { get; set; }
        public string Email { get; set; }
        public string Telefon { get; set; }
        public int RegionID { get; set; }

        // Tato vlastnost vrátí název regionuSpravce
        public string NazevRegionu { get; set; }
        public string FullName { get { return Jmeno + " " + Prijmeni; } }
    }
}
