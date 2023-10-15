using FileSystemVisitor;

const string rootPath = @"c:\temp";

FSVisitor fileSystemVisitor =
    new(rootPath,
            false,
                (searchItem => Path.GetExtension(searchItem) == ".jpg"),
                    (excludeItem => Path.GetFileNameWithoutExtension(excludeItem) == "image2323"));

foreach (string path in fileSystemVisitor.SearchFiles())
{
    if (Path.HasExtension(path))
    {
        Console.WriteLine($"File location: {path}");
    }
    else Console.WriteLine($"Folder location: {path}");
}

Console.ReadLine();