#!/bin/bash

psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" --dbname "$POSTGRES_DB" <<-EOSQL
	CREATE USER system WITH PASSWORD 'system';
	CREATE DATABASE systemdb ENCODING 'UTF8';
	GRANT ALL PRIVILEGES ON DATABASE systemdb TO system;
EOSQL