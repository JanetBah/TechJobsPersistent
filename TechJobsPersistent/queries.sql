--Part 1
SELECT * FROM jobs

--Part 2
SELECT * FROM Employers where employers.Location = "St Louis";
insert into employers (Name, Location) values ("exScripts", "St Louis");

--Part 3
SELECT Name, Description FROM skills
WHERE name  is not null
order by name ASC;

