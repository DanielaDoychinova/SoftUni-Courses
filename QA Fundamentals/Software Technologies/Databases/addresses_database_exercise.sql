CREATE DATABASE IF NOT EXISTS `address_database`;
USE `address_database`;

CREATE TABLE `addresses`(
street_number varchar(20),
street_name varchar(80),
area varchar(80),
town varchar(80),
district varchar(80),
country varchar(50),
continent varchar(50)
);

alter table `addresses`
add column isCapital boolean;

insert into addresses (street_number, street_name, area, town, district, country, continent, isCapital)
values
 ('10', 'Main Street', 'Downtown', 'New York', 'Manhattan', 'United States', 'North America', false),
 ('25A' , 'Elm Avenue', 'West End', 'London', 'Westminster', 'United Kingdom', 'Europe', true),
 ('7', 'Rue de la Paix', 'Le Marais', 'Paris', 'Ile-de-France', 'France', 'Europe', true),
 ('1234', 'Oak Lane', 'Green Meadows', 'Los Angeles', 'California', 'United States', 'North America', false),
 ('42', 'High Street', 'City Center', 'Sydney', 'New South Wales', 'Australia', 'Australia & Oceania', true),
 ('568', 'Maple Road', 'Northside', 'Toronto', 'Ontario', 'Canada', 'North America', false),
 ('9', 'Kaiserstrasse', 'Mitte', 'Berlin', 'Berlin', 'Germany', 'Europe', true),
 ('17', 'Plaza Mayor', 'Centro', 'Madrid', 'Madrid', 'Spain', 'Europe', true),
 ('3', 'Piazza San Marco', 'San Marco', 'Venice', 'Veneto', 'Italy', 'Europe', false),
 ('1001', 'Avenida Paulista', 'Jardins', 'Sao Paulo', 'Sao Paulo', 'Brazil', 'South America', false),
 ('8/15', 'Princes Street', 'New Town', 'Edinburgh', 'Edinburgh', 'United Kingdom', 'Europe', false),
 ('27', 'Koningsplein', 'Centrum', 'Amsterdam', 'North Holland', 'Netherlands', 'Europe', true),
 ('42A', 'Hauptstrasse', 'Mitte', 'Vienna', 'Vienna', 'Austria', 'Europe', true),
 ('100', 'Collins Street', 'CBD', 'Melbourne', 'Victoria', 'Australia', 'Oceania', false),
 ('123', 'Rua da Boavista', 'Baixa', 'Porto', 'Porto', 'Portugal', 'Europe' , false),
 ('5', 'Knez Mihailova', 'Stari Grad', 'Belgrade', 'Belgrade', 'Serbia', 'Europe' , true),
 ('9876', 'Ginza', 'Chuo', 'Tokyo', 'Tokyo', 'Japan', 'Asia', true),
 ('18', 'Connell Street', 'North City', 'Dublin', 'Dublin', 'Ireland', 'Europe' , true), 
 ('75C', 'Friedrichstrasse', 'Kreuzberg', 'Berlin', 'Berlin', 'Germany', 'Europe', true),
 ('22', 'Gran Vía', 'Malasana', 'Madrid', 'Madrid','Spain', 'Europe', true);


ALTER TABLE `addresses`
ADD COLUMN isCapital BOOLEAN;

SELECT * FROM addresses WHERE isCapital = true;

SELECT * FROM addresses WHERE continent = 'South America';

SELECT * FROM addresses
WHERE continent = 'Europe' and isCapital = false;