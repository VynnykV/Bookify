# NOTE: 
Replace serviceBase in authService.js with localhost address of API.

If rebuilding a dropped database, change *DropCreateIfModelChanges* to *DropCreateAlways*. After the database is created, revert to *DropCreateIfModelChanges*.

If changing a model in the database, drop it **manually** as DropCreateIfModelChanges will refuse to do so because 'there is an open connection to the database'
