insert into Account(AccountId,FullName,UserName,Email,PhoneNumber,Password,IsAdmin,Picture,IsActive) values (NEWID() ,'Khuc Huy Loc','Admin','Loc@gmail.com','09382372783',lower(CONVERT(VARCHAR(32),HashBytes('md5','1234'),2)),1,'testimonial-2.jpg',1)

select * from Account

insert into CategoryPet(species) values
(N'Chó'),
(N'Mèo'),
(N'Chim'),
(N'Sóc'),
(N'Thỏ')


INSERT INTO Pet(name, Images, breed, age, price, quantity, description,CategoryPetId) 
VALUES 
(N'Chó AlasKa', 'Cho1.jpg', N'Giống Nga', 1, 10000000, 10, '',1),
(N'Chó Corgi', 'cho2.jpg', N'Giống Mập Lùn', 1, 8000000, 10, '',1),
(N'Mèo Trắng vàng', 'meo1.jpg', N'Giống ta', 1, 2000000, 10, '',2),
(N'Mèo đen trắng', 'meo2.jpg', N'Giống Nga', 1, 10000000, 10, '',2),
(N'Thỏ trắng', 'tho1.jpg', N'Giống Tạp Lai', 1, 5000000, 10, '',4),
(N'Thỏ Vàng', 'tho2.jpg', N'Giống Tạp Lai', 1, 6000000, 10, '',4);	