using System.Xml.Serialization;
using Serialization.Entities;

Department department = new Department("IT");

Employee emp1 = new Employee("Osman");
Employee emp2 = new Employee("Ali");

department.AddEmployee(emp1);
department.AddEmployee(emp2);

Type[] extraTypes = new Type[1];
extraTypes[0] = typeof(Employee);

XmlSerializer xmlSerializer = new XmlSerializer(typeof(Department), extraTypes);
TextWriter writer = new StreamWriter("XMLSerialization.xml");

//Serialize
xmlSerializer.Serialize(writer, department);

//Deserialize,
Department deserializedDepartment;
using (Stream reader = new FileStream("XMLSerialization.xml", FileMode.Open))
{
    deserializedDepartment = (Department)xmlSerializer.Deserialize(reader);
}

Console.WriteLine("Deserialize:\n" + deserializedDepartment);
Console.ReadKey();