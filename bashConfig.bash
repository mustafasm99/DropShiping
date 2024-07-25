dotnet new webapi                      # create the project API 
dotnet tool install --global dotnet-ef # to install the ef tool for migrations 
dotnet ef migrations add firstMigrate  # to create  migrations to the database            => py manage.py makemigrations
dotnet ef database update              # to update the data base to send the migrations   => py manage.py migrate
