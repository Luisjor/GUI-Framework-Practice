Feature: Items actions operations
    As a logged user, the user wants to be able to perform different actions on the items.

    Background:
        Given the user is logged in

    @create.project.Cleaning @UI_View_More @Actions
    Scenario: View More Options Successfully
        When the user clicks on 'Project Button' <Cleaning> at 'Project Component'
            And clicks on 'More Option' at 'Items Component'
        Then the 'More Options Panel' should be displayed

    @create.project.Construction @UI_View_Less @Actions
    Scenario: View Less Options Successfully
        When the user clicks on 'Project Button' <Construction> at 'Project Component'
            And clicks on 'More Option' at 'Items Component'
            And clicks on 'Less Option'
        Then the 'Less Options Panel' should not be displayed

    @Smoke @Regression @create.project.Kids @create.item.EatLunch @delete.projects @UI_Priority_Item @Actions
    Scenario Outline: Place item priority
        When the user clicks on 'Project Button' <Kids> at 'Project Component'
            And the user hovers on "Get Item" <EatLunch> at 'Items Component'
            And the user clicks on "Item Contextmenu" <EatLunch> on "<Priority>" option at "Items Component"
        Then the <EatLunch> color should be <Color>

        Examples:
            | Priority | Color             |
            | 1        | rgb(255, 51, 0)   |
            | 2        | rgb(22, 139, 184) |
            | 3        | rgb(81, 153, 45)  |
            | 4        | rgb(0, 0, 0)      |

    @Smoke @create.project.Washing @create.item.Car @Actions
    Scenario: Checking an Item in a Project successfully
        When the user clicks on 'Project Button' <Washing> at 'Project Component'
            And the user checks "Car"
        Then "Car" should be listed in the Done Items

    @smoke @create.project.Kids @create.item.Lunch @create.item.Sleep @delete.projects @Actions
    Scenario Outline: Successfully addition of an item above or below an existing item
        When the user clicks on 'Project Button' <Kids> at 'Project Component'
            And the user hovers on "Get Item" <Lunch> at 'Items Component'
            And the user clicks on "Item Contextmenu" <Lunch> on "Add item <position>" option at "Items Component"
        Then the user enters "<new_item>" and saves it
            And the "<new_item>" should be added <position> the "Lunch"

        Examples:
            | position | new_item  |
            | above    | Breakfast |
            | below    | Dinner    |

