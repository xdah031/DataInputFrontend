namespace DataInputt
{
    using System;
    using System.Collections.Generic;

    using DataInputt.Models;

    public interface IProjectRepoSingleton
    {
        event ProjectRepo.ProjectsCollectionImportedEventHandler ProjectCollectionImported;

        List<Project> Projects { get; set; }

        void OnProjectCollectionImport();
    }

    public class ProjectRepo : IProjectRepoSingleton
    {
        private static ProjectRepo _instance;

        private ProjectRepo()
        {
        }

        public delegate void ProjectsCollectionImportedEventHandler(object sender, EventArgs e);

        public event ProjectsCollectionImportedEventHandler ProjectCollectionImported;

        public List<Project> Projects { get; set; }

        public static IProjectRepoSingleton Instance => _instance ?? (_instance = new ProjectRepo());

        public void OnProjectCollectionImport()
        {
            ProjectCollectionImported(Projects, new EventArgs());
        }
    }
}