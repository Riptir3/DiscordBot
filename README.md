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
- Csunya szavak torlese + felhasznalo figyelmeztetese  
- Uj felhasznalok koszontese  
- Egyelore az admin parancsok logolasa egy fajlba, amely kereses eseten elkuldoheto egy biztonsagos csatornara  

---

## Konfiguráció

- A konfigurációs file a `Config` mappában, a `ClientConfig.json` tartalmazza
- Token: a bot egyedi **Access Tokene**, igénylése: https://discord.com/developers
- Prefix: prefix parancsok első karaktere
- forbiddenWords: lista, ami a tiltott szavakat tartalmazza
- LogFilePath: log file neve
- SafeChannelId & WelcomeChannelId: discord csatorna egyedi azonosítói
- Apis: tartalmazza az apik url címét
-MovieApiKey: igénylése: https://developer.themoviedb.org/docs/getting-started

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
