using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DE6ZVJ_SGUI_2022_23_2.WpfClientt.ViewModels
{
    public class MainWindowViewModel : ObservableRecipient
    {

        public RelayCommand ManageAuthorsCommand { get; set; }
        public RelayCommand ManageBooksCommand { get; set; }
        public RelayCommand ManageReviewsCommand { get; set; }


        public MainWindowViewModel()
        {
            ManageAuthorsCommand = new RelayCommand(OpenAuthorWindow);
            ManageBooksCommand = new RelayCommand(OpenBookWindow);
            ManageReviewsCommand = new RelayCommand(OpenReviewWindow);
        }
        private void OpenAuthorWindow()
        {
            new AuthorWindow().Show();
        }
        private void OpenBookWindow()
        {
            new BookWindow().Show();
        }
        private void OpenReviewWindow()
        {
            new ReviewWindow().Show();
        }

    }
}

