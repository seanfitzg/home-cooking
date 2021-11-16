CREATE USER homecooking WITH PASSWORD 'homecooking';

-- CREATE DATABASE homecooking; -- created by EF.
CREATE DATABASE dapr;
GRANT ALL PRIVILEGES ON DATABASE homecooking TO homecooking;
GRANT ALL PRIVILEGES ON DATABASE dapr TO homecooking;

-- CREATE TABLE Recipes (
--     Id SERIAL NOT NULL UNIQUE,
--     UserId varchar NOT NULL,
--     Name varchar NULL,
--     Method varchar NULL,
--     Description varchar NULL
-- );

-- CREATE TABLE Ingredients (
--     Id UUID PRIMARY KEY NOT NULL,
--     Item varchar NULL,
--     Amount varchar NULL,
--     RecipeId int NOT NULL references Recipes(Id)
-- );
