# Simple message board 

### Time Limit: Approximately 1 hr
### Brief: Please write a production ready dotnet core application that works like a simple message board. This app should expose a REST interface that allows an anonymous user to submit messages and to retrieve a list of the submitted messages. Please follow sound engineering practices and due to the limited time available, please document any trade-offs that you had to make whilst building this app.

## document any trade-offs
Completing to a high standard in around an hour was tough. It’s a very open-ended question and I wasn’t sure what you where looking to see. What I did pull together was an API that you can POST a message to and GET all messages posted. Backed by EF core to SQL server. 
To run the app you need to alter the connections string and run the below command line to set up the simple DB

```
cd \simple-message-board\src\MessageBoard.Data 
dotnet ef --startup-project ../MessageBoard.RestApi/ database update
```

There is a swagger doc at /swagger/v1/swagger.json
It was hard demonstrate unit testing or TDD as most of the app doesn’t do a lot. I have added end to end tests. They can use SQL server, however to ensure the build works in GitHub actions, I swapped it out for an in memory DB. This isn’t recommended but given more time I would have addressed this.
Given more time, the addition of a health check url would ensure smoother operating of a production system. Adding a docker and docker compose would also have standardised the deployments. Paging of messages would be needed if this was used in production. With it being anonymous, security hasn’t been addressed including issues such as CSRF.
