using Microsoft.Data.Sqlite;
using StromApp.Models;
using System;
using System.Collections.Generic;

namespace StromApp.Handlers
{
    public class SQLiteHandler
    {
        private readonly string _connectionString = "Data Source=data.db";

        public void InitializeDatabase()
        {
            // Vytvoření tabulek
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var createRegionTable = @"
                CREATE TABLE IF NOT EXISTS Region (
                    ID INTEGER PRIMARY KEY AUTOINCREMENT,
                    NazevRegionu TEXT NOT NULL
                );";

                var createSpravceTable = @"
                CREATE TABLE IF NOT EXISTS Spravce (
                    ID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Jmeno TEXT NOT NULL,
                    Prijmeni TEXT NOT NULL,
                    Email TEXT,
                    Telefon TEXT,
                    RegionID INTEGER,
                    FOREIGN KEY (RegionID) REFERENCES Region(ID)
                );";

                var createStromTable = @"
                CREATE TABLE IF NOT EXISTS Strom (
                    ID INTEGER PRIMARY KEY AUTOINCREMENT,
                    DruhStromu INTEGER,
                    SpravceID INTEGER,
                    DatumZasazeni TEXT,
                    DatumPridaniZaznamu TEXT,
                    Lokace TEXT,
                    Vyska REAL,
                    PrumerKmeni REAL,
                    TypKury TEXT,
                    FOREIGN KEY (SpravceID) REFERENCES Spravce(ID)
                );";

                using (var command = new SqliteCommand(createRegionTable, connection))
                {
                    command.ExecuteNonQuery();
                }

                using (var command = new SqliteCommand(createSpravceTable, connection))
                {
                    command.ExecuteNonQuery();
                }

                using (var command = new SqliteCommand(createStromTable, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public void InsertInitialData()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                // Kontrola, zda existují regiony
                var checkRegionQuery = "SELECT COUNT(*) FROM Region;";
                using (var command = new SqliteCommand(checkRegionQuery, connection))
                {
                    var regionCount = Convert.ToInt32(command.ExecuteScalar());
                    if (regionCount == 0)
                    {
                        // Pokud nejsou žádné regiony, vložíme počáteční data
                        var insertRegionQuery = @"
                    INSERT INTO Region (NazevRegionu) VALUES
                    ('Default'),
                    ('Sever'),
                    ('Jih'),
                    ('Vychod'),
                    ('Západ');";
                        using (var insertCommand = new SqliteCommand(insertRegionQuery, connection))
                        {
                            insertCommand.ExecuteNonQuery();
                        }
                    }
                }

                // Kontrola, zda existují správci
                var checkSpravceQuery = "SELECT COUNT(*) FROM Spravce;";
                using (var command = new SqliteCommand(checkSpravceQuery, connection))
                {
                    var spravceCount = Convert.ToInt32(command.ExecuteScalar());
                    if (spravceCount == 0)
                    {
                        // Pokud nejsou žádní správci, vložíme počáteční data
                        var insertSpravceQuery = @"
                    INSERT INTO Spravce (Jmeno, Prijmeni, Email, Telefon, RegionID) VALUES
                    ('Default', 'Spravce', '', '', 1),
                    ('Petr', 'Svoboda', 'petr.svoboda@example.com', '987654321', 2);";
                        using (var insertCommand = new SqliteCommand(insertSpravceQuery, connection))
                        {
                            insertCommand.ExecuteNonQuery();
                        }
                    }
                }

                // Kontrola, zda existují stromy
                var checkStromQuery = "SELECT COUNT(*) FROM Strom;";
                using (var command = new SqliteCommand(checkStromQuery, connection))
                {
                    var stromCount = Convert.ToInt32(command.ExecuteScalar());
                    if (stromCount == 0)
                    {
                        // Pokud nejsou žádné stromy, vložíme počáteční data
                        var insertStromQuery = @"
                    INSERT INTO Strom (DruhStromu, SpravceID, DatumZasazeni, DatumPridaniZaznamu, Lokace, Vyska, PrumerKmeni, TypKury) VALUES
                    (0, 1, '2023-04-10', '2023-04-10', 'Les 1', 15.5, 0.3, 'Hladká'),
                    (1, 2, '2022-06-15', '2022-06-15', 'Les 2', 18.3, 0.35, 'Drsná');";
                        using (var insertCommand = new SqliteCommand(insertStromQuery, connection))
                        {
                            insertCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        // ------------------------------------
        // REGIONY
        // ------------------------------------
        public List<Region> GetAllRegiony()
        {
            var regiony = new List<Region>();

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var query = "SELECT ID, NazevRegionu FROM Region";

                using (var command = new SqliteCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        regiony.Add(new Region
                        {
                            ID = reader.GetInt32(0),
                            NazevRegionu = reader.GetString(1)
                        });
                    }
                }
            }

            return regiony;
        }
        public Region AddRegion(Region newRegion)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var insertRegionQuery = "INSERT INTO Region (NazevRegionu) VALUES (@NazevRegionu);";
                using (var command = new SqliteCommand(insertRegionQuery, connection))
                {
                    command.Parameters.AddWithValue("@NazevRegionu", newRegion.NazevRegionu);
                    command.ExecuteNonQuery();
                }

                // Získání ID nového regionu
                var getIdQuery = "SELECT last_insert_rowid();";
                using (var command = new SqliteCommand(getIdQuery, connection))
                {
                    newRegion.ID = Convert.ToInt32(command.ExecuteScalar());
                }

                return newRegion;
            }
        }

        // Metoda pro odstranění regionu podle ID
        public void DeleteRegion(int id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var query = "DELETE FROM Region WHERE ID = @ID";

                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    command.ExecuteNonQuery();
                }
            }
        }
        public void UpdateRegion(Region region)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var updateRegionQuery = "UPDATE Region SET NazevRegionu = @NazevRegionu WHERE ID = @ID;";
                using (var command = new SqliteCommand(updateRegionQuery, connection))
                {
                    command.Parameters.AddWithValue("@NazevRegionu", region.NazevRegionu);
                    command.Parameters.AddWithValue("@ID", region.ID);
                    command.ExecuteNonQuery();
                }
            }
        }

        // ------------------------------------
        // Správci
        // ------------------------------------
        public List<Spravce> GetAllSpravci()
        {
            var spravciList = new List<Spravce>();

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var query = @"
                    SELECT s.ID, s.Jmeno, s.Prijmeni, s.Email, s.Telefon, s.RegionID, r.NazevRegionu
                    FROM Spravce s
                    INNER JOIN Region r ON s.RegionID = r.ID";

                using (var command = new SqliteCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var spravce = new Spravce
                        {
                            ID = reader.GetInt32(0),
                            Jmeno = reader.GetString(1),
                            Prijmeni = reader.GetString(2),
                            Email = reader.GetString(3),
                            Telefon = reader.GetString(4),
                            RegionID = reader.GetInt32(5),
                            NazevRegionu = reader.GetString(6)
                        };
                        spravciList.Add(spravce);
                    }
                }
            }

            return spravciList;
        }

        public Spravce AddSpravce(Spravce newSpravce)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var insertSpravceQuery = @"
                    INSERT INTO Spravce (Jmeno, Prijmeni, Email, Telefon, RegionID) 
                    VALUES (@Jmeno, @Prijmeni, @Email, @Telefon, @RegionID);";

                using (var command = new SqliteCommand(insertSpravceQuery, connection))
                {
                    command.Parameters.AddWithValue("@Jmeno", newSpravce.Jmeno);
                    command.Parameters.AddWithValue("@Prijmeni", newSpravce.Prijmeni);
                    command.Parameters.AddWithValue("@Email", newSpravce.Email);
                    command.Parameters.AddWithValue("@Telefon", newSpravce.Telefon);
                    command.Parameters.AddWithValue("@RegionID", newSpravce.RegionID);
                    command.ExecuteNonQuery();
                }

                // Získání ID nového správce
                var getIdQuery = "SELECT last_insert_rowid();";
                using (var command = new SqliteCommand(getIdQuery, connection))
                {
                    newSpravce.ID = Convert.ToInt32(command.ExecuteScalar());
                }

                return newSpravce;
            }
        }

        // Metoda pro odstranění správce podle ID
        public void DeleteSpravce(int id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var query = "DELETE FROM Spravce WHERE ID = @ID";

                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        // ------------------------------------
        // Stromy
        // ------------------------------------

        // Metoda pro získání všech stromů
        public List<Strom> GetAllStromy()
        {
            var stromy = new List<Strom>();

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var query = @"
                    SELECT 
                        s.ID,
                        s.DruhStromu,
                        s.SpravceID,
                        s.DatumZasazeni,
                        s.DatumPridaniZaznamu,
                        s.Lokace,
                        s.Vyska,
                        s.PrumerKmeni,
                        s.TypKury,
                        spr.Jmeno || ' ' || spr.Prijmeni AS SpravceNazev,
                        r.NazevRegionu AS RegionNazev
                    FROM Strom s
                    LEFT JOIN Spravce spr ON s.SpravceID = spr.ID
                    LEFT JOIN Region r ON spr.RegionID = r.ID";

                using (var command = new SqliteCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        stromy.Add(new Strom
                        {
                            ID = reader.GetInt32(0),
                            DruhStromu = (DruhyStromuTyp)reader.GetInt32(1),
                            SpravceID = reader.GetInt32(2),
                            DatumZasazeni = reader.GetDateTime(3),
                            DatumPridaniZaznamu = reader.GetDateTime(4),
                            Lokace = reader.GetString(5),
                            Vyska = reader.GetDouble(6),
                            PrumerKmeni = reader.GetDouble(7),
                            TypKury = reader.IsDBNull(8) ? null : reader.GetString(8),
                            Spravce = reader.GetString(9)
                        });
                    }
                }
            }

            return stromy;
        }

        public Strom AddStrom(Strom newStrom)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var insertStromQuery = @"
                    INSERT INTO Strom (DruhStromu, SpravceID, DatumZasazeni, DatumPridaniZaznamu, Lokace, Vyska, PrumerKmeni, TypKury) 
                    VALUES (@DruhStromu, @SpravceID, @DatumZasazeni, @DatumPridaniZaznamu, @Lokace, @Vyska, @PrumerKmeni, @TypKury);";

                using (var command = new SqliteCommand(insertStromQuery, connection))
                {
                    command.Parameters.AddWithValue("@DruhStromu", (int)newStrom.DruhStromu);
                    command.Parameters.AddWithValue("@SpravceID", newStrom.SpravceID);
                    command.Parameters.AddWithValue("@DatumZasazeni", newStrom.DatumZasazeni);
                    command.Parameters.AddWithValue("@DatumPridaniZaznamu", newStrom.DatumPridaniZaznamu);
                    command.Parameters.AddWithValue("@Lokace", newStrom.Lokace);
                    command.Parameters.AddWithValue("@Vyska", newStrom.Vyska);
                    command.Parameters.AddWithValue("@PrumerKmeni", newStrom.PrumerKmeni);
                    command.Parameters.AddWithValue("@TypKury", newStrom.TypKury);
                    command.ExecuteNonQuery();
                }

                // Získání ID nového stromu
                var getIdQuery = "SELECT last_insert_rowid();";
                using (var command = new SqliteCommand(getIdQuery, connection))
                {
                    newStrom.ID = Convert.ToInt32(command.ExecuteScalar());
                }

                return newStrom;
            }
        }

        // Metoda pro odstranění stromu podle ID
        public void DeleteStrom(int id)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var query = "DELETE FROM Strom WHERE ID = @ID";

                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Metoda pro zrušení všech stromů (mazání všech záznamů)
        public void ClearStromy()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var query = "DELETE FROM Strom";

                using (var command = new SqliteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        // Metoda pro hledání stromů podle textu (např. Lokace, Typ Kůry, Region)
        public List<Strom> SearchStromy(string searchText)
        {
            var stromy = new List<Strom>();

            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                var query = @"
                    SELECT 
                        s.ID,
                        s.DruhStromu,
                        s.SpravceID,
                        s.DatumZasazeni,
                        s.DatumPridaniZaznamu,
                        s.Lokace,
                        s.Vyska,
                        s.PrumerKmeni,
                        s.TypKury,
                        spr.Jmeno || ' ' || spr.Prijmeni AS SpravceNazev,
                        r.NazevRegionu AS RegionNazev
                    FROM Strom s
                    LEFT JOIN Spravce spr ON s.SpravceID = spr.ID
                    LEFT JOIN Region r ON spr.RegionID = r.ID
                    WHERE s.Lokace LIKE @SearchText OR s.TypKury LIKE @SearchText OR r.NazevRegionu LIKE @SearchText";

                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SearchText", "%" + searchText + "%");

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            stromy.Add(new Strom
                            {
                                ID = reader.GetInt32(0),
                                DruhStromu = (DruhyStromuTyp)reader.GetInt32(1),
                                SpravceID = reader.GetInt32(2),
                                DatumZasazeni = reader.GetDateTime(3),
                                DatumPridaniZaznamu = reader.GetDateTime(4),
                                Lokace = reader.GetString(5),
                                Vyska = reader.GetDouble(6),
                                PrumerKmeni = reader.GetDouble(7),
                                TypKury = reader.IsDBNull(8) ? null : reader.GetString(8)
                            });
                        }
                    }
                }
            }

            return stromy;
        }
    }
}
