# Bouncing Balls

## A little silly app i made as a lesson task that i'm weirdly mesmerized by.

## Table of contents
* [General info](#general-info)
* [Technologies](#technologies)
* [Features](#features)
* [Setup](#setup)

## General info
This project is simple wpf, multithread application. The balls in the canvas are bouncing of its borders and each other.
User can start or stop the movement of the balls on the canvas and manage their amount by adding or deleting them.

## Technologies
* **Framework:** WPF (Windows Presentation Foundation)
* **Backend (Code-behind):** C# (.NET 8)
* **Frontend (UI):** XAML
* **Concurrency:** System.Threading (Background Threads & Dispatcher)

## Features
* **Multithreaded Physics:** Movement calculations are performed on a separate thread to prevent UI freezing.
* **Thread Synchronization:** Implementation of `Dispatcher.Invoke` ensures safe updates to UI elements from a background thread.
* **Collision Detection:** Custom logic for ball-to-wall and ball-to-ball elastic collisions.
* **Dynamic Control:** Users can add or remove balls in real-time, with smart button state management to prevent race conditions or application crashes.
* **Initial Spawn Safety:** Built-in check to prevent balls from overlapping during creation.

## Setup
To run this project locally, follow these steps:

1. **Clone the repository:**
   ```bash
   git clone https://github.com/Navytree/BouncingBalls.git

2. **Run the application:**
  Press F5 in Visual Studio 2022 or use the command:
     ```bash
     dotnet run
