# Network Unity Lab 2 - Game Lobby with TCP/UDP Connections  

🖧 **Local Networking Practice in Unity**  

This project is part of a networking systems **handout exercise**, focusing on implementing a **game lobby system** using **TCP and UDP connections**. Initially, the system used separate scenes for the **host and clients**, but it has evolved into a **single application** that handles all roles dynamically.  

## 🛠️ Features  
- **Lobby System**: Players can **host** or **join** a lobby.  
- **UDP & TCP Support**: Connection type is selected via UI before joining/hosting.  
- **Username & Lobby Setup**: Players set their **username** and **lobby name** before hosting.  
- **Real-time Lobby Chat**: Multiple users can communicate in the same lobby.  
- **User Management**: Tracks multiple connections and ensures proper handling.  
- **Seamless Disconnection**: Players can leave the lobby at any time.  

## 📌 Implementation Details  
- **Single Scene Approach**: The system was refactored from two separate scenes (host/client) into a unified interface.  
- **Lobby Manager Console**: The central UI for all player interactions.  
- **Supports Multiple Connections**: Users can join using both **TCP** and **UDP** methods.  

## 🚀 Future Improvements  
- Improve UI feedback for connection status.  
- Implement message encryption for better security.  
- Expand with game-ready matchmaking features.  

---

This project successfully implements **TCP and UDP connections** in Unity, allowing multiple users to interact in a lobby-based system.
