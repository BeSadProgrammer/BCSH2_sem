using Microsoft.Data.Sqlite;
using StromApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StromApp.Handlers
{
    public class SQLiteHandler
    {
        private const string DatabaseFile = "data.db";

        // Establish a connection to the SQLite database
        private static SqliteConnection GetConnection()
        {
            var connectionString = new SqliteConnectionStringBuilder { DataSource = DatabaseFile }.ToString();
            return new SqliteConnection(connectionString);
        }

        // Initialize the database (create tables if they don't exist)
        public static void InitializeDatabase()
        {
            using var connection = GetConnection();
            connection.Open();

            // Create Regions table
            var createRegionsTableCmd = @"CREATE TABLE IF NOT EXISTS Regions (
                RegionID INTEGER PRIMARY KEY AUTOINCREMENT,
                NazevRegionu TEXT
            )";
            using var command = new SqliteCommand(createRegionsTableCmd, connection);
            command.ExecuteNonQuery();

            // Create Spravce table
            var createSpravceTableCmd = @"CREATE TABLE IF NOT EXISTS Spravce (
                SpravceID INTEGER PRIMARY KEY AUTOINCREMENT,
                Jmeno TEXT NOT NULL,
                Prijmeni TEXT NOT NULL,
                Email TEXT NOT NULL,
                Telefon TEXT NOT NULL,
                RegionID INTEGER,
                FOREIGN KEY (RegionID) REFERENCES Regions (RegionID)
            )";
            command.CommandText = createSpravceTableCmd;
            command.ExecuteNonQuery();

            // Create Strom table
            var createStromTableCmd = @"CREATE TABLE IF NOT EXISTS Strom (
                ID INTEGER PRIMARY KEY AUTOINCREMENT,
                StromTyp TEXT NOT NULL,
                SpravceID INTEGER,
                DatumZasazeni TEXT NOT NULL,
                DatumPridaniZaznamu TEXT NOT NULL,
                Lokace TEXT NOT NULL,
                Vyska REAL NOT NULL,
                PrumerKmene REAL NOT NULL,
                TypKury TEXT NOT NULL,
                FOREIGN KEY (SpravceID) REFERENCES Spravce (SpravceID)
            )";
            command.CommandText = createStromTableCmd;
            command.ExecuteNonQuery();
        }

        // Insert a Region
        public static void InsertRegion(Region region)
        {
            using var connection = GetConnection();
            connection.Open();

            var cmd = @"INSERT INTO Regions (NazevRegionu) VALUES (@NazevRegionu)";
            using var command = new SqliteCommand(cmd, connection);
            command.Parameters.AddWithValue("@NazevRegionu", region.NazevRegionu);
            command.ExecuteNonQuery();
        }

        // Get all Regions
        public static List<Region> GetRegions()
        {
            using var connection = GetConnection();
            connection.Open();

            var cmd = "SELECT * FROM Regions";
            using var command = new SqliteCommand(cmd, connection);
            using var reader = command.ExecuteReader();

            var regions = new List<Region>();
            while (reader.Read())
            {
                var region = new Region(reader.GetString(1)) { RegionID = reader.GetInt32(0) };
                regions.Add(region);
            }
            return regions;
        }

        // Insert a Spravce (Manager)
        public static void InsertSpravce(Spravce spravce)
        {
            using var connection = GetConnection();
            connection.Open();

            var cmd = @"INSERT INTO Spravce (Jmeno, Prijmeni, Email, Telefon, RegionID) 
                        VALUES (@Jmeno, @Prijmeni, @Email, @Telefon, @RegionID)";
            using var command = new SqliteCommand(cmd, connection);
            command.Parameters.AddWithValue("@Jmeno", spravce.Jmeno);
            command.Parameters.AddWithValue("@Prijmeni", spravce.Prijmeni);
            command.Parameters.AddWithValue("@Email", spravce.Email);
            command.Parameters.AddWithValue("@Telefon", spravce.Telefon);
            command.Parameters.AddWithValue("@RegionID", spravce.Region.RegionID);
            command.ExecuteNonQuery();
        }

        // Get all Spravce (Managers)
        public static List<Spravce> GetSpravce()
        {
            using var connection = GetConnection();
            connection.Open();

            var cmd = "SELECT * FROM Spravce";
            using var command = new SqliteCommand(cmd, connection);
            using var reader = command.ExecuteReader();

            var spravceList = new List<Spravce>();
            while (reader.Read())
            {
                var region = GetRegions().FirstOrDefault(r => r.RegionID == reader.GetInt32(5));
                var spravce = new Spravce(
                    reader.GetString(1),
                    reader.GetString(2),
                    reader.GetString(3),
                    reader.GetString(4),
                    region
                )
                { SpravceID = reader.GetInt32(0) };
                spravceList.Add(spravce);
            }
            return spravceList;
        }

        // Insert a Strom (Tree)
        public static void InsertStrom(Strom strom)
        {
            using var connection = GetConnection();
            connection.Open();

            var cmd = @"INSERT INTO Strom (StromTyp, SpravceID, DatumZasazeni, DatumPridaniZaznamu, Lokace, Vyska, PrumerKmene, TypKury) 
                        VALUES (@StromTyp, @SpravceID, @DatumZasazeni, @DatumPridaniZaznamu, @Lokace, @Vyska, @PrumerKmene, @TypKury)";
            using var command = new SqliteCommand(cmd, connection);
            command.Parameters.AddWithValue("@StromTyp", strom.StromTyp.ToString());
            command.Parameters.AddWithValue("@SpravceID", strom.Spravce.SpravceID);
            command.Parameters.AddWithValue("@DatumZasazeni", strom.DatumZasazeni.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@DatumPridaniZaznamu", strom.DatumPridaniZaznamu.ToString("yyyy-MM-dd"));
            command.Parameters.AddWithValue("@Lokace", strom.Lokace);
            command.Parameters.AddWithValue("@Vyska", strom.Vyska);
            command.Parameters.AddWithValue("@PrumerKmene", strom.PrumerKmene);
            command.Parameters.AddWithValue("@TypKury", strom.TypKury);
            command.ExecuteNonQuery();
        }

        // Get all Trees
        public static List<Strom> GetStromy()
        {
            using var connection = GetConnection();
            connection.Open();

            var cmd = "SELECT * FROM Strom";
            using var command = new SqliteCommand(cmd, connection);
            using var reader = command.ExecuteReader();

            var stromyList = new List<Strom>();
            while (reader.Read())
            {
                var spravce = GetSpravce().FirstOrDefault(s => s.SpravceID == reader.GetInt32(2));
                var strom = new Strom(
                    (DruhyStromuTyp)Enum.Parse(typeof(DruhyStromuTyp), reader.GetString(1)),
                    spravce,
                    DateTime.Parse(reader.GetString(3)),
                    DateTime.Parse(reader.GetString(4)),
                    reader.GetString(5),
                    reader.GetDouble(6),
                    reader.GetDouble(7),
                    reader.GetString(8)
                )
                {
                    ID = reader.GetInt32(0)
                };
                stromyList.Add(strom);
            }
            return stromyList;
        }

        // Delete Strom by ID
        public static void DeleteStrom(int stromID)
        {
            using var connection = GetConnection();
            connection.Open();

            var cmd = "DELETE FROM Strom WHERE ID = @ID";
            using var command = new SqliteCommand(cmd, connection);
            command.Parameters.AddWithValue("@ID", stromID);
            command.ExecuteNonQuery();
        }

        // Delete Region by ID
        public static void DeleteRegion(int regionID)
        {
            using var connection = GetConnection();
            connection.Open();

            var cmd = "DELETE FROM Regions WHERE RegionID = @RegionID";
            using var command = new SqliteCommand(cmd, connection);
            command.Parameters.AddWithValue("@RegionID", regionID);
            command.ExecuteNonQuery();
        }
    }
}
