using System;

namespace BCSH2.Models
{
    [Serializable]
    public class Region
    {
        private static int _nextID = 1;

        public int RegionID { get; set; }
        public string NazevRegionu { get; set; }

        public Region(string nazevRegionu)
        {
            RegionID = _nextID++;
            NazevRegionu = nazevRegionu;
        }

        public static void SetNextID(int nextID)
        {
            _nextID = nextID;
        }
    }
}
