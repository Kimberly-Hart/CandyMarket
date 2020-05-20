create table Candy(
ID int primary key identity (1,1) not null,
[Name] varchar(255) not null,
FlavorCategory varchar(255) not null,
Manufacturer varchar(255) not null)

create table [User](
ID int primary key identity (1,1) not null,
[Name] varchar(255) not null,
)


create table UserCandy(
ID int primary key identity (1,1) not null,
DateReceived datetime not null, 
CandyId int not null,
UserId int not null,
IsEaten bit default 0
)


alter table UserCandy
add foreign key (UserId)References [User](Id),
foreign key (CandyId) References Candy(ID);


insert into Candy ([Name],FlavorCategory,Manufacturer)
values('Snickers','Chocolate','Mars'),
('Reeses','Peanut Butter','Hershey'),
('Starburst','Fruity','Mars');


insert into [User] ([Name])
values('Jamie'),('John'),('Ivan'),('Ray');


insert into UserCandy (DateReceived, CandyId, UserId)
values('2020-05-01 13:30:00.000',1,1),
('2020-05-05 13:30:00.000',2,3),
('2020-05-10 13:30:00.000',3,2),
('2020-05-11 13:30:00.000',3,4),
('1912-04-01 13:30:00.000',2,3)
