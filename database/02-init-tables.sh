#!/bin/bash

psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" --dbname "systemdb" <<-EOSQL 
	CREATE EXTENSION IF NOT EXISTS "uuid-ossp";
	CREATE EXTENSION IF NOT EXISTS "pgcrypto";

	CREATE TABLE public.users (
		user_id        uuid        NOT NULL DEFAULT uuid_generate_v4(),
		first_name     TEXT        NOT NULL,
		last_name      TEXT        NOT NULL,
		email_address  TEXT        NOT NULL UNIQUE,
		pd             TEXT        NOT NULL,
		is_active      BOOLEAN     NOT NULL,
		PRIMARY KEY (user_id)
	);

	INSERT INTO public.users (user_id,first_name,last_name,email_address,pd,is_active) VALUES 
	('cc78f65d-5289-428e-9bf6-c19953521f6a','Rohat','Sagar','rohat-sagar.urif-sonu@informatik.hs-fulda.de',PGP_SYM_ENCRYPT('rohat','q3t6w9zCFJNcRfUj'),true);

	INSERT INTO public.users (user_id,first_name,last_name,email_address,pd,is_active) VALUES 
	('c67e42ce-fd6d-4b62-b0df-aaee8003902d','Nisha','Devi','nisha.devi@informatik.hs-fulda.de',PGP_SYM_ENCRYPT('nisha','q3t6w9zCFJNcRfUj'),true);

	CREATE TABLE public.tasks (
		task_id                 uuid        NOT NULL DEFAULT uuid_generate_v4(),
		title                   TEXT        NOT NULL,
		description             TEXT        NULL,
		status                  INT         NOT NULL,
		priority                INT         NOT NULL,
		assigned_user_id        uuid        NULL,
		created_on_utc          TIMESTAMP   NOT NULL,
		modified_on_utc         TIMESTAMP   NOT NULL,
		assigned_on_utc         TIMESTAMP   NULL,
		cancelled_on_utc        TIMESTAMP   NULL,
		completed_on_utc        TIMESTAMP   NULL,
		due_date_utc            DATE        NOT NULL,
		percentage_completed    DECIMAL     NOT NULL DEFAULT 0.0,
		created_by              uuid        NOT NULL,
		modified_by             uuid        NOT NULL,
		is_deleted              BOOLEAN     NOT NULL DEFAULT FALSE,
		PRIMARY KEY (task_id),
		FOREIGN KEY(assigned_user_id) REFERENCES public.users(user_id),
		FOREIGN KEY(modified_by) REFERENCES public.users(user_id),
		FOREIGN KEY(created_by) REFERENCES public.users(user_id)
	);

	CREATE TABLE public.pinned_tasks (
		task_id        uuid        NOT NULL,
		user_id        uuid        NOT NULL,
		PRIMARY KEY (task_id, user_id),
		FOREIGN KEY(task_id) REFERENCES public.tasks(task_id),
		FOREIGN KEY(user_id) REFERENCES public.users(user_id)
	);

	CREATE TABLE public.notifications (
		notification_id         uuid        NOT NULL DEFAULT uuid_generate_v4(),
		created_by              uuid        NOT NULL,
		task_id                 uuid        NOT NULL,
		user_id                 uuid        NOT NULL,
		type                    INT         NOT NULL,
		created_on_utc          TIMESTAMP   NOT NULL,
		unread                  BOOLEAN     NOT NULL DEFAULT true,
		PRIMARY KEY (notification_id),
		FOREIGN KEY(created_by) REFERENCES public.users(user_id),
		FOREIGN KEY(task_id)    REFERENCES public.tasks(task_id),
		FOREIGN KEY(user_id)    REFERENCES public.users(user_id)
	);
EOSQL