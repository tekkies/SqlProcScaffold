if(object_id('sp_sproc_wrapper_test') IS NOT NULL) BEGIN
    DROP PROCEDURE sp_sproc_wrapper_test
END
GO
CREATE PROCEDURE sp_sproc_wrapper_test
    @intNoDefault int,
    @intNullDefault int = null,
    @intNumericDefault int = 1,
	@varcharNoDefault VARCHAR(20),
	@varcharNullDefault VARCHAR(20) = NULL,
	@varcharValueDefault VARCHAR(MAX) = 'max'
AS
    SELECT @intNoDefault, @intNullDefault, @intNumericDefault