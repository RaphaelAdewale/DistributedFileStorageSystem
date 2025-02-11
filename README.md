# DistributedFileStorage [![NuGet version](https://badge.fury.io/nu/DistributedFileStorage.svg)](http://badge.fury.io/nu/DistributedFileStorage)
Distributed file storage


## Features
* Storing metadata in the database (SQL/NoSQL)
* Storing files in the file system
* Deduplication of files by content
* Distributed storage (multiple disks)



## Example: Simple console app
```
dotnet new console --name "DfsExample"
cd DfsExample
dotnet add package DistributedFileStorage.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
```

