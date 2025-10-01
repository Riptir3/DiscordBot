# 🧙‍♂️ Harry Botter

Egy **C#-ban írt Discord bot**, amely adminisztrációs parancsokat és szórakoztató funkciókat kombinál.  
Célja, hogy egyszerre legyen **praktikus moderációs eszköz** és **szórakoztató társ** a szervereden.  

---

## 📖 Tartalom
- [Funkciók & Használat](#funkciók)
- [Konfiguráció](#konfiguráció)
- [Telepítés](#telepítés)
- [Képernyőképek](#képernyőképek)

---

## Funkciók

### 🔒 Admin parancsok

| Parancs | Leírás |
|---------|--------|
| `/kick <user>` | Felhasználó kirúgása a szerverről |
| `/ban <user>` | Felhasználó kitiltása a szerverről |
| `/banlist` | A kitiltott felhasználók listája |
| `/unban <user.id>` | Korábban kitiltott felhasználó feloldása |
| `/timeout <user> <time>` | Felhasználó üzenetküldésének korlátozása meghatározott ideig |
| `/getlog` | Admin parancsok log fájljának lekérése |

---

### 🎮 Prefix parancsok (API-k használatával)

| Parancs | Leírás |
|---------|--------|
| `!joke` | Véletlenszerű vicc visszaadása |
| `!weather <város>` | Időjárás megjelenítése a megadott városban |
| `!recommend <film>` | Hasonló filmek ajánlása a megadott filmhez |

---

### 🛡️ Moderáció & Logolás

- Csúnya szavak automatikus törlése és a felhasználó figyelmeztetése  
- Új felhasználók üdvözlése a szerveren  
- Az admin parancsok logolása fájlba történik, amely `/getlog` kérésre biztonságos csatornára küldhető  

> ⚠️ **Figyelem:** Jelenleg a logolás csak fájlba történik. Győződj meg róla, hogy a `SafeChannelId` helyes.

---

## Konfiguráció

- **Konfigurációs fájl helye:** `Config/ClientConfig.json`  
- **Token:** A bot egyedi [Discord Access Token-je](https://discord.com/developers)  
- **Prefix:** Prefix parancsok első karaktere  
- **forbiddenWords:** Tiltott szavak listája  
- **LogFilePath:** Log fájl neve  
- **SafeChannelId & WelcomeChannelId:** Discord csatornák egyedi azonosítói  
- **Apis:** Az API-k URL-jei  
- **MovieApiKey:** Igényelhető a [TMDB Developer](https://developer.themoviedb.org/docs/getting-started) oldalon  

> 💡 **Tipp:** A konfigurációt a `Config/BotConfigReader` olvassa be. Minden osztály a `Program.cs`-ben ugyanazt a `config` példányt kapja, így egységes marad.

---

## Képernyőképek

<img width="467" height="232" alt="Screenshot" src="https://github.com/user-attachments/assets/1e23a3ca-ae8c-49a4-b2ff-5768f8dc3147" />

---

## Telepítés

### Előfeltételek

- Minimum [.NET 4 SDK](https://dotnet.microsoft.com/)  
- Discord bot token  
- TMDB API token  

### Lépések

```bash
# Visual Studio megnyitása

# Repo klónozása
git clone https://github.com/Riptir3/DiscordBot

# Bot futtatása
