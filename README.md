# htmlparser

## Подключение к базе данных
Файл создания: ```database.sql```

В ```db_work.cs``` поменять: 
```sh
 public db_work()
        {
           
            SqlConnectionStringBuilder Connect = new SqlConnectionStringBuilder
            {
                DataSource = "имя",//Сервер
                InitialCatalog = "имя",//База данных
                IntegratedSecurity = true

            };
            Connection = new SqlConnection(Connect.ConnectionString);
          
        }
```
