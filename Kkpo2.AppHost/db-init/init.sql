-- Postgres init script
CREATE DATABASE postgresdb2;
\c postgresdb2
CREATE TABLE IF NOT EXISTS users
(
    user_id uuid PRIMARY KEY DEFAULT gen_random_uuid(),
    firstName text NOT NULL,
    lastName text,
    email text NOT NULL
);

CREATE TABLE IF NOT EXISTS userLocation
(
    user_id uuid REFERENCES users(user_id) ON DELETE CASCADE,
    country text NOT NULL,
    city text NOT NULL,
    zipCode text,
    latitude float,
    longitude float
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