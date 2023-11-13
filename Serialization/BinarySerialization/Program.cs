using System.Text;
using Serialization.Entities;

Department department = new Department("IT");

Employee emp1 = new Employee("Osman");
Employee emp2 = new Employee("Ali");

department.AddEmployee(emp1);
department.AddEmployee(emp2);

//Serialize
using (var stream = new FileStream("Department.bin", FileMode.Create, FileAccess.Write, FileShare.None))
{
    using (var writer = new BinaryWriter(stream, Encoding.UTF8, false))
    {
        writer.Write(department.DepartmentName);
        foreach (Employee item in department.Employees)
        {
            writer.Write(item.EmployeeName);
        }
    }
}

//Desiralize
Department? deserializedDepartment;
using (var streamRead = new FileStream("Department.bin", FileMode.Open, FileAccess.Read, FileShare.None))
{
    using (var reader = new BinaryReader(streamRead))
    {
        deserializedDepartment = new Department(reader.ReadString());
        while (reader.BaseStream.Position != reader.BaseStream.Length)
        {
            Employee emp = new Employee(reader.ReadString());
            deserializedDepartment.AddEmployee(emp);
        }
    }
}

Console.WriteLine("Deserialize:\n" + deserializedDepartment);
Console.ReadKey();