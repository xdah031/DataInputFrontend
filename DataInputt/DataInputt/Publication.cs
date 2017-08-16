using System.ComponentModel;
using System.Runtime.CompilerServices;
using DataInputt.Annotations;

namespace DataInputt
{
    public class Publication : INotifyPropertyChanged
    {
        private string date;
        private string name;
        private string description;
        private string link;
        private string type;
        private string medium;
        private string publisher;
        private string publisherUrl;
        private int id;

        public string Date
        {
            get { return date; }
            set
            {
                if (value == date) return;
                date = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (value == name) return;
                name = value;
                OnPropertyChanged();
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                if (value == description) return;
                description = value;
                OnPropertyChanged();
            }
        }

        public string Link
        {
            get { return link; }
            set
            {
                if (value == link) return;
                link = value;
                OnPropertyChanged();
            }
        }

        public string Type
        {
            get { return type; }
            set
            {
                if (value == type) return;
                type = value;
                OnPropertyChanged();
            }
        }

        public string Medium
        {
            get { return medium; }
            set
            {
                if (value == medium) return;
                medium = value;
                OnPropertyChanged();
            }
        }

        public string Publisher
        {
            get { return publisher; }
            set
            {
                if (value == publisher) return;
                publisher = value;
                OnPropertyChanged();
            }
        }

        public string PublisherUrl
        {
            get { return publisherUrl; }
            set
            {
                if (value == publisherUrl) return;
                publisherUrl = value;
                OnPropertyChanged();
            }
        }

        public int Id
        {
            get { return id; }
            set
            {
                if (value == id) return;
                id = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}