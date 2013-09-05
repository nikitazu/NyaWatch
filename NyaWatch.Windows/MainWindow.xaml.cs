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

        private void CategoryDrop(object sender, DragEventArgs e)
        {
            var categoryButton = sender as Button;
            var targetCategory = categoryButton.CommandParameter as string;
#if DEBUG
            if (categoryButton == null)
            {
                MessageBox.Show("Wrong target");
                return;
            }

            if (string.IsNullOrWhiteSpace(targetCategory))
            {
                MessageBox.Show("Empty category");
                return;
            }
#endif
            if (!e.Data.GetDataPresent(DropFormats.InternalReference))
            {
                MessageBox.Show("Wrong data");
                return;
            }

            var anime = e.Data.GetData(DropFormats.InternalReference) as ViewModel.Anime;
            if (anime == null)
            {
                MessageBox.Show("Bad data");
                return;
            }

            Model.MoveAnimeToCategory(anime, targetCategory);
        }

        private void AnimeElementBorderMouseMove(object sender, MouseEventArgs e)
        {
            if (e.MouseDevice.LeftButton != MouseButtonState.Pressed) { return; }

            var source = sender as Border;
            var anime = source.Tag as ViewModel.Anime;
#if DEBUG
            if (source == null || anime == null) { return; }
#endif
            Model.SelectedAnime = anime;
            var package = new DataObject(DropFormats.InternalReference, anime);
            DragDrop.DoDragDrop(source, package, DragDropEffects.Move);
        }

        private readonly Core.Parsing.AnimeUrlResolver _animeUrlResolver = 
            new Core.Parsing.AnimeUrlResolver();

        private void OnAnimeSearchBoxKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Return && e.Key != Key.Enter) {
                return;
            }

            var searchBox = sender as TextBox;
            if (searchBox == null) {
                return;
            }

            var searchData = searchBox.Text;
            var parser = _animeUrlResolver.CreateParserFor(searchData);
            if (parser == null)
            {
                MessageBox.Show(
                    "Only direct urls to anime are supported for now.\n" +
                    "Please, provide url like:\n" +
                    "http://www.world-art.ru/animation/animation.php?id=395", 
                    "Incorrect input",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            var animeData = parser.ParseAnimeFromWeb(searchData);
            var anime = new ViewModel.Anime(
                animeData["title"],
                animeData["type"],
                int.Parse(animeData["episodes"]),
                0,
                animeData["airingStart"],
                animeData["airingEnd"],
                int.Parse(animeData["year"]));
            cd.Anime.Put(cd.Categories.PlanToWatch, anime);
            Model.ChangeCurrentCategory.Execute(cd.Categories.PlanToWatch.ToString());
        }
    }
}
