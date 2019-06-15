if(object_id('sp_make_pizza') IS NOT NULL) BEGIN
    DROP PROCEDURE sp_make_pizza
END
GO
CREATE PROCEDURE sp_make_pizza
	@name VARCHAR(50),
	@baseType VARCHAR(50)='stonebaked',
	@crust VARCHAR(50)='regular',
	@anchovies bit = false
AS
    SELECT @name, @baseType