declare @page int = 1,
		@pageSize int = 7,
		@searchValue nvarchar(255) = N'%ec%';
/*phan trang*/
select * 
from(
select * , ROW_NUMBER() over(order by SupplierID) as RowNumber
from Suppliers
where (@searchValue = N'') or (CompanyName like @searchValue)
) as t
where t.RowNumber between (@page -1) * @pageSize +1  and @page* @pageSize
order by t.RowNumber