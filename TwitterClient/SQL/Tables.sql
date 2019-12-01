create database twitter
go

use twitter
go

create table pictures
(
	idPicture int primary key identity(1,1),
	fileNames nvarchar(50),
	datas image
);
go

create table users
(
	idUser int primary key identity(1,1),
	username nvarchar(50),
	pass nvarchar(100),
	names nvarchar(50),
	phone nvarchar(50),
	email nvarchar(50),
	idProfilePicture int,
	memberSince DateTime,
	bio nvarchar(160),
	locations nvarchar(50),
	birthDate DateTime,
	foreign key (idProfilePicture) references pictures(idPicture)
);
go

create table tweets
(
	idTweet int primary key identity(1,1),
	idUser int,
	texts nvarchar(280),
	idPicture int,
	likes int,
	dates DateTime,
	foreign key (idUser) references users(idUser),
	foreign key (idPicture) references pictures(idPicture)
);
go

create table comments
(
	idComment int primary key identity(1,1),
	idPost int,
	idUser int,
	texts nvarchar(280),
	likes int,
	dates DateTime,
	foreign key (idPost) references tweets(idTweet),
	foreign key (idUser) references users(idUser)
);
go

create table trends
(
	idTrend int primary key identity(1,1),
	hashtag nvarchar(50)
);
go

create table notifications
(
	idNotification int primary key identity(1,1),
	idUser int,
	title nvarchar(50),
	idPost int,
	foreign key (idUser) references users(idUser),
	foreign key (idPost) references tweets(idTweet)
);
go

create table followers
(
	idFollower int,
	idFollowing int,
	foreign key (idFollower) references users(idUser),
	foreign key (idFollowing) references users(idUser)
);
go

create table savedPosts
(
	idUser int,
	idPost int,
	foreign key (idUser) references users(idUser),
	foreign key (idPost) references tweets(idTweet)
);
go