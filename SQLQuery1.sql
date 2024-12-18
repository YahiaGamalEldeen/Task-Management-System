CREATE DATABASE TaskManagement;
GO
USE TaskManagement;
GO

-- إنشاء جدول المستخدمين وإضافة بيانات عينة
CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) ,
    Password NVARCHAR(100) NOT NULL
);
GO


INSERT INTO Users (Name, Email, Password)
VALUES ('Ahmed Ali', 'ahmed@example.com', 'password123'),
       ('Sara Mohamed', 'sara@example.com', 'password123'),
       ('Khaled Hassan', 'khaled@example.com', 'password123');
GO

-- إنشاء جدول المشاريع وإضافة بيانات عينة
CREATE TABLE Projects (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500)
);
GO

INSERT INTO Projects (Name, Description)
VALUES ('Project Management System', 'System for managing projects and tasks'),
       ('E-commerce Platform', 'Platform for online shopping'),
       ('Learning Management System', 'System for managing online courses');
GO

-- إنشاء جدول المهام وإضافة بيانات عينة
CREATE TABLE Tasks (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(100) NOT NULL,
    Description NVARCHAR(500),
    DueDate DATETIME,
    Priority NVARCHAR(50),
    Status NVARCHAR(50),
    UserId INT,
    ProjectId INT,
    CONSTRAINT FK_Tasks_Users FOREIGN KEY (UserId) REFERENCES Users(Id),
    CONSTRAINT FK_Tasks_Projects FOREIGN KEY (ProjectId) REFERENCES Projects(Id)
);
GO
INSERT INTO Tasks (Title, Description, DueDate, Priority, Status, UserId, ProjectId)
VALUES ('Design Database', 'Design the initial database schema', '2024-11-01', 'High', 'In Progress', 1, 1),
       ('Develop API', 'Develop the API endpoints', '2024-11-15', 'Medium', 'Not Started', 2, 1),
       ('Create Frontend', 'Develop the frontend interface', '2024-12-01', 'Low', 'Not Started', 3, 2);
GO