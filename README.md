# Microservices Application

A .NET 8-based microservices solution for asset value calculation, featuring MediatR for CQRS, AutoMapper for object mapping, and structured logging with Microsoft.Extensions.Logging.

## Features

- **Asset Value Calculation:** Calculates the final value of an asset based on initial value, CDI, and bank fee.
- **CQRS Pattern:** Uses MediatR for clean separation of commands and queries.
- **AutoMapper Integration:** Simplifies object-to-object mapping.
- **Structured Logging:** Provides detailed request and response logs.

## Technologies

- .NET 8
- MediatR
- AutoMapper
- Microsoft.Extensions.Logging

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)

### Setup

1. **Clone the repository:**
   

2. **Restore dependencies:**


3. **Build the solution:**
   

4. **Run the API:**
   

## Usage

Send a POST request to the `/api/v1/asset/value` endpoint with the following JSON body: { "initialValue": 1000.0, "cdi": 0.13, "bankFee": 0.01 }


**Response:**

{ "finalValue": 1013.0, "initialValue": 1000.0, "cdi": 0.13, "bankFee": 0.01 }   

## Project Structure

- `src/Microservices.Api`: API controllers and endpoints.
- `src/Microservices.Application`: Application logic, handlers, and queries.
- `src/Microservices.CrossCutting`: Shared infrastructure (e.g., AutoMapper profiles).

## Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## License

[MIT](LICENSE)
   