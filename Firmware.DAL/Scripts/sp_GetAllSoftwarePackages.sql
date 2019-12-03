CREATE PROCEDURE Inventory.usp_GetAllSoftwarePackages(@PageNo INT, @PageSize INT, @SearchText VARCHAR(100), @SortColumn VARCHAR(50), @SortDirection VARCHAR(10))

AS BEGIN 
	SET NOCOUNT ON
	SET XACT_ABORT ON

	SELECT SWPKG.SwPkgUID , SWPKG.SwPkgVersion, SWPKG.SwColorStandardID, SWPKG.AddedDate, SWPKG.Manufacturer, SWPKG.DeviceType ,FD.FileName, FD.FileSize, FD.FileFormat
	  FROM Inventory.SoftwarePackage AS SWPKG
	  INNER JOIN Inventory.FileDetails AS FD 
	  ON SWPKG.SwPkgUID = FD.SwPkgUID

	  INNER JOIN Inventory.SwPackageModelMap SM
	  ON SM.SwPkgUID = SWPKG.SwPkgUID

	  WHERE FD.FileFormat = 'bin' AND SWPKG.IsDeleted = 0 AND ( FD.FileName LIKE '%' +@SearchText + '%'
	  OR SM.DeviceModelName LIKE '%' +@SearchText+ '%')

	  GROUP BY SWPKG.SwPkgUID , SWPKG.SwPkgVersion, SWPKG.SwColorStandardID, SWPKG.AddedDate, SWPKG.Manufacturer, SWPKG.DeviceType ,FD.FileName, FD.FileSize, FD.FileFormat
	  ORDER BY 
		CASE 
			 WHEN @SortDirection <> 'ASC' THEN '' 
			 WHEN @SortColumn =  'FILENAME' THEN FD.FileName 
		END ASC,
		CASE
			 WHEN @SortDirection <> 'ASC' THEN ''
			 WHEN @SortColumn =  'VERSION' THEN SWPKG.SwPkgVersion
		END ASC,
		CASE
			 WHEN @SortDirection <> 'ASC' THEN 0
			 WHEN @SortColumn =  'TYPE' THEN SWPKG.SwColorStandardID
		END ASC,
		CASE
			 WHEN @SortDirection <> 'ASC' THEN ''
			 WHEN @SortColumn =  'MANUFACTURER' THEN SWPKG.Manufacturer
		END ASC,
		CASE
			 WHEN @SortDirection <> 'ASC' THEN ''
			 WHEN @SortColumn =  'DEVICE' THEN SWPKG.DeviceType
		END ASC,
		CASE
			 WHEN @SortDirection <> 'ASC' THEN ''
			 WHEN @SortColumn =  'SIZE' THEN FD.FileSize
		END ASC,
		CASE
			 WHEN @SortDirection <> 'ASC' THEN CAST(NULL AS DATE)
			 WHEN @SortColumn =  'DATEADDED' THEN SWPKG.AddedDate
		END	 ASC,
		
		CASE 
			 WHEN @SortDirection <> 'DESC' THEN '' 
			 WHEN @SortColumn =  'FILENAME' THEN FD.FileName 
		END DESC,
		CASE
			 WHEN @SortDirection <> 'DESC' THEN ''
			 WHEN @SortColumn =  'VERSION' THEN SWPKG.SwPkgVersion
		END DESC,
		CASE
			 WHEN @SortDirection <> 'DESC' THEN 0
			 WHEN @SortColumn =  'TYPE' THEN SWPKG.SwColorStandardID
		END DESC,
		CASE
			 WHEN @SortDirection <> 'DESC' THEN ''
			 WHEN @SortColumn =  'MANUFACTURER' THEN SWPKG.Manufacturer
		END DESC,
		CASE
			 WHEN @SortDirection <> 'DESC' THEN ''
			 WHEN @SortColumn =  'DEVICE' THEN SWPKG.DeviceType
		END DESC,
		CASE
			 WHEN @SortDirection <> 'DESC' THEN ''
			 WHEN @SortColumn =  'SIZE' THEN FD.FileSize
		END DESC,
		CASE
			 WHEN @SortDirection <> 'DESC' THEN CAST(NULL AS DATE)
			 WHEN @SortColumn =  'DATEADDED' THEN SWPKG.AddedDate
		END	 DESC,
		CASE
			 WHEN @SortColumn =  'DEFAULT' THEN SWPKG.AddedDate
		END	 DESC

	  --DESC
	  
	   
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
	  --WHERE SP.SwPkgUID IN()
END