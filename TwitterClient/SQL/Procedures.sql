use twitter
go

create procedure getPicture
(
	@idProfilePicture int,
	@hasError bit out
)
as
begin try
	set @haserror = 0;
	select * from pictures where idPicture = @idProfilePicture
end try
begin catch
	set @haserror = 1;
end catch
go

create procedure savePicture
(
	@fileName nvarchar(50),
	@data image,
	@hasError bit out
)
as
begin try
	set @haserror = 0;
	insert into pictures values (@fileName, @data)
end try
begin catch
	set @haserror = 1;
end catch
go

create procedure post
(
	@idUser int,
	@text nvarchar(280),
	@idPicture int,
	@likes int,
	@date DateTime,
	@hasError bit out
)
as
begin try
	set @hasError = 0;
	insert into tweets values (@idUser, @text, @idPicture, @likes, @date)
end try
begin catch
	set @hasError = 1;
end catch
go

create procedure comment
(
	@idPost int,
	@idUser int,
	@text nvarchar(280),
	@likes int,
	@date DateTime,
	@hasError bit out
)
as
begin try
	set @hasError = 0;
	insert into comments values(@idPost, @idUser, @text, @likes, @date)
end try
begin catch
	set @hasError = 1;
end catch
go

create procedure follow
(
	@idFollower int,
	@idFollowing int,
	@hasError bit out
)
as
begin try
	set @hasError = 0;
	if exists(select top 1 1 from followers where idFollower = @idFollower and idFollowing = @idFollowing)
	begin
		set @hasError = 1;
	end
	else
	begin
		insert into followers values (@idFollower, @idFollowing)
	end
end try
begin catch
	set @hasError = 1;
end catch
go

create procedure unfollow
(
	@idFollower int,
	@idFollowing int,
	@hasError bit out
)
as
begin try
	set @hasError = 0;
	if exists(select top 1 1 from followers where idFollower = @idFollower and idFollowing = @idFollowing)
	begin
		delete from followers where idFollower = @idFollower and idFollowing = @idFollowing
	end
	else
		set @hasError = 1;
end try
begin catch
	set @hasError = 1;
end catch
go

create procedure savePost
(
	@idUser int,
	@idPost int,
	@hasError bit out
)
as
begin try
	set @hasError = 0;
	if exists(select top 1 1 from savedPosts where idUser = @idUser and idPost = @idPost)
	begin
		set @hasError = 1;
	end
	else
		insert into savedPosts values (@idUser, @idPost)
end try
begin catch
	set @hasError = 1;
end catch
go

create procedure getNotifications
(
	@idUser int,
	@hasError bit out
)
as
begin try
	set @hasError = 0;
	select * from notifications where idUser = @idUser
end try
begin catch
	set @hasError = 1;
end catch
go

create procedure notify
(
	@idUser int,
	@title nvarchar(50),
	@idPost int,
	@hasError bit out
)
as
begin try
	set @hasError = 0;
	insert into notifications values(@idUser,@title, @idPost)
end try
begin catch
	set @hasError = 1;
end catch
go

create procedure getPosts
(
	@idUser int,
	@hasError bit out
)
as
begin try
	set @hasError = 0;
	select * from tweets where idUser = @idUser
end try
begin catch
	set @hasError = 1;
end catch
go

create procedure getComments
(
	@idPost int,
	@hasError bit out
)
as
begin try
	set @hasError = 0;
	select * from comments where idPost = @idPost
end try
begin catch
	set @hasError = 1;
end catch
go

create procedure getFollowers
(
	@idUser int,
	@hasError bit out
)
as
begin try
	set @hasError = 0;
	select * from users
	join followers f on f.idFollowing = users.idUser
	where f.idFollower = @idUser
end try
begin catch
	set @hasError = 1;
end catch
go

create procedure getFollowing
(
	@idUser int,
	@hasError bit out
)
as
begin try
	set @hasError = 0;
	select * from users
	join followers f on f.idFollower = users.idUser
	where f.idFollowing = @idUser
end try
begin catch
	set @hasError = 1;
end catch
go

create procedure getPost
(
	@idPost int,
	@hasError bit out
)
as
begin try
	set @hasError = 0;
	select * from tweets where idTweet = @idPost
end try
begin catch
	set @hasError = 1;
end catch
go

create procedure getUserById
(
	@idUser int,
	@hasError bit out
)
as
begin try
	set @hasError = 0;
	select * from users where idUser = @idUser
end try
begin catch
	set @hasError = 1;
end catch
go

create procedure getUser
(
	@username nvarchar(50),
	@password nvarchar(100),
	@hasError bit out
)
as
begin try
	set @hasError = 0;
	select * from users where username = @username and pass = @password
end try
begin catch
	set @hasError = 1;
end catch
go

create procedure getSavedPosts
(
	@idUser int,
	@hasError bit out
)
as
begin try
	set @hasError = 0;
	select * from tweets
	join savedPosts sp on idTweet = sp.idPost
	where sp.idUser = @idUser
end try
begin catch
	set @hasError = 1;
end catch
go

create procedure verifyUser
(
	@username nvarchar(50),
	@email nvarchar(50),
	@haserror bit out
)
as
set @haserror = 1
begin try
if exists(select top 1 1 from users where username = @username or email = @email)
begin
	set @haserror = 0
end
end try
begin catch
	set @haserror = 1;
end catch
go

create procedure addUser
(
	@username nvarchar(50),
	@password nvarchar(100),
	@names nvarchar(50),
	@phone nvarchar(50),
	@email nvarchar(50),
	@memberSince DateTime,
	@bio nvarchar(160),
	@locations nvarchar(50),
	@birthDate DateTime,
	@haserror bit out
)
as
set @haserror = 1
begin try
if not exists(select top 1 1 from users where username = @username AND email = @email)
begin
	set @haserror = 0;
	insert into users
	values
	(@username,@password,@names,@phone,@email,null,@memberSince,@bio,@locations,@birthDate)
end
end try
begin catch
	set @haserror = 1;
end catch
go