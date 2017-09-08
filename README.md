# ToDoApp
A simple ToDo List application project for creating, reading, updating, and deleting ToDo events. The events have a task description and a details field. The events are assigned due dates. 

This application uses the *Prism Library* for providing MVVM design pattern for the data domain, code logic, and the UI Interface; and to provide navigation between functional views in the application. The ToDo list data is stored in a local file in JSON format; storage is in the application's directory, in a subfolder named _Data_. 

## Application Framework
- Prism 6 Library
- Entity Framework
- NuGet Package installation

The Solution and Projects were built in **Microsoft** *Visual Studio 2015 Community Edition.*
Please **Rebuild** the solution in Visual Studio to fetch the NuGet Packages and restore the **References** used in the projects. 

## Specifications
The TODO app was built with the following goals in mind: 

Create an application that provides the ubiquitous _"TODO list"_ example application, with the basic functionality given below: 

- **As a user, I can add a _TODO_ to the list.**
- **As a user, I can see all the _TODOs_ on the list in an overview.** 
- **As a user, I can drill into a _TODO_ to see more information about the TODO.**
- **As a user, I can delete a _TODO_.**
- **As a user, I can mark a _TODO_ as completed.**
- **As a user, when I see all the _TODOs_ in the overview, if today's date is past the TODO's deadline, highlight it.**


### __A _TODO_ consists of__

- [x] **a task (just a simple sentence or two is fine)**
- [x] **a deadline date**
- [x] **a completed flag (stored as a Date), and**
- [x] **an optional "more details" field that allows for more details to be given (a single large text area is fine for this).**
- [ ] *the date that it was created is also stored*

#### Project Considerations
> The list of TODOs is kept in memory, and stored in a text file. 
- This is a "native GUI" application. 
- The Project is using
  - the C# language 
  - WPF for the UI
  - an MVVM design pattern      
    - [**Model** \~ *View* \~ **View-Model**]
- .NET libraries and external libraries are pulled in as part of a build process with NuGet.

## Application Usage
##### In the current version, the behavior for the ToDo item list overview View:
- Overdue items are highlighted in RED.
- Items due today are highlighted in ORANGE.
- Completed items are highlighted in GRAY.

- There is a search box in the upper left corner of the screen. It can be used to search and filter out task descriptions. 
- The columns of the to do list can be sorted in ascending or descending order also, by clicking on the column headers. 
- The action buttons in the selected row of the list brighten up. Select the respective button to view more details, flag the task as complete, or delete the task.
- There is a button in the upper right hand corner to add new tasks to the ToDo task list. 




### Submitted to Capstone Technology for evaluation.

###### Thank you for your interest in this project! :+1:
