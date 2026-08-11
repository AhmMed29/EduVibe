# Student Dashboard Design Document

## Architecture
- Standard MVC navigation.
- Shared partial view `_DashboardNav.cshtml` for consistent navigation buttons.

## Layout
- Cards will be implemented using Bootstrap card classes with custom CSS:
  ```css
  .nav-card {
      border: 1px solid black;
      background-color: white;
      padding: 20px;
      margin: 10px;
      display: inline-block;
      text-decoration: none;
      color: black;
  }
  ```

## Components
- `_DashboardNav.cshtml`: Contains the two "Add Student" and "Get All Students" buttons styled as cards.
- `Dashboard.cshtml`: Main dashboard view including `_DashboardNav.cshtml`.
- `Add.cshtml`: View for adding a student, includes `_DashboardNav.cshtml`.
- `List.cshtml`: View for listing students, includes `_DashboardNav.cshtml`.

## Data Flow
- User clicks a card, which acts as a standard link to the corresponding controller action.
- Controller action returns the view.
- View renders the layout + `_DashboardNav.cshtml` + the specific page content.

## Error Handling
- Controller actions will continue to use try-catch blocks for database access, as implemented.

## Testing
- Verify that clicking cards navigates to the correct views and that the UI matches the design (cards side-by-side, border style).
