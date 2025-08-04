
# 🤖 ChatbotAssistance

**Purpose:** Internal support assistant for HR, IT, or Sales departments.

ChatbotAssistance is a multi-project solution built with **Blazor Server**, **ASP.NET Core Web API**, and **.NET Console**, designed to serve as an AI-based internal assistant. It integrates OpenAI, Bot Framework, and SignalR to deliver real-time, intelligent conversations for enterprise users.

---

## 🏗️ Project Structure

- **ChatbotAssistance.Blazor** - UI layer (Blazor Server)
- **ChatbotAssistance.API** - Web API for AI responses and routing
- **ChatbotAssistance.Bot** - Bot logic and OpenAI integration
- **ChatbotAssistance.Shared** - Shared models and contracts

---

## 🚀 Setup Instructions

### 1. Create Solution and Projects

```bash
dotnet new sln -n ChatbotAssistance
dotnet workload install wasm-tools

dotnet new blazorserver -n ChatbotAssistance.Blazor
dotnet new webapi -n ChatbotAssistance.API
dotnet new classlib -n ChatbotAssistance.Shared
dotnet new console -n ChatbotAssistance.Bot
````

---

### 2. Install Required NuGet Packages

📦 In **ChatbotAssistance.API**:

```bash
dotnet add package Microsoft.Extensions.Http
dotnet add package OpenAI --version 1.6.5
```

📦 In **ChatbotAssistance.Bot**:

```bash
dotnet add package Microsoft.Bot.Builder.Integration.AspNet.Core
dotnet add package Microsoft.Bot.Builder.Dialogs
dotnet add package Microsoft.Bot.Connector
```

📦 In **ChatbotAssistance.Blazor**:

```bash
dotnet add package Microsoft.AspNetCore.SignalR.Client
```

---

### 3. Project References

```bash
# Shared → API
dotnet add ./ChatbotAssistance.API/ChatbotAssistance.API.csproj reference ./ChatbotAssistance.Shared/ChatbotAssistance.Shared.csproj

# Shared → Blazor
dotnet add ./ChatbotAssistance.Blazor/ChatbotAssistance.Blazor.csproj reference ./ChatbotAssistance.Shared/ChatbotAssistance.Shared.csproj

# Shared → Bot
dotnet add ./ChatbotAssistance.Bot/ChatbotAssistance.Bot.csproj reference ./ChatbotAssistance.Shared/ChatbotAssistance.Shared.csproj

# API → Bot
dotnet add ./ChatbotAssistance.Bot/ChatbotAssistance.Bot.csproj reference ./ChatbotAssistance.API/ChatbotAssistance.API.csproj
```

---

## ▶️ Run the Projects

```bash
dotnet run --project "ChatbotAssistance.Bot/ChatbotAssistance.Bot.csproj"
dotnet run --project "ChatbotAssistance.Blazor/ChatbotAssistance.Blazor.csproj"
dotnet run --project "ChatbotAssistance.API/ChatbotAssistance.API.csproj"
```

---

## ✨ Features

* AI Chatbot with OpenAI integration
* Realtime SignalR-based UI
* Role-based support for HR, IT, and Sales
* Document generation & routing options
* Chat timestamps and avatars for better UX
