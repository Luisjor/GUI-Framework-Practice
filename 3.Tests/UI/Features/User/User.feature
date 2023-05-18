Feature: User
    As a non-logged in user, the user should be able to login into the site

    @Smoke
    Scenario: Login into the site succesfully
        Given the user navigates to the URL
        When the user introduces his credentials
            And clicks on 'Login Button' at 'Login Page'
            And clicks on 'Burger Menu' at 'Home Page'
        Then the 'Logout Button' should be displayed at 'Home Page'

    @Smoke
    Scenario: Logout off the site succesfully
        Given the user is logged in
        When the user clicks on 'Burger Menu' at 'Home Page'
            And the user clicks on 'Logout Button' at 'Home Page'
        Then the 'Login Button' should be displayed at 'Login Page'

