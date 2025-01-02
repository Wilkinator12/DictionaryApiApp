# Dictionary API App

This app uses two UIs (WPF and the Console) to access a dictionary api (located at dictionaryapi.dev) and display the definitions of user-specified words back to the user.


## Current Features:
- Lookup word definitions using the Console or a WPF UI


## Programming Concepts Used
- Console
- WPf
- API calls
- Logging


## Local Dependencies
### ConsoleLibrary
This is a library that I use for generating menus and prompts to the console easily. This library is provided with the repository as "ConsoleLibrary.dll".

### WpfLibrary
This is a library that I use to store code that can be used across all WPF applications. Most specifically, for this application, it is used to put the main TextBox into "Idle Mode". This means that the TextBox will display default text when it is empty and not in focus.
