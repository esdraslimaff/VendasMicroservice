---

# 📦 Vendas Microservices

Este projeto tem como objetivo o estudo e aplicação prática da **arquitetura de microsserviços**, com foco em **comunicação assíncrona entre serviços independentes**.

---

## 🧩 Arquitetura

O sistema é composto por dois microsserviços principais:

### 🟢 Orders (Pedidos)

Responsável por:

* Criação de pedidos
* Persistência dos dados
* Publicação de eventos de integração

---

### 🔵 Inventory (Estoque)

Responsável por:

* Gerenciamento de produtos
* Controle de estoque
* Consumo de eventos para atualização automática

---

## 🔄 Comunicação entre serviços

A comunicação entre os microsserviços é feita de forma **assíncrona**, utilizando o **RabbitMQ** como infraestrutura de mensageria.

### ✔ Como funciona:

* O serviço **Orders** publica um evento ao criar um pedido
* Esse evento é enviado para o RabbitMQ
* O serviço **Inventory** consome esse evento
* O estoque é atualizado automaticamente

---

## 🧠 Event Bus (Barramento de Eventos)

O projeto implementa um **Event Bus próprio**, abstraindo a comunicação entre os serviços:

* `IEventBus` → contrato do barramento
* `RabbitMqEventBus` → implementação usando RabbitMQ

👉 Isso permite desacoplamento total entre os serviços, que não dependem diretamente um do outro.

---

## 📡 Eventos de Integração

Os eventos seguem o padrão de **Integration Events**, utilizados para comunicação entre sistemas:

* `IntegrationEvent` → classe base
* `OrderCreatedIntegrationEvent` → evento disparado ao criar um pedido

---

## ⚙️ Fluxo de Funcionamento

1. Um pedido é criado no serviço **Orders**
2. O pedido é persistido no banco de dados
3. Um evento `OrderCreatedIntegrationEvent` é publicado
4. O RabbitMQ recebe e roteia a mensagem
5. O serviço **Inventory** consome o evento
6. O estoque dos produtos é atualizado automaticamente

---

## 🎯 Objetivos do Projeto

* Aplicar conceitos de **Microsserviços**
* Implementar **comunicação assíncrona**
* Praticar **baixo acoplamento entre serviços**
* Entender o uso de **Event Bus e mensageria**
* Trabalhar com **RabbitMQ na prática**

---

## 🚀 Tecnologias Utilizadas

* .NET (ASP.NET Core)
* Entity Framework Core
* RabbitMQ
* SQL Server
* Docker

---

## 🧠 Conceitos Aplicados

* Microsserviços
* Event-Driven Architecture
* Integration Events
* Event Bus
* Clean Architecture
* DDD (Domain-Driven Design)

---

## 📌 Resumo

O sistema demonstra como microsserviços podem se comunicar de forma eficiente e desacoplada utilizando eventos:

```text
Orders → publica evento → RabbitMQ → Inventory → consome evento
```

---
