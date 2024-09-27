# REST API Testing with SpecFlow and NUnit

This project demonstrates automated REST API testing using **SpecFlow**, **NUnit**, and **RestSharp** for the ReqRes API. The tests are designed to validate the endpoints and ensure that they return the expected responses, including both success and error scenarios.

## Features

- Automated API testing using **SpecFlow** and **NUnit**
- Supports testing the ReqRes API (https://reqres.in)
- Verifies HTTP status codes, response contents, and specific data fields
- Handles positive test cases (successful user retrieval) and negative test cases (user not found)
  
## Project Structure

- **Feature Files**: Contains Gherkin-based scenarios for API tests.
- **Step Definitions**: Implements the step definitions using **RestSharp** to make API calls.
- **Utils**: Contains utility classes to handle API requests and response parsing.
- **Consts**: Stores constants like endpoint URLs.

## Prerequisites

- **.NET Core SDK**
- **Visual Studio 2022** (or another C# IDE)
- **NuGet Packages**:
  - SpecFlow
  - NUnit
  - RestSharp
  - Newtonsoft.Json

## Getting Started

1. Clone the repository:
   ```bash
   git clone https://github.com/RestAPISampleAutoTestsSpecFlow/rest-api-testing.git
   cd rest-api-testing
   ```
2. Restore NuGet packages:
  ```bash
dotnet restore
```
3. Run the tests:
  ```bash
dotnet test
```

## Writing New Tests
- Create a new .feature file in the Features directory for your test scenarios.
- Implement step definitions in the StepDefinitions folder to handle API interactions.

## Example Scenario
```bash
Scenario: Retrieve a single user
  Given I have the base URL of the API
  When I send a GET request to "SingleUserEndpoint" with user ID 2
  Then the response status code should be 200
  And the user's email in the response should be "janet.weaver@reqres.in"
  And the user's first name in the response should be "Janet"
  And the user's last name in the response should be "Weaver"
```

## Running Specific Scenarios
You can run tests for a specific scenario or tag by adding SpecFlow tags to your scenarios and executing tests with specific tags.
```bash
dotnet test --filter TestCategory=SingleUser
```

## Folder structure
```bash
RestAPISampleAutoTestsSpecFlow/
│
├── Configuration/
│   └── RestAPISampleAutoTestsConfiguration.cs
│
├── Consts/
│   └── UserEndpoints.cs
│
├── Features/
│   ├── SingleUserEndpoint.feature
│   ├── UserNotFound.feature
│   └── UsersEndpoint.feature
│
├── Hooks/
│   └── ReportHooks.cs
│
├── Schemas/
│   └── users-schema.json
│
├── Steps/
│   ├── SingleUserEndpointStepDefinition.cs
│   └── UsersEndpointStepDefinition.cs
│
├── Utils/
│   ├── ApiClient.cs
│   └── JsonSchemas.cs
│
├── .gitattributes
└── .gitignore
```
## License
This project is licensed under the MIT License.
