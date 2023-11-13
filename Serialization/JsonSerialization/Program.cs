using System.Text.Json;
using Serialization.Entities;

Department department = new Department("IT");

Employee emp1 = new Employee("Osman");
Employee emp2 = new Employee("Ali");

department.AddEmployee(emp1);
department.AddEmployee(emp2);

//Serialize
string fileName = "JSONSerialization.json";
var options = new JsonSerializerOptions { WriteIndented = true, IncludeFields = true, };
string jsonString = JsonSerializer.Serialize<Department>(department, options);
File.WriteAllText(fileName, jsonString);

Console.WriteLine("Serialize:\n" + File.ReadAllText(fileName));

//Deserialize
Department? deserializedDepartment = JsonSerializer.Deserialize<Department>(File.ReadAllText(fileName), options);

Console.WriteLine("Deserialize:\n" + deserializedDepartment);

Console.ReadLine();