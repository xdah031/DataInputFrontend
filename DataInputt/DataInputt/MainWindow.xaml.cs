using System.Collections.Generic;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.Windows.Controls;
using DataInputt.Models;
using System;

namespace DataInputt
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Delete delete;
        ItemCollection x = new ListView().Items;
        ItemCollection j = new ListView().Items;
        PublicationData data = new PublicationData();

        public MainWindow()
        {
            InitializeComponent();
            this.delete = Delete.GetInstance();
            delete.SomethingDeleted += Delete_SomethingDeleted;
            foreach (var dataType in data.types)
            {
                this.typ.Items.Add(dataType);
            }
            //Publication publication1 = new Publication()
            //{
            //    Id = 1,
            //    Name = "Publikation1",
            //    Description = "My first publication",
            //    Date = "02.08.2017",
            //    Type = "Workshop",
            //    MediumId = 1
            //};
            //Publication publication2 = new Publication()
            //{
            //    Id = 2,
            //    Name = "Publikation2",
            //    Description = "My second publication",
            //    Date = "03.08.2017",
            //    Type = "Usergroup",
            //    MediumId = 2
            //};
            //publikationen.Items.Add(publication1);
            //publikationen.Items.Add(publication2);
        }

        private void Delete_SomethingDeleted(object sender, DeleteEventArgs e)
        {
            if(sender.GetType() == typeof(Page1))
            {
                if (e.Object is Medium medi)
                {
                    for(int i = 0; i < publikationen.Items.Count; i++)
                    {
                        var item = publikationen.Items[i];
                        if (((Publication)item).MediumId == medi.Id)
                        {
                            int j = publikationen.Items.IndexOf(item);
                            Publication ppub = new Publication();
                            ppub.Id = ((Publication)item).Id;
                            ppub.Name = ((Publication)item).Name;
                            ppub.Description = ((Publication)item).Description;
                            ppub.Date = ((Publication)item).Date;
                            ppub.Link = ((Publication)item).Link;
                            ppub.Reviewed = ((Publication)item).Reviewed;
                            ppub.Type = ((Publication)item).Type;
                            publikationen.Items.RemoveAt(j);
                            publikationen.Items.Insert(j, ppub);
                        }
                    }
                }
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
            this.medium.SelectedIndex = -1;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var publication = new Publication();

            if (!string.IsNullOrEmpty(this.id.Text))
            {
                var id = int.Parse(this.id.Text);
                Publication foundPublication = null;
                foreach (Publication item in this.publikationen.Items)
                {
                    if (item.Id == id)
                    {
                        foundPublication = item;
                    }
                }

                foundPublication.Link = this.link.Text;
                foundPublication.Date = this.DatePicker.Text;
                foundPublication.Description = this.Beschreibung.Text;
                foundPublication.Name = this.name.Text;
                foundPublication.Type = this.typ.Text;
                foundPublication.MediumId = ((Medium) this.medium.SelectedItem).Id;
            }
            else
            {
                publication.Id = GetId();

                publication.Link = this.link.Text;
                publication.Date = this.DatePicker.Text;
                publication.Description = this.Beschreibung.Text;
                publication.Name = this.name.Text;
                publication.Type = this.typ.Text;
                publication.MediumId = ((Medium) this.medium.SelectedItem).Id;

                this.publikationen.Items.Add(publication);
            }
            this.Beschreibung.Text = "";
            this.DatePicker.SelectedDate = null;
            this.link.Text = "";
            this.medium.SelectedIndex = -1;
            this.name.Text = "";
            this.typ.SelectedIndex = -1;
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
            Medium medi = null;
            foreach(var item in this.medium.Items)
            {
                if (((Medium)item).Id == publication.MediumId)
                {
                    medi = (Medium)item;
                    break;
                }
            }
            this.medium.SelectedItem = medi != null ? medi : null;
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
            saveFileDialog.DefaultExt = "";

            if (showDialog == true)
            {
                string json = JsonConvert.SerializeObject(publicationData, Formatting.Indented);
                MediaRepoInstance mri = new MediaRepoInstance();
                mri.Media = MediaRepo.Media;
                string json2 = JsonConvert.SerializeObject(mri, Formatting.Indented);
                PublisherRepoInstance pri = new PublisherRepoInstance();
                pri.Publisher = PublisherRepo.Publisher;
                string json3 = JsonConvert.SerializeObject(pri, Formatting.Indented);
                ProjectRepoInstance prori = new ProjectRepoInstance();
                prori.Projects = ProjectRepo.Projects;
                string json4 = JsonConvert.SerializeObject(prori, Formatting.Indented);
                string directoryPath = Path.GetDirectoryName(saveFileDialog.FileName);
                string folderName = Path.GetFileNameWithoutExtension(saveFileDialog.FileName);
                Directory.CreateDirectory(directoryPath + "\\" + folderName);
                File.WriteAllText(directoryPath + "\\" + folderName + "\\publications.json", json);
                File.WriteAllText(directoryPath + "\\" + folderName + "\\media.json", json2);
                File.WriteAllText(directoryPath + "\\" + folderName + "\\publisher.json", json3);
                File.WriteAllText(directoryPath + "\\" + folderName + "\\projects.json", json4);
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            var showDialog = openFileDialog.ShowDialog();
            if (showDialog == true)
            {
                var json = File.ReadAllText(openFileDialog.FileName);
                if (json.Contains("\"Publications\": ["))
                {
                    if (json.Contains("\"PublisherUrl\":"))
                    {
                        var publicationData = JsonConvert.DeserializeObject<OldPublicationData>(json);

                        MediaRepo.Media.Clear();
                        PublisherRepo.Publisher.Clear();
                        this.publikationen.Items.Clear();

                        foreach (var publication in publicationData.Publications)
                        {
                            Publisher publ = new Publisher();
                            publ.Id = PublisherRepo.Publisher.Count + 1;
                            publ.Name = publication.Publisher;
                            publ.Link = publication.PublisherUrl;
                            PublisherRepo.Publisher.Add(publ);

                            Medium med = new Medium();
                            med.Id = MediaRepo.Media.Count + 1;
                            med.Name = publication.Medium;
                            med.PublisherId = publ.Id;
                            MediaRepo.Media.Add(med);

                            Publication pub = new Publication();
                            pub.Id = publication.Id;
                            pub.Name = publication.Name;
                            pub.Description = publication.Description;
                            pub.Date = publication.Date;
                            pub.Link = publication.Link;
                            pub.Reviewed = false;
                            pub.Type = publication.Type;
                            pub.MediumId = med.Id;

                            this.publikationen.Items.Add(pub);

                            PublisherRepo.OnPublisherCollectionImport();
                            MediaRepo.OnMediaCollectionImport();
                        }
                    }
                    else
                    {
                        var publicationData = JsonConvert.DeserializeObject<PublicationData>(json);

                        foreach (var publication in publicationData.Publications)
                        {
                            this.publikationen.Items.Add(publication);
                        }
                    }
                }
                else if (json.Contains("\"Publisher\": ["))
                {
                    var publicationData = JsonConvert.DeserializeObject<PublisherRepoInstance>(json);
                    PublisherRepo.Publisher = publicationData.Publisher;
                    PublisherRepo.OnPublisherCollectionImport();
                }
                else if (json.Contains("\"Media\": ["))
                {
                    var publicationData = JsonConvert.DeserializeObject<MediaRepoInstance>(json);
                    MediaRepo.Media = publicationData.Media;
                    MediaRepo.OnMediaCollectionImport();
                }
                else if (json.Contains("\"Projects\": ["))
                {
                    var publicationData = JsonConvert.DeserializeObject<ProjectRepoInstance>(json);
                    ProjectRepo.Projects = publicationData.Projects;
                    ProjectRepo.OnProjectCollectionImport();
                }
            }
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            this.DatePicker.Text = "";
            this.Beschreibung.Text = "";
            this.id.Text = "";
            this.link.Text = "";
            this.name.Text = "";
            this.typ.Text = "";
            this.medium.SelectedIndex = -1;
        }

        private void tabCtrl_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (e.OriginalSource.GetType() == typeof(ComboBox) || e.OriginalSource.GetType() == typeof(ListView))
                return;

            if (e.RemovedItems.Count > 0 && (string)((TabItem)e.RemovedItems[0]).Header == "Publisher")
            {
                x = ((ListView)((Grid)((Grid)((Grid)((PublisherPage)((Frame)((TabItem)e.RemovedItems[0]).Content).Content).Content).Children[1]).Parent).Children[0]).Items;
            }
            if (e.RemovedItems.Count > 0 && (string)((TabItem)e.RemovedItems[0]).Header == "Media")
            {
                j = ((ListView)((StackPanel)((Page1)((Frame)((TabItem)e.RemovedItems[0]).Content).Content).Content).Children[0]).Items;
            }
            if (e.AddedItems.Count > 0 && 5 == 5 && (string)((TabItem) e.AddedItems[0]).Header == "Media")
            {
                ((ComboBox)((StackPanel)((StackPanel)((Page1)((Frame)((TabItem)e.AddedItems[0]).Content).Content).Content).Children[3]).Children[1]).ItemsSource = x;
            }
            if (e.AddedItems.Count > 0 && (string)((TabItem)e.AddedItems[0]).Header == "Publikationen")
            {
                ((ComboBox)((Grid)((TabItem)e.AddedItems[0]).Content).Children[12]).ItemsSource = j;
            }
        }
    }
}
