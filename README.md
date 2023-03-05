# PRN221_MVC
PRN221 MVC final project. Shopping website.
## Feature
Login/logout, confirm email, 2FA login, forgot password, register

Client
- Search/filter/view products
- add/update/delete/checkout cart items
- Comment
- Order history, order detail

Shop owner
- Dashboard sales
- CRUD, detail product
- Order history, order detail

Admin
- CU user account
- RD, detail user account

## How to build?
Open **Package Manager Console** in **Visual Studio** IDE and run `Update-Database`

## How to modify database?
1. Modify entities
> Open **Package Manager Console** in **Visual Studio** IDE
2. `Drop-Database`
3. `Remove-Migration`
4. `Add-Migration "name of migration"` (after this step, if your database doesn't update, continue to step 5
5. In case step 4 is not working, add `Update-Database` command
