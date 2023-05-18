Feature: Items
    As a logged in user, the user should be able to surf thorugh the items

    @Smoke @items
    Scenario Outline: Select given item
        Given the user is logged in
        When the user clicks on 'Item Card' '<Item Name>' at 'Home Page'
        Then the 'Large Item Card' "<Item Name>" should be displayed at 'Home Page'

    Examples:
        | Item Name           |
        | Sauce Labs Backpack |
        | Sauce Labs Bike Light |
        | Sauce Labs Bolt T-Shirt |
        | Sauce Labs Fleece Jacket |


