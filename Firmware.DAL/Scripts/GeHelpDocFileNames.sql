

CREATE PROCEDURE [Inventory].[usp_GetHelpDocFileNames]
AS BEGIN 
	SET NOCOUNT ON
	SET XACT_ABORT ON

	SELECT SWPKG.SwPkgUID, FD.FileName
	  FROM Inventory.SoftwarePackage AS SWPKG
	  INNER JOIN 
	  Inventory.FileDetails AS FD 
	  ON SWPKG.SwPkgUID = FD.SwPkgUID 
	  WHERE FD.FileFormat = 'pdf'

END
GO


