CREATE VIEW [dbo].[EmployeeInfo]
	AS SELECT Employee.Id AS EmployeeId, 
			  Person.FirstName + ' ' + Person.LastName AS EmployeeFullName,
			  Address.ZipCode + '_' + Address.State + ', ' + Address.City + '-' + Address.Street AS EmployeeFullAddress,
			  Employee.CompanyName + ' (' + Employee.Position + ')' AS EmployeeCompanyInfo
			  FROM Employee
		INNER JOIN Address
			ON Employee.AddressId = Address.Id
		INNER JOIN Person
			ON Employee.PersonId = Person.Id
		ORDER BY Employee.CompanyName, Address.City OFFSET 0 ROWS;