Feature: User Not Found
  As a ReqRes API user
  I want to verify that a request for a non-existent user returns a 404 status code
  So that I can ensure the API handles invalid user requests properly

@SingleUser
Scenario: Retrieve a non-existent user
  Given I have the base URL of the API
  When I send a GET request to "SingleUserEndpoint" with user ID 23
  Then the response status code should be 404
  And the response content should indicate "{}"
