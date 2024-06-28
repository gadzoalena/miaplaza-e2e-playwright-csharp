# Miaplaza E2E Playwright C# Tests

This repository contains end-to-end (E2E) tests for the Miaplaza online school application using Microsoft Playwright and C#. The tests automate the process of interacting with various pages, filling out forms, and verifying the results to ensure the application's functionality.

## Table of Contents

- [Prerequisites](#prerequisites)
- [Installation](#installation)
- [Running the Tests](#running-the-tests)

## Prerequisites

Before you can run the tests in this project, you'll need to have the following software installed:

1. **.NET SDK**: You can download the latest version from [here](https://dotnet.microsoft.com/download).
2. **Node.js**: Playwright relies on Node.js runtime, which can be downloaded from [here](https://nodejs.org/).

## Installation

### Cloning the Repository

1. Clone the repository using Git:
    ```sh
    git clone https://github.com/gadzoalena/miaplaza-e2e-playwright-csharp.git
    cd miaplaza-e2e-playwright-csharp
    ```

### Setting Up the Project

2. Install necessary .NET packages:
    ```sh
    dotnet restore
    ```

3. Install Playwright:
    ```sh
    dotnet tool install --global Microsoft.Playwright.CLI
    ```

4. Install Playwright Browsers:
    ```sh
    playwright install
    ```

## Running the Tests

1. **Build the Project**:
    ```sh
    dotnet build
    ```

2. **Run the Tests**:
    ```sh
    dotnet test
    ```
