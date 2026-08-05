# Smart Task Manager

A desktop task management application developed with **C#** and **WPF**.

## Features

- Add new tasks
- Set priority levels (Low, Medium, High)
- Select due dates
- Mark tasks as completed
- Remove tasks
- Clear completed selection
- Automatic local data persistence using JSON
- User-friendly WPF interface with DataGrid

## Technologies

- C#
- .NET
- WPF (Windows Presentation Foundation)
- XAML
- System.Text.Json

## Project Structure

- `MainWindow.xaml` – User interface
- `MainWindow.xaml.cs` – UI event handling
- `TaskManager.cs` – Business logic
- `TaskItem.cs` – Task model
- `tasks.json` – Local data storage

## How It Works

Tasks are stored locally in a JSON file, allowing all data to persist after the application is closed. Users can create, complete, delete, and manage tasks through a simple graphical interface.
