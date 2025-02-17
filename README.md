# Solution overview
## Design overview
Solution is based on microservices architecture in .net 9 and run inside docker containers.
For API Gateway and Load Balancing Ocelot was choosen with help of Consul for service discovery.
Online shop will contain services for hadling products, customers, orders that will run inside docker containers and will register to Consul service registery.
Products data is stored in MongoDB database for schemaless flexibility. 
Customers and orders data will be stored inside MS SQL database for transactional data.
For logging Serilog was choosen to log requests, errors and etc.
Communication between services will be event-based using MassTransit library and RabbitMQ queue.

Futher improvements that can be done:
* switching for event-based communication to Azure Service Bus for async processing
* swiching from Consul service discovery to Kubernetes
* introduction of ElasticSearch for full-text search capabilities - especially for large datasets of products with different schema

```text: Solution architecture
OnlineShopDemo
|- Api Gateway & Load Balancer: Ocelot that will discover services through Service Registry
  |- Dockerfile for Api Gateway
|- Service Registry: HashiCorp Consul to which all spawn services will register
|- Services
    |- Product Service
        |- Dockerfile for Product Service
    |- Customer Service
        |- Dockerfile for Customer Service
    |- Order Service
        |- Dockerfile for Order Service
|- docker-compose.yml    

```
