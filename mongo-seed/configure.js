db = db.getSiblingDB('admin');
print(db);
db.createUser({ user: "siteUserAdmin", pwd: "unPasswordQuiVaBien", roles: [{ role: "userAdminAnyDatabase", db: "admin" }] });
db.auth("siteUserAdmin", "unPasswordQuiVaBien");
db.adminCommand({ authSchemaUpgrade: 1 });

db = db.getSiblingDB('angularmovies');
print(db);
db.createUser({ user: "angularUser", pwd: "password", roles: ["dbOwner"] });
db.auth("angularUser", "password");
db.createCollection("movies");