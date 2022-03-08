USE [ContPaqi001]

/***************************************************************
*  Table: seUsuario                                                            
***************************************************************/
IF object_id(N'seUsuario','U') IS NULL
BEGIN
	CREATE TABLE [dbo].[seUsuario]
	(
		UsuarioID				INT IDENTITY(1,1) 	NOT NULL,
		NombreUsuario           VARCHAR(60)         NOT NULL,	
		CodigoUsuario           VARCHAR(10)         NOT NULL,
		[Password]	            NVARCHAR(200)		NOT NULL,		
		Estatus					BIT 				NOT NULL DEFAULT ((1)),			
		CorreoElectronico 		VARCHAR(100) 		NOT NULL,				
		FechaModificacion 		DATETIME			NOT NULL DEFAULT('1900-01-01'),			
		FechaCreacion 			DATETIME			NOT NULL DEFAULT(GETDATE())	
		
	)            
	
	ALTER TABLE [dbo].[seUsuario]
	ADD CONSTRAINT PK_seUsuario_UsuarioID
	PRIMARY KEY NONCLUSTERED (UsuarioID);	

	ALTER TABLE [dbo].[seUsuario]
	ADD CONSTRAINT UQ_seUsuario_CodigoUsuario
	UNIQUE (CodigoUsuario); 	
END
            
GO

/***************************************************************
*  Table: cocktail                                                            
***************************************************************/
IF object_id(N'cocktail','U') IS NULL
BEGIN
	CREATE TABLE [dbo].[cocktail]
	(
		idDrink INT NOT NULL  ,
		strDrink VARCHAR(1000) NOT NULL  ,
		strDrinkAlternate VARCHAR(1000) NULL  ,
		strTags VARCHAR(1000) NULL  ,
		strVideo VARCHAR(1000) NULL  ,
		strCategory VARCHAR(1000) NULL  ,
		strIBA VARCHAR(1000) NULL  ,
		strAlcoholic VARCHAR(1000) NULL  ,
		strGlass VARCHAR(1000) NULL  ,
		strInstructions VARCHAR(1000) NULL  ,
		strInstructionsES VARCHAR(1000) NULL  ,
		strInstructionsDE VARCHAR(1000) NULL  ,
		strInstructionsFR VARCHAR(1000) NULL  ,
		strInstructionsIT VARCHAR(1000) NULL  ,
		strInstructionsZH_HANS VARCHAR(1000) NULL  ,
		strInstructionsZH_HANT VARCHAR(1000) NULL  ,
		strDrinkThumb VARCHAR(1000) NULL  ,
		strIngredient1 VARCHAR(1000) NULL  ,
		strIngredient2 VARCHAR(1000) NULL  ,
		strIngredient3 VARCHAR(1000) NULL  ,
		strIngredient4 VARCHAR(1000) NULL  ,
		strIngredient5 VARCHAR(1000) NULL  ,
		strIngredient6 VARCHAR(1000) NULL  ,
		strIngredient7 VARCHAR(1000) NULL  ,
		strIngredient8 VARCHAR(1000) NULL  ,
		strIngredient9 VARCHAR(1000) NULL  ,
		strIngredient10 VARCHAR(1000) NULL  ,
		strIngredient11 VARCHAR(1000) NULL  ,
		strIngredient12 VARCHAR(1000) NULL  ,
		strIngredient13 VARCHAR(1000) NULL  ,
		strIngredient14 VARCHAR(1000) NULL  ,
		strIngredient15 VARCHAR(1000) NULL  ,
		strMeasure1 VARCHAR(1000) NULL  ,
		strMeasure2 VARCHAR(1000) NULL  ,
		strMeasure3 VARCHAR(1000) NULL  ,
		strMeasure4 VARCHAR(1000) NULL  ,
		strMeasure5 VARCHAR(1000) NULL  ,
		strMeasure6 VARCHAR(1000) NULL  ,
		strMeasure7 VARCHAR(1000) NULL  ,
		strMeasure8 VARCHAR(1000) NULL  ,
		strMeasure9 VARCHAR(1000) NULL  ,
		strMeasure10 VARCHAR(1000) NULL  ,
		strMeasure11 VARCHAR(1000) NULL  ,
		strMeasure12 VARCHAR(1000) NULL  ,
		strMeasure13 VARCHAR(1000) NULL  ,
		strMeasure14 VARCHAR(1000) NULL  ,
		strMeasure15 VARCHAR(1000) NULL  ,
		strImageSource VARCHAR(1000) NULL  ,
		strImageAttribution VARCHAR(1000) NULL  ,
		strCreativeCommonsConfirmed VARCHAR(1000) NULL  ,
		dateModified VARCHAR(1000) NULL 		
	)	
END
            
GO