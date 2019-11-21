CREATE PROCEDURE Inventory.usp_GetAllSoftwarePackages(@PageNo INT, @PageSize INT)

AS BEGIN 
	SET NOCOUNT ON
	SET XACT_ABORT ON

	
	SELECT SWPKG.SwPkgID, SWPKG.SwPkgUID , SWPKG.SwPkgVersion, SWPKG.SwColorStandardID, SWPKG.AddedDate, FD.FileName, FD.FileSize, FD.FileFormat
	  FROM Inventory.SoftwarePackage AS SWPKG
	  INNER JOIN 
	  Inventory.FileDetails AS FD 
	  ON SWPKG.SwPkgUID = FD.SwPkgUID
	  ORDER BY SWPKG.AddedDate desc 
      OFFSET @PageSize * (@PageNo - 1) ROWS
      FETCH NEXT @PageSize ROWS ONLY; 

END