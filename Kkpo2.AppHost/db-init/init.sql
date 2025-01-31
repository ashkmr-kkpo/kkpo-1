-- Postgres init script
CREATE DATABASE IF NOT EXISTS postgresdb;

CREATE TABLE IF NOT EXISTS postgresdb
(
    id INT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    firstName text NOT NULL,
);

-- Create the Todos table
CREATE TABLE IF NOT EXISTS Todos
(
    Id SERIAL PRIMARY KEY,
    Title text UNIQUE NOT NULL,
    IsComplete boolean NOT NULL DEFAULT false
);

-- Insert some sample data into the Todos table
INSERT INTO Todos (Title, IsComplete)
VALUES
    ('Give the dog a bath', false),
    ('Wash the dishes', false),
    ('Do the groceries', false)
ON CONFLICT DO NOTHING;