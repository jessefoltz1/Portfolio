-- Check for existing copy of database and junk/rebuild if so
USE master;
GO
DROP DATABASE IF EXISTS CarRepairTracker;
GO
CREATE DATABASE CarRepairTracker;
GO
USE CarRepairTracker
GO

-- Set structure/default data for database
BEGIN TRANSACTION;

CREATE TABLE [role] (
	id integer identity,
	name varchar(32) NOT NULL,

	CONSTRAINT pk_role PRIMARY KEY (id)
);

CREATE TABLE [user] (
	id integer identity,
	role_id integer NOT NULL,
	username varchar(32) NOT NULL UNIQUE,
	first_name varchar(64) NOT NULL,
	last_name varchar(64) NOT NULL,
	email varchar(64) NOT NULL UNIQUE,
	phone_number varchar(14) NOT NULL,
	hash varchar(64) NOT NULL,
	salt varchar(64) NOT NULL,

	CONSTRAINT pk_user PRIMARY KEY (id),
	CONSTRAINT fk_user_role FOREIGN KEY (role_id) REFERENCES [role] (id)
);

CREATE TABLE [vehicle] (
	id integer identity,
	vin varchar(17) NOT NULL,
	user_id integer NOT NULL,
	make varchar(32) NOT NULL,
	model varchar(32) NOT NULL,
	year integer NOT NULL,
	color varchar(32) NOT NULL,

	CONSTRAINT pk_vehicle PRIMARY KEY (id),
	CONSTRAINT fk_vehicle_user FOREIGN KEY (user_id) REFERENCES [user] (id),
	CONSTRAINT chk_year CHECK (year >= 1885)
);

CREATE TABLE [incident] (
	id integer identity,
	vehicle_id integer NOT NULL,
	description varchar(1000) NOT NULL,
	submitted_date Date NOT NULL,
	pickup_date Date,
	paid bit NOT NULL,
	completed bit NOT NULL,

	CONSTRAINT pk_incident PRIMARY KEY (id),
	CONSTRAINT fk_incident_vehicle FOREIGN KEY (vehicle_id) REFERENCES [vehicle] (id)
);

CREATE TABLE [incident_itemizated] (
	id integer identity,
	incident_id integer NOT NULL,
	description varchar(200) NOT NULL,
	cost decimal NOT NULL,
	time_hours integer NOT NULL,
	approved bit NOT NULL,
	declined bit NOT NULL,

	CONSTRAINT pk_incident_itemizated PRIMARY KEY (id),
	CONSTRAINT fk_incident_itemizated_incident FOREIGN KEY (incident_id) REFERENCES [incident] (id)
);

-- Default roles for the database
INSERT INTO [role] (name) VALUES ('Administrator');
INSERT INTO [role] (name) VALUES ('Employee');
INSERT INTO [role] (name) VALUES ('Customer');

-- One Users for each Roles, User required for vehicle (FK)
INSERT INTO [user] (username, role_id, first_name, last_name, email, phone_number, hash, salt)
VALUES ('Admin', 1, 'White Hats', 'Only Please', 'admin@techelevator.com', '999-999-9999', 'Rv1z7cWmGETlcSU1eG58NrR9f9Q=', 'a+g3+0EAjHCXpcdSgC6vaw==');

INSERT INTO [user] (username, role_id, first_name, last_name, email, phone_number, hash, salt)
VALUES ('cwolf', 2, 'Charles', 'Wolf', 'cwolf@gmail.com', '513-891-6347', 'dQbPi178Jv4V4mbsGezdr9HY8WI=', 'K8vIAFX1LRGzCuvaJ28tXQ==');

INSERT INTO [user] (username, role_id, first_name, last_name, email, phone_number, hash, salt)
VALUES ( 'JMT', 3, 'Jason','Thomas', 'jasothomas@gmail.com', '513-703-3057', '5fMRmGp3bUpatpJKx7qeRkxzFlg=', 'xz+2WXg81d3iJh/Mjo5jgQ==');

INSERT INTO [user] (username, role_id, first_name, last_name, email, phone_number, hash, salt)
VALUES ( 'KSchott', 3, 'Kevin','Schott', 'KSchott@msn.com', '513-891-6349', 'ghCpvCAZ9zFiWezLOi+MQno38+A=', '26F1+jgOZWovC7YTo3qHQQ==');

INSERT INTO [user] (username, role_id, first_name, last_name, email, phone_number, hash, salt)
VALUES ('JFoltz', 2, 'Jesse', 'Foltz', 'JFoltz@yahoo.com', '513-123-4567', '38FJJuj33ASIKh5weCvb8WEWsSs=', 'P0DBnTCRhYyaAeJXV1vBMQ==');

-- Default data for ShowCase --

-- Insert Vehicles to start, vehicle required for incident (FK)

INSERT INTO vehicle ( vin, user_id, make, model, year, color)
VALUES ('JH4KA7660NC003110', 3, 'Acura', 'Legend', 2010, 'Red' );

INSERT INTO vehicle ( vin, user_id, make, model, year, color)
VALUES ('1HD1KEM1XDB602203', 4, 'Harley Davidson', 'Flhtk', 2016, 'Black' );

INSERT INTO vehicle ( vin, user_id, make, model, year, color)
VALUES ('JNKBV61E57M729118', 3, 'Infiniti', 'G35', 2007, 'Blue' );

INSERT INTO vehicle ( vin, user_id, make, model, year, color)
VALUES ('JT2BG22K3Y0485107', 4, 'Toyota', 'Camry', 2000, 'Silver' );

--Insert Incident, Incident required for Item Line (FK)

INSERT INTO incident (vehicle_id, description, submitted_date, pickup_date, paid, completed )
VALUES (1, 'Automatic transmission. First below zero morning, suddenly the car would not shift out of park. Ran for approximately 30 minutes and was able to shift if slammed the brake down very hard. Weather temps went up and thought it was a weird glitch. Temps have dropped again and problem has returned. I think there is an issue with the brake light switch.', '2019-12-19', NULL, 0, 0);

INSERT INTO incident (vehicle_id, description, submitted_date, pickup_date, paid, completed )
VALUES (2, 'Had to slam on the breaks to miss a deer and the car stalled. Has also happened a few times before.', '2019-12-18', NULL, 0, 0);

INSERT INTO incident (vehicle_id, description, submitted_date, pickup_date, paid, completed )
VALUES (3, 'Hello, I was driving my car yesterday and while going up a hill I revved the engine really high and than imidiality smelled something burning and lost most of the power to my car. Now it vibrates a lot when idling and has a hard time accelerating and increasing rpms. It now has an error code p0171 which I read means a bad air fuel ratio. I read some possibilities and have some ideas but was wondering what you guys think. One thing I should mention is there is no burning smell now. Only initially right as the car first started acting up.', '2020-01-01', NULL, 0, 0);

INSERT INTO incident (vehicle_id, description, submitted_date, pickup_date, paid, completed )
VALUES (1, 'So, my wife and I were coming home when we experienced these issues, we were driving through a massive rainstorm at the time which lasted for a good 15 minutes of the drive home. We were almost home when all of a sudden, the engine starting working really hard, and the vehicle was losing power. The power steering went out as well. We have to pull into a parking lot. We turned the car off, waited a minute, then started it back up and drove for about 20 seconds, and the same thing happened again.', '2019-12-23', NULL, 0, 0);

INSERT INTO incident (vehicle_id, description, submitted_date, pickup_date, paid, completed )
VALUES (2, 'My car has been losing coolant slowly for months without visible leak anywhere, overheating, oil in coolant, milky oil, sweet smell and bubble in recovery tank. The coolant level in tank droped about 1cm every 40-50km driving in average. There is minor condensation on top of oil fill cap, it reappears few days after I wipe it, but oil from dipstick is clear and clean. After cold start, some damp white smoke and a few drips from tailpipe lasted about 15 minutes and did not reappear after engine completely warmed up. Every time when I loose the recovery tank cap with engine completely cold, I can hear a popping sound that seems like air in coolant system escaping out. The coolant system can hold 18 psi pressure for 5 hours with cold engine. I am at a loss.', '2019-12-21', NULL, 0, 0);

INSERT INTO incident (vehicle_id, description, submitted_date, pickup_date, paid, completed )
VALUES (3, 'I went out and started it this morning like I usually do to let it warm up. I ran out a few minutes later because the car sounded like it was ready to die. Smelled like it was loading up really bad. I shut it off and waited. When I tried starting it up again I had nothing. I have power but no ignition. Car wont even crank over now. We have been having some transmission issues and we have replaced the speed sensor which helped a little bit.. And our speedometer went out as well. Any and all suggestions are welcome. This is the only vehicle I have.', '2020-10-01', NULL, 0, 0);

INSERT INTO incident (vehicle_id, description, submitted_date, pickup_date, paid, completed )
VALUES (1, 'Weird problem — my power steering kicks out when going up or down hill. The pump started making weird noises typical of a failing pump. I had it replaced with a new one. No more noises. It seemed fine. A month or so later, we were driving in the mountains and it kept kicking off, when we were going up or down. It doesn’t seem to happen on small grades. I brought it back to mechanic who warrantied it out and replaced it again about a week ago. Today driving up a steep hill, it kicked off again... the mechanic seemed puzzled last time this happened. I doubt he’ll have any ideas this time.', '2019-11-19', NULL, 0, 0);


--Insert Incident Line Items

INSERT INTO incident_itemizated (incident_id, description, cost, time_hours, approved, declined )
VALUES (1, 'Repair IAC', 100, 1, 0 ,0);

INSERT INTO incident_itemizated (incident_id, description, cost, time_hours, approved, declined )
VALUES (1, 'Replace valves and hoses.', 200, 2, 0 ,0);

INSERT INTO incident_itemizated (incident_id, description, cost, time_hours, approved, declined )
VALUES (2, 'Wheels.', 500, 8, 0 ,0);

INSERT INTO incident_itemizated (incident_id, description, cost, time_hours, approved, declined )
VALUES (2, 'Replace coolant resevour.', 500, 8, 0 ,0);

INSERT INTO incident_itemizated (incident_id, description, cost, time_hours, approved, declined )
VALUES (3, 'Replace air filters.', 200, 2, 0 ,0);

COMMIT TRANSACTION;

--Users:
--UN		/	PW		/	Role
--Admin			pass		Administrator
--JFoltz		1234		Employee
--cwolf			1234		Employee
--JMT			1234		Customer
--KSchott		1234		Customer
