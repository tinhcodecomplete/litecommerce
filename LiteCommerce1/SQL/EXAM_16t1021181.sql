-- cau1 
 CREATE VIEW view_Project_2020
 AS
    SELECT  ProjectID ,
            ProjectName ,
            StartDate, 
			EndDate
    FROM    Project
    WHERE   YEAR(StartDate) = 2020
            AND EndDate IS NOT NULL;

GO
SELECT * FROM view_Project_2020

GO
UPDATE view_Project_2020 
SET EndDate = NULL
WHERE EndDate IS NOT NULL 

GO

--cau2 a
CREATE  PROCEDURE proc_Project_Insert
    @ProjectID INT ,
    @ProjectName NVARCHAR(50) ,
    @StartDate DATE ,
    @EndDate DATE = NULL
AS
    BEGIN
        IF ( NOT EXISTS ( SELECT    ProjectID
                          FROM      Project
                          WHERE     ProjectID = @ProjectID )
           )
            BEGIN
                INSERT  INTO Project
                        ( ProjectID ,
                          ProjectName ,
                          StartDate ,
                          EndDate
	                    )
                VALUES  ( @ProjectID , 
                          @ProjectName , 
                          @StartDate , 
                          @EndDate  
	                    );
                PRINT 'insert thanh cong';
            END;
        ELSE
            PRINT 'insert khong thanh cong';
	
    END;
GO



EXEC proc_Project_Insert @ProjectID = 6, 
    @ProjectName = N'project F', 
    @StartDate = '2020-1-29', 
    @EndDate = '2020-4-5'
GO
--cau 2b
CREATE  PROCEDURE proc_Project_UpdateEndDate
    @ProjectID INT ,
    @EndDate DATE
AS
    BEGIN
        DECLARE @StartDate DATE;
        SELECT  @StartDate = StartDate
        FROM    Project
        WHERE   ProjectID = @ProjectID;
        IF ( DATEDIFF(DAY, @StartDate, @EndDate) > 0 )
            BEGIN
                UPDATE  Project
                SET     EndDate = @EndDate
				WHERE ProjectID = @ProjectID
				
                PRINT 'cap nhat EndDate thanh cong';
            END;
        ELSE
            PRINT 'cap nhat EndDate khong thanh cong';
		

    END;
GO
EXEC proc_Project_UpdateEndDate @ProjectID = 5, 
    @EndDate = '2010-12-29 10:56:00' 
GO

--cau 2c
CREATE PROCEDURE proc_ListEmployees
    @Page INT =1 ,
    @PageSize INT=1
AS
    BEGIN
		
        SELECT  *
        FROM    ( SELECT  * ,ROW_NUMBER() OVER ( ORDER BY EmployeeID ) AS RowNumber
                  FROM  Employee
                ) AS trang
        WHERE   trang.RowNumber BETWEEN ( @Page - 1 ) * @PageSize + 1 AND    @Page * @PageSize
        ORDER BY trang.RowNumber;
    END;

GO
EXEC proc_ListEmployees @Page = 1, -- int
    @PageSize = 2 -- int

GO

--cau 3
CREATE TRIGGER trg_Assignment_Insert ON Assignment
    FOR UPDATE
AS
    BEGIN
        DECLARE @ngaygiao DATE;
        DECLARE @ngayketthuc DATE;
        DECLARE @projectID INT;

        SELECT  @projectID = Inserted.ProjectID ,
                @ngaygiao = Inserted.AssignedTime
        FROM    Inserted;
        SELECT  @ngayketthuc = EndDate
        FROM    Project 
		WHERE ProjectID =@projectID
        IF ( DATEDIFF(DAY, @ngaygiao, @ngayketthuc) > 0 )
            PRINT 'cap nhat thanh cong';
        ELSE
            BEGIN
                PRINT 'cap nhat khong thanh cong';
                ROLLBACK TRANSACTION;
            END;

    END;
GO
INSERT INTO Assignment
        ( EmployeeID ,
          ProjectID ,
          AssignedTime ,
          Role
        )
VALUES  ( N'EM04' , -- EmployeeID - nvarchar(50)
          3 , -- ProjectID - int
          '2022/1/1' , -- AssignedTime - date
          N'giam doc'  -- Role - nvarchar(50)
        )
GO

--cau 4 a
CREATE FUNCTION func_CountAssignment(@ProjectID INT)
RETURNS INT
AS
BEGIN
    RETURN(SELECT COUNT(EmployeeID) FROM Assignment 
	WHERE ProjectID = @ProjectID
	GROUP BY ProjectID
	)
END

GO
SELECT func_CountAssignment(1)
GO

-- cau4 b
CREATE FUNCTION func_GetAssignments ( @ProjectID INT )
RETURNS @table TABLE
    (
      hoten NVARCHAR(50) ,
      ngaysinh DATE ,
      email NVARCHAR(50) ,
      dienthoai NVARCHAR(50) ,
      thoidiemgiaoviec DATE
    )
AS
    BEGIN
        INSERT  INTO @table
                SELECT  FullName ,
                        BirthDate ,
                        Email ,
                        Phone ,
                        AssignedTime
                FROM    Employee
                        JOIN Assignment ON Assignment.EmployeeID = Employee.EmployeeID
                WHERE   ProjectID = @ProjectID;
        RETURN;
    END;


GO

SELECT * FROM func_GetAssignments(1)
GO

--cau5

ALTER  PROCEDURE proc_SummaryByMonth @Year INT
AS
    BEGIN
	SELECT  MONTH(StartDate) ,COUNT(ProjectID) AS [So du an]
                FROM    Project
                WHERE   YEAR(StartDate) = @Year
				GROUP BY MONTH(StartDate);
    END;

GO
EXEC proc_SummaryByMonth @Year = 2020 -- int

--cau 6
CREATE LOGIN baithisql WITH PASSWORD ='123'
CREATE USER normal_user FOR LOGIN baithisql

GRANT SELECT,UPDATE,DELETE,INSERT
ON view_Project_2020 
TO normal_user

GRANT ALL
ON proc_ListEmployees 
TO normal_user

GRANT SELECT
ON proc_Project_Insert
TO normal_user

GRANT SELECT
ON proc_Project_UpdateEndDay
TO normal_user


GRANT ALL
ON func_CountAssignment
TO normal_user

GRANT ALL
ON func_GetAssignments
TO normal_user

GRANT ALL
ON proc_Project_UpdateEndDay
TO normal_user

