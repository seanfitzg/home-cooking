To run with dapr:

    [From Logging project root]
    dapr run --app-id logging-app --app-port 80 --dapr-http-port 3500 --components-path "..\dapr" dotnet run

Docker commands

    [From solution root]
    docker build -f HomeCooking.Logging/Dockerfile -t logging-app:dev .
    docker run -d -p 5001:80 --name logging-app logging-app:dev

To start RabbitMQ:

    docker run -d -p 5672:5672 -p 15672:15672 --name rabbitmq --rm rabbitmq:3-management-alpine