using ConsoleTools;
using DE6ZVJ_ADT_2022_23_1.Modells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace DE6ZVJ_ADT_2022_23_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            System.Threading.Thread.Sleep(8000);
            RestService rest = new RestService("http://localhost:26320");

            var MenuForBookssadmin = new ConsoleMenu()
                .Add(">> READ By Id", () =>ReadBookById(rest))
                .Add(">> READ All", () => ReadAllBooks(rest))
                .Add(">> DELETE", () => DeleteBook(rest))
                .Add(">> Longest book (non-crud)", () => LongestBook(rest))
                .Add(">>shortest book (non-crud)", () => ShortestBook(rest))
                .Add(">> GO BACK TO MENU", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.SelectedItemBackgroundColor = ConsoleColor.Green;
                });
            var MenuForBooks = new ConsoleMenu()
                .Add(">> CREATE", () => AddNewBook(rest))
                .Add(">> Read all Books", () => ReadAllBooks(rest))
                .Add(">> Read all Authors", () => ReadAllAuthors(rest))
                
                .Add(">> DELETE", () => DeleteBook(rest))
                .Add(">> GO BACK TO MENU", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.SelectedItemBackgroundColor = ConsoleColor.Green;
                });
            var MenuForAuthors = new ConsoleMenu()
                .Add(">> CREATE", () => AddNewAuthor(rest))
                .Add(">> READ By Id", () => ReadAuthorById(rest))
                .Add(">> READ All", () => ReadAllAuthors(rest))
               .Add(">> First name",()=>FirstName(rest))
                .Add(">> Second name", () => SecondName(rest))
                 .Add(">> All Caps", () => AllCaps(rest))
                .Add(">> DELETE", () => DeleteAuthor(rest))
              
                .Add(">> GO BACK TO MENU", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.SelectedItemBackgroundColor = ConsoleColor.Green;
                });
            var MenuForReviews = new ConsoleMenu()
                .Add(">> CREATE", () => AddNewReview(rest))
                .Add(">> READ By Id", () => ReadReviewById(rest))
                .Add(">> READ All", () => ReadAllReviews(rest))
             
                .Add(">> DELETE", () => DeleteReview(rest))
                .Add(">> GO BACK TO MENU", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.SelectedItemBackgroundColor = ConsoleColor.Green;
                });
           

            var menuForAdministrator = new ConsoleMenu(args, level: 0)
                .Add(">> Books", () => MenuForBookssadmin.Show())
                .Add(">> Authors ", () => MenuForAuthors.Show())
                .Add(">> Reviews ", () => MenuForReviews.Show())
                
                .Add(">> Exit", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.SelectedItemBackgroundColor = ConsoleColor.Blue;
                });
            var MainMenu = new ConsoleMenu(args, level: 0)
                .Add(">> Book", () => MenuForBooks.Show())
                .Add(">> Authors ", () => menuForAdministrator.Show())
                .Add(">> Exit", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.SelectedItemBackgroundColor = ConsoleColor.Blue;
                });

            MainMenu.Show();
        }
        #region BooksMethods
        private static void AddNewBook(RestService rest)
        {
            try
            {
                Console.WriteLine("\n:: New Book ::\n");
                Console.Write("Book's Name : ");
                string title = Console.ReadLine();

                Console.Write("Book's Genre : ");
                string genre = Console.ReadLine();

                Console.Write("Book's pages : ");
                int pages = int.Parse(Console.ReadLine());

                

                Book book = new Book() { Title=title , Genre=genre,Pages=pages};

                rest.Post<Book>(book, "Books");

                Console.WriteLine("\n A book with name " + title.ToString().ToUpper() + " has been added to the Database\n");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
        private static void ReadBookById(RestService rest)
        {
            Console.Write("\n ID of Book :  ");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\n{"Id",3} | {"Title",-20} {"genre",-28} {"page",10}");
                Console.ResetColor();
                Console.WriteLine(rest.Get<Book>(id, "books").ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void LongestBook(RestService rest)
        {
            Console.Write("Book's ID : ");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Blue;
                var coun = rest.Get<Book>("NonCrudBookController/LongestBook");
                Console.WriteLine("This book is the longest : " + coun );
                Console.ResetColor();
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
        private static void ReadAllBooks(RestService rest)
        {
            Console.WriteLine("\n   ALL Books :  \n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\n{"Id",3} | {"Title",-20} {"genre",-28} {"page",10}");
            Console.ResetColor();
            var books = rest.Get<Book>("books");
            books.ForEach(x => Console.WriteLine(x.ToString()));
            Console.ReadLine();
        }
        
        private static void DeleteBook(RestService rest)
        {
            Console.WriteLine("\n Book's ID :  \n");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("\n  Book who will be deleted  has ID : " + id);
                rest.Delete(id, "books");
                Console.WriteLine("  Book deleted! ");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void ShortestBook(RestService rest)
        {
            var shortestbook = rest.Get<Book>("NonCrudBookController/ShortestBook");
            foreach (var item in shortestbook)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Best Book Id : " + item.Id);
                Console.ResetColor();
            }
            Console.ReadLine();
        }
     
        #endregion

        #region AuthorMethods
        private static void AddNewAuthor(RestService rest)
        {
            Console.WriteLine("\n:: New Author ::\n");
            Console.Write("Author's Name : ");
            string name = Console.ReadLine();

          

            rest.Post<Author>(new Author() { Name = name,  }, "authors");

            Console.WriteLine("\n An author with the name  " + name.ToString().ToUpper() + " has been added to the Database\n");

            Console.ReadLine();
        }
        private static void ReadAuthorById(RestService rest)
        {
            Console.WriteLine("\n ID of Author :  \n");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\n{"Id",3} | {"Name",15}");
                Console.ResetColor();
                Console.WriteLine(rest.Get<Author>(id, "authors").ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void FirstName(RestService rest)
        {
            var names = rest.Get<Author>("NonCrudAuthorController/FirstName");
            foreach (var item in names)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("First names : " + item.Name);
                Console.ResetColor();
            }
            Console.ReadLine();
        }
        private static void SecondName(RestService rest)
        {
            var names = rest.Get<Author>("NonCrudAuthorController/SecondName");
            foreach (var item in names)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Second names : " + item.Name);
                Console.ResetColor();
            }
            Console.ReadLine();
        }
        private static void AllCaps(RestService rest)
        {
            var names = rest.Get<Author>("NonCrudAuthorController/AllCaps");
            foreach (var item in names)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("All caps : " + item.Name);
                Console.ResetColor();
            }
            Console.ReadLine();
        }
        private static void ReadAllAuthors(RestService rest)
        {
            Console.WriteLine("\n   ALL Authors :  \n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\n{"Id",3} |{"Name",15}");
            Console.ResetColor();
            var authors = rest.Get<Author>("authors");
            authors.ForEach(x => Console.WriteLine(x.ToString()));
            Console.ReadLine();
        }
    
        private static void DeleteAuthor(RestService rest)
        {
            Console.WriteLine("\n Author's ID :  \n");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("\n  Author to be deleted has ID :  " + id);
                rest.Delete(id, "authors");
                Console.WriteLine("  Author deleted! ");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
      
       
        #endregion

        #region ReviewMethods
        private static void AddNewReview(RestService rest)
        {
            Console.WriteLine("\n:: New Review ::\n");


            Console.Write("Book ID  : ");
            int bookId = int.Parse(Console.ReadLine());

            Console.WriteLine("Content : ");
            string content = Console.ReadLine();
            Console.WriteLine("name : ");
            string name = Console.ReadLine();
            Console.WriteLine("Rating : ");
            string rating = Console.ReadLine();

            try
            {
                rest.Post<Review>(new Review() { BookId =bookId , Description=content,Name=name,Rating=rating }, "ratings");
                Console.WriteLine("\n A Review for a book with ID " + bookId + " has been added to the Database\n");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void ReadReviewById(RestService rest)
        {
            Console.WriteLine("\n ID of Review :  \n");
            try
            {
                int id = int.Parse(Console.ReadLine());
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"\n{"Id",3} | {"Book Id ",-20} {"review",-20} {"Description",25} {"Name",25}");
                Console.ResetColor();
                var re = rest.Get<Review>(id, "reviews");
                Console.WriteLine(re.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private static void ReadAllReviews(RestService rest)
        {
            Console.WriteLine("\n   ALL Reviews :  \n");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"\n{"Id",3} | {"Book Id ",-20} {"review",-20} {"Description",25} {"Name",25}");
            Console.ResetColor();
            var reservations = rest.Get<Review>("reviews");
            reservations.ForEach(x => Console.WriteLine(x.ToString()));
            Console.ReadLine();
        }
        
        private static void DeleteReview(RestService rest)
        {
            Console.WriteLine("Review's ID which will be deleted ");

            int id = int.Parse(Console.ReadLine());
            rest.Delete(id, "reviews");
            Console.WriteLine("  Review deleted! ");

            Console.ReadLine();
        }
        #endregion

        
    }
}
