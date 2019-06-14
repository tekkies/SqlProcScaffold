if(object_id('SprocWrapperTest') IS NOT NULL) BEGIN
    DROP PROCEDURE SprocWrapperTest
END
GO
CREATE PROCEDURE SprocWrapperTest
    @intNoDefault int,
    @intNullDefault int = null,
    @intNumericDefault int = 1
AS
    SELECT 1