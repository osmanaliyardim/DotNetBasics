using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml.Serialization;

namespace Serialization.Entities
{
    [Serializable]
    public class Department : ICloneable
    {
        public string DepartmentName { get; set; }

        public List<Employee> Employees;

        public Department()
        {
            Employees = new List<Employee>();
        }

        public Department(string departmentName)
        {
            DepartmentName = departmentName;
            Employees = new List<Employee>();
        }

        public void AddEmployee(Employee employee)
        {
            Employees.Add(employee);
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine();
            foreach (Employee e in Employees)
                stringBuilder.AppendLine("\t" + e.EmployeeName);
            return $"Department: Department Name: {DepartmentName} \nEmployees: {stringBuilder}";
        }

        public object Clone()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                if (this.GetType().IsSerializable)
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, this);
                    stream.Position = 0;
                    return formatter.Deserialize(stream);
                }
                return null;
            }
        }
    }
}