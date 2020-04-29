DROP TABLE IF EXISTS comment;
DROP TABLE IF EXISTS question_tag;
DROP TABLE IF EXISTS tag;
DROP TABLE IF EXISTS answer;
DROP TABLE IF EXISTS question;

CREATE TABLE question (
  id SERIAL PRIMARY KEY,
  submission_time TIMESTAMP DEFAULT NOW(),
  view_number INTEGER DEFAULT 0,
  vote_number INTEGER DEFAULT 0,
  title TEXT DEFAULT NULL,
  message TEXT DEFAULT NULL,
  image TEXT DEFAULT NULL
);

CREATE TABLE answer (
  id SERIAL PRIMARY KEY,
  question_id INTEGER NOT NULL REFERENCES question(id),
  submission_time TIMESTAMP DEFAULT NOW(),
  vote_number INTEGER DEFAULT 0,
  message TEXT DEFAULT NULL,
  image TEXT DEFAULT NULL
);

CREATE TABLE tag (
  id SERIAL PRIMARY KEY,
  name TEXT NOT NULL
);

CREATE TABLE question_tag (
  question_id INTEGER NOT NULL REFERENCES question(id),
  tag_id INTEGER NOT NULL REFERENCES tag(id)
);

CREATE TABLE comment (
  id SERIAL PRIMARY KEY,
  question_id INTEGER DEFAULT NULL REFERENCES question(id),
  answer_id INTEGER DEFAULT NULL REFERENCES answer(id),
  message TEXT DEFAULT NULL,
  submission_time TIMESTAMP DEFAULT NOW(),
  edited_number INTEGER DEFAULT 0
);
