CREATE FUNCTION Inventory.CheckExistingSwPackage(
				@FileName NVARCHAR(200),
				@SwPkgVersion VARCHAR(100)
) RETURNS INT
AS 
BEGIN 
	DECLARE @maxVersion INT;

	SELECT @maxVersion = (
				SELECT Max(SWPKG.SwVersion) FROM Inventory.SoftwarePackage AS SWPKG
				INNER JOIN
				Inventory.FileDetails AS FD
				ON SWPKG.SwPkgUID = FD.SwPkgUID
				WHERE SWPKG.SwPkgVersion = @SwPkgVersion AND FD.FileName = @FileName
	)

	RETURN @maxVersion
END
