using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using NyaWatch.Windows.WPF;
using cd = NyaWatch.Core.Domain;

namespace NyaWatch.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ViewModel.Root Model { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Model = new ViewModel.Root();
            DataContext = Model;
        }

        #region Categories filters

        private void PlanToWatchClick(object sender, RoutedEventArgs e)
        {
            Model.SelectedCategory = cd.Categories.PlanToWatch;
        }

        private void WatchingClick(object sender, RoutedEventArgs e)
        {
            Model.SelectedCategory = cd.Categories.Watching;
        }

        private void CompletedClick(object sender, RoutedEventArgs e)
        {
            Model.SelectedCategory = cd.Categories.Completed;
        }

        private void OnHoldClick(object sender, RoutedEventArgs e)
        {
            Model.SelectedCategory = cd.Categories.OnHold;
        }

        private void DroppedClick(object sender, RoutedEventArgs e)
        {
            Model.SelectedCategory = cd.Categories.Dropped;
        }

        #endregion

        #region Increment/decrement

        private void IncrementClick(object sender, RoutedEventArgs e)
        {
            var anime = (sender as Control).Tag as ViewModel.Anime;
            Model.SelectedAnime = anime;
            Model.SelectedAnime.Watched += 1;
            cd.Anime.Save(Model.SelectedCategory, Model.SelectedAnime);
        }

        private void DecrementClick(object sender, RoutedEventArgs e)
        {
            var anime = (sender as Control).Tag as ViewModel.Anime;
            Model.SelectedAnime = anime;
            Model.SelectedAnime.Watched -= 1;
            cd.Anime.Save(Model.SelectedCategory, Model.SelectedAnime);
        }

        #endregion
    }
}
