using DE6ZVJ_ADT_2022_23_1.Modells;
using DE6ZVJ_SGUI_2022_23_2.WpfClientt.Client;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DE6ZVJ_SGUI_2022_23_2.WpfClientt.ViewModels
{
    class BookViewModel : ObservableRecipient
    {
        private ApiClient ApiClient = new ApiClient();

    public ObservableCollection<Book> Books { get; set; }
    public IList<KeyValuePair<string, int>> ShortestBook { get; set; }
    public IList<KeyValuePair<string, int>> LongestBooks { get; set; }
  

    private Book selectedBook;

    public Book SelectedBook
    {
        get => selectedBook;
        set
        {
            SetProperty(ref selectedBook, value);
        }
    }
    private int selectedBookIndex;

    public int SelectedBookIndex
    {
        get => selectedBookIndex;
        set
        {
            SetProperty(ref selectedBookIndex, value);
        }
    }
    public RelayCommand AddBookCommand { get; set; }
    public RelayCommand EditBookCommand { get; set; }
    public RelayCommand DeleteBookCommand { get; set; }
    public RelayCommand LongestBooksCommand { get; set; }
    public RelayCommand ShortestBookCommand { get; set; }
    public BookViewModel()
    {
        Books = new ObservableCollection<Book>();
        ShortestBook = new List<KeyValuePair<string, int>>();
        LongestBooks = new List<KeyValuePair<string, int>>();
       


        ApiClient
            .GetAsync<List<Book>>("http://localhost:23079/books")
            .ContinueWith((books) =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    books.Result.ForEach((book) =>
                    {
                        Books.Add(book);
                    });
                });
            });

        ApiClient
           .GetAsync<List<KeyValuePair<string, int>>>("http://localhost:23079/Noncrudbook/Shortest")
           .ContinueWith((LongestBook) =>
           {
               Application.Current.Dispatcher.Invoke(() =>
               {
                   LongestBook.Result.ForEach((longbook) =>
                   {
                       ShortestBook.Add(longbook);
                   });
               });
           });
        ApiClient
           .GetAsync<List<KeyValuePair<string, int>>>("http://localhost:23079/Noncrudbook/Longest")
           .ContinueWith((LongestBook) =>
           {
               Application.Current.Dispatcher.Invoke(() =>
               {
                   LongestBook.Result.ForEach((longestbook) =>
                   {
                       LongestBooks.Add(longestbook);
                   });
               });
           });
      


        AddBookCommand = new RelayCommand(AddBook);
        EditBookCommand = new RelayCommand(EditBook);
        DeleteBookCommand = new RelayCommand(DeleteBook);
        ShortestBookCommand = new RelayCommand(ShortestBookCalculation);
        LongestBooksCommand = new RelayCommand(LongestBooksCalculation);
        


    }


    #region CRUD
    private void AddBook()
    {
        Book n = new Book
        {
            Title = SelectedBook.Title,
            Pages = selectedBook.Pages,
            Genre = SelectedBook.Genre,
            Author = SelectedBook.Author

        };

        ApiClient
            .PostAsync(n, "http://localhost:23079/books")
            .ContinueWith((task) =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Books.Add(n);
                });
            });
    }

    private void EditBook()
    {
        ApiClient
            .PutAsync(SelectedBook, "http://localhost:23079/books")
            .ContinueWith((task) =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    int i = SelectedBookIndex;
                    Book a = SelectedBook;
                    Books.Remove(SelectedBook);
                    Books.Insert(i, a);
                });
            });
    }
    private void DeleteBook()
    {
        ApiClient
            .DeleteAsync(SelectedBook.Id, "http://localhost:23079/books")
            .ContinueWith((task) =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Books.Remove(SelectedBook);
                });
            });
    }
    #endregion

    #region NON-CRUD
    private void ShortestBookCalculation()
    {
        new ShortestBookWindow().Show();
    }
    private void LongestBooksCalculation()
    {

        new LongestBookWindow().Show();
    }
   
    #endregion

}
}



