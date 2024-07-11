USE taskboard;
UPDATE `tasks`
SET `Title`='Edited task', `Description`='Edited task description'
WHERE `Id`=5;

SELECT `OwnerId` FROM `tasks`
WHERE `Id`=5;


SELECT * FROM `tasks`
WHERE `OwnerId`='63ea3995-8b23-4eef-b7e2-49ab80333a09';