# 🧙‍♂️ Harry Botter

Egy **C#-ban írt Discord bot**, amely adminisztrációs parancsokat és szórakoztató funkciókat valósít meg.  
Célja, hogy egyszerre legyen **praktikus moderációs eszköz** és **szórakoztató társ** a szervereden.  

---

## 📖 Tartalom
- [Funkciók](#-funkciók)
- [Telepítés](#-telepítés)
- [Használat](#-használat)
- [Konfiguráció](#-konfiguráció)
- [Logolás](#-logolás)
- [Licenc](#-licenc)

---

## ✨ Funkciók

### 🔒 Admin parancsok
- `/kick <user>` → felhasználó kirúgása  
- `/ban <user>` → felhasználó kitiltása
- `/banlist` → bannont felhasználók listája
- `/unban <user.id>` → bannont felhasználó feloldása
- `/timeout <user><time>` → írás korlátozása, időkorlát beállításával
- `/getlog` → admin parancsokhoz tartozó log file lekérése

### 🎮 Prefix parancsok(API-k felhasználásával)
- `!joke` → random viccet ad vissza   
- `!weather <város>` → időjárás megjelenítése a megadott városban  
- `!recommend <film>` → hasonló filmeket ajánl az adott filmhez  

### 🛡️ Moderáció & logolás
- Csúnya szavak törlése + felhasználó figyelmeztetése  
- Új felhasználók köszöntése  
- Egyelőre az admin parancsok logolása egy fájlba, amely kérés esetén elküldhető egy biztonságos csatornára  

---

## ⚙️ Telepítés

Előfeltételek:
- [.NET 6/7 SDK](https://dotnet.microsoft.com/)  
- Discord bot token
- TMDB api token

Lépések:
```bash
# Repó klónozása
git clone https://github.com/Riptir3/DiscordBot

# Mappába lépés
cd harry-botter

# Build és futtatás
dotnet run
