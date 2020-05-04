-- https://www.postgresql.org/docs/12/textsearch-tables.html#TEXTSEARCH-TABLES-INDEX
ALTER TABLE question ADD COLUMN document TSVECTOR
GENERATED ALWAYS AS (
    setweight(to_tsvector('english', coalesce(title, '')), 'A') ||
    setweight(to_tsvector('english', coalesce(message, '')), 'B')
) STORED;

ALTER TABLE answer ADD COLUMN document TSVECTOR
GENERATED ALWAYS AS (
    to_tsvector('english', coalesce(message, ''))
) STORED;


