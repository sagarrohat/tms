#!/bin/bash

psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" --dbname "tmsdb" <<-EOSQL
	CREATE USER api WITH PASSWORD 'api';

	GRANT SELECT ON public.users TO "api";
	GRANT SELECT, INSERT, UPDATE ON public.tasks TO "api";
	GRANT SELECT, INSERT, DELETE ON public.pinned_tasks TO "api";
	GRANT SELECT, INSERT, UPDATE, DELETE ON public.notifications TO "api";
EOSQL