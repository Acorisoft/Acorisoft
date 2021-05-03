using System;
using System.IO;
using LibGit2Sharp;
using LibGit2Sharp.Handlers;

namespace WPF.Git
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = Repository.Init(@"D:\Repo\Test\Repo1");
            var repository = new Repository(path);
            //var content = "Commit this!";
            //File.WriteAllText(Path.Combine(repository.Info.WorkingDirectory, "fileToCommit.txt"), content);

            //// Stage the file
            //repository.Index.Add("fileToCommit.txt");
            //repository.Index.Write();

            //// Create the committer's signature and commit
            //Signature author = new Signature("James", "@jugglingnutcase", DateTime.Now);
            //Signature committer = author;

            //// Commit to the repository
            //Commit commit = repository.Commit("Here's a commit i made!", author, committer);
            var dir = repository.Info.WorkingDirectory;
            var status = repository.RetrieveStatus();
            repository.Dispose();
        }

        static void StashAll(Repository repository)
        {
            

        }
    }
}
