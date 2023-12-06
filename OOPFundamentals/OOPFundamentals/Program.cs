using System.Text.RegularExpressions;
using OOPFundamentals;
using OOPFundamentals.Entities;

Console.WriteLine("ses");

var patent = new Patent("Test Title", new List<string> { "Osman" }, new DateTime(2019, 7, 26), new DateTime(2019, 7, 26).AddMonths(5));
var localizedBook = new LocalizedBook("LBook", new List<string> { "Ali", "Osman" }, new DateTime(2019, 7, 26), 50, "EN", "DE", "TR");

var docRepository = new DocumentRepository();
docRepository.Write(patent);
docRepository.Write(localizedBook);

Regex regex = new Regex(@"./(.*?)_");
//var regex2 = Regex.Matches(repository.Search(1).First(),$"");
//var result = regex.Match(repository.Search(1).First()).Groups[1].Value;

Console.WriteLine(docRepository.Search(2).First());
Console.ReadKey();