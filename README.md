# R6ReadRecFile  Siege Replay Parser

[![Build and Test](https://github.com/SacBruine237275/R6ReadRecFile/actions/workflows/test.yml/badge.svg)](https://github.com/SacBruine237275/R6ReadRecFile/actions/workflows/test.yml)
[![MIT License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

`R6ReadRecFile` is a command-line tool built with .NET 10 that reads Rainbow Six Siege replay files (`.rec`) and extracts match information from them. It also supports replays from Siege X.

## ✨ Features

* **Match information**: game version, date, map and game mode.
* **Players**: name, team, operator and spawn.
* **Player statistics**: kills, deaths, assists and score.
* **Folder mode**: parse every `.rec` file in a folder at once (`-r`).
* **JSON export**: write the results to a JSON file (`-json`).

### Planned
* Kill feed (who killed whom, and when).

## 🚀 Getting Started

### Download

Prebuilt self-contained binaries for Windows and Linux are available on the [Releases](https://github.com/SacBruine237275/R6ReadRecFile/releases) page. No .NET installation is needed to run them.

### Build from source

#### Prerequisites

* [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

#### Instructions

1.  **Clone the repository:**
    ```bash
    git clone https://github.com/SacBruine237275/R6ReadRecFile.git
    cd R6ReadRecFile
    ```

2.  **Run the application:**
    ```bash
    dotnet run --project R6ReadRecFile.CLI -- "PATH/TO/YOUR/FILE.rec"
    ```

3.  **Run the tests:**
    ```bash
    dotnet test
    ```

### Usage

```text
R6ReadRecFile.CLI "<path_to_file.rec>" [options]

-r            Recursive mode: give a folder instead of a file, every .rec file inside is parsed.
-json [name]  Write the output as JSON. Without a name, output.json is created in the current folder.
-silent, -s   Suppress console output (useful with -json).
-help, -h     Show the help message.
```

Example: parse a whole match folder and only write JSON:

```bash
R6ReadRecFile.CLI "C:/Replays/Match-2026-11-27" -r -json match.json -s
```

## 🗂️ Project Structure

| Project | Description |
| --- | --- |
| `R6ReadRecFile.Core` | Parsing library (header reader, zstd stream parser, models). |
| `R6ReadRecFile.CLI` | Command-line application. |
| `R6ReadRecFile.Core.Tests` | xUnit tests. |

## 🤝 How to Contribute

Contributions are welcome! Please see the contribution guide: [**CONTRIBUTING.md**](CONTRIBUTING.md).

## 🔒 Security

To report a vulnerability, please read [**SECURITY.md**](SECURITY.md).

## 📄 License

This project is licensed under the MIT License. See the [`LICENSE`](LICENSE) file for more details.

---

## Français

`R6ReadRecFile` est un outil en ligne de commande développé en .NET 10 qui lit les fichiers de replay (`.rec`) de Rainbow Six Siege et en extrait les informations du match. Les replays de Siege X sont également pris en charge.

## ✨ Fonctionnalités

* **Informations du match** : version du jeu, date, carte et mode de jeu.
* **Joueurs** : nom, équipe, opérateur et spawn.
* **Statistiques par joueur** : kills, morts, assists et score.
* **Mode dossier** : analyse tous les fichiers `.rec` d'un dossier en une fois (`-r`).
* **Export JSON** : écrit les résultats dans un fichier JSON (`-json`).

### Prévu
* Kill feed (qui a tué qui, et à quel moment).

## 🚀 Démarrage Rapide

### Téléchargement

Des exécutables autonomes pour Windows et Linux sont disponibles sur la page [Releases](https://github.com/SacBruine237275/R6ReadRecFile/releases). Aucune installation de .NET n'est nécessaire pour les lancer.

### Compiler depuis les sources

#### Prérequis

* [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

#### Instructions

1.  **Clonez le dépôt :**
    ```bash
    git clone https://github.com/SacBruine237275/R6ReadRecFile.git
    cd R6ReadRecFile
    ```

2.  **Lancez l'application :**
    ```bash
    dotnet run --project R6ReadRecFile.CLI -- "CHEMIN_VERS_VOTRE_FICHIER.rec"
    ```

3.  **Lancez les tests :**
    ```bash
    dotnet test
    ```

### Utilisation

```text
R6ReadRecFile.CLI "<chemin_vers_fichier.rec>" [options]

-r            Mode récursif : donnez un dossier au lieu d'un fichier, tous les .rec qu'il contient sont analysés.
-json [nom]   Écrit la sortie en JSON. Sans nom, output.json est créé dans le dossier courant.
-silent, -s   Supprime l'affichage console (utile avec -json).
-help, -h     Affiche l'aide.
```

Exemple : analyser tout le dossier d'un match et écrire uniquement le JSON :

```bash
R6ReadRecFile.CLI "C:/Replays/Match-2026-11-27" -r -json match.json -s
```

## 🗂️ Structure du Projet

| Projet | Description |
| --- | --- |
| `R6ReadRecFile.Core` | Bibliothèque d'analyse (lecture de l'en-tête, parsing du flux zstd, modèles). |
| `R6ReadRecFile.CLI` | Application en ligne de commande. |
| `R6ReadRecFile.Core.Tests` | Tests xUnit. |

## 🤝 Comment Contribuer

Les contributions sont les bienvenues ! Consultez le guide de contribution : [**CONTRIBUTING.md**](CONTRIBUTING.md).

## 🔒 Sécurité

Pour signaler une vulnérabilité, consultez [**SECURITY.md**](SECURITY.md).

## 📄 Licence

Ce projet est sous licence MIT. Voir le fichier [`LICENSE`](LICENSE) pour plus de détails.
