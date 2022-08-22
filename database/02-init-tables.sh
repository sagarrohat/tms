#!/bin/bash

psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" --dbname "tmsdb" <<-EOSQL 
	CREATE TABLE public.users (
		user_id        uuid        NOT NULL,
		first_name     TEXT        NOT NULL,
		last_name      TEXT        NOT NULL,
		email_address  TEXT        NOT NULL UNIQUE,
		pd             TEXT        NOT NULL,
		is_active      BOOLEAN     NOT NULL,
		PRIMARY KEY (user_id)
	);

	INSERT INTO public.users (user_id,first_name,last_name,email_address,pd,is_active) VALUES 
	('cc78f65d-5289-428e-9bf6-c19953521f6a','Rohat','Sagar','rohat-sagar.urif-sonu@informatik.hs-fulda.de','fdcfd0eb2f18fcfc16229143939109aa09d33619797ab685cef7bdebf48c29e7',true);

	INSERT INTO public.users (user_id,first_name,last_name,email_address,pd,is_active) VALUES 
	('c67e42ce-fd6d-4b62-b0df-aaee8003902d','Nisha','Devi','nisha.devi@informatik.hs-fulda.de','b89a61619bbc2a3d21afce76b35587f9ed796e30b873a83f6ab985f7a3522987',true);

	CREATE TABLE public.tasks (
		task_id                 uuid        NOT NULL,
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
		notification_id         uuid        NOT NULL,
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