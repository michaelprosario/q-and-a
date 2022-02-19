
DB migration
============

Install EF tools if needed ...
dotnet tool install --global dotnet-ef

Early stage iteration
- Navigate to Infra project
- Delete migrations
- Navigate to QA.Server

Windows...
dotnet ef migrations add setup1 --project ..\QA.Infra --context QAContext

Mac/linux
dotnet ef migrations add setup1 --project ../QA.Infra --context QAContext

dotnet ef database update
dotnet ef migrations script --output setup_db.sql


Image builds...

docker build -f QA.Server/Dockerfile -t qa-app:latest .

References:
- https://code-maze.com/angular-security-with-asp-net-core-identity/




Links
- https://github.com/sparksuite/simplemde-markdown-editor
