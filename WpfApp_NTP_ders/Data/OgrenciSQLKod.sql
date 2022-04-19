CREATE DATABASE VT2022
GO
USE VT2022
GO
CREATE TABLE [dbo].[Ogrenci]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [okulno] INT NULL, 
    [ad] VARCHAR(50) NOT NULL, 
    [soyad] VARCHAR(50) NOT NULL,
    [sinif] INT NULL, 
)
CREATE TABLE [dbo].[Okul]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [kisa_ad] VARCHAR(20) NULL, 
    [tam_ad] VARCHAR(50) NULL, 
    [mevcut] INT NULL
)
CREATE TABLE [dbo].[Sinif]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [seviye] INT NOT NULL,
    [sube] VARCHAR(3) NOT NULL, 
    [okul] INT NOT NULL
)

ALTER TABLE [dbo].[Ogrenci]
ADD CONSTRAINT [FK_Ogrenci_Sinif] FOREIGN KEY ([sinif]) REFERENCES [Sinif]([id])

ALTER TABLE [dbo].[Sinif]
ADD CONSTRAINT [FK_Sinif_Okul] FOREIGN KEY ([okul]) REFERENCES [Okul]([id])

INSERT INTO Okul(kisa_ad,tam_ad,mevcut) VALUES 
    ('AMP','ANADOLU MESLEK PROGRAMI',500),
    ('ATP','ANADOLU MESLEK PROGRAMI',100),
    ('MESEM','MESLEKi EÐÝTÝM MERKEZÝ',100)

INSERT INTO Sinif(seviye,sube,okul) VALUES 
    (9,'A',1),
    (9,'B',1),
    (9,'C',1),
    (10,'A',1),
    (10,'B',2),
    (10,'C',2),
    (11,'A',3)

INSERT INTO Ogrenci(okulno,ad,soyad,sinif) VALUES
    (100,'Ogrenci','Soyad',1),
    (101,'Ogrenci1','Soyad1',1),
    (102,'Ogrenci2','Soyad2',1),
    (103,'Ogrenci3','Soyad3',2),
    (104,'Ogrenci4','Soyad4',2),
    (105,'Ogrenci5','Soyad5',3),
    (106,'Ogrenci6','Soyad6',3),
    (107,'Ogrenci7','Soyad7',4),
    (108,'Ogrenci8','Soyad8',4),
    (109,'Ogrenci9','Soyad9',5),
    (110,'Ogrenci10','Soyad10',5),
    (111,'Ogrenci11','Soyad11',6)