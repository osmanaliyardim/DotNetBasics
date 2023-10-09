using HelloWorldStandard;

SalutingStandardClass salute = new SalutingStandardClass();

Console.WriteLine("Please, enter a name..");

string username = Console.ReadLine();

Console.WriteLine(salute.SaluteMe(username));