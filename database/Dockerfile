FROM postgres

ENV POSTGRES_PASSWORD docker

# The official postgres docker image will run .sql scripts found in the /docker-entrypoint-initdb.d/ folder.
COPY 01-init-db.sh /docker-entrypoint-initdb.d/
COPY 02-init-tables.sh /docker-entrypoint-initdb.d/
COPY 03-init-user.sh /docker-entrypoint-initdb.d/

#
# Build an image from the Dockerfile and give it a name of 'tms-db' at current directory
# docker build -t tms-db ./
#

# 
# -d means that you enable Docker to run the container in the background
# --name name of container 
# -p plus the port numbers means you map the containers port 5432 to the external port 5432, 
#       this allows you to connect to it from the outside
# and at the last image we want to run so in our case it is 'tms-db' 
# docker run -d --name tms-db-container -p 5432:5432 tms-db
#