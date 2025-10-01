# 🧙‍♂️ Harry Botter

Egy **C#-ban írt Discord bot**, amely adminisztrációs parancsokat és szórakoztató funkciókat valósít meg.  
Célja, hogy egyszerre legyen **praktikus moderációs eszköz** és **szórakoztató társ** a szervereden.  

---

## 📖 Tartalom
- [Funkciok & Hasznalat](#funkciók)
- [Konfiguráció](#konfiguráció)
- [Telepites](#telepítés)
  
---

## Funkciók

### 🔒 Admin parancsok
- `/kick <user>` → felhasználó kirúgása  
- `/ban <user>` → felhasználó kitiltása
- `/banlist` → bannolt felhasználók listája
- `/unban <user.id>` → bannolt felhasználó feloldása
- `/timeout <user><time>` → írás korlátozása, időkorlát beállításával
- `/getlog` → admin parancsokhoz tartozó log file lekérése

### 🎮 Prefix parancsok (API-k felhasználásával)
- `!joke` → random viccet ad vissza   
- `!weather <varos>` → idojaras megjelenitese a megadott varosban  
- `!recommend <film>` → hasonlo filmeket ajanl az adott filmhez  

### 🛡️ Moderacio & logolas
- Csúnya szavak törlése + felhasználó figyelmeztetése  
- Új felhasználók köszöntése  
- Egyelőre az admin parancsok logolasa egy fajlba, amely kérés `/getlog` esetén elkuldoheto egy biztonsagos csatornara  

---

## Konfiguráció

- A konfigurációs file a `Config` mappában, a `ClientConfig.json` tartalmazza
- Token: a bot egyedi **Access Tokene**, igénylése: https://discord.com/developers
- Prefix: prefix parancsok első karaktere
- forbiddenWords: lista, ami a tiltott szavakat tartalmazza
- LogFilePath: log file neve
- SafeChannelId & WelcomeChannelId: discord csatorna egyedi azonosítói
- Apis: tartalmazza az apik url címét
- MovieApiKey: igénylése: https://developer.themoviedb.org/docs/getting-started


<img width="467" height="232" alt="Képernyőkép 2025-10-01 174502" src="https://github.com/user-attachments/assets/1e23a3ca-ae8c-49a4-b2ff-5768f8dc3147" />

- Beolvasást a `Config/BotConfigReader` végzi
- A `Program.cs`-ben kapja meg az összes többi osztály úgyanazt a `config` példányt

---
## Telepítés

Előfeltételek:
- Minimum [.NET 4 SDK](https://dotnet.microsoft.com/)  
- Discord bot token
- TMDB api token

Lépések:
```bash
# Visual Studio megnyítása

# Repo klónozasa
git clone https://github.com/Riptir3/DiscordBot

# Futtatás
