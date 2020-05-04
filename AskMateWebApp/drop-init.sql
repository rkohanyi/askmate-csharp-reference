DROP TABLE IF EXISTS comment;
DROP TABLE IF EXISTS question_tag;
DROP TABLE IF EXISTS tag;
DROP TABLE IF EXISTS answer;
DROP TABLE IF EXISTS question;
DROP TABLE IF EXISTS "user";
DROP FUNCTION IF EXISTS generate_password;
DROP FUNCTION IF EXISTS assign_random_user_id_to_question;

CREATE OR REPLACE FUNCTION generate_password() RETURNS TRIGGER AS $$
BEGIN
    IF NEW.password IS NULL THEN
        NEW.password := digest(NEW.username, 'sha512');
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TABLE "user" (
  id SERIAL PRIMARY KEY,
  username TEXT UNIQUE NOT NULL,
  password BYTEA UNIQUE NOT NULL,
  registration_time TIMESTAMP DEFAULT NOW()
);

CREATE TRIGGER generate_password_trigger BEFORE INSERT ON "user" FOR EACH ROW EXECUTE PROCEDURE generate_password();

CREATE OR REPLACE FUNCTION assign_random_user_id_to_question() RETURNS TRIGGER AS $$
BEGIN
    NEW.user_id := (SELECT id FROM "user" WHERE registration_time < NEW.submission_time ORDER BY random() LIMIT 1);
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TABLE question (
  id SERIAL PRIMARY KEY,
  submission_time TIMESTAMP DEFAULT NOW(),
  view_number INTEGER DEFAULT 0,
  vote_number INTEGER DEFAULT 0,
  title TEXT DEFAULT NULL,
  message TEXT DEFAULT NULL,
  image TEXT DEFAULT NULL,
  user_id INTEGER REFERENCES "user"(id)
);

CREATE TRIGGER assign_random_user_id_to_question_trigger BEFORE INSERT ON question FOR EACH ROW EXECUTE PROCEDURE assign_random_user_id_to_question();

CREATE OR REPLACE FUNCTION assign_random_user_id_to_answer() RETURNS TRIGGER AS $$
BEGIN
    NEW.user_id := (SELECT id FROM "user" WHERE registration_time < NEW.submission_time ORDER BY random() LIMIT 1);
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TABLE answer (
  id SERIAL PRIMARY KEY,
  question_id INTEGER NOT NULL REFERENCES question(id),
  submission_time TIMESTAMP DEFAULT NOW(),
  vote_number INTEGER DEFAULT 0,
  message TEXT DEFAULT NULL,
  image TEXT DEFAULT NULL,
  user_id INTEGER REFERENCES "user"(id)
);

CREATE TRIGGER assign_random_user_id_to_answer_trigger BEFORE INSERT ON answer FOR EACH ROW EXECUTE PROCEDURE assign_random_user_id_to_answer();

CREATE TABLE tag (
  id SERIAL PRIMARY KEY,
  name TEXT UNIQUE NOT NULL
);

CREATE TABLE question_tag (
  question_id INTEGER NOT NULL REFERENCES question(id),
  tag_id INTEGER NOT NULL REFERENCES tag(id),
  PRIMARY KEY (question_id, tag_id)
);

CREATE OR REPLACE FUNCTION assign_random_user_id_to_comment() RETURNS TRIGGER AS $$
BEGIN
    NEW.user_id := (SELECT id FROM "user" WHERE registration_time < NEW.submission_time ORDER BY random() LIMIT 1);
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TABLE comment (
  id SERIAL PRIMARY KEY,
  question_id INTEGER DEFAULT NULL REFERENCES question(id),
  answer_id INTEGER DEFAULT NULL REFERENCES answer(id),
  message TEXT DEFAULT NULL,
  submission_time TIMESTAMP DEFAULT NOW(),
  edited_number INTEGER DEFAULT 0,
  user_id INTEGER REFERENCES "user"(id)
);

CREATE TRIGGER assign_random_user_id_to_comment_trigger BEFORE INSERT ON comment FOR EACH ROW EXECUTE PROCEDURE assign_random_user_id_to_comment();
