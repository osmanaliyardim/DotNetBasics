--﻿CREATE TRIGGER [dbo].[TR_EmployeeAfterInsert]
--	ON [dbo].[Employee]
--	AFTER INSERT
--	AS
--	BEGIN

--		INSERT INTO Company (Name, AddressId)
--			SELECT inserted.CompanyName, inserted.AddressId 
--			FROM inserted
--	END