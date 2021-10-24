# HackedNews
A news site built on ASP.Net Core 3.1.
Features: Create, edit, delete, view news. 
Implemented mechanics in the site: Pagination, Data Validation, Search, Authorization, Registration. 
Technologies used: ASP.Net Core,Bootstrap,Html5,Css3,jQuery(for validation)


Если у Вас возникли проблемы с библиотекой Korzh.EasyQuery.Linq, тогда ее необходимо установить через NuGet.

Я использовал СУБД Sql Server. 
Необходимо будет смигрировать имеющиеся:
EntityFrameworkCore\Update-DataBase -context AppDbContext
EntityFrameworkCore\Update-DataBase -context AppIdentityDbContext

или создать новые миграции
EntityFrameworkCore\Add-migration  <Имя_миграции> -context AppDbContext
EntityFrameworkCore\Update-DataBase -context AppDbContext

EntityFrameworkCore\Add-migration <Имя_миграции>  -context AppIdentityDbContext
EntityFrameworkCore\Update-DataBase -context AppIdentityDbContext

Чтобы получить доступ к админ панели необходимо использовать:
Логин:
ArtemAdmin_74@tut.by
Пароль:
Admin_123
