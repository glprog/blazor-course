# Migration commands via Package Manager Console
- Add migration:
Add-Migration {migrationName}
- Update database:
update-database
- Rollback all migrations
update-database 0
- Remove last migration that has not yet run
remove-migration