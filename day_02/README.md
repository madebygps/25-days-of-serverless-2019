# Challenge 2: Task scheduler

[Description of challenge](https://25daysofserverless.com/calendar/2)


Create a task scheduler that will tell Lucy exactly when she should relight candles, pour coffee into cups, and deliver batches of coffee.



[Solution template](https://github.com/madebygps/25-days-of-serverless-2019/blob/master/day_02/template.json)
[Solution parameters](https://github.com/madebygps/25-days-of-serverless-2019/blob/master/day_02/parameters.json)

I used Azure Logic apps for this. This is what the workflow looks like. 

![Workflow](logicapp.png "Workflow")

Once it is manually triggered, it will use a  delay to give Lucy enough time for the task, then alert her via email for the next task.


![Inbox](email.gif "Inbox")

![Inbox](emails.png "Inbox")
