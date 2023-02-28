Feature: Full name update
    As a logged in user, the user should be able to update his full name from his profile settings.

    Background:
        Given the user is logged in

    @Smoke @Regression @restore.default.fullname
    Scenario: Update full name succesfully
        When the user clicks on 'Settings' at 'Home Page'
            And types "New Name" on 'FullName' at 'Profile Page'
            And clicks on 'Ok'
        Then the 'NonDisplayedClose' should not be displayed
        When the user clicks on 'Settings' at 'Home Page'
        Then the 'FullName' value is updated with 'New Name' at 'Profile Page'

    # @Negative
    # Scenario: Fail to update email with empty input
    #     When the user inputs a new full name "" on the Full Name input
    #         And clicks on the OK button
    #     Then an alert should appear with the message "Full Name cannot be empty" 
    #         And an accept button is displayed
