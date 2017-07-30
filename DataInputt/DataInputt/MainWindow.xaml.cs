using System.Collections.Generic;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace DataInputt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PublicationData data = new PublicationData();

        public MainWindow()
        {
            InitializeComponent();

            foreach (var dataType in data.types)
            {
                this.typ.Items.Add(dataType);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.id.Text))
            {
                object itemToDelete = null;
                foreach (var item in this.publikationen.Items)
                {
                    var publikation = (Publication) item;
                    if (publikation.Id == int.Parse(this.id.Text))
                    {
                        itemToDelete = publikation;
                    }
                }

                if (itemToDelete != null)
                {
                    this.publikationen.Items.Remove(itemToDelete);
                }
            }

            this.DatePicker.Text = "";
            this.Beschreibung.Text = "";
            this.id.Text = "";
            this.link.Text = "";
            this.name.Text = "";
            this.typ.Text = "";
            this.publisher.Text = "";
            this.medium.Text = "";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var publication = new Publication();

            publication.Id = GetId();

            publication.Link = this.link.Text;
            publication.Date = this.DatePicker.Text;
            publication.Description = this.Beschreibung.Text;
            publication.Name = this.name.Text;
            publication.Type = this.typ.Text;
            publication.Publisher = this.publisher.Text;
            publication.Medium = this.medium.Text;

            this.publikationen.Items.Add(publication);
        }

        private int GetId()
        {
            if (!string.IsNullOrEmpty(this.id.Text))
            {
                return int.Parse(this.id.Text);
            }

            int highestId = 0;
            foreach (var item in this.publikationen.Items)
            {
                var publikation = (Publication)item;
                if (highestId < publikation.Id)
                {
                    highestId = publikation.Id;
                }
            }

            return ++highestId;
        }

        private void publikationen_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var publication = (Publication)this.publikationen.SelectedItem;

            if (publication == null)
            {
                return;
            }

            this.link.Text = publication.Link;
            this.DatePicker.Text = publication.Date;
            this.Beschreibung.Text = publication.Description;
            this.name.Text = publication.Name;
            this.typ.Text = publication.Type;
            this.publisher.Text = publication.Publisher;
            this.medium.Text = publication.Medium;
            this.id.Text = publication.Id.ToString();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var publicationData = new PublicationData();
            publicationData.Publications = new List<Publication>();

            foreach (var item in this.publikationen.Items)
            {
                var publikation = (Publication) item;

                publicationData.Publications.Add(publikation);
            }

            var saveFileDialog = new SaveFileDialog();
            var showDialog = saveFileDialog.ShowDialog(this);
            saveFileDialog.DefaultExt = ".json";

            if (showDialog == true)
            {
                string json = JsonConvert.SerializeObject(publicationData);
                var file = saveFileDialog.OpenFile();

                using (StreamWriter outputFile = new StreamWriter(file))
                {
                    outputFile.WriteLine(json);
                }
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            var showDialog = openFileDialog.ShowDialog();
            if (showDialog == true)
            {
                var readAllText = File.ReadAllText(openFileDialog.FileName);
                var publicationData = JsonConvert.DeserializeObject<PublicationData>(readAllText);

                foreach (var publication in publicationData.Publications)
                {
                    this.publikationen.Items.Add(publication);
                }
            }
        }
    }
}
