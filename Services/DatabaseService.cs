using AdsDB.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AdsDB.Services
{
    public class DatabaseService
    {
        private readonly string _connectionString;

        public DatabaseService(string connectionString)
        {
            _connectionString = connectionString;
        }

        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public void InitializeDatabase()
        {
            using (var connection = CreateConnection())
            {
                connection.Open();

                connection.Execute(@"
                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Categories' AND xtype='U')
                    CREATE TABLE Categories (
                        Id INT PRIMARY KEY IDENTITY(1,1),
                        Name NVARCHAR(100) NOT NULL
                    );

                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Ads' AND xtype='U')
                    CREATE TABLE Ads (
                        Id INT PRIMARY KEY IDENTITY(1,1),
                        Title NVARCHAR(255) NOT NULL,
                        Description NVARCHAR(MAX),
                        Price DECIMAL(18, 2) NOT NULL,
                        CategoryId INT NOT NULL,
                        FOREIGN KEY (CategoryId) REFERENCES Categories(Id)
                    );
                ");
            }
        }

        public IEnumerable<Category> GetCategories()
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                var query = "SELECT * FROM Categories"; // Hämta alla kategorier från databasen
                return connection.Query<Category>(query);
            }
        }

        public IEnumerable<Ad> GetAds()
        {
            using (var connection = CreateConnection())
            {
                var query = @"
                    SELECT Ads.*, Categories.Name AS CategoryName
                    FROM Ads
                    INNER JOIN Categories ON Ads.CategoryId = Categories.Id";
                return connection.Query<Ad>(query);
            }
        }

        public void AddAd(Ad ad)
        {
            // Kontrollera att CategoryId finns i tabellen Categories
            using (var connection = CreateConnection())
            {
                var categoryExists = connection.ExecuteScalar<int>(
                    "SELECT COUNT(1) FROM Categories WHERE Id = @CategoryId", new { CategoryId = ad.CategoryId }) > 0;

                if (!categoryExists)
                {
                    throw new InvalidOperationException("The specified CategoryId does not exist in the Categories table.");
                }

                // Om CategoryId är giltigt, fortsätt med att lägga till annonsen
                connection.Execute("INSERT INTO Ads (Title, Description, Price, CategoryId) VALUES (@Title, @Description, @Price, @CategoryId)", ad);
            }
        }

        public void UpdateAd(Ad ad)
        {
            using (var connection = CreateConnection())
            {
                connection.Execute("UPDATE Ads SET Title = @Title, Description = @Description, Price = @Price, CategoryId = @CategoryId WHERE Id = @Id", ad);
            }
        }

        public void DeleteAd(int id)
        {
            using (var connection = CreateConnection())
            {
                connection.Execute("DELETE FROM Ads WHERE Id = @Id", new { Id = id });
            }
        }
    }
}
