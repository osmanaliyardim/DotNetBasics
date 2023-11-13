namespace Serialization.Entities
{
    [Serializable]
    public class Employee
    {
        public string EmployeeName { get; set; }

        public Employee()
        {
            EmployeeName = string.Empty;
        }

        public Employee(string employeeName)
        {
            EmployeeName = employeeName;
        }
    }
}