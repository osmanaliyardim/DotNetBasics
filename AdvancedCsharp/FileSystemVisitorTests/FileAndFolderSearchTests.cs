using FileSystemVisitor;

namespace FileSystemVisitorTests
{
    public class FileAndFolderSearchTests
    {
        private readonly string _testFolder;

        public FileAndFolderSearchTests()
        {
            _testFolder = Path.Combine(Path.GetTempPath(), "TestFolder");
            Directory.CreateDirectory(_testFolder);

            Directory.CreateDirectory(Path.Combine(_testFolder, "folderAndrew"));
            Directory.CreateDirectory(Path.Combine(_testFolder, "folderJack"));

            File.WriteAllText(Path.Combine(_testFolder, "textFile1.txt"), "test");
            File.WriteAllText(Path.Combine(_testFolder, "textFile2.txt"), "test");
            File.WriteAllText(Path.Combine(_testFolder, "folderAndrew", "textFile3.txt"), "test");
            File.WriteAllText(Path.Combine(_testFolder, "folderJack", "textFile4.txt"), "test");
            File.WriteAllText(Path.Combine(_testFolder, "image1.jpg"), "test");
            File.WriteAllText(Path.Combine(_testFolder, "folderAndrew", "image2.png"), "test");
            File.WriteAllText(Path.Combine(_testFolder, "folderJack", "image3.jpg"), "test");
        }

        [Fact]
        public void Search_FilesAndFolders_ReturnsAllFilesAndFolders()
        {
            FSVisitor fileSystemVisitor = new FSVisitor(_testFolder);
            List<string> actualFiles = fileSystemVisitor.SearchFiles().ToList();

            List<string> expectedFiles = new List<string> {
                    Path.Combine(_testFolder, "image1.jpg"),
                    Path.Combine(_testFolder, "textFile1.txt"),
                    Path.Combine(_testFolder, "textFile2.txt"),
                    Path.Combine(_testFolder, "folderAndrew", "image2.png"),
                    Path.Combine(_testFolder, "folderAndrew", "textFile3.txt"),
                    Path.Combine(_testFolder, "folderJack", "image3.jpg"),
                    Path.Combine(_testFolder, "folderJack", "textFile4.txt")
                };

            Assert.Equal(expectedFiles, actualFiles);
        }

        [Fact]
        public void Search_FilteredFilesAndFolders_ReturnsFilteredFilesAndFolders()
        {
            FSVisitor fileSystemVisitor = new FSVisitor(_testFolder, false, (x => Path.GetExtension(x) == ".txt"));
            List<string> actualFiles = fileSystemVisitor.SearchFiles().ToList();

            List<string> expectedFiles = new List<string> {
                    Path.Combine(_testFolder, "textFile1.txt"),
                    Path.Combine(_testFolder, "textFile2.txt"),
                    Path.Combine(_testFolder, "folderAndrew", "textFile3.txt"),
                    Path.Combine(_testFolder, "folderJack", "textFile4.txt")
                };

            Assert.Equal(expectedFiles, actualFiles);
        }

        [Fact]
        public void Search_FilesAndFoldersWithExclude_ReturnsFilesAndFoldersWithoutExcludes()
        {
            FSVisitor fileSystemVisitor = new FSVisitor(_testFolder, false, null, (excludeItem => Path.GetFileNameWithoutExtension(excludeItem) == "textFile3"));
            List<string> actualFiles = fileSystemVisitor.SearchFiles().ToList();

            List<string> expectedFiles = new List<string> {
                    Path.Combine(_testFolder, "image1.jpg"),
                    Path.Combine(_testFolder, "textFile1.txt"),
                    Path.Combine(_testFolder, "textFile2.txt"),
                    Path.Combine(_testFolder, "folderAndrew", "image2.png"),
                    Path.Combine(_testFolder, "folderJack", "image3.jpg"),
                    Path.Combine(_testFolder, "folderJack", "textFile4.txt")
                };

            Assert.Equal(expectedFiles, actualFiles);
        }
    }
}