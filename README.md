# SendReportApp

This is a mobile application was created to send reports on problems encountered in cities, villages by their residents but it doesn't have to be limited to just that for example if you are holding event you can use this app in the same way. Currently app style is customised for Łeba city becouse I work there. In later designs style can be changed.
## Writing raport
Creating raport is divided into several steps with each step is a sub-page:
- Make photo
- Chose report category
- (Optional) add detailed description
- Share your localization and check on map if it's ok
- Send report
You can move between steps using the flow buttons at the bottom.
## Demo
https://github.com/user-attachments/assets/d61f83f0-69b3-4cc2-a433-53ab33a5c086

## Sending data to api
After writing report your data will be sended to combined location of api endpoint (BaseUrl) and route (UploadPath) from **appsettings.json** (current settings are for testing purposes).

### Sending data structure
```json
{
    "Id": "id (create on server side)",
    "Message": "message",
    "Base64Image": "image in base64",
    "Category": 1,
    "Latitude": 1.0,
    "Longitude": 1.0
}
```


## Tech

**Multi platform app framework** - .NET MAUI \
**Street view map implementation in .NET MAUI** - <a href="https://mapsui.com/">Mapsui<a>

## Roadmap

- Currently only support for Polish language so 3 new leaguages support - Anglish, Deuth, Ukrainian

- More pleasing visual effects
