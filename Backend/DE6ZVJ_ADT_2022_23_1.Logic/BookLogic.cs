using DE6ZVJ_ADT_2022_23_1.Modells;
using DE6ZVJ_ADT_2022_23_1.Repository;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace DE6ZVJ_ADT_2022_23_1.Logic
{
    public class BookLogic : IBookLogic
    {
        protected IBookRepository bookrepo;

        public BookLogic(IBookRepository bookrepo)
        {
            this.bookrepo
                = bookrepo;
        }

        public Book AddNewBook(Book book)
        {
            if (book.Title == null)
            {
                throw new ArgumentException("Please enter the book's title");
            }
            else
            {
                this.bookrepo.Add(book);
                return book;
            }
        }

        public void DeleteBook(int id)
        {
            Book DeleteBook = this.bookrepo.GetOne(id);
            if (DeleteBook != null)
            {
                this.bookrepo.Delete(DeleteBook);
            }
            else
            {
                throw new Exception("The ID is not in the Authors database.");
            }
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return this.bookrepo.GetAll();
        }

        public Book GetBook(int id)
        {
            Book BookReturn = this.bookrepo.GetOne(id);
            if (BookReturn != null)
            {
                return BookReturn;
            }
            else
            {
                throw new Exception("The ID is not in the books database.");
            }
        }


        public int LongestBookQuery(IEnumerable<Book> books)
        {
            var longestBook = books.OrderByDescending(book => book.Pages).FirstOrDefault();
            return longestBook?.Pages ?? 0;
        }

        public int ShortestBookQuery(IEnumerable<Book> books)
        {
            var shortestBook = books.OrderBy(b => b.Pages).FirstOrDefault();
            return shortestBook?.Pages ?? 0;
        }
    }
    }

