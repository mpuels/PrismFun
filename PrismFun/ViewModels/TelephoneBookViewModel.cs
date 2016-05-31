using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using PrismFun.Events;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace PrismFun.ViewModels
{
    public class TelephoneBookViewModel : BindableBase
    {

        #region constructors
        public TelephoneBookViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            vorname = "Marc";
            nachname = "Püls";
            telefonnummer = "0176 40328486";

            eintraege = beispieleintraege();

            Eintraege = new ListCollectionView(eintraege);
            Eintraege.CurrentChanged += selectedItemChanged;

            SubmitCommand = new DelegateCommand<object>(onSubmit, canSubmit)
                                .ObservesProperty(() => Vorname)
                                .ObservesProperty(() => Nachname)
                                .ObservesProperty(() => Telefonnummer);
        }
        #endregion

        #region public properties
        public string Vorname
        {
            get { return vorname; }
            set
            {
                SetProperty(ref vorname, value);
                OnPropertyChanged(() => Initialen);
            }
        }

        public string Nachname
        {
            get { return nachname; }
            set
            {
                SetProperty(ref nachname, value);
                OnPropertyChanged(() => Initialen);
            }
        }

        public string Telefonnummer
        {
            get { return telefonnummer; }
            set { SetProperty(ref telefonnummer, value); }
        }

        public string Initialen
        {
            get
            {
                if (vorname.Length > 0 && nachname.Length > 0)
                {
                    return String.Format("{0}. {1}.",
                                         vorname.Substring(0, 1),
                                         nachname.Substring(0, 1));
                }
                else
                {
                    return string.Empty;
                }

            }
        }

        public ICollectionView Eintraege { get; private set; }
        #endregion

        #region public commands
        public ICommand SubmitCommand { get; private set; }
        #endregion

        #region private command handlers
        private void onSubmit(object arg)
        {
            var eintrag = new Telefonbucheintrag()
            {
                Vorname = this.Vorname,
                Nachname = this.Nachname,
                Telefonnummer = this.Telefonnummer,
            };

            eintraege.Add(eintrag);
            eventAggregator.GetEvent<InsertEvent>().Publish(eintrag.ToString());
        }

        private bool canSubmit(object arg)
        {
            return startsWithCapitalLetter(Vorname)
                && startsWithCapitalLetter(Nachname)
                && startsWithZero(Telefonnummer);
        }
        #endregion

        #region private fields
        private IEventAggregator eventAggregator;

        private string vorname;
        private string nachname;
        private string telefonnummer;
        private ObservableCollection<Telefonbucheintrag> eintraege;
        #endregion

        #region private helpers
        private void selectedItemChanged(object sender, EventArgs e)
        {
            Telefonbucheintrag eintrag =
                Eintraege.CurrentItem as Telefonbucheintrag;

            Vorname = eintrag.Vorname;
            Nachname = eintrag.Nachname;
            Telefonnummer = eintrag.Telefonnummer;
        }

        private ObservableCollection<Telefonbucheintrag> beispieleintraege()
        {
            var beispieldaten = new ObservableCollection<Telefonbucheintrag>();

            beispieldaten.Add(new Telefonbucheintrag()
            {
                Vorname = "Marc",
                Nachname = "Püls",
                Telefonnummer = "0176 40328487"
            });

            beispieldaten.Add(new Telefonbucheintrag()
            {
                Vorname = "Sylvia",
                Nachname = "Püls",
                Telefonnummer = "0176 40328487"
            });

            beispieldaten.Add(new Telefonbucheintrag()
            {
                Vorname = "Carmen",
                Nachname = "Püls",
                Telefonnummer = "0176 40234677"
            });

            beispieldaten.Add(new Telefonbucheintrag()
            {
                Vorname = "Anni",
                Nachname = "Püls",
                Telefonnummer = "0176 12983287"
            });

            beispieldaten.Add(new Telefonbucheintrag()
            {
                Vorname = "Walter",
                Nachname = "Püls",
                Telefonnummer = "0134 12983287"
            });

            beispieldaten.Add(new Telefonbucheintrag()
            {
                Vorname = "Rob",
                Nachname = "Brinded",
                Telefonnummer = "0134 12983287"
            });

            return beispieldaten;
        }

        private bool startsWithCapitalLetter(string s)
        {
            return s.Length > 0 && char.IsUpper(s[0]);
        }

        private bool startsWithZero(string s)
        {
            return s.Length > 0 && s[0] == '0';
        }
        #endregion
    }
}
