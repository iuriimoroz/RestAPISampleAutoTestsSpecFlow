Feature: Single User Endpoint
  As a ReqRes API user
  I want to retrieve single users by their ID
  So that I can verify the users' data and the API response

Background:
	Given I have the base URL of the API

@SingleUser
Scenario Outline: Retrieve a single user by ID
	When I send a GET request to "SingleUserEndpoint" with user ID <userId>
	Then the response status code should be 200
	And the response content should not be empty
	And the user ID in the response should be <expectedId>
	And the user's email in the response should be "<expectedEmail>"
	And the user's first name in the response should be "<expectedFirstName>"
	And the user's last name in the response should be "<expectedLastName>"
	And the user's avatar in the response should be "<expectedAvatar>"

	Examples:
		| userId | expectedId | expectedEmail          | expectedFirstName | expectedLastName | expectedAvatar                          |
		| 1      | 1          | george.bluth@reqres.in | George            | Bluth            | https://reqres.in/img/faces/1-image.jpg |
		| 2      | 2          | janet.weaver@reqres.in | Janet             | Weaver           | https://reqres.in/img/faces/2-image.jpg |
		| 3      | 3          | emma.wong@reqres.in    | Emma              | Wong             | https://reqres.in/img/faces/3-image.jpg |