CREATE PROCEDURE Inventory.usp_AddSoftwarePackage(@Swpackage VARBINARY(MAX),@Swhelpdoc VARBINARY(MAX),@SwPkgUID UNIQUEIDENTIFIER,@SwAddedDate DATETIME,@SwPkgVersion VARCHAR (100),@SwPkgDescription NVARCHAR (500),@SwColorStandardID	INT,
													@SwFileDetailsUID UNIQUEIDENTIFIER,@SwFileName NVARCHAR (200),@SwFileFormat NVARCHAR (30),@SwFileSize VARCHAR (30),@SwFileURL NVARCHAR (512),@SwFileUploadDate DATETIME2,@SwFileChecksum VARCHAR (120), @SwFileChecksumType VARCHAR (60), @SwCreatedBy NVARCHAR (200),
													@HdFileDetailsUID UNIQUEIDENTIFIER,@HdFileName NVARCHAR (200),@HdFileFormat NVARCHAR (30),@HdFileSize VARCHAR (30),
													@BlobUID UNIQUEIDENTIFIER, @BlobDescription VARCHAR(15))



AS BEGIN
	SET NOCOUNT ON
	SET XACT_ABORT ON

	DECLARE @BlobUidSwPackg AS UNIQUEIDENTIFIER = NEWID(),
		@BlobUidHelpDoc AS UNIQUEIDENTIFIER = NEWID();
	DECLARE @SwVersion AS INT;
	SELECT @SwVersion =  MAX([SwVersion]) from [Inventory].[SoftwarePackage];

		 BEGIN TRANSACTION
			
			--SoftwarePackage
				INSERT INTO [Inventory].[SoftwarePackage]
           ([SwPkgUID] ,[SwPkgDescription],[SwPkgVersion],[SwColorStandardID],[AddedDate],[SwVersion],[ReleaseDate],[IsMajor],[IsObsolete],[IsSupportSwUpdate],[IsSupportPwUpdate],[IsSupportNTP],[StoreInDB],[IsDeleted])
     VALUES
           (@SwPkgUID,@SwPkgDescription, @SwPkgVersion,@SwColorStandardID,@SwAddedDate,@SwVersion +1 ,null,null,null,null,null,null,null,0)
		  --File details software package
		   INSERT INTO [Inventory].[FileDetails]([FileDetailsUID],[SwPkgUID],[ParentDirectory],[FileName],[FileFormat],[FileSize],[FileURL],[FileUploadDate],[FileChecksum],[FileChecksumType],[CreatedBy])
     VALUES
           (@SwFileDetailsUID,@SwPkgUID, null,  @SwFileName, @SwFileFormat, @SwFileSize, @SwFileURL, @SwFileUploadDate, @SwFileChecksum, @SwFileChecksumType, @SwCreatedBy)

		   ----File details HelpDoc
		   INSERT INTO [Inventory].[FileDetails]([FileDetailsUID],[SwPkgUID],[ParentDirectory],[FileName],[FileFormat],[FileSize],[FileURL],[FileUploadDate],[FileChecksum],[FileChecksumType],[CreatedBy])
     VALUES
           (@HdFileDetailsUID, @SwPkgUID,null, @HdFileName ,@HdFileFormat ,@HdFileSize , @SwFileURL, @SwFileUploadDate, @SwFileChecksum, @SwFileChecksumType, @SwCreatedBy)

		   --Software package
		   INSERT INTO [Inventory].[Blobs]([BlobUID],[Description],[BlobTypeUID],[Blob])
     VALUES
           (@BlobUidSwPackg, @BlobDescription, '151C28A2-7B47-4764-85AE-940B3901BA97', @Swpackage)
	 
	   ---SW package blobs map
		   INSERT INTO [Inventory].[SwPackageBlobsMap]([BlobUID],[SwPkgUID])
     VALUES
           ( @BlobUidSwPackg, @SwPkgUID)
		   ---

		   --Help doc
		   INSERT INTO [Inventory].[Blobs]([BlobUID],[Description],[BlobTypeUID],[Blob])
     VALUES
           (@BlobUidHelpDoc, @BlobDescription, 'B1CD7C3D-DA88-4608-834B-86F388813D8C', @Swhelpdoc)
		   --Map 
		  INSERT INTO [Inventory].[SwPackageBlobsMap]([BlobUID],[SwPkgUID])
     VALUES
           ( @BlobUidHelpDoc, @SwPkgUID)
	COMMIT
END