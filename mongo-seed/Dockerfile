FROM mongo

COPY init.js /init.js
CMD mongo admin --host $MONGO_HOST --port $MONGO_PORT < init.js