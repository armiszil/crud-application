using DE6ZVJ_ADT_2022_23_1.Modells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DE6ZVJ_ADT_2022_23_1.Logic
{
    public interface IBookLogic
    {
        public Book AddNewBook(Book book);
        public void DeleteBook(int id);
        Book GetBook(int id);
        IEnumerable<Book> GetAllBooks();

        int LongestBookQuery(IEnumerable<Book> books);
        int ShortestBookQuery(IEnumerable<Book> books);



    }
}
