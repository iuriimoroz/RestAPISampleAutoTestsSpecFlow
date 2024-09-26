Feature: Users Endpoint
  As a ReqRes API user
  I want to get a list of users from the default page and other pages
  So that I can verify the users' data and API response

Background:
    Given I have the base URL of the API

@UsersList
Scenario: Retrieve a list of users from the default page
    When I send a GET request to "ListUsersEndpoint"
    Then the response status code should be 200
    And the response content should not be empty
    And the response should match the expected JSON schema for users

@UsersList
Scenario: Retrieve a list of users from the second page
    When I send a GET request to "ListUsersByPageEndpoint" for page 2
    Then the response status code should be 200
    And the response content should not be empty
    And the current page in the response should be 2