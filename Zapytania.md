# Użytkownik - `/api/user`
### Autentykacja  
`POST /authenticate`  
Request body:  
```
{
	"Username": "string",
	"Password": "string"
}
```
Respone body:
```
{
	"token": "string",
	"refresh": "string",
	"expires": "datetime"
}
```
### Odświeżenie tokenu  
`GET /authenticate/refresh`  
Header:
`Authorization: Bearer [TOKEN]`  
Respone body:
```
{
	"token": "string",
	"refresh": "string",
	"expires": "datetime"
}
```
### Odzyskiwanie tokenu  
`POST /authenticate/recover`  
Request body:  
```
{
	"Token": "string"
}
```
Uwaga! Tutaj wykorzystujemy Refresh Token!  
Respone body:
```
{
	"token": "string",
	"refresh": "string",
	"expires": "datetime"
}
```
### Rejestracja  
`POST`
Request body:
```
{
	"UserName": "string",
	"Email": "string",
	"Password": "string",
	"FirstName": "string",
	"LastName": "string"
}
```
