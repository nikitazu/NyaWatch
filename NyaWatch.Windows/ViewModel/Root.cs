using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

using NyaWatch.Core.ComponentModel;
using cd = NyaWatch.Core.Domain;

namespace NyaWatch.Windows.ViewModel
{
    public class Root : INotifyPropertyChanged
    {
        List<Anime> _animes;
        public List<Anime> Animes 
        {
            get { return _animes; }
            set { PropertyChanged.ChangeAndNotify(this, ref _animes, value, () => Animes); }
        }

        Anime _selectedAnime;
        public Anime SelectedAnime 
        {
            get { return _selectedAnime; }
            set {  PropertyChanged.ChangeAndNotify(this, ref _selectedAnime, value, () => SelectedAnime); }
        }

        cd.Categories _selectedCategory;
        public cd.Categories SelectedCategory 
        {
            get { return _selectedCategory; }
            set 
            {
                PropertyChanged.ChangeAndNotify(this, ref _selectedCategory, value, () => SelectedCategory);
                Animes = cd.Anime.Find<ViewModel.Anime>(value).Select(a => a.WithRoot(this)).ToList();
            }
        }

        public Root()
        {
            InitCommands();

            Animes = new List<Anime>();
            SelectedCategory = cd.Categories.Watching;
        }

        #region Commands

        public ICommand Save { get; private set; }
        public ICommand Load { get; private set; }
        public ICommand Reset { get; private set; }

        public ICommand ChangeCurrentCategory { get; private set; }
        public ICommand IncrementWatched { get; private set; }
        public ICommand DecrementWatched { get; private set; }
        public ICommand TogglePinned { get; private set; }

        public ICommand MoveToPlanToWatch { get; private set; }
        public ICommand MoveToWatching { get; private set; }
        public ICommand MoveToCompleted { get; private set; }
        public ICommand MoveToOnHold { get; private set; }
        public ICommand MoveToDropped { get; private set; }

        void InitCommands()
        {
            Save = new RelayCommand(_ => cd.Anime.Save());
            Load = new RelayCommand(_ => cd.Anime.Load());
            Reset = new RelayCommand(_ => cd.Anime.Drop());

            ChangeCurrentCategory = new RelayCommand<string>(
                cat => SelectedCategory = (cd.Categories)Enum.Parse(typeof(cd.Categories), cat),
                cat => SelectedCategory != (cd.Categories)Enum.Parse(typeof(cd.Categories), cat));

            IncrementWatched = new RelayCommand<Anime>(
                anime => cd.Anime.Save(SelectedCategory, SelectedAnime = anime.IncrementWatched()),
                anime => anime != null && anime.CanIncrementWatched());

            DecrementWatched = new RelayCommand<Anime>(
                anime => cd.Anime.Save(SelectedCategory, SelectedAnime = anime.DecrementWatched()),
                anime => anime != null && anime.CanDecrementWatched());

            TogglePinned = new RelayCommand<Anime>(
                anime => 
                { 
                    anime.Pinned = !anime.Pinned; 
                    cd.Anime.Save(SelectedCategory, anime); 
                    SelectedCategory = SelectedCategory; 
                },
                anime => anime != null);

            Func<cd.Categories, RelayCommand<Anime>> moveTo = target => 
                new RelayCommand<Anime>(anime =>
                {
                    cd.Anime.Move(SelectedCategory, target, anime);
                    SelectedCategory = SelectedCategory;
                }, 
                _ => SelectedCategory != target);

            MoveToPlanToWatch = moveTo(cd.Categories.PlanToWatch);
            MoveToWatching = moveTo(cd.Categories.Watching);
            MoveToCompleted = moveTo(cd.Categories.Completed);
            MoveToOnHold = moveTo(cd.Categories.OnHold);
            MoveToDropped = moveTo(cd.Categories.Dropped);
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;


        public void MoveAnimeToCategory(Anime anime, string categoryName)
        {
            cd.Categories targetCategory;
            if (!Enum.TryParse<cd.Categories>(categoryName, out targetCategory))
            {
                throw new InvalidOperationException("Unknown category " + categoryName);
            }

            cd.Anime.Move(SelectedCategory, targetCategory, anime);
            SelectedCategory = SelectedCategory;
            SelectedAnime = Animes.FirstOrDefault();
        }
    }
}
