CREATE TABLE Vaccination
(
	IDVaccination int primary key identity,
	VaccinationDate Date,
	Manufacturer nvarchar(20) not null,
	ManufacturerPicture varbinary(max) null,
	PersonID int
	FOREIGN KEY (PersonID) REFERENCES Person(IDPerson) ON DELETE CASCADE
)
GO

CREATE PROC GetVaccinationsForPerson
	@personID int
AS
SELECT * FROM Vaccination where PersonID = @personID
GO


CREATE PROC DeleteVaccination
	@idVaccination int
AS
DELETE FROM Vaccination WHERE IDVaccination = @idVaccination
GO

CREATE PROC AddVaccination
	@vaccinationDate Date,
	@manufacturer nvarchar(20),
	@manufacturerPicture varbinary(max),
	@personID int,
	@idVaccination INT OUTPUT
AS 
BEGIN
INSERT INTO Vaccination VALUES (@vaccinationDate, @manufacturer, @manufacturerPicture, @personID)
	SET @idVaccination = SCOPE_IDENTITY()
END
GO

CREATE PROC UpdateVaccination
	@vaccinationDate Date,
	@manufacturer nvarchar(20),
	@manufacturerPicture varbinary(max),
	@personID int,
	@idVaccination int  
AS
UPDATE Vaccination SET 
		VaccinationDate = @vaccinationDate,
		Manufacturer = @manufacturer,
		ManufacturerPicture =@manufacturerPicture,
		PersonID = @personID
	WHERE 
		IDVaccination = @idVaccination
go

CREATE PROC GetVaccination
	@idVaccination int
AS
SELECT * FROM Vaccination where IDVaccination = @idVaccination
GO