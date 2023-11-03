CREATE PROCEDURE [dbo].[SP_InsertEmployeeInfo]
	@EmployeeName nvarchar(100) = NULL,
	@FirstName nvarchar(50) = NULL,
	@LastName nvarchar(50) = NULL,
	@CompanyName nvarchar(20) = NULL,
	@Position nvarchar(30) = NULL,
	@Street nvarchar(50) = NULL,
	@City nvarchar(20) = NULL,
	@State nvarchar(50) = NULL,
	@ZipCode nvarchar(50) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    --Check that at least one of them is not empty or only spaces
    IF (LEN(RTRIM(COALESCE(@EmployeeName, ''))) = 0 AND LEN(RTRIM(COALESCE(@FirstName, ''))) = 0 AND LEN(RTRIM(COALESCE(@LastName, ''))) = 0)
    BEGIN
        RAISERROR('At least one name field must have a value', 16, 1);
    END;

    --Truncate CompanyName if it is longer than 20 chars
    SET @CompanyName = LEFT(@CompanyName, 20);

    --Insert the data into Employee and Address tables
    DECLARE @AddressId int;
    INSERT INTO Address (Street, City, State, ZipCode)
    VALUES (@Street, @City, @State, @ZipCode)
    SET @AddressId = SCOPE_IDENTITY();

    DECLARE @PersonId int;
    INSERT INTO Person (FirstName, LastName)
    VALUES (@FirstName, @LastName)
    SET @PersonId = SCOPE_IDENTITY();

    INSERT INTO Employee (AddressId, PersonId, CompanyName, Position, EmployeeName)
    VALUES (@AddressId, @PersonId, @CompanyName, @Position, @EmployeeName);
END