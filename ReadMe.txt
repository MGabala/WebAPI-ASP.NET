--WebAPI-ASP.NET6 readme file.

1: To get a token you should send request via Post method on this url:
https://localhost:7033/api/authentication/authenticate
with credentials in example: 
Body:raw as JSON -> { "user" : "Mateusz", "password": "SecretPassword" } 