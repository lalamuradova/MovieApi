using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MovieApi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string Minute { get; set; }
        public string Description { get; set; }

        public string Rating { get; set; }
        public dynamic Data { get; set; }
        public dynamic SingleData { get; set; }

        HttpClient httpclient = new HttpClient();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }
        public int Index { get; set; } = 0;
        public void GetMovie()
        {
            var name = movieTxtBox.Text;
            HttpResponseMessage response = new HttpResponseMessage();
            response = httpclient.GetAsync($@"http://www.omdbapi.com/?apikey=4e6e4efc&s={name}&plot=full").Result;
            var str = response.Content.ReadAsStringAsync().Result;
            Data = JsonConvert.DeserializeObject(str);
            response = httpclient.GetAsync($@"http://www.omdbapi.com/?apikey=4e6e4efc&t={Data.Search[Index].Title}&plot=full").Result;

            str = response.Content.ReadAsStringAsync().Result;
            SingleData = JsonConvert.DeserializeObject(str);


            Movieimage.Source = SingleData.Poster;
            Movieimage1.Source = SingleData.Poster;
            Minute = SingleData.Runtime;
            Description = SingleData.Genre;

            
            movienamelabel.Content = SingleData.Title + " (" + SingleData.Year + ")";
            movieGenrelabel.Content = Minute + " | " + Description;

            Rating = SingleData.imdbRating;
            double rating = double.Parse(Rating);
            if (rating >= 0 && rating <= 2) 
            {
                star1.Visibility = Visibility.Visible;
            }
            else if (rating > 2 && rating <= 4)
            {
                star1.Visibility = Visibility.Visible;
                star2.Visibility = Visibility.Visible;
            }
            else if (rating > 4 && rating <= 6)
            {
                star1.Visibility = Visibility.Visible;
                star2.Visibility = Visibility.Visible;
                star3.Visibility = Visibility.Visible;
            }
            else if(rating > 6 && rating <= 8)
            {
                star1.Visibility = Visibility.Visible;
                star2.Visibility = Visibility.Visible;
                star3.Visibility = Visibility.Visible;
                star4.Visibility = Visibility.Visible;
            }
            else if (rating > 8 && rating <= 10)
            {
                star1.Visibility = Visibility.Visible;
                star2.Visibility = Visibility.Visible;
                star3.Visibility = Visibility.Visible;
                star4.Visibility = Visibility.Visible;
                star5.Visibility = Visibility.Visible;
            }
            RatingLbl.Content = "Rating ";
            RatingLbl2.Content = Rating;
            PlotTxtBlock.Text = SingleData.Plot;
        }
        

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Index++;
                GetMovie();
                prev.Visibility = Visibility.Visible;
            }
            catch (Exception)
            {
                next.Visibility = Visibility.Hidden;
                --Index;
            }

        }

        private void Image_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (Index == 0)
            {
                prev.Visibility = Visibility.Hidden;
                next.Visibility = Visibility.Visible;
            }
            else
            {
                Index--;
                GetMovie();
            }

        }

        private void seachImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            GetMovie();
        }


    }
}