drop table if exists KommuneData;
create table KommuneData (
	id int identity(1,1) primary key not null,
	KommuneCode int foreign key references Kommune(Code) not null,
	age_0_17 real not null,
	age_17_64 real not null,
	age_65 real not null,
);



insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (161,19.8,62.9,18.4);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (253,21.9,58.0,21.4);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (259,20.2,60.6,20.4);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (265,20.6,60.6,20.2);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (269,23.6,57.9,20.0);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (306,15.3,53.3,32.6);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (316,20.2,59.2,22.0);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (320,18.7,59.2,23.4);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (326,18.2,57.6,25.5);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (329,20.1,62.3,18.9);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (330,18.4,60.1,22.7);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (336,18.7,56.8,25.8);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (340,20.4,58.8,22.2);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (350,22.0,56.7,22.6);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (360,15.5,54.6,30.9);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (370,19.0,59.5,22.7);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (376,16.8,56.4,27.9);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (390,16.6,55.9,28.7);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (410,19.8,57.4,24.1);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (420,19.4,57.4,24.4);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (430,19.7,56.4,25.1);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (440,18.7,56.0,26.4);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (450,18.8,57.1,25.4);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (461,18.2,65.1,17.8);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (479,19.5,56.7,25.1);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (480,19.2,57.7,24.4);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (482,13.2,50.9,36.8);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (492,13.8,49.9,37.2);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (510,19.1,58.1,24.1);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (530,20.6,58.9,21.8);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (540,18.5,57.4,25.3);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (550,18.6,55.8,26.8);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (561,19.5,59.6,22.0);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (563,16.9,46.6,37.6);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (573,20.1,57.2,23.9);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (575,20.8,58.6,21.9);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (580,19.3,57.1,24.8);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (607,19.2,60.2,21.7);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (621,20.7,60.6,19.9);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (630,21.3,60.6,19.4);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (615,21.1,61.4,18.7);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (657,20.8,59.9,20.6);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (661,21.2,58.5,21.6);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (665,17.5,55.8,28.0);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (671,18.2,55.4,27.7);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (706,19.7,55.1,26.3);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (707,17.3,57.6,26.5);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (710,23.9,58.1,19.5);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (727,20.7,56.0,24.6);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (730,19.5,60.5,21.1);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (740,22.0,59.2,20.2);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (741,15.3,50.0,35.5);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (746,25.2,57.4,18.8);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (751,17.8,68.0,15.2);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (756,21.4,59.1,20.9);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (760,20.2,57.4,23.8);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (766,21.0,59.3,21.1);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (779,18.3,57.3,25.6);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (791,20.9,58.9,21.5);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (773,18.4,55.5,27.4);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (787,19.5,56.6,25.2);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (810,21.3,57.3,22.9);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (813,17.2,55.9,28.1);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (820,19.4,56.8,25.1);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (825,12.5,47.6,41.0);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (840,23.8,57.1,20.5);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (846,18.8,57.7,24.7);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (849,19.8,55.9,25.6);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (851,18.0,64.8,18.3);
insert into KommuneData (KommuneCode, age_0_17, age_17_64, age_65) values (860,18.8,57.3,25.2);
