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
Response body:
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
Response body:
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
Response body:
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
# Dzieci - `/api/children'
### Pobranie danych dziecka
`GET /{publicId}`  
Response body:
```
{
  "PublicId": "string",
  "Name": "string",
  "ParentPublicId": "string",
  "ParentUsername": "string",
  "InstitutionPublicId": "string",
  "InstitutionName": "string"
}
```
### Edytowanie danych dziecka
`PUT /{publicId}`
Request body:
```
{
  "FirstName": "string",
  "LastName": "string",
  "ParentPublicId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "InstitutionPublicId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```  
### Usuwanie dziecka z bazy
`DELETE /{publicId}`

### Pobranie listy wszystkich dzieci
`GET`
Response Body:
```
[
  {
    "PublicId": "string",
    "Name": "string",
    "ParentPublicId": "string",
    "ParentUsername": "string",
    "InstitutionPublicId": "string",
    "InstitutionName": "string"
  },
  {
    "PublicId": "string",
    "Name": "string",
    "ParentPublicId": "string",
    "ParentUsername": "string",
    "InstitutionPublicId": "string",
    "InstitutionName": "string"
  }
]
```
### Dodawanie dziecka
`POST`
Request body:
```
{
  "FirstName": "string",
  "LastName": "string",
  "ParentPublicId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "InstitutionPublicId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```
### Pobranie listy dzieci przypisanych do zalogowanego uzytkownika
`GET /mychildren`
Response body:
```
[
  {
    "PublicId": "string",
    "Name": "string",
    "ParentPublicId": "string",
    "ParentUsername": "string",
    "InstitutionPublicId": "string",
    "InstitutionName": "string"
  },
  {
    "PublicId": "string",
    "Name": "string",
    "ParentPublicId": "string",
    "ParentUsername": "string",
    "InstitutionPublicId": "string",
    "InstitutionName": "string"
  }
]
```
### Pobranie listy dzieci przypisanych do instytucji
`GET /institution/{InstitutionPublicId}`
Response body:
```
[
  {
    "PublicId": "string",
    "Name": "string",
    "ParentPublicId": "string",
    "ParentUsername": "string",
    "InstitutionPublicId": "string",
    "InstitutionName": "string"
  },
  {
    "PublicId": "string",
    "Name": "string",
    "ParentPublicId": "string",
    "ParentUsername": "string",
    "InstitutionPublicId": "string",
    "InstitutionName": "string"
  }
]
```
# Instytucje - `/api/institutions'
### Pobranie danych instytucji
`GET /{publicId}`
Response body:
```
{
  "PublicId": "string",
  "Name": "string",
  "Street": "string",
  "ZipCode": "string",
  "City": "string",
  "OwnerPublicId": "string",
  "OwnerUsername": "string"
}
```
### Edytowanie danych instytucji
`PUT /{publicId}`
Request body:
```
{
  "Name": "string",
  "Street": "string",
  "ZipCode": "string",
  "City": "string",
  "OwnerPublicId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```
### Usuwanie instytucji z bazy
`DELETE /{publicId}`
### Pobranie listy instytucji
`GET`
Response body :
```
[
  {
    "PublicId": "string",
    "Name": "string",
    "Street": "string",
    "ZipCode": "string",
    "City": "string",
    "OwnerPublicId": "string",
    "OwnerUsername": "string"
  },
  {
    "PublicId": "string",
    "Name": "string",
    "Street": "string",
    "ZipCode": "string",
    "City": "string",
    "OwnerPublicId": "string",
    "OwnerUsername": "string"
  }
]
```
### Dodawanie instytucji
`POST`
Request body:
```
{
  "Name": "string",
  "Street": "string",
  "ZipCode": "string",
  "City": "string",
  "OwnerPublicId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
}
```
