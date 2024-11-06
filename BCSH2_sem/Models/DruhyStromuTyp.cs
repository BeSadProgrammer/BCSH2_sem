using System.Collections;
using System;

namespace BCSH2.Models
{
    public enum DruhyStromuTyp
    {
        None,
        Borovice,
        Buk,
        Dub,
        Modrin,
        Smrk,
        Jedle,
        Jasan,
        Topol,
        Briza,
        Javor
    }

    public static class StromTypInfo
    {
        public static readonly int Count = Enum.GetValues(typeof(DruhyStromuTyp)).Length;

        public static IEnumerable Items
        {
            get
            {
                yield return DruhyStromuTyp.None;
                yield return DruhyStromuTyp.Borovice;
                yield return DruhyStromuTyp.Buk;
                yield return DruhyStromuTyp.Dub;
                yield return DruhyStromuTyp.Modrin;
                yield return DruhyStromuTyp.Smrk;
                yield return DruhyStromuTyp.Jedle;
                yield return DruhyStromuTyp.Jasan;
                yield return DruhyStromuTyp.Topol;
                yield return DruhyStromuTyp.Briza;
                yield return DruhyStromuTyp.Javor;
            }
        }

        public static string GetName(DruhyStromuTyp stromTyp)
        {
            return stromTyp switch
            {
                DruhyStromuTyp.None => "",
                DruhyStromuTyp.Borovice => "Borovice",
                DruhyStromuTyp.Buk => "Buk",
                DruhyStromuTyp.Dub => "Dub",
                DruhyStromuTyp.Modrin => "Modřín",
                DruhyStromuTyp.Smrk => "Smrk",
                DruhyStromuTyp.Jedle => "Jedle",
                DruhyStromuTyp.Jasan => "Jasan",
                DruhyStromuTyp.Topol => "Topol",
                DruhyStromuTyp.Briza => "Bříza",
                DruhyStromuTyp.Javor => "Javor",
                _ => ""
            };
        }
    }
}
