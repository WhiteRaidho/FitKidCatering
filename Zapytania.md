### Użytkownik - `/api/user`
	- autentykacja  
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
	- odświeżenie tokenu  
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
	- odzyskiwanie tokenu  
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
	- rejestracja  
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
