# PRN221_MVC
PRN221 MVC final project. Shopping website.
## Feature
Login/logout, confirm email, 2FA login

Client
- Search/filter/view products
- add/update/delete/checkout cart items
- rating/ review

Shop owner
- Dashboard sales
- CRUD product

Admin
- CU user account
- RD user account

## How to build?
Open **Package Manager Console** in **Visual Studio** IDE and run `Update-Database`

## How to modify database?
1. Modify entities
> Open **Package Manager Console** in **Visual Studio** IDE
2. `Drop-Database`
3. `Remove-Migration`
4. `Add-Migration "name of migration"`
