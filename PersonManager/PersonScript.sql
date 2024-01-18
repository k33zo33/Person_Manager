CREATE TABLE Person
(
	IDPerson int primary key identity,
	FirstName nvarchar(20) not null,
	LastName nvarchar(20) not null,
	Age int not null,
	Email nvarchar(30) not null,
	Picture varbinary(max) null
)
GO

CREATE PROC GetPeople
AS
SELECT * FROM Person
GO

CREATE PROC GetPerson
	@idPerson int
AS
SELECT * FROM Person WHERE IDPerson = @idPerson
GO

CREATE PROC DeletePerson
	@idPerson int
AS
DELETE FROM Person WHERE IDPerson = @idPerson
GO

CREATE PROC AddPerson
	@firstname nvarchar(20),
	@lastname nvarchar(20),
	@age int,
	@email nvarchar(30),
	@picture varbinary(max),
	@idPerson INT OUTPUT
AS 
BEGIN
INSERT INTO Person VALUES (@firstname, @lastname, @age, @email, @picture)
	SET @idPerson = SCOPE_IDENTITY()
END
GO

CREATE PROC UpdatePerson
	@firstname nvarchar(20),
	@lastname nvarchar(20),
	@age int,
	@email nvarchar(30),
	@picture varbinary(max),
	@idPerson int
AS
UPDATE Person SET 
		FirstName = @firstname,
		LastName = @lastname,
		Age = @age,
		Email = @email,
		Picture = @picture
	WHERE 
		IDPerson = @idPerson