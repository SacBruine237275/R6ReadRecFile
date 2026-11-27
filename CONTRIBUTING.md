# Contribution Guide for R6ReadRecFile

Thank you for your interest in contributing to `R6ReadRecFile`! All help is welcome.

## 💬 How can I help?

* **Report Bugs**: The easiest way to contribute is to report a bug. Open an "Issue" describing the problem as precisely as possible (game version / season, and a sample `.rec` file if you can share one).
* **Suggest Enhancements**: If you have an idea for a new feature, open an "Issue" to discuss it.
* **Submit Code**: If you want to fix a bug or add a feature.
* **Security issues**: Do not open a public issue, see [SECURITY.md](SECURITY.md).

## 🛠️ Development Setup

1.  Install the [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0).
2.  Clone your fork and build the solution:
    ```bash
    dotnet build
    ```

### Debugging with a replay file (`launchSettings.json`)

`R6ReadRecFile.CLI/Properties/launchSettings.json` is **ignored by git** (it contains a personal path to a replay file). If you want to run/debug the CLI from Visual Studio or Rider with a `.rec` file, create it yourself with this content (replace the path with your own replay):

```json
{
  "profiles": {
    "R6ReadRecFile.CLI": {
      "commandName": "Project",
      "commandLineArgs": "\"<path to your .rec file>\""
    }
  }
}
```

You can add any CLI option to `commandLineArgs` (for example `-json -s`). Never commit this file.

## ✅ Pull Request (PR) Process

1.  **Fork** the repository on your GitHub account.
2.  **Create a branch** from `dev` for your changes (`git checkout -b feature/my-new-feature`).
3.  **Code!** Make sure to respect the existing code style.
4.  **Add tests** for your new features or bug fixes. This is very important!
5.  **Verify that all tests pass** by running the `dotnet test` command at the root of the project. The same tests run automatically on every PR (GitHub Actions).
6.  **Submit your Pull Request** to the `dev` branch of the original repository.
7.  **Clearly describe** the changes you have made in the PR description, and reference the related issue (e.g. `#30`).

## 🎨 Coding Style

* We follow the standard Microsoft C# style conventions.
* Use English for variable and method names to facilitate international collaboration.
* Comment complex parts of the code.

## 🤖 Use of AI

Using AI to help write code is allowed, but it must be stated in the PR description: say which parts were generated or heavily assisted by AI. You remain responsible for the code you submit, so make sure you understand it and that it is tested.

We look forward to your contributions!

---

## Français

# Guide de Contribution pour R6ReadRecFile

Un grand merci pour votre intérêt à contribuer à `R6ReadRecFile` ! Toute aide est la bienvenue.

## 💬 Comment puis-je aider ?

* **Signaler des bugs** : Le moyen le plus simple de contribuer est de signaler un bug. Ouvrez une "Issue" en décrivant le problème le plus précisément possible (version du jeu / saison, et un fichier `.rec` d'exemple si vous pouvez le partager).
* **Suggérer des améliorations** : Si vous avez une idée pour une nouvelle fonctionnalité, ouvrez une "Issue" pour en discuter.
* **Proposer du code** : Si vous souhaitez corriger un bug ou ajouter une fonctionnalité.
* **Problèmes de sécurité** : N'ouvrez pas d'issue publique, consultez [SECURITY.md](SECURITY.md).

## 🛠️ Mise en place de l'environnement

1.  Installez le [SDK .NET 10](https://dotnet.microsoft.com/download/dotnet/10.0).
2.  Clonez votre fork et compilez la solution :
    ```bash
    dotnet build
    ```

### Déboguer avec un fichier de replay (`launchSettings.json`)

`R6ReadRecFile.CLI/Properties/launchSettings.json` est **ignoré par git** (il contient un chemin personnel vers un fichier de replay). Pour lancer/déboguer le CLI depuis Visual Studio ou Rider avec un fichier `.rec`, créez-le vous-même avec ce contenu (remplacez le chemin par celui de votre replay) :

```json
{
  "profiles": {
    "R6ReadRecFile.CLI": {
      "commandName": "Project",
      "commandLineArgs": "\"<chemin vers votre fichier .rec>\""
    }
  }
}
```

Vous pouvez ajouter n'importe quelle option du CLI dans `commandLineArgs` (par exemple `-json -s`). Ne commitez jamais ce fichier.

## ✅ Processus de Pull Request (PR)

1.  **Forkez** le dépôt sur votre compte GitHub.
2.  **Créez une branche** à partir de `dev` pour votre modification (`git checkout -b feature/ma-nouvelle-feature`).
3.  **Codez !** Assurez-vous de respecter le style de code existant.
4.  **Ajoutez des tests** pour vos nouvelles fonctionnalités ou corrections de bugs. C'est très important !
5.  **Vérifiez que tous les tests passent** en lançant la commande `dotnet test` à la racine du projet. Les mêmes tests sont lancés automatiquement sur chaque PR (GitHub Actions).
6.  **Soumettez votre Pull Request** vers la branche `dev` du dépôt original.
7.  **Décrivez clairement** les changements que vous avez effectués dans la description de la PR, et référencez l'issue concernée (ex. `#30`).

## 🎨 Style de Code

* Nous suivons les conventions de style C# standard de Microsoft.
* Utilisez les noms de variables et de méthodes en anglais pour faciliter la collaboration internationale.
* Commentez les parties complexes du code.

## 🤖 Utilisation de l'IA

L'utilisation de l'IA pour écrire du code est autorisée, mais elle doit être signalée dans la description de la PR : précisez quelles parties ont été générées ou fortement assistées par l'IA. Vous restez responsable du code que vous proposez, assurez-vous donc de le comprendre et qu'il soit testé.

Nous sommes impatients de voir vos contributions !
