create type PkgUidList AS Table(
	PkgUid Uniqueidentifier
);
go

CREATE PROCEDURE Inventory.usp_DeleteFirmware(@PackageIds as PkgUidList Readonly , @DeleteAll TINYINT)
	
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
			UPDATE Inventory.SoftwarePackage SET IsDeleted =1 WHERE SoftwarePackage.SwPkgUID IN (select * from @PackageIds);
			END
		COMMIT
END