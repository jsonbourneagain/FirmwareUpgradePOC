CREATE TYPE Inventory.PkgUidList AS TABLE(
	PkgUid UNIQUEIDENTIFIER
);
go

CREATE PROCEDURE Inventory.usp_DeleteFirmware(@PackageIds as Inventory.PkgUidList Readonly , @DeleteAll TINYINT)
	
AS BEGIN
	SET NOCOUNT ON
	SET XACT_ABORT ON

	
		BEGIN TRANSACTION

		IF @DeleteAll = 1
			BEGIN
			UPDATE Inventory.SoftwarePackage SET IsDeleted = 1;
			END
		ELSE
			BEGIN
			UPDATE Inventory.SoftwarePackage SET IsDeleted =1 WHERE SoftwarePackage.SwPkgUID IN (SELECT * FROM @PackageIds);
			END
		COMMIT
END