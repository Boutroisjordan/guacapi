# Guacapi

L'api guacapi est un projet CUBE du CESI.


___


## Pré-requis

### Environnements

L'api nécessite l'envrionement suivant:

+ .NET 6.0
+ AspNetCore
+ Entity Framework 7.0


### Packages

Avant de lancer l'application, assurez-vous d'avoir installer les packages suivant:

https://www.nuget.org/

+ Microsoft.EntityFrameworkCore.Design
+ Microsoft.EntityFrameWork.sqlServer
+ Microsoft.EntityFrameWork.tools

### Connection String

Pour le moment la base de données étant en local, vous devrez vous créer votre propre base de données et insérer votre connection string dans le fichier appsettings.json.

Indented code

    // Some comments
    "ConnectionStrings": {
    "DefaultConnection": "Ici votre connection string"
  }


