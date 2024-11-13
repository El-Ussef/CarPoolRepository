Feature: Authentication
All features related to user authentication

    @authentication
    Scenario: Driver Registration Should Succeed
        When a new driver registers with following details:
          | Email           | Password | FirstName | LastName | PhoneNumber | CarBrand | CarColor | CarNumber |
            | driver@test.com | Test123! | John      | Doe      | 1234567890  | Toyota   | Black    | ABC123    |
        Then the registration should be successful
        And the user should be able to login with these credentials

    @authentication
    Scenario: Client Registration Should Succeed
        When a new client registers with following details:
          | Email           | Password | FirstName | LastName | PhoneNumber |
          | client8@test.com | Test123! | Jane      | Doe      | 1234567890  |
        Then the registration should be successful
        And the user should be able to login with these credentials
        
    @authentication
    Scenario: User Login Should Succeed
        Given a registered user with following credentials:
          | Email            | Password |
          | client7@test.com | Test123! |
        When the user attempts to login
        Then the login should be successful
        And a valid JWT token should be returned