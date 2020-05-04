CREATE EXTENSION IF NOT EXISTS pgcrypto WITH SCHEMA public;

DROP TABLE IF EXISTS comment;
DROP TABLE IF EXISTS question_tag;
DROP TABLE IF EXISTS tag;
DROP TABLE IF EXISTS answer;
DROP TABLE IF EXISTS question;
DROP TABLE IF EXISTS "user";

CREATE TABLE "user" (
    id SERIAL PRIMARY KEY,
    username TEXT UNIQUE NOT NULL,
    password BYTEA NOT NULL,
    registration_time TIMESTAMP WITHOUT TIME ZONE DEFAULT NOW()
);

CREATE TABLE question (
    id SERIAL PRIMARY KEY,
    submission_time TIMESTAMP WITHOUT TIME ZONE DEFAULT NOW(),
    view_number INTEGER DEFAULT 0,
    vote_number INTEGER DEFAULT 0,
    title TEXT DEFAULT NULL,
    message TEXT DEFAULT NULL,
    image TEXT DEFAULT NULL,
    user_id INTEGER REFERENCES "user"(id),
    document TSVECTOR GENERATED ALWAYS AS (
        setweight(to_tsvector('english', coalesce(title, '')), 'A') ||
        setweight(to_tsvector('english', coalesce(message, '')), 'B')
    ) STORED
);

CREATE INDEX question_fts_idx ON question USING GIN (document);

CREATE TABLE answer (
    id SERIAL PRIMARY KEY,
    question_id INTEGER NOT NULL REFERENCES question(id),
    submission_time TIMESTAMP WITHOUT TIME ZONE DEFAULT NOW(),
    vote_number INTEGER DEFAULT 0,
    message TEXT DEFAULT NULL,
    image TEXT DEFAULT NULL,
    user_id INTEGER REFERENCES "user"(id),
    document TSVECTOR GENERATED ALWAYS AS (
        to_tsvector('english', coalesce(message, ''))
    ) STORED
);

CREATE INDEX answer_fts_idx ON answer USING GIN (document);

CREATE TABLE tag (
    id SERIAL PRIMARY KEY,
    name TEXT UNIQUE NOT NULL
);

CREATE TABLE question_tag (
    question_id INTEGER NOT NULL REFERENCES question(id),
    tag_id INTEGER NOT NULL REFERENCES tag(id),
    PRIMARY KEY (question_id, tag_id)
);

CREATE TABLE comment (
    id SERIAL PRIMARY KEY,
    question_id INTEGER DEFAULT NULL REFERENCES question(id),
    answer_id INTEGER DEFAULT NULL REFERENCES answer(id),
    message TEXT DEFAULT NULL,
    submission_time TIMESTAMP WITHOUT TIME ZONE DEFAULT NOW(),
    edited_number INTEGER DEFAULT 0,
    user_id INTEGER REFERENCES "user"(id)
);
