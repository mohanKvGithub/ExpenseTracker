Create Table [User](
	[Id]  int identity(1,1) not null primary key clustered(Id),
	[Name] varchar(30),
	[Role] varchar(20),
	[Password] varchar(200),
	[Salt] varchar(20),
	[CreatedOn] datetime,
	[UpdatedOn] datetime,
	[IsDeleted] bit
)
GO
Create Table [Budget](
	[Id]  int identity(1,1) not null primary key clustered(Id),
	[UserId] int not null,
	[Amount] decimal,
	[Reason] varchar(200),
	[IsActive] bit,
	[CreatedOn] datetime,
	[UpdatedOn] datetime,
	[IsDeleted] bit,
	constraint FK_Budget_User foreign key(UserId) references [User](Id)
)
Go
Create Table [BudgetReset](
	[Id]  int identity(1,1) not null primary key clustered(Id),
	[UserId] int not null,
	[ResetDate] datetime,
	[CreatedOn] datetime,
	[UpdatedOn] datetime,
	[IsDeleted] bit,
	constraint FK_BudgetReset_User foreign key(UserId) references [User](Id)
)
Go
Create Table [PaymentType](
	[Id]  int identity(1,1) not null primary key clustered(Id),
	[AccountName] varchar(20),
	[Type] varchar(10),
	[CreatedOn] datetime,
	[UpdatedOn] datetime,
	[IsDeleted] bit,
)
Go
Create Table [TransactionType](
	[Id]  int identity(1,1) not null primary key clustered(Id),
	[Type] varchar(20),
	[CreatedOn] datetime,
	[UpdatedOn] datetime,
	[IsDeleted] bit,
)
Go
Create Table [Transaction](
	[Id]  int identity(1,1) not null primary key clustered(Id),
	[UserId] int not null,
	[TransactionTypeId] int not null,
	[PaymentTypeId] int not null,
	[Dr] decimal,
	[Cr] decimal,
	[Description] varchar(200),
	[CreatedOn] datetime,
	[UpdatedOn] datetime,
	[IsDeleted] bit,
	constraint FK_Transaction_User foreign key(UserId) references [User](Id),
	constraint FK_Transaction_TransactionType foreign key(TransactionTypeId) references [TransactionType](Id),
	constraint FK_Transaction_PaymentType foreign key(PaymentTypeId) references [PaymentType](Id)
)
GO
alter table [User] add  Email varchar(20)
GO
alter table [Transaction] add  TransactedOn datetime
GO
INSERT INTO TransactionType([Type],[CreatedOn],[UpdatedOn],[IsDeleted]) VALUES ('Income',GETDATE(),GETDATE(),0);
GO
INSERT INTO TransactionType([Type],[CreatedOn],[UpdatedOn],[IsDeleted]) VALUES ('Expense',GETDATE(),GETDATE(),0);
GO
INSERT INTO PaymentType([AccountName],[Type],[CreatedOn],[UpdatedOn],[IsDeleted]) VALUES ('Cash','Cash',GETDATE(),GETDATE(),0);
GO
INSERT INTO PaymentType([AccountName],[Type],[CreatedOn],[UpdatedOn],[IsDeleted]) VALUES ('HDFC','Bank',GETDATE(),GETDATE(),0);
GO
INSERT INTO PaymentType([AccountName],[Type],[CreatedOn],[UpdatedOn],[IsDeleted]) VALUES ('BOB','Bank',GETDATE(),GETDATE(),0);