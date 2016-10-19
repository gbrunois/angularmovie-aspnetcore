FROM mongo

COPY configure.js /configure.js
COPY movies.json /movies.json

CMD mongo admin --host $MONGO_HOST --port $MONGO_PORT < ./configure.js;\
    mongoimport --host $MONGO_HOST --port $MONGO_PORT -d angularmovies -c movies --drop --jsonArray ./movies.json