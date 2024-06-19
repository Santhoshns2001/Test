﻿using Microsoft.Extensions.Configuration;
using ModelLayer;
using RepositaryLayer.Entities;
using RepositaryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositaryLayer.Service
{
    public class BookRepositary : IBookRepo
    {
        private readonly SqlConnection conn = new SqlConnection();

        private readonly string sqlConnectionString;

        private readonly IConfiguration configuration;

        public BookRepositary(IConfiguration configuration)
        {
            this.configuration = configuration;
            sqlConnectionString = configuration.GetConnectionString("DbConnection");
            conn.ConnectionString = sqlConnectionString;
        }

        public Book AddBook(BookModel bookModel)
        {
            Book book = null;
            try
            {
                if (conn != null)
                {
                    SqlCommand cmd = new SqlCommand("usp_AddBook", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Title", bookModel.Title);
                    cmd.Parameters.AddWithValue("@Author", bookModel.Author);
                    cmd.Parameters.AddWithValue("@Description", bookModel.Description);
                    cmd.Parameters.AddWithValue("@OriginalPrice", bookModel.OriginalPrice);
                    cmd.Parameters.AddWithValue("@DiscountPercentage", bookModel.DiscountedPrice);
                    cmd.Parameters.AddWithValue("@Quantity", bookModel.Quantity);
                    cmd.Parameters.AddWithValue("@Image", bookModel.Image);
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        book = new Book()
                        {
                            BookId = (int)reader["BookId"],
                            Title = (string)reader["Title"],
                            Author = (string)reader["Author"],
                            Description = (string)reader["Description"],
                            OriginalPrice = (int)reader["OriginalPrice"],
                            DiscountPercentage = (int)reader["DiscountPercentage"],
                            Quantity = (int)reader["Quantity"],
                            Image = (string)reader["Image"],
                            Rating = (decimal)reader["Rating"],
                            RatingCount = (int)reader["RatingCount"],
                            Price = (int)reader["Price"]
                        };
                    }
                    return book;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex) { throw ex; }
            finally { conn.Close(); }
        }




        public List<Book> GetAllBooks()
        {
            List<Book> books = new List<Book>();

            try
            {
                if (conn != null)
                {
                    SqlCommand cmd = new SqlCommand("usp_GetAllBooks", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Book book = new Book()
                        {
                            BookId = (int)reader["BookId"],
                            Title = (string)reader["Title"],
                            Author = (string)reader["Author"],
                            Description = (string)reader["Description"],
                            OriginalPrice = (int)reader["OriginalPrice"],
                            DiscountPercentage = (int)reader["DiscountPercentage"],
                            Quantity = (int)reader["Quantity"],
                            Image = (string)reader["Image"],
                            Rating = (decimal)reader["Rating"],
                            RatingCount = (int)reader["RatingCount"],
                            Price = (int)reader["Price"]
                        };
                        books.Add(book);
                    }
                    return books.ToList();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex) { throw ex; }
            finally { conn.Close(); }
        }

        public Book GetByBookId(int bookId)
        {
            Book book = null;
            try
            {
                if (conn != null)
                {
                    SqlCommand cmd = new SqlCommand("usp_GetByBookId", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@bookId", bookId);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        book = new Book()
                        {
                            BookId = (int)reader["BookId"],
                            Title = (string)reader["Title"],
                            Author = (string)reader["Author"],
                            Description = (string)reader["Description"],
                            OriginalPrice = (int)reader["OriginalPrice"],
                            DiscountPercentage = (int)reader["DiscountPercentage"],
                            Quantity = (int)reader["Quantity"],
                            Image = (string)reader["Image"],
                            Rating = (decimal)reader["Rating"],
                            RatingCount = (int)reader["RatingCount"],
                            Price = (int)reader["Price"]
                        };
                    }
                    return book;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { conn.Close(); }
        }


        public List<Book> FetchByAuthorOrTitle(string author,string title)
        {
           List< Book> books = new List<Book>();
            try
            {
                if (conn != null)
                {
                    SqlCommand cmd = new SqlCommand("usp_FetchByTitle_Author", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Author", author);
                    cmd.Parameters.AddWithValue("@Title", title);
                    conn.Open();

                    SqlDataReader reader= cmd.ExecuteReader();
                    while (reader.Read())
                    {
                       Book book = new Book()
                        {
                            BookId = (int)reader["BookId"],
                            Title = (string)reader["Title"],
                            Author = (string)reader["Author"],
                            Description = (string)reader["Description"],
                            OriginalPrice = (int)reader["OriginalPrice"],
                            DiscountPercentage = (int)reader["DiscountPercentage"],
                            Quantity = (int)reader["Quantity"],
                            Image = (string)reader["Image"],
                            Rating = (decimal)reader["Rating"],
                            RatingCount = (int)reader["RatingCount"],
                            Price = (int)reader["Price"]
                        };
                        books.Add(book);
                    }
                    return books.ToList();
                   

                }
                else
                {
                    throw new Exception("connection was not established");
                }
            }
            catch (Exception ex) { throw ex; }
            finally { conn.Close(); }
        }

        public Book UpdateBook(int bookId,BookModel bookModel)
        {
            Book book = null;
            try
            {
                if (conn != null)
                {
                    SqlCommand cmd = new SqlCommand("usp_UpdateBook", conn);
                    cmd.CommandType=CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@bookId", bookId);
                    cmd.Parameters.AddWithValue("@Title", bookModel.Title);
                    cmd.Parameters.AddWithValue("@Author", bookModel.Author);
                    cmd.Parameters.AddWithValue("@Description", bookModel.Description);
                    cmd.Parameters.AddWithValue("@OriginalPrice", bookModel.OriginalPrice);
                    cmd.Parameters.AddWithValue("@DiscountPercentage", bookModel.DiscountedPrice);
                    cmd.Parameters.AddWithValue("@Quantity", bookModel.Quantity);
                    cmd.Parameters.AddWithValue("@Image", bookModel.Image);
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        book = new Book()
                        {
                            BookId = (int)reader["BookId"],
                            Title = (string)reader["Title"],
                            Author = (string)reader["Author"],
                            Description = (string)reader["Description"],
                            OriginalPrice = (int)reader["OriginalPrice"],
                            DiscountPercentage = (int)reader["DiscountPercentage"],
                            Quantity = (int)reader["Quantity"],
                            Image = (string)reader["Image"],
                            Rating = (decimal)reader["Rating"],
                            RatingCount = (int)reader["RatingCount"],
                            Price = (int)reader["Price"]
                        };
                    }
                    return book;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }

        }

        public bool DeleteBookById(int bookId)
        {
            try
            {
                if (conn != null)
                {
                    SqlCommand cmd = new SqlCommand("usp_DeleteBookById", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@bookId", bookId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex) { throw ex; }
            finally { conn.Close(); }
        }


    }
}