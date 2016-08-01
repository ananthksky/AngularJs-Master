using AngularJs_BasicCRUD.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AngularJs_BasicCRUD.Controllers
{
    public class CRUDAPIController : ApiController
    {
        const string sConnString = "Data Source=ANANTH-PC;Persist Security Info=False;" +
            "Initial Catalog=Learning;User Id=sa;Password=sa@123;Connect Timeout=30;";

        // LIST OBJECT WILL HOLD AND RETURN A LIST OF BOOKS.
        List<Books> MyBooks = new List<Books>();

        [HttpPost()]
        public IEnumerable<Books> Perform_CRUD(Books list)
        {
            bool bDone = false;

            using (SqlConnection con = new SqlConnection(sConnString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT *FROM dbo.Books"))
                {
                    cmd.Connection = con;
                    con.Open();

                    switch (list.Operation)
                    {
                        case "SAVE":
                            if (list.BookName != "" & list.Category != "" & list.Price > 0)
                            {
                                cmd.CommandText = "INSERT INTO dbo.Books (BookName, Category, Price) " +
                                    "VALUES (@BookName, @Category, @Price)";

                                cmd.Parameters.AddWithValue("@BookName", list.BookName.Trim());
                                cmd.Parameters.AddWithValue("@Category", list.Category.Trim());
                                cmd.Parameters.AddWithValue("@Price", list.Price);

                                bDone = true;
                            }
                            break;

                        case "UPDATE":
                            if (list.BookName != "" & list.Category != "" & list.Price > 0)
                            {
                                cmd.CommandText = "UPDATE dbo.Books SET BookName = @BookName, Category = @Category, " +
                                    "Price = @Price WHERE BookID = @BookID";

                                cmd.Parameters.AddWithValue("@BookName", list.BookName.Trim());
                                cmd.Parameters.AddWithValue("@Category", list.Category.Trim());
                                cmd.Parameters.AddWithValue("@Price", list.Price);
                                cmd.Parameters.AddWithValue("@BookID", list.BookID);

                                bDone = true;
                            }
                            break;

                        case "DELETE":
                            cmd.CommandText = "DELETE FROM dbo.Books WHERE BookID = @BookID";
                            cmd.Parameters.AddWithValue("@BookID", list.BookID);

                            bDone = true;
                            break;
                    }

                    if (bDone)
                    {
                        cmd.ExecuteNonQuery();
                    }

                    con.Close();
                }
            }

            if (bDone)
            {
                GetData();
            }
            return MyBooks;
        }

        // EXTRACT ALL TABLE ROWS AND RETURN DATA TO THE CLIENT APP.
        private void GetData()
        {
            using (SqlConnection con = new SqlConnection(sConnString))
            {
                SqlCommand objComm = new SqlCommand("SELECT *FROM dbo.Books ORDER BY BookID DESC", con);
                con.Open();

                SqlDataReader reader = objComm.ExecuteReader();

                // POPULATE THE LIST WITH DATA.
                while (reader.Read())
                {
                    MyBooks.Add(new Books
                    {
                        BookID = Convert.ToInt32(reader["BookID"]),
                        BookName = reader["BookName"].ToString(),
                        Category = reader["Category"].ToString(),
                        Price = Convert.ToDecimal(reader["Price"])
                    });
                }

                con.Close();
            }
        }

    }
}
