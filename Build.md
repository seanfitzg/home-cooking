# How to build & run

Run all commands from the project root directory

Run everything:

    docker-compose up

Build & Run everything:

    docker-compose up --build

### HomeCooking.Ui
...from the solution directory

Build:

    docker build -f ./HomeCooking.Ui/Dockerfile -t homecooking-ui:dev ./HomeCooking.Ui

Run (this time _cd_ into the HomeCooking.Ui directory)

    yarn start

Run with dapr (this time _cd_ into the HomeCooking.Ui directory):

    dapr run --app-id homecooking-ui --app-port 80 yarn start

### HomeCooking.Api
...again, from the solution directory

Build:

    docker build -f HomeCooking.Api/Dockerfile -t homecooking-app:dev .

Run inside docker:

    docker run -d -p 5000:80 --name homecooking-app homecooking-app:dev

Run with dapr (this time _cd_ into the HomeCooking.Api directory):

    dapr run --app-id homecooking-app --app-port 80 --dapr-http-port 3500 --components-path "..\dapr" dotnet run

### HomeCooking.Logging
...again, from the solution directory

Build:

    docker build -f HomeCooking.Logging/Dockerfile -t logging-app:dev .

Run inside docker:

    docker run -d -p 5001:80 --name logging-app logging-app:dev

Run with dapr (this time _cd_ into the HomeCooking.Logging directory):

    dapr run --app-id logging-app --app-port 80 --dapr-http-port 3501 --components-path "..\dapr" dotnet run

To start RabbitMQ:

    docker run -d -p 5672:5672 -p 15672:15672 --name rabbitmq --rm rabbitmq:3-management-alpine