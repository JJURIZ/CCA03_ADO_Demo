using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.Data.SqlClient;

namespace AuthorManager.Models
{
    public class AuthorRepositoryADO : IAuthorRepository
    {
        private string connStr = "Server=(localdb)\\mssqllocaldb;Database=aspnet-AuthorManager-31AB7442-C78C-456E-AEFF-A455B6AA3A6F;Trusted_Connection=True;MultipleActiveResultSets=true";

        private string selectQuery = "SELECT Id, FName, LName, Email\n" +
            "FROM Authors\n";

        private string selectByIdClause = "WHERE Id = @id\n";

        private string orderByName = "ORDER BY LName desc, FName\n";

        private string insertAuthorQuery = "INSERT INTO Authors\n" +
            "(FName, LName, Email)\n" +
            "values(@fname, @lname, @email)\n";


        public List<Author> ListAll()
        {
            List<Author> authors = new List<Author>();

            using (SqlConnection conn = new SqlConnection(connStr)) // This should implement IDisposable
            {
                SqlCommand command = new SqlCommand(selectQuery + orderByName, conn);

                try
                {
                    conn.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while(reader.Read())
                    {
                        Author newAuthor = new Author
                        {
                            ID = int.Parse(reader[0].ToString()),
                            FName = reader[1].ToString(),
                            LName = reader[2].ToString(),
                            Email = reader[3].ToString()
                        };

                        authors.Add(newAuthor);
                    }
                }
                catch (Exception ex)
                {

                }
            }
                return authors;
        }

        public Author GetById(int id)
        {
            Author author = new Author();

            using (var conn = new SqlConnection(connStr))
            {
                SqlCommand command = new SqlCommand(selectQuery + selectByIdClause, conn);

                command.Parameters.AddWithValue("@id",id);

                try
                {
                    conn.Open();

                    SqlDataReader reader = command.ExecuteReader();

                    while(reader.Read())
                    {
                        author = new Author
                        {
                            ID = int.Parse(reader[0].ToString()),
                            FName = reader[1].ToString(),
                            LName = reader[2].ToString(),
                            Email = reader[3].ToString()
                        };
                    }
                }
                catch
                {
                    //TODO Add Logging

                    throw;
                }
            }
            return author;
        }

        public void AddAuthor(Author newAuthor)
        {
            using var conn = new SqlConnection(connStr);
            try
            {
                var command = new SqlCommand(insertAuthorQuery, conn);

                command.Parameters.AddWithValue("@fname", newAuthor.FName);
                command.Parameters.AddWithValue("@lname", newAuthor.LName);
                command.Parameters.AddWithValue("@email", newAuthor.Email);

                conn.Open();

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //TODO Add Logging

                throw;
            }
        }
    }
}
