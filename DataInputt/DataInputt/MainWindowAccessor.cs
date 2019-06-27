namespace DataInputt.Tests
{
    using System.Collections;
    using System.Collections.Generic;

    using DataInputt.Models;

    public class MainWindowAccessor : MainWindow
    {
        public void Save()
        {
            base.Save(null, null);
        }

        public void SetPublication(Publication publication)
        {
            this.name.Text = publication.Name;
        }

        public void SelectMedia(Medium medium)
        {
            this.medium.Items.Add(medium);
            this.medium.SelectedItem = medium;
        }

        public IEnumerable<Publication> GetPublications()
        {
            var publications = new List<Publication>();
            foreach (Publication publikationenItem in this.publikationen.Items)
            {
                publications.Add(publikationenItem);
            }

            return publications;
        }
    }
}