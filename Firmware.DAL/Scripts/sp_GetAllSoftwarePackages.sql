CREATE PROCEDURE Inventory.usp_GetAllSoftwarePackages(@PageNo INT, @PageSize INT)

AS BEGIN 
	SET NOCOUNT ON
	SET XACT_ABORT ON

	
	SELECT SWPKG.SwPkgUID , SWPKG.SwPkgVersion, SWPKG.SwColorStandardID, SWPKG.AddedDate, SWPKG.Manufacturer, SWPKG.DeviceType ,FD.FileName, FD.FileSize, FD.FileFormat
	  FROM Inventory.SoftwarePackage AS SWPKG
	  INNER JOIN 
	  Inventory.FileDetails AS FD 
	  ON SWPKG.SwPkgUID = FD.SwPkgUID
	  WHERE FD.FileFormat = 'bin' AND SWPKG.IsDeleted = 0
	  ORDER BY SWPKG.AddedDate desc 
      OFFSET @PageSize * (@PageNo - 1) ROWS
      FETCH NEXT @PageSize ROWS ONLY;

	  SELECT COUNT(SWPKG.SwPkgUID) AS TotalRecords
	  FROM Inventory.SoftwarePackage AS SWPKG
	  INNER JOIN 
	  Inventory.FileDetails AS FD 
	  ON SWPKG.SwPkgUID = FD.SwPkgUID
	  WHERE FD.FileFormat = 'bin' AND SWPKG.IsDeleted = 0

	  SELECT SWPKG.SwPkgUID, FD.FileName
	  FROM Inventory.SoftwarePackage AS SWPKG
	  INNER JOIN 
	  Inventory.FileDetails AS FD 
	  ON SWPKG.SwPkgUID = FD.SwPkgUID 
	  WHERE FD.FileFormat = 'pdf'

	  SELECT SM.SwPkgUID, SM.DeviceModelName FROM Inventory.SwPackageModelMap SM
	  INNER JOIN
	  Inventory.SoftwarePackage SP
	  ON
	  SM.SwPkgUID = SP.SwPkgUID

END