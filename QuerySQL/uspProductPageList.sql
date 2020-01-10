SELECT*FROM Products

CREATE PROCEDURE uspProductPageList
@startRow INT,
@endRow INT
AS 
BEGIN
 SET NOCOUNT ON;

 WITH ProductResult AS
 (
	SELECT ProductID,
		   ProductName,
		   SupplierID,
		   UnitPrice,
		   QuantityPerUnit,
		   Discontinued,
		   ROW_NUMBER() OVER (ORDER BY ProductID) AS Rownum	 -- LE PONE UN NUMERO A CADA FILA DEL SELECT	
    FROM [Northwind].[dbo].Products		
 )

 SELECT ProductID,
		   ProductName,
		   SupplierID,
		   UnitPrice,
		   QuantityPerUnit,
		   Discontinued	
    FROM ProductResult WHERE Rownum BETWEEN @startRow AND @endRow
 END