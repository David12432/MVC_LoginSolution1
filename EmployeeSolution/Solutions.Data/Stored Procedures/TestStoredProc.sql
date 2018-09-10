IF EXISTS(SELECT * FROM sys.objects 
			WHERE object_id = OBJECT_ID('procedure') 
				AND type IN ('P', 'NP'))
DROP PROCEDURE dbo.[procedure]
GO

create procedure dbo.[procedure]
(
@variable1 int,
@variable2  varchar(30)
)
as
select * from Employees
go