# Technologies Of Programming Course Work
## BusinessLayer (a.k.a back-end)

:shipit: HOW TO RUN:

:white_check_mark: Requirements: `Docker` with `Docker-Compose` as  CLI addon

Docker Compose is using a .env variables

To container to run properly you have to create .env file in the directory where docker-compose.yml file stored

```yaml
#HERE TO USE YOUR ENVIRONMENTAL VARIABLES FOR DOCKER-COMPOSE
ENVIRONMENT=Development #Production
VERSION_TAG=prerelease-latest #latest
DB_USER=application #DB Username
DB_PASSWORD=application #DB Password
DB_NAME=database #DB name
DB_PORT=5432 #DB port
DB_HOST=database #DB host link as here will be (database:5432)
AUTH_TOKEN=somerandomkeyforjwttokens #Key for JWT auth service
```

### Booting up
To boot up the application type in the terminal:<br/>
:red_circle: `docker compose up`  which uses default `docker-compose.yml` config (use it for local development).<br/>
:orange_circle: `docker compose up -f docker-compose.dev.yml` which uses dev `docker-compose.dev.yml` config (use it for dev testing on server)<br/>
:green_circle: `docker compose up -f docker-compose.prod.yml` which uses prod `docker-compose.prod.yml` config (use it for production)

### API deployments
[:whale: Prerelease deploy](http://api.top-course-work.dev.daniilda.xyz/index.html)
[:no_entry: Release deploy](http://api.top-course-work.daniilda.xyz/index.html)
