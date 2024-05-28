DROP TABLE IF EXISTS post_tag;
DROP TABLE IF EXISTS tag;
DROP TABLE IF EXISTS post_media;
DROP TABLE IF EXISTS media;
DROP TABLE IF EXISTS post;
DROP TABLE IF EXISTS user_authentication;
DROP TABLE IF EXISTS users;
DROP FUNCTION IF EXISTS update_row_modification_date;

CREATE OR REPLACE FUNCTION update_row_modification_date() 
RETURNS TRIGGER AS $$
BEGIN NEW.modification_date = now();
RETURN NEW;
END;
$$ language 'plpgsql';

CREATE TABLE users (
                      user_id BIGSERIAL PRIMARY KEY,
                      display_name TEXT NOT NULL UNIQUE,
                      email TEXT NOT NULL UNIQUE,
                      creation_date TIMESTAMPTZ NOT NULL DEFAULT NOW(),
                      modification_date TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

CREATE TRIGGER trigger__update__user__modification_date
    BEFORE UPDATE ON users
    FOR EACH ROW EXECUTE PROCEDURE update_row_modification_date();


CREATE TABLE user_authentication (
                                     user_authentication_id BIGSERIAL PRIMARY KEY,
                                     user_id BIGINT NOT NULL REFERENCES users,
                                     hash TEXT NOT NULL,
                                     hash_algorithm TEXT NOT NULL,
                                     hash_algorithm_parameters TEXT NULL,
                                     creation_date TIMESTAMPTZ NOT NULL DEFAULT NOW(),
                                     modification_date TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

CREATE TRIGGER trigger__update__user_authentication__modification_date
    BEFORE UPDATE ON user_authentication
    FOR EACH ROW EXECUTE PROCEDURE update_row_modification_date();


CREATE TABLE post (
                      post_id BIGSERIAL PRIMARY KEY,
                      content TEXT NOT NULL,
                      user_id BIGINT NOT NULL REFERENCES users,
                      creation_date TIMESTAMPTZ NOT NULL DEFAULT NOW(),
                      modification_date TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

CREATE TRIGGER trigger__update__post__modification_date
    BEFORE UPDATE ON post
    FOR EACH ROW EXECUTE PROCEDURE update_row_modification_date();

CREATE TABLE media (
                       media_id BIGSERIAL PRIMARY KEY,
                       hash TEXT NOT NULL,
                       hash_algorithm TEXT NOT NULL,
                       hash_algorithm_parameters TEXT NULL,
                       media_type TEXT NOT NULL,
                       media_url TEXT NOT NULL,
                       creation_date TIMESTAMPTZ NOT NULL DEFAULT NOW(),
                       modification_date TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

CREATE TRIGGER trigger__update__media__modification_date
    BEFORE UPDATE ON media
    FOR EACH ROW EXECUTE PROCEDURE update_row_modification_date();

CREATE TABLE post_media (
                            id BIGSERIAL PRIMARY KEY,
                            media_id BIGINT NOT NULL REFERENCES media,
                            post_id BIGINT NOT NULL REFERENCES post,
                            creation_date TIMESTAMPTZ NOT NULL DEFAULT NOW(),
                            modification_date TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

CREATE TRIGGER trigger__update__post_media__modification_date
    BEFORE UPDATE ON post_media
    FOR EACH ROW EXECUTE PROCEDURE update_row_modification_date();

CREATE TABLE tag (
                     tag_id BIGSERIAL PRIMARY KEY,
                     tag_name TEXT NOT NULL UNIQUE,
                     creation_date TIMESTAMPTZ NOT NULL DEFAULT NOW(),
                     modification_date TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

CREATE TRIGGER trigger__update__tag__modification_date
    BEFORE UPDATE ON tag
    FOR EACH ROW EXECUTE PROCEDURE update_row_modification_date();

CREATE TABLE post_tag (
                          id BIGSERIAL PRIMARY KEY,
                          tag_id BIGINT NOT NULL REFERENCES tag ON DELETE RESTRICT,
                          post_id BIGINT NOT NULL REFERENCES post ON DELETE CASCADE,
                          creation_date TIMESTAMPTZ NOT NULL DEFAULT NOW(),
                          modification_date TIMESTAMPTZ NOT NULL DEFAULT NOW()
);

CREATE TRIGGER trigger__update__post_tag__modification_date
    BEFORE UPDATE ON post_tag
    FOR EACH ROW EXECUTE PROCEDURE update_row_modification_date();