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

        private void AnimeElementBorderMouseDown(object sender, MouseButtonEventArgs e)
        {
#if DEBUG
            var source = sender as Border;
            if (source == null)
            {
                return;
            }

            var anime = source.Tag as ViewModel.Anime;
            if (anime == null)
            {
                return;
            }
#endif
            var package = new DataObject(DropFormats.InternalReference, anime);
            DragDrop.DoDragDrop(source, package, DragDropEffects.Move);
        }

        private void CategoryDrop(object sender, DragEventArgs e)
        {
#if DEBUG
            var categoryButton = sender as Button;
            if (categoryButton == null)
            {
                MessageBox.Show("Wrong target");
                return;
            }

            var targetCategory = categoryButton.CommandParameter as string;
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
    }
}
