CREATE TABLE highschool(
id_school int IDENTITY(1,1) PRIMARY KEY NOT NULL,
name varchar(80) NOT NULL,
street varchar(80) NOT NULL,
city varchar(80) NOT NULL,
psc varchar(7) NOT NULL
);
CREATE TABLE student(
id_card varchar(9) PRIMARY KEY NOT NULL,
first_name varchar(80) NOT NULL,
last_name varchar(80) NOT NULL,
email varchar(80) NOT NULL,
street varchar(80) NOT NULL,
city varchar(80) NOT NULL,
psc varchar(7) NOT NULL
);
CREATE TABLE study_program(
id_program int IDENTITY(1,1) PRIMARY KEY NOT NULL,
id_school int NOT NULL,
name varchar(80) NOT NULL,
description varchar(255) NOT NULL,
capacity int NOT NULL
CONSTRAINT FK_ProgramSchool FOREIGN KEY (id_school) REFERENCES highschool(id_school)
);
CREATE TABLE application_form(
id_form int IDENTITY(1,1) PRIMARY KEY NOT NULL,
id_student int NOT NULL,
date_submit DATETIME NOT NULL
);
CREATE TABLE program_application(
id_form int NOT NULL,
id_program int NOT NULL,
CONSTRAINT FK_formpa FOREIGN KEY (id_form) REFERENCES application_form(id_form),
CONSTRAINT FK_programpa FOREIGN KEY (id_program) REFERENCES study_program(id_program)
);

