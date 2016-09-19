use admin;
db.createUser(
    {
        user: "siteUserAdmin",
        pwd: "unPasswordQuiVaBien",
        roles: [{ role: "userAdminAnyDatabase", db: "admin" }]
    }
);
db.auth("siteUserAdmin", "unPasswordQuiVaBien");
db.adminCommand({authSchemaUpgrade: 1});

use angularmovies;
db.createUser(
    {
        user: "angularUser",
        pwd: "password",
        roles: ["dbOwner"]
    }
);
db.auth("angularUser", "password");
db.createCollection("movies");
