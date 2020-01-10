SELECT*FROM Suppliers

CREATE PROCEDURE uspSupplierPageList
@startRow INT,
@endRow INT
AS 
BEGIN
 SET NOCOUNT ON;

 WITH SupplierResult AS
 (
	SELECT SupplierID,
		   CompanyName,
		   ContactName,
		   ContactTitle,
		   City,
		   Country,
		   Phone,
		   Fax,
		   ROW_NUMBER() OVER (ORDER BY SupplierID) AS Rownum	 -- LE PONE UN NUMERO A CADA FILA DEL SELECT	
    FROM [Northwind].[dbo].Suppliers		
 )

 SELECT SupplierID,
		   CompanyName,
		   ContactName,
		   ContactTitle,
		   City,
		   Country,
		   Phone,
		   Fax	
    FROM SupplierResult WHERE Rownum BETWEEN @startRow AND @endRow
 END