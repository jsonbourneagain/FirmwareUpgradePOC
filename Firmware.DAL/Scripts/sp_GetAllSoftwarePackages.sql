CREATE PROCEDURE Inventory.usp_GetAllSoftwarePackages(@PageNo INT, @PageSize INT)
--@SwPkgUID UNIQUEIDENTIFIER OUT, @SwPkgVersion VARCHAR (100) OUT, @SwColorStandardID	INT OUT, @SwAddedDate DATETIME OUT, @SwFileName NVARCHAR (200) OUT, @SwFileSize VARCHAR (30) OUT 
AS BEGIN 
	SET NOCOUNT ON
	SET XACT_ABORT ON

	DECLARE @startPos INT
	DECLARE @endPos  INT

	SET @startPos = (@PageNo -1) * @PageSize + 1 
	SET @endPos = @PageNo * @PageSize

	BEGIN TRANSACTION

	SELECT SWPKG.SwPkgID, SWPKG.SwPkgUID , SWPKG.SwPkgVersion, SWPKG.SwColorStandardID, SWPKG.AddedDate, FD.FileName, FD.FileSize, FD.FileFormat
	  FROM Inventory.SoftwarePackage AS SWPKG
	  INNER JOIN 
	  Inventory.FileDetails AS FD 
	  ON SWPKG.SwPkgUID = FD.SwPkgUID

	  WHERE SWPKG.SwPkgID > @startPos AND SWPKG.SwPkgID < @endPos AND FD.FileFormat = 'bin'

	COMMIT
END