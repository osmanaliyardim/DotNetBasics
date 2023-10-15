namespace FileSystemVisitor
{
    public class FSVisitor
    {
        private readonly string _rootFolder;
        private readonly Func<string, bool> _searchAlgorithm;
        private readonly bool _abortSearch;
        private readonly Func<string, bool> _excludeAlgorithm;

        public event SearchStarted StartSearch;
        public delegate void SearchStarted(FSVisitor sender, EventArgs e);

        public event SearchEnded EndSearch;
        public delegate void SearchEnded(FSVisitor sender, EventArgs e);

        public event FileFound StartFindFile;
        public delegate void FileFound(FSVisitor sender, EventArgs e);

        public event DirectoryFound StartFindDirectory;
        public delegate void DirectoryFound(FSVisitor sender, EventArgs e);

        public event FilteredFileFound StartFindFilteredFile;
        public delegate void FilteredFileFound(FSVisitor sender, EventArgs e);

        public event FilteredDirectoryFound StartFindFilteredDirectory;
        public delegate void FilteredDirectoryFound(FSVisitor sender, EventArgs e);


        public FSVisitor(string rootFolder, 
                            bool abortSearch = false, 
                                Func<string, bool> searchAlgorithm = null,
                                    Func<string, bool> excludeAlgorithm = null)
        {
            _rootFolder = rootFolder;
            _abortSearch = abortSearch;
            _searchAlgorithm = searchAlgorithm ?? (x => true);
            _excludeAlgorithm = excludeAlgorithm ?? (x => false);
        }

        public virtual void OnSearch(EventArgs e)
        {
            if (StartSearch != null)
                StartSearch(this, e);

            Console.WriteLine("Search operation has been started!");
        }

        public virtual void OnSearchEnd(EventArgs e)
        {
            if (EndSearch != null)
                EndSearch(this, e);

            Console.WriteLine("Search operation has been ended!");
        }

        public virtual void OnFileFound(EventArgs e, string fileName)
        {
            if (StartFindFile != null)
                StartFindFile(this, e);

            Console.WriteLine(fileName + " -> file found");
        }

        public virtual void OnFolderFound(EventArgs e, string directoryName)
        {
            if (StartFindDirectory != null)
                StartFindDirectory(this, e);

            Console.WriteLine(directoryName + " -> directory found");
        }

        public IEnumerable<string> SearchFiles()
        {
            OnSearch(new EventArgs());

            foreach (string file in TraverseDirectory(_rootFolder))
            {
                yield return file;
            }

            OnSearchEnd(new EventArgs());
        }

        // Traverse all files in the directory passed in, recurse on any directories
        // that are found, and process the files they contain.
        public IEnumerable<string> TraverseDirectory(string directory)
        {
            // Traverse & process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(directory);
            foreach (string fileName in fileEntries)
            {
                if (_abortSearch == false && _searchAlgorithm(fileName) && !(_excludeAlgorithm(fileName)))
                {
                    OnFileFound(new EventArgs(), Path.GetFileNameWithoutExtension(fileName));

                    yield return fileName;
                }
                else if (_abortSearch == true)
                {
                    yield return fileName;
                }
            }

            // Recurse into subdirectories of this directory.
            string[] subdirectoryEntries = Directory.GetDirectories(directory);
            foreach (string subdirectory in subdirectoryEntries)
            {
                OnFolderFound(new EventArgs(), subdirectory);

                foreach (var file in TraverseDirectory(subdirectory))
                {
                    if (_abortSearch == false && _searchAlgorithm(file))
                    {
                        OnFileFound(new EventArgs(), Path.GetFileNameWithoutExtension(file));

                        yield return file;
                    }
                    else if (_abortSearch == true)
                    {
                        yield return file;
                    }
                }
            }
        }
    }
}