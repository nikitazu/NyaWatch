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

        private void PlanToWatchClick(object sender, RoutedEventArgs e)
        {
            Model.Animes = Core.Domain.Anime.Find<ViewModel.Anime>(Core.Domain.Categories.PlanToWatch);
        }

        private void WatchingClick(object sender, RoutedEventArgs e)
        {
            Model.Animes = Core.Domain.Anime.Find<ViewModel.Anime>(Core.Domain.Categories.Watching);
        }

        private void CompletedClick(object sender, RoutedEventArgs e)
        {
            Model.Animes = Core.Domain.Anime.Find<ViewModel.Anime>(Core.Domain.Categories.Completed);
        }

        private void OnHoldClick(object sender, RoutedEventArgs e)
        {
            Model.Animes = Core.Domain.Anime.Find<ViewModel.Anime>(Core.Domain.Categories.OnHold);
        }

        private void DroppedClick(object sender, RoutedEventArgs e)
        {
            Model.Animes = Core.Domain.Anime.Find<ViewModel.Anime>(Core.Domain.Categories.Dropped);
        }
    }
}
