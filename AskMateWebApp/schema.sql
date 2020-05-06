CREATE EXTENSION IF NOT EXISTS pgcrypto WITH SCHEMA public;

DROP VIEW IF EXISTS v_user;
DROP TABLE IF EXISTS comment;
DROP TABLE IF EXISTS question_tag;
DROP TABLE IF EXISTS tag;
DROP TABLE IF EXISTS answer;
DROP TABLE IF EXISTS question;
DROP TABLE IF EXISTS "user";
DROP FUNCTION IF EXISTS set_question_id_for_answer_comment();

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
    user_id INTEGER NOT NULL REFERENCES "user"(id),
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
    user_id INTEGER NOT NULL REFERENCES "user"(id),
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
    question_id INTEGER NOT NULL REFERENCES question(id),
    answer_id INTEGER DEFAULT NULL REFERENCES answer(id),
    message TEXT DEFAULT NULL,
    submission_time TIMESTAMP WITHOUT TIME ZONE DEFAULT NOW(),
    edited_number INTEGER DEFAULT 0,
    user_id INTEGER NOT NULL REFERENCES "user"(id)
);

CREATE VIEW v_user AS
SELECT
    u.*,
    COUNT(DISTINCT q.id) AS question_count,
    COUNT(DISTINCT a.id) AS answer_count,
    COUNT(DISTINCT c.id) AS comment_count
FROM "user" AS u
LEFT JOIN question AS q ON q.user_id = u.id
LEFT JOIN answer AS a ON a.user_id = u.id
LEFT JOIN comment AS c ON c.user_id = u.id
GROUP BY u.id
ORDER BY u.id;

CREATE FUNCTION set_question_id_for_answer_comment() RETURNS TRIGGER AS $$
BEGIN
    IF NEW.question_id IS NULL THEN
        NEW.question_id := (SELECT question_id FROM answer WHERE id = NEW.answer_id);
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION add_question_tag(p_user_id INTEGER, p_question_id INTEGER, p_tag_ids INTEGER[]) RETURNS VOID AS $$
DECLARE
    user_id INTEGER;
BEGIN
    SELECT q.user_id FROM question AS q WHERE q.id = p_question_id INTO user_id;
    IF user_id <> p_user_id THEN
        RAISE EXCEPTION 'Not authorized' USING ERRCODE = 45000;
    END IF;
    INSERT INTO question_tag (question_id, tag_id) VALUES (p_question_id, unnest(p_tag_ids));
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION update_question(p_user_id INTEGER, p_question_id INTEGER, p_title TEXT, p_message TEXT, p_image TEXT) RETURNS VOID AS $$
DECLARE
    user_id INTEGER;
BEGIN
    SELECT q.user_id FROM question AS q WHERE q.id = p_question_id INTO user_id;
    IF user_id <> p_user_id THEN
        RAISE EXCEPTION 'Not authorized' USING ERRCODE = 45000;
    END IF;
    UPDATE
        question
    SET
        title = p_title,
        message = p_message,
        image = p_image
    WHERE
        id = p_question_id AND
        user_id = p_user_id;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION vote_question(p_user_id INTEGER, p_question_id INTEGER, p_votes INTEGER) RETURNS VOID AS $$
DECLARE
    user_id INTEGER;
BEGIN
    SELECT q.user_id FROM question AS q WHERE q.id = p_question_id INTO user_id;
    IF user_id = p_user_id THEN
        RAISE EXCEPTION 'Cannot vote on owned entity' USING ERRCODE = 45001;
    END IF;
    UPDATE question SET vote_number = vote_number + p_votes WHERE id = p_question_id;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION delete_question(p_user_id INTEGER, p_question_id INTEGER) RETURNS VOID AS $$
DECLARE
    user_id INTEGER;
BEGIN
    SELECT q.user_id FROM question AS q WHERE q.id = p_question_id INTO user_id;
    IF user_id <> p_user_id THEN
        RAISE EXCEPTION 'Not authorized' USING ERRCODE = 45000;
    END IF;
    DELETE FROM question WHERE id = p_question_id;
END;
$$ LANGUAGE plpgsql;

CREATE OR REPLACE FUNCTION delete_comment(p_user_id INTEGER, p_comment_id INTEGER) RETURNS VOID AS $$
DECLARE
    user_id INTEGER;
BEGIN
    SELECT c.user_id FROM comment AS c WHERE c.id = p_comment_id INTO user_id;
    IF user_id <> p_user_id THEN
        RAISE EXCEPTION 'Not authorized' USING ERRCODE = 45000;
    END IF;
    DELETE FROM comment WHERE id = p_comment_id;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER set_question_id_for_answer_comment_trigger
BEFORE INSERT ON comment
FOR EACH ROW EXECUTE FUNCTION set_question_id_for_answer_comment();
