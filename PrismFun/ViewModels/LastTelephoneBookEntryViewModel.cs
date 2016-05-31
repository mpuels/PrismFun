using System;
using Prism.Events;
using Prism.Mvvm;
using PrismFun.Events;

namespace PrismFun.ViewModels
{
    public class LastTelephoneBookEntryViewModel : BindableBase
    {
        public LastTelephoneBookEntryViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

            this.eventAggregator.GetEvent<InsertEvent>().Subscribe(OnInsertEvent);

            LastEntry = "EMPTY";
        }

        private void OnInsertEvent(string obj)
        {
            LastEntry = obj;
        }

        private IEventAggregator eventAggregator;

        private string lastEntry;

        public string LastEntry
        {
            get { return lastEntry; }
            set { SetProperty<string>(ref lastEntry, value); }
        }
    }
}
