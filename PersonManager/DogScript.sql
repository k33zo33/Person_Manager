-- Create Dog table
create table Dogs
(
    IDDog int primary key identity,
    Name nvarchar(20) not null,
    Age int not null,
    Breed nvarchar(50) not null,
    DogPicture varbinary(max) null,
    PersonID int,
    foreign key (PersonID) references Person(IDPerson) on delete cascade
)
go


CREATE PROC GetDogs
AS
SELECT * FROM Dogs
GO

-- Get all dogs for a single person
drop procedure if exists GetDogsForPerson
go

create proc GetDogsForPerson
    @personID int
as
select * from Dogs where PersonID = @personID
go

-- Add a new dog
drop procedure if exists AddDog
go

create proc AddDog
    @name nvarchar(20),
    @age int,
    @breed nvarchar(50),
    @dogPicture varbinary(max),
    @personID int,
    @idDog int output
as 
begin
    insert into Dogs (Name, Age, Breed, DogPicture, PersonID) values (@name, @age, @breed, @dogPicture, @personID)
    set @idDog = SCOPE_IDENTITY()
end
go

-- Update dog information
drop procedure if exists UpdateDog
go

create proc UpdateDog
    @name nvarchar(20),
    @age int,
    @breed nvarchar(50),
    @dogPicture varbinary(max),
    @personID int,
	@idDog int
as 
update Dogs SET 
        Name = @name,
        Age = @age,
        Breed = @breed,
        DogPicture = @dogPicture,
        PersonID = @personID
where 
    IDDog = @idDog
go

-- Delete a dog
drop procedure if exists DeleteDog
go

create proc DeleteDog
    @dogID int
as
delete from Dogs where IDDog = @dogID
go
