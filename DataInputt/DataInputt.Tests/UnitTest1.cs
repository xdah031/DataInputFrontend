using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataInputt.Tests
{
    using System.Linq;

    using DataInputt.Models;

    [TestClass]
    public class PublicationSaveTests
    {
        [TestMethod]
        public void We_can_save_if_all_given_data_is_valid()
        {
            var sut = new MainWindowAccessor();
            sut.SetPublication(new Publication(){Name = "Karl"});
            sut.SelectMedia(new Medium { Name = "Peter" });

            sut.Save();

            var publications = sut.GetPublications();

            Assert.IsTrue(publications.Any(x => x.Name == "Karl"));
        }
    }
}
