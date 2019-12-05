# Challenge 4: API EndPoints

[Description of challenge](https://25daysofserverless.com/calendar/4)


Build an HTTP API that lets Ezra's friends add food dishes they want to bring to the potluck, change or remove them if they need to (plans change!), and see a list of what everybody's committed to bring.

## Solution 

I created a few Azure Functions for the API endpoints. All connected to a [free tier MongoDB Atlas culster](https://azure.microsoft.com/en-us/blog/microsoft-azure-tutorial-how-to-integrate-azure-functions-with-mongodb/) that is deployed on Azure. [MongoDB Azure Function Bindings](https://www.nuget.org/packages/Kevsoft.Azure.WebJobs.Extensions.MongoDB/).

### View all dishes

To view all dishes, please visit this link. This will display all documents in the MongoDB Collection.
- https://day-04-25daysofserverless2019.azurewebsites.net/api/potluckdishes/listall

### Add a dish

To add a dish, send json to https://day-04-25daysofserverless2019.azurewebsites.net/api/potluckdishes/ formatted in this way
```json
{
    "Name":"Dish name",
    "FriendName":"Your name"
} 
```
- You are returned an ID, make note of it, you will need it to remove a dish.

### Remove a dish

To remove a dish, please simply visit this URL.

- https://day-04-25daysofserverless2019.azurewebsites.net/api/potluckdishes/delete/{ID}

- Replace {ID} with dish ID to remove that dish.

## Demo

![Demo](day_04_demo.gif "Demo")