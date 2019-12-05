# Challenge 3: Webhooks

[Description of challenge](https://25daysofserverless.com/calendar/4)


Build an HTTP API that lets Ezra's friends add food dishes they want to bring to the potluck, change or remove them if they need to (plans change!), and see a list of what everybody's committed to bring.

## Solution 

I created a few Azure Functions for the API endpoints. All connected to a [free tier MongoDB Atlas culster](https://azure.microsoft.com/en-us/blog/microsoft-azure-tutorial-how-to-integrate-azure-functions-with-mongodb/) that is deployed on Azure.

### View all dishes
- [To view all dishes](https://25daysofserverless.com/calendar/4)

- https://day-04-25daysofserverless2019.azurewebsites.net/api/potluckdishes/listall

### Add a dish

To add a dish, send json formatted in this way
```json
{
    "Name":"Dish name",
    "FriendName":"Your name"
} 
```
- to https://day-04-25daysofserverless2019.azurewebsites.net/api/potluckdishes/

- You are returned an ID, make note of it, you will need it to remove a dish.

### Remove a dish

- https://day-04-25daysofserverless2019.azurewebsites.net/api/potluckdishes/delete/{ID}

- Replace {ID} with dish ID to remove that dish.
