# TOP-Course-Work Business Layer

HOW TO RUN:

Docker Compose is using a .env variables

To container to run properly you have to create .env file in directory where docker-compose.yml file stored

```yaml
#HERE TO USE YOUR ENVIRONMENTAL VARIABLES FOR DOCKER-COMPOSE
ENVIRONMENT=Development #Production
VERSION_TAG=prerelease-latest #latest
DB_USER=application #DB Username
DB_PASSWORD=application #DB Password
DB_NAME=database #DB name
DB_PORT=5432 #DB port
DB_HOST=database #DB host link as here will be (database:5432)
```

To boot up the application type `/docker compose up` in the terminal