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
    public class ReviewViewModel : ObservableRecipient
    {
        private ApiClient ApiClient = new ApiClient();

        public ObservableCollection<Review> Review { get; set; }
        private Review selectedReview;

        public Review SelectedReview
        {
            get => selectedReview;
            set
            {
                SetProperty(ref selectedReview, value);
            }
        }
        private int selectedReviewIndex;

        public int SelectedReviewIndex
        {
            get => selectedReviewIndex;
            set
            {
                SetProperty(ref selectedReviewIndex, value);
            }
        }
        public RelayCommand AddReviewCommand { get; set; }
        public RelayCommand EditReviewCommand { get; set; }
        public RelayCommand DeleteReviewCommand { get; set; }
        public ReviewViewModel()
        {
            Review = new ObservableCollection<Review>();

            ApiClient
                .GetAsync<List<Review>>("http://localhost:23079/reviews")
                .ContinueWith((reviews) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        reviews.Result.ForEach((revs) =>
                        {
                            Review.Add(revs);
                        });
                    });
                });

            AddReviewCommand = new RelayCommand(AddReview);
            EditReviewCommand = new RelayCommand(EditReview);
            DeleteReviewCommand = new RelayCommand(DeleteReview);

        }
        private void AddReview()
        {
            Review n = new Review
            {
                Name = SelectedReview.Name,
                Id = SelectedReview.Id,
                Description = SelectedReview.Description
            };

            ApiClient
                .PostAsync(n, "http://localhost:23079/reviews")
                .ContinueWith((task) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Review.Add(n);
                    });
                });

        }
        private void EditReview()
        {
            ApiClient
                .PutAsync(SelectedReview, "http://localhost:23079/reviews")
                .ContinueWith((task) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        int i = SelectedReviewIndex;
                        Review a = SelectedReview;
                        Review.Remove(SelectedReview);
                        Review.Insert(i, a);
                    });
                });
        }
        private void DeleteReview()
        {
            ApiClient
                .DeleteAsync(SelectedReview.Id, "http://localhost:23079/reviews")
                .ContinueWith((task) =>
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        Review.Remove(SelectedReview);
                    });
                });
        }
    }
}


