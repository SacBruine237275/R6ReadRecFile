# Security Policy

## Supported Versions

Only the **latest release** (and the `dev` branch) receives security fixes. Please update to the latest version from the [Releases](https://github.com/SacBruine237275/R6ReadRecFile/releases) page before reporting an issue.

| Version | Supported |
| --- | --- |
| Latest release | ✅ |
| Older releases | ❌ |

## Reporting a Vulnerability

**Please do not report security vulnerabilities through public GitHub issues, discussions or pull requests.**

Instead, report them privately through GitHub: go to the **Security** tab of the repository and click **Report a vulnerability** ([direct link](https://github.com/SacBruine237275/R6ReadRecFile/security/advisories/new)).

Please include as much of the following as you can:

* A description of the issue and its impact.
* The version (or commit) affected.
* Steps to reproduce, and if possible a `.rec` file (or a minimal crafted file) that triggers the problem.
* Any suggested fix.

## What to Expect

This is a project maintained on free time, so response times are best effort:

* You should receive an acknowledgement within **7 days**.
* Once the issue is confirmed, a fix will be prepared and released as soon as possible.
* You will be credited in the release notes unless you prefer to stay anonymous.

## Scope

`R6ReadRecFile` reads `.rec` files, which may come from untrusted sources (files shared by other players, downloaded from the internet, etc.). Examples of issues in scope:

* A crafted `.rec` file that causes excessive memory or CPU usage, or hangs the parser.
* A crafted `.rec` file that leads to writing files outside the expected location.
* Vulnerabilities in dependencies (e.g. `ZstdSharp.Port`) that affect this project.

Out of scope: a malformed `.rec` file that simply makes the tool exit with an error message. Those are regular bugs, please open a normal issue.

---

## Français

# Politique de Sécurité

## Versions Supportées

Seule la **dernière release** (ainsi que la branche `dev`) reçoit des correctifs de sécurité. Merci de mettre à jour vers la dernière version depuis la page [Releases](https://github.com/SacBruine237275/R6ReadRecFile/releases) avant de signaler un problème.

| Version | Supportée |
| --- | --- |
| Dernière release | ✅ |
| Releases plus anciennes | ❌ |

## Signaler une Vulnérabilité

**Merci de ne pas signaler de vulnérabilité via les issues, discussions ou pull requests publiques de GitHub.**

Signalez-les plutôt de manière privée via GitHub : allez dans l'onglet **Security** du dépôt et cliquez sur **Report a vulnerability** ([lien direct](https://github.com/SacBruine237275/R6ReadRecFile/security/advisories/new)).

Merci d'inclure autant que possible les éléments suivants :

* Une description du problème et de son impact.
* La version (ou le commit) concernée.
* Les étapes pour reproduire, et si possible un fichier `.rec` (ou un fichier minimal fabriqué) qui déclenche le problème.
* Une proposition de correctif, si vous en avez une.

## À quoi s'attendre

Ce projet est maintenu sur du temps libre, les délais de réponse sont donc indicatifs :

* Vous devriez recevoir un accusé de réception sous **7 jours**.
* Une fois le problème confirmé, un correctif sera préparé et publié dès que possible.
* Vous serez crédité dans les notes de version, sauf si vous préférez rester anonyme.

## Périmètre

`R6ReadRecFile` lit des fichiers `.rec` qui peuvent provenir de sources non fiables (fichiers partagés par d'autres joueurs, téléchargés sur internet, etc.). Exemples de problèmes dans le périmètre :

* Un fichier `.rec` fabriqué qui provoque une consommation excessive de mémoire ou de CPU, ou qui bloque le parser.
* Un fichier `.rec` fabriqué qui entraîne l'écriture de fichiers en dehors de l'emplacement prévu.
* Des vulnérabilités dans les dépendances (ex. `ZstdSharp.Port`) qui affectent ce projet.

Hors périmètre : un fichier `.rec` mal formé qui fait simplement quitter l'outil avec un message d'erreur. Ce sont des bugs classiques, merci d'ouvrir une issue normale.
