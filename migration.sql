IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [ActivityLogs] (
    [Id] int NOT NULL IDENTITY,
    [ActionType] nvarchar(max) NULL,
    [EntityName] nvarchar(max) NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [Timestamp] datetime2 NOT NULL,
    CONSTRAINT [PK_ActivityLogs] PRIMARY KEY ([Id])
);

CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [FirstName] nvarchar(max) NOT NULL,
    [LastName] nvarchar(max) NOT NULL,
    [ProfilePicture] nvarchar(max) NULL,
    [DateCreated] datetime2 NOT NULL,
    [IsActive] bit NOT NULL,
    [RefreshToken] nvarchar(max) NULL,
    [RefreshTokenExpiryTime] datetime2 NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);

CREATE TABLE [Departments] (
    [Id] int NOT NULL IDENTITY,
    [Name] NVarchar(100) NOT NULL,
    [Description] NVarchar(500) NULL,
    [CreatedAt] datetime2 NULL,
    [UpdatedAt] datetime2 NULL,
    CONSTRAINT [PK_Departments] PRIMARY KEY ([Id])
);

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Course] (
    [Id] int NOT NULL IDENTITY,
    [Title] NVarchar(100) NOT NULL,
    [Description] NVarchar(500) NOT NULL,
    [Credits] Int NOT NULL,
    [DurationInHours] int NULL,
    [PricePerHour] decimal(18,2) NULL,
    [DepartmentId] int NOT NULL,
    [CreatedAt] datetime2 NULL,
    [UpdatedAt] datetime2 NULL,
    CONSTRAINT [PK_Course] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Course_Departments_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [Departments] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Instructor] (
    [Id] int NOT NULL IDENTITY,
    [Fname] NVarchar(50) NOT NULL,
    [Lname] NVarchar(50) NOT NULL,
    [Address_City] NVarchar(50) NOT NULL,
    [Address_Country] NVarchar(50) NOT NULL,
    [Email] NVarchar(100) NOT NULL,
    [PhoneNumber] NVarchar(50) NOT NULL,
    [DateOfBirth] Date NOT NULL,
    [ProfileImageUrl] nvarchar(max) NULL,
    [Username] nvarchar(max) NULL,
    [Password] nvarchar(max) NULL,
    [CreatedAt] datetime2 NULL,
    [UpdatedAt] datetime2 NULL,
    [Salary] Decimal(18,2) NULL,
    [DepartmentId] int NULL,
    CONSTRAINT [PK_Instructor] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Instructor_Departments_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [Departments] ([Id])
);

CREATE TABLE [Student] (
    [Id] int NOT NULL IDENTITY,
    [Fname] NVarchar(50) NOT NULL,
    [Lname] NVarchar(50) NOT NULL,
    [Address_City] NVarchar(50) NOT NULL,
    [Address_Country] NVarchar(50) NOT NULL,
    [Email] NVarchar(100) NOT NULL,
    [PhoneNumber] NVarchar(50) NOT NULL,
    [DateOfBirth] Date NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UpdatedAt] datetime2 NULL,
    [Gender] nvarchar(max) NULL,
    [DepartmentId] int NULL,
    CONSTRAINT [PK_Student] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Student_Departments_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [Departments] ([Id])
);

CREATE TABLE [CourseSchedule] (
    [Id] int NOT NULL IDENTITY,
    [CourseId] int NOT NULL,
    [DayOfWeek] nvarchar(max) NOT NULL,
    [StartTime] time NOT NULL,
    [EndTime] time NOT NULL,
    [Room] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NULL,
    [UpdatedAt] datetime2 NULL,
    CONSTRAINT [PK_CourseSchedule] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_CourseSchedule_Course_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Course] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [InstructorCourses] (
    [Id] int NOT NULL IDENTITY,
    [InstructorId] nvarchar(max) NOT NULL,
    [CourseId] int NOT NULL,
    CONSTRAINT [PK_InstructorCourses] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_InstructorCourses_Course_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Course] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [Enrollment] (
    [StudentId] Int NOT NULL,
    [CourseId] Int NOT NULL,
    [Grades] Decimal(5,2) NOT NULL DEFAULT 0.0,
    [CreatedAt] datetime2 NULL,
    [UpdatedAt] datetime2 NULL,
    CONSTRAINT [PK_Enrollment] PRIMARY KEY ([StudentId], [CourseId]),
    CONSTRAINT [FK_Enrollment_Course_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Course] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Enrollment_Student_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [Student] ([Id]) ON DELETE CASCADE
);

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;

CREATE INDEX [IX_Course_DepartmentId] ON [Course] ([DepartmentId]);

CREATE INDEX [IX_CourseSchedule_CourseId] ON [CourseSchedule] ([CourseId]);

CREATE INDEX [IX_Enrollment_CourseId] ON [Enrollment] ([CourseId]);

CREATE INDEX [IX_Instructor_DepartmentId] ON [Instructor] ([DepartmentId]);

CREATE INDEX [IX_InstructorCourses_CourseId] ON [InstructorCourses] ([CourseId]);

CREATE INDEX [IX_Student_DepartmentId] ON [Student] ([DepartmentId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260812160411_InitialCreate', N'9.0.12');

COMMIT;
GO

