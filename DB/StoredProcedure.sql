USE ContPaqi001

/*****************************************************************************
*   sp_seUsuario_All
******************************************************************************/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

IF object_id(N'sp_seUsuario_All','P') IS NOT NULL
BEGIN
      DROP PROCEDURE dbo.sp_seUsuario_All
      PRINT '<<<WARNING: STORED PROCEDURE dbo.sp_seUsuario_All en la Base de Datos: ' + DB_NAME() + ' en el Servidor: ' + @@servername + ' se ha ELIMINADO! >>>'
END
            
GO

CREATE PROCEDURE [dbo].[sp_seUsuario_All]

/*****************************************************************************
*  Tipo de objeto: Stored Procedure
*  Funcion: Regresa todos los usuario 
*  Autor :  Omar Martinez
*  Fecha de Creación: 04/03/2022
*  Log de Mantenimiento: 
*  Date          Modified By             Description            
*  ----------    --------------------    -------------------------------------
*  
******************************************************************************/

AS
	
	SELECT	UsuarioID,
	        NombreUsuario,
			CodigoUsuario,			
			[Password],
			Estatus,			
			CorreoElectronico,			
			FechaModificacion,			
			FechaCreacion						
	FROM seUsuario
	ORDER BY UsuarioID ASC
	
GO

IF object_id(N'dbo.sp_seUsuario_All','P') IS NOT NULL
BEGIN
      PRINT '<<<Correcto: STORED PROCEDURE dbo.sp_seUsuario_All en la Base de Datos: ' + db_name() + ' en el Servidor: ' + @@servername + ' se ha CREADO!  >>>'	  
END
ELSE
BEGIN
      PRINT '<<<WARNING: No ha sido posible crear el STORED PROCEDURE dbo.sp_seUsuario_All en la Base de Datos: ' + db_name() + ' en el Servidor: ' + @@servername + '  >>>'	  
END
GO

/*****************************************************************************
*   sp_seUsuario_GetUsuarioID
******************************************************************************/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

IF object_id(N'sp_seUsuario_GetUsuarioID','P') IS NOT NULL
BEGIN
      DROP PROCEDURE dbo.sp_seUsuario_GetUsuarioID
      PRINT '<<<WARNING: STORED PROCEDURE dbo.sp_seUsuario_GetUsuarioID en la Base de Datos: ' + DB_NAME() + ' en el Servidor: ' + @@servername + ' se ha ELIMINADO! >>>'
END
            
GO

CREATE PROCEDURE [dbo].[sp_seUsuario_GetUsuarioID]

/*****************************************************************************
*  Tipo de objeto: Stored Procedure
*  Funcion: Obtiene un Usuario filtando por el mismo
*  Autor :  Omar Martinez
*  Fecha de Creación: 04/03/2022
*  Log de Mantenimiento: 
*  Date          Modified By             Description            
*  ----------    --------------------    -------------------------------------
*  
******************************************************************************/

@USUARIOID INT

AS	
	SELECT	UsuarioID,
	        NombreUsuario,
			CodigoUsuario,			
			[Password],
			Estatus,			
			CorreoElectronico,			
			FechaModificacion,			
			FechaCreacion		
	FROM seUsuario
	WHERE USUARIOID = @USUARIOID
	ORDER BY UsuarioID	
	
	IF @@ROWCOUNT = 0 
	BEGIN
		RETURN 1	    
	END
	
	RETURN 0

GO

IF object_id(N'dbo.sp_seUsuario_GetUsuarioID','P') IS NOT NULL
BEGIN
      PRINT '<<<Correcto: STORED PROCEDURE dbo.sp_seUsuario_GetUsuarioID en la Base de Datos: ' + db_name() + ' en el Servidor: ' + @@servername + ' se ha CREADO!  >>>'	  
END
ELSE
BEGIN
      PRINT '<<<WARNING: No ha sido posible crear el STORED PROCEDURE dbo.sp_seUsuario_GetUsuarioID en la Base de Datos: ' + db_name() + ' en el Servidor: ' + @@servername + '  >>>'	  
END
GO

/*****************************************************************************
*   sp_seUsuario_Insert
******************************************************************************/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

IF object_id(N'sp_seUsuario_Insert','P') IS NOT NULL
BEGIN
	DROP PROCEDURE dbo.sp_seUsuario_Insert 	
	PRINT '<<<WARNING: STORED PROCEDURE dbo.sp_seUsuario_Insert en la Base de Datos: ' + DB_NAME() + ' en el Servidor: ' + @@servername + ' se ha ELIMINADO! >>>'
END
            
GO

CREATE PROCEDURE [dbo].[sp_seUsuario_Insert]

/*****************************************************************************
*  Tipo de objeto: Stored Procedure
*  Funcion: Inserta Usuarios
*  Autor :  Omar Martinez
*  Fecha de Creación: 04/03/2022
*  Log de Mantenimiento: 
*  Date          Modified By             Description            
*  ----------    --------------------    -------------------------------------
*  
******************************************************************************/

@NOMBREUSUARIO			VARCHAR(60),
@CODIGOUSUARIO			VARCHAR(10),
@PASSWORD				NVARCHAR(200),
@ESTATUS     			BIT,
@CORREOELECTRONICO		VARCHAR(100),         
@USUARIOID 				INT OUTPUT

AS
	-- Declarar las variables utilizadas en la comprobación de errores .
	DECLARE @ErrorVar INT;
	DECLARE @RowCountVar INT;
			
	INSERT INTO [dbo].[seUsuario] ( NombreUsuario, CodigoUsuario, [Password], estatus, CorreoElectronico )
	VALUES ( @NOMBREUSUARIO, @CODIGOUSUARIO,  @PASSWORD, @ESTATUS, @CORREOELECTRONICO)
	
	SELECT	@ErrorVar = @@ERROR, 
			@RowCountVar = @@ROWCOUNT
			
	IF @ErrorVar <> 0 OR @RowCountVar = 0
	BEGIN
		SET @USUARIOID = 0		
	END 

	--Busca el ultimo USUARIOID IDENTITY Insertado
	SET @USUARIOID = SCOPE_IDENTITY()
	
GO

IF object_id(N'dbo.sp_seUsuario_Insert','P') IS NOT NULL
BEGIN
	PRINT '<<<Correcto: STORED PROCEDURE dbo.sp_seUsuario_Insert en la Base de Datos: ' + db_name() + ' en el Servidor: ' + @@servername + ' se ha CREADO!  >>>'
END	  
ELSE
BEGIN
	PRINT '<<<WARNING: No ha sido posible crear el STORED PROCEDURE dbo.sp_seUsuario_Insert en la Base de Datos: ' + db_name() + ' en el Servidor: ' + @@servername + '  >>>'	  
END
GO

/*****************************************************************************
*   sp_seUsuario_Update
******************************************************************************/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

IF object_id(N'sp_seUsuario_Update','P') IS NOT NULL
BEGIN
      DROP PROCEDURE dbo.sp_seUsuario_Update
      PRINT '<<<WARNING: STORED PROCEDURE dbo.sp_seUsuario_Update en la Base de Datos: ' + DB_NAME() + ' en el Servidor: ' + @@servername + ' se ha ELIMINADO! >>>'
END
            
GO

CREATE PROCEDURE [dbo].[sp_seUsuario_Update]

/*****************************************************************************
*  Tipo de objeto: Stored Procedure
*  Funcion: Actualiza los datos del Usuario
*  Autor :  Omar Martinez
*  Fecha de Creación: 04/03/2022
*  Log de Mantenimiento: 
*  Date          Modified By             Description            
*  ----------    --------------------    -------------------------------------
*  
******************************************************************************/

@NOMBREUSUARIO			VARCHAR(60),
@CODIGOUSUARIO			VARCHAR(10),
@PASSWORD				NVARCHAR(200),
@ESTATUS     			BIT,
@CORREOELECTRONICO		VARCHAR(100),	          
@USUARIOID 				INT

AS
	UPDATE dbo.seUsuario
	SET NombreUsuario = @NOMBREUSUARIO,
		CodigoUsuario = @CODIGOUSUARIO,		
		[Password] = @PASSWORD,
		Estatus = @ESTATUS,		
		CorreoElectronico = @CORREOELECTRONICO,		
		FechaModificacion = GETDATE()				
	WHERE UsuarioID = @USUARIOID	
	
	IF @@ROWCOUNT = 0 AND @@ERROR <> 0 
	BEGIN		
		RETURN 1003
	END 
	
	RETURN 1004	

GO

IF object_id(N'dbo.sp_seUsuario_Update','P') IS NOT NULL
      PRINT '<<<CORRECTO: STORED PROCEDURE dbo.sp_seUsuario_Update en la Base de Datos: ' + db_name() + ' en el Servidor: ' + @@servername + ' se ha CREADO!  >>>'	  
ELSE
      PRINT '<<<WARNING: No ha sido posible crear el STORED PROCEDURE dbo.sp_seUsuario_Update en la Base de Datos: ' + db_name() + ' en el Servidor: ' + @@servername + '  >>>'	  
GO

/*****************************************************************************
*   sp_seUsuario_Delete
******************************************************************************/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

IF object_id(N'sp_seUsuario_Delete','P') IS NOT NULL
BEGIN
      DROP PROCEDURE dbo.sp_seUsuario_Delete
      PRINT '<<<WARNING: STORED PROCEDURE dbo.sp_seUsuario_Delete en la Base de Datos: ' + DB_NAME() + ' en el Servidor: ' + @@servername + ' se ha ELIMINADO! >>>'
END
            
GO

CREATE PROCEDURE [dbo].[sp_seUsuario_Delete]

/*****************************************************************************
*  Tipo de objeto: Stored Procedure
*  Funcion: Elimina un usuario en especifico por ID
*  Autor :  Omar Martinez
*  Fecha de Creación: 04/03/2022
*  Log de Mantenimiento: 
*  Date          Modified By             Description            
*  ----------    --------------------    -------------------------------------
*  
******************************************************************************/
@USUARIOID INT

AS	

	DELETE 
	FROM dbo.seUsuario
	WHERE USUARIOID = @USUARIOID
	
	IF @@ROWCOUNT = 0 AND @@ERROR <> 0 
	BEGIN		
		RETURN 1005
	END 
	
	RETURN 1006		
	
GO

IF object_id(N'dbo.sp_seUsuario_Delete','P') IS NOT NULL
BEGIN
	PRINT '<<<CORRECTO: STORED PROCEDURE dbo.sp_seUsuario_Delete en la Base de Datos: ' + db_name() + ' en el Servidor: ' + @@servername + ' se ha CREADO!  >>>'	  
END	  
ELSE
BEGIN
	PRINT '<<<WARNING: No ha sido posible crear el STORED PROCEDURE dbo.sp_seUsuario_Delete en la Base de Datos: ' + db_name() + ' en el Servidor: ' + @@servername + '  >>>'	  
END
GO


/*****************************************************************************
*   sp_cocktail_allSave
******************************************************************************/
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON

IF object_id(N'sp_cocktail_allSave','P') IS NOT NULL
BEGIN
      DROP PROCEDURE dbo.sp_cocktail_allSave
      PRINT '<<<WARNING: STORED PROCEDURE dbo.sp_cocktail_allSave en la Base de Datos: ' + DB_NAME() + ' en el Servidor: ' + @@servername + ' se ha ELIMINADO! >>>'
END
            
GO

CREATE PROCEDURE [dbo].[sp_cocktail_allSave]

/*****************************************************************************
*  Tipo de objeto: Stored Procedure
*  Funcion: Obtiene un Usuario filtando por el mismo
*  Autor :  Omar Martinez
*  Fecha de Creación: 04/03/2022
*  Log de Mantenimiento: 
*  Date          Modified By             Description            
*  ----------    --------------------    -------------------------------------
*  
******************************************************************************/

@REGISTROSXML XML,
@RTNVALUE   INT OUTPUT

AS	

	;WITH CTECocktails AS
	(
		SELECT	T.N.value('idDrink[1]', 'int') idDrink,
				T.N.value('strDrink[1]', 'VARCHAR(1000)') strDrink,
				T.N.value('strDrinkAlternate[1]', 'VARCHAR(1000)') strDrinkAlternate,
				T.N.value('strTags[1]', 'VARCHAR(1000)') strTags,
				T.N.value('strVideo[1]', 'VARCHAR(1000)') strVideo,
				T.N.value('strCategory[1]', 'VARCHAR(1000)') strCategory,
				T.N.value('strIBA[1]', 'VARCHAR(1000)') strIBA,
				T.N.value('strAlcoholic[1]', 'VARCHAR(1000)') strAlcoholic,
				T.N.value('strGlass[1]', 'VARCHAR(1000)') strGlass,
				T.N.value('strInstructions[1]', 'VARCHAR(1000)') strInstructions,
				T.N.value('strInstructionsES[1]', 'VARCHAR(1000)') strInstructionsES,
				T.N.value('strInstructionsDE[1]', 'VARCHAR(1000)') strInstructionsDE,
				T.N.value('strInstructionsFR[1]', 'VARCHAR(1000)') strInstructionsFR,
				T.N.value('strInstructionsIT[1]', 'VARCHAR(1000)') strInstructionsIT,
				T.N.value('strInstructionsZH_HANS[1]', 'VARCHAR(1000)') strInstructionsZH_HANS,
				T.N.value('strInstructionsZH_HANT[1]', 'VARCHAR(1000)') strInstructionsZH_HANT,
				T.N.value('strDrinkThumb[1]', 'VARCHAR(1000)') strDrinkThumb,
				T.N.value('strIngredient1[1]', 'VARCHAR(1000)') strIngredient1,
				T.N.value('strIngredient2[1]', 'VARCHAR(1000)') strIngredient2,
				T.N.value('strIngredient3[1]', 'VARCHAR(1000)') strIngredient3,
				T.N.value('strIngredient4[1]', 'VARCHAR(1000)') strIngredient4,
				T.N.value('strIngredient5[1]', 'VARCHAR(1000)') strIngredient5,
				T.N.value('strIngredient6[1]', 'VARCHAR(1000)') strIngredient6,
				T.N.value('strIngredient7[1]', 'VARCHAR(1000)') strIngredient7,
				T.N.value('strIngredient8[1]', 'VARCHAR(1000)') strIngredient8,
				T.N.value('strIngredient9[1]', 'VARCHAR(1000)') strIngredient9,
				T.N.value('strIngredient10[1]', 'VARCHAR(1000)') strIngredient10,
				T.N.value('strIngredient11[1]', 'VARCHAR(1000)') strIngredient11,
				T.N.value('strIngredient12[1]', 'VARCHAR(1000)') strIngredient12,
				T.N.value('strIngredient13[1]', 'VARCHAR(1000)') strIngredient13,
				T.N.value('strIngredient14[1]', 'VARCHAR(1000)') strIngredient14,
				T.N.value('strIngredient15[1]', 'VARCHAR(1000)') strIngredient15,
				T.N.value('strMeasure1[1]', 'VARCHAR(1000)') strMeasure1,
				T.N.value('strMeasure2[1]', 'VARCHAR(1000)') strMeasure2,
				T.N.value('strMeasure3[1]', 'VARCHAR(1000)') strMeasure3,
				T.N.value('strMeasure4[1]', 'VARCHAR(1000)') strMeasure4,
				T.N.value('strMeasure5[1]', 'VARCHAR(1000)') strMeasure5,
				T.N.value('strMeasure6[1]', 'VARCHAR(1000)') strMeasure6,
				T.N.value('strMeasure7[1]', 'VARCHAR(1000)') strMeasure7,
				T.N.value('strMeasure8[1]', 'VARCHAR(1000)') strMeasure8,
				T.N.value('strMeasure9[1]', 'VARCHAR(1000)') strMeasure9,
				T.N.value('strMeasure10[1]', 'VARCHAR(1000)') strMeasure10,
				T.N.value('strMeasure11[1]', 'VARCHAR(1000)') strMeasure11,
				T.N.value('strMeasure12[1]', 'VARCHAR(1000)') strMeasure12,
				T.N.value('strMeasure13[1]', 'VARCHAR(1000)') strMeasure13,
				T.N.value('strMeasure14[1]', 'VARCHAR(1000)') strMeasure14,
				T.N.value('strMeasure15[1]', 'VARCHAR(1000)') strMeasure15,
				T.N.value('strImageSource[1]', 'VARCHAR(1000)') strImageSource,
				T.N.value('strImageAttribution[1]', 'VARCHAR(1000)') strImageAttribution,
				T.N.value('strCreativeCommonsConfirmed[1]', 'VARCHAR(1000)') strCreativeCommonsConfirmed,
				T.N.value('dateModified[1]', 'VARCHAR(1000)') dateModified
		FROM @REGISTROSXML.nodes('/DocumentElement/dtCtl') AS T(N)		
	)
	
	INSERT INTO DBO.cocktail	
	SELECT a.*
	FROM CTECocktails a
	LEFT JOIN dbo.cocktail b
		ON a.idDrink = b.idDrink
	WHERE b.idDrink is null			
	
	SET @RTNVALUE = @@ROWCOUNT		
	


GO

IF object_id(N'dbo.sp_cocktail_allSave','P') IS NOT NULL
BEGIN
      PRINT '<<<Correcto: STORED PROCEDURE dbo.sp_cocktail_allSave en la Base de Datos: ' + db_name() + ' en el Servidor: ' + @@servername + ' se ha CREADO!  >>>'	  
END
ELSE
BEGIN
      PRINT '<<<WARNING: No ha sido posible crear el STORED PROCEDURE dbo.sp_cocktail_allSave en la Base de Datos: ' + db_name() + ' en el Servidor: ' + @@servername + '  >>>'	  
END
GO