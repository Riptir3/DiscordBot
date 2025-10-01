# 🧙‍♂️ Harry Botter

A **Discord bot written in C#**, combining administrative commands with fun features.  
Its goal is to be both a **practical moderation tool** and an **entertaining companion** on your server.  

---

## 📖 Table of Contents
- [Features & Usage](#features)
- [Configuration](#configuration)
- [Installation](#installation)
- [Screenshots](#screenshots)

---

## Features

### 🔒 Admin Commands

| Command | Description |
|---------|-------------|
| `/kick <user>` | Kick a user from the server |
| `/ban <user>` | Ban a user from the server |
| `/banlist` | Show a list of banned users |
| `/unban <user.id>` | Unban a previously banned user |
| `/timeout <user> <time>` | Restrict user messages for a set duration |
| `/getlog` | Retrieve the admin command log file |

---

### 🎮 Prefix Commands (API-based)

| Command | Description |
|---------|-------------|
| `!joke` | Returns a random joke |
| `!weather <city>` | Shows weather information for the specified city |
| `!recommend <movie>` | Suggests movies similar to the given one |

---

### 🛡️ Moderation & Logging

- Automatically delete offensive words and warn users  
- Welcome new members to the server  
- Admin command logs are saved in a file and can be sent to a secure channel on `/getlog` request  

> ⚠️ **Note:** Logging to a file is currently the only supported method. Make sure your `SafeChannelId` is correct.

---

## Configuration

- **Config file location:** `Config/ClientConfig.json`  
- **Token:** Your bot’s unique [Discord Access Token](https://discord.com/developers)  
- **Prefix:** Character used for prefix commands  
- **forbiddenWords:** List of banned words  
- **LogFilePath:** Name of the log file  
- **SafeChannelId & WelcomeChannelId:** Unique Discord channel IDs  
- **Apis:** Contains URLs of APIs  
- **MovieApiKey:** Request at [TMDB Developer](https://developer.themoviedb.org/docs/getting-started)  

> 💡 **Tip:** Configuration is read via `Config/BotConfigReader`. All other classes in `Program.cs` receive the same `config` instance to ensure consistency.

---

## Screenshots

<img width="467" height="232" alt="Screenshot" src="https://github.com/user-attachments/assets/1e23a3ca-ae8c-49a4-b2ff-5768f8dc3147" />

---

## Installation

### Requirements

- Minimum [.NET 4 SDK](https://dotnet.microsoft.com/)  
- Discord bot token  
- TMDB API token  

### Steps

```bash
# Open Visual Studio

# Clone the repository
git clone https://github.com/Riptir3/DiscordBot

# Run the bot
