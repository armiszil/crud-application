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
    public class AuthorViewModel : ObservableRecipient
    {
        private ApiClient ApiClient = new ApiClient();

        public ObservableCollection<Author> Author { get; set; }
        private Author selectedAuthor;

        public Author SelectedAuthor
        {
            get => selectedAuthor;
            set
            {
                SetProperty(ref selectedAuthor, value);
            }
        }
        private int selectedAuthorIndex;

        public int SelectedAuthorIndex
        {
            get => selectedAuthorIndex;
            set
            {
                SetProperty(ref selectedAuthorIndex, value);
            }
        }
        public RelayCommand AddAuthorCommand { get; set; }
        public RelayCommand EditAuthorCommand { get; set; }
        public RelayCommand DeleteAuthorCommand { get; set; }
        public AuthorViewModel()
        {
            Author = new ObservableCollection<Author>();

            ApiClient
                .GetAsync<List<Author>>("http://localhost:23079/authors")
                .ContinueWith((authors) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        authors.Result.ForEach((authorsss) =>
                        {
                            Author.Add(authorsss);
                        });
                    });
                });

            AddAuthorCommand = new RelayCommand(AddAuthor);
            EditAuthorCommand = new RelayCommand(EditAuthor);
            DeleteAuthorCommand = new RelayCommand(DeleteAuthor);

        }
        private void AddAuthor()
        {
            Author n = new Author
            {
                Name = SelectedAuthor.Name,
                Id = SelectedAuthor.Id,
                Age = SelectedAuthor.Age
            };

            ApiClient
                .PostAsync(n, "http://localhost:23079/authors")
                .ContinueWith((task) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Author.Add(n);
                    });
                });

        }
        private void EditAuthor()
        {
            ApiClient
                .PutAsync(SelectedAuthor, "http://localhost:23079/authors")
                .ContinueWith((task) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        int i = SelectedAuthorIndex;
                        Author a = SelectedAuthor;
                        Author.Remove(SelectedAuthor);
                        Author.Insert(i, a);
                    });
                });
        }
        private void DeleteAuthor()
        {
            ApiClient
                .DeleteAsync(SelectedAuthor.Id, "http://localhost:23079/authors")
                .ContinueWith((task) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Author.Remove(SelectedAuthor);
                    });
                });
        }
    }
}

