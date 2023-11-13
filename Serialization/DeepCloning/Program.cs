using Serialization.Entities;

Department department = new Department("IT");

Employee emp1 = new Employee("Osman");
Employee emp2 = new Employee("Ali");

department.AddEmployee(emp1);
department.AddEmployee(emp2);

//Deep clone the object
Department deepCopiedDepartment = (Department)department.Clone();

//Change orginal object's properties
department.Employees[0].EmployeeName = "OsmanChanged";
department.DepartmentName = ".NET";

Console.WriteLine("Department: " + department);
Console.WriteLine("Deep Copied Department: " + deepCopiedDepartment);
Console.ReadKey();