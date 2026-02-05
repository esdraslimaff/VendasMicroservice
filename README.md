## 🧩 Arquitetura de Microsserviços

Este projeto tem como objetivo o estudo da **arquitetura de microsserviços**, explorando a comunicação assíncrona entre serviços independentes.

O sistema é composto pelos microsserviços:

- **Pedido**
- **Estoque**

A comunicação entre eles é realizada por meio do **RabbitMQ**, utilizado como **Event Bus** para publicação e consumo de eventos assíncronos, garantindo **baixo acoplamento** e **melhor desempenho**.
