# **Dodgeball Multiplayer Project \- Exam Preparation**

Welcome to the Dodgeball Multiplayer starter project. This repository contains the base local single-player gameplay (Player vs. PC). Your task is to transform this project into a fully functional **Online Multiplayer** game using the **Client-Host architecture** and the techniques covered in this course.

**⚠️⚠️⚠️ This readme file doesn't contain all of the requirements for the exam!!!⚠️⚠️⚠️**

*For the complete and final requirements, please refer to the Exam Requirements PDF posted on Leho, under the Exam Project module.*

## **🎯 Project Goal**

Convert the existing local gameplay into a networked experience where two players can compete over the internet. You are responsible for implementing the **networking logic**, **user authentication** via PlayFab, and a **custom ASP.NET Core backend**.

## **📑 Exam Expectations & Grading**

On the day of the exam, you must arrive with a **fully functional starter project** containing all features listed below.

* **Duration:** 3 \+ 1 hours.
* **The Challenge:** During the exam, you will be asked to implement new features or modify existing ones on top of your prepared project.  
* **Grading Criteria:** \* Points are awarded based on the **correct use of the techniques taught in class** 
  * **Oral Defense:** You must be able to explain everything. Code you cannot explain will not contribute to your grade.  
  * **Originality:** While collaboration during the semester is encouraged, ensure you write the code yourself. Familiarity with your own codebase is critical for speed during the exam.

## **🛠️ Requirements**

### **1\. Authentication & Lobby**

* **Login Screen:** Implement PlayFab authentication, including a registration flow.  
* **Lobby System:** \* Players can create a new lobby (Max 2 players).  
  * Players can browse and join existing lobbies.  
* **Game Setup:** After joining, players enter the GameScreen to select their team.  
* **Match Start:** The game transitions to the gameplay scene only once both players have toggled their "Ready" status.

### **2\. Custom Backend (ASP.NET Core \+ SQL Server)**

You must deploy a custom backend and SQL database to **Azure**. The Unity client should interact with this API for the following:

| Endpoint | Responsibility | Caller |
| :---- | :---- | :---- |
| SetPlayerInfo | Stores/updates PlayFabId and DisplayName in the Player table. | Client |
| GetPlayerInfo | Retrieves stored player details. | Client |
| StartMatch | Creates an entry in the Match table; returns a unique MatchId. | **Host Only** |
| PlayerHit | Logs a hit event (Attacker ID vs. Victim ID) to the database. | **Host Only** |

### **3\. Gameplay Networking (Client-Host)**

* Synchronize player movement.  
* Implement networked ball physics (throwing, grabbing, dropping).  
* Handle "Hit" detection on the Server/Host and propagate the state to clients.  
* Ensure UI elements (scores, timers) are synced across all instances.  
* Use Server-Authority where possible\!

## **🚀 Getting Started**

1. **Analyze the Local Logic:** Understand how the MVP pattern works in the current single-player version.  
2. **Implement Networking:** Synchronize the game state using Unity NetCode for GameObjects.  
3. **Setup PlayFab:** Create your title in the PlayFab dashboard and link the SDK in Unity.  
4. **Setup Unity Lobby & Relay**: Create a project in the Unity Cloud and link it in Unity.  
5. **Develop the API:** Build your ASP.NET Core Web API, set up the SQL schema, and deploy to an Azure App Service.

## **⚠️ Important Reminders**

* **Security:** Never hardcode database strings in the Unity client. Use your API as the middle layer.  
* **Deployment:** Ensure your Azure services are active and reachable before the exam starts.  
* **Documentation:** Keep your code clean and commented; it will help you during the oral explanation.

**Good luck with the preparation\!**
