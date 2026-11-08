# Wolves of Democracy — Back-office d'administration (Blazor)

Application web d'administration du jeu **Wolves of Democracy**, un jeu de rôle social en ligne inspiré des *Loups-Garous de Thiercelieux*.
Elle permet aux administrateurs de gérer les joueurs et de modérer les échanges du chat de partie.

> Projet universitaire réalisé en équipe de 4 dans le cadre d'une SAÉ lors de mon BUT.

![Page d'accueil de l'espace administrateur](Images-ReadMe/soutenance%20finale%20SAE%281%29.png)

![Gestion des utilisateurs](Images-ReadMe/soutenance%20finale%20SAE.png)

---

## 🎯 Contexte

Dans *Wolves of Democracy*, les joueurs débattent et votent pour démasquer les loups garous cachés parmi eux. C'est un jeu basé avant tout sur la discussion et a donc besoin d'outils de modération : c'est le rôle de ce projet, qui s'intègre dans une architecture plus large (API, base de données, services d'authentification, de jeu et d'utilisateurs).

## 🛠️ Stack technique

- **C# / .NET 9**, **Blazor** (Interactive Server)
- **Composants UI** : QuickGrid, MudBlazor, Blazored.Modal
- **Persistance** : Blazored.LocalStorage alimenté par des données de test (stubs JSON) ; un service HTTP est prêt pour la connexion à l'API du jeu
- **Localisation** : fichiers de ressources `.resx` + `RequestLocalization` ASP.NET Core
- **Qualité & DevOps** : pipeline GitLab CI (build → analyse SonarQube → build de l'image Docker → déploiement), Dockerfile multi-stage, documentation Doxygen

## 🧱 Architecture et bonnes pratiques

- **Séparation des responsabilités** : services découpés en interfaces spécialisées (`IEditUserService`, `IDataService`, `IBanService`) et injectés par **injection de dépendances** : on peut passer des données locales à l'API réelle sans toucher aux pages.
- **Modèles distincts par couche** : modèles métier, modèles d'interface (`UIModels`) et **DTO** pour les échanges avec l'API.
- **Pattern Factory** pour convertir les modèles d'une couche à l'autre.
- **Composants réutilisables** : layouts, modales de confirmation, menu de navigation, sélecteur de langue.
- **Code-behind** (`.razor.cs`) pour séparer la présentation de la logique.

```
BlazorSAE/BlazorSAE/
├── Components/      # Pages, layouts, modales et composants partagés
├── Services/        # Interfaces + implémentations (local storage, API)
├── Models/          # Modèles métier et DTO
├── UIModels/        # Modèles liés aux formulaires
├── Factories/       # Conversion entre modèles
├── Resources/       # Traductions (en français et anglais)
└── wwwroot/         # CSS, JS (thème), images, données de test
```
## 👤 Ma contribution 

- Mise en place du projet Blazor, des layouts de base et de l'espace administrateur
- Gestion complète des utilisateurs : affichage avec QuickGrid, ajout, modification, suppression avec confirmation
- Service de bannissement / débannissement et sa fenêtre de confirmation
- Pattern Factory, DTO et découpage des services en interfaces
- Service de communication avec l'API
- Système d'authentification (formulaires de connexion et d'inscription, faux service d'authentification)

## 🚀 Lancer le projet

**Prérequis** : [.NET 9 SDK](https://dotnet.microsoft.com/download)

```bash
git clone https://codefirst.iut.uca.fr/gitlab/nabarbara/blazor-sae-2a.git
cd blazor-sae-2a
dotnet run --project BlazorSAE/BlazorSAE
```

Ou avec Visual Studio : ouvrir `BlazorSAE/BlazorSAE.sln` et lancer le projet.

### Authentification

Le système d'authentification fonctionne sur une autre branche mais par soucis de compatibilité avec le changement de langue j'ai du le conserver sur une autre branche.

- Identifiant : `admin@test.com`
- Mot de passe : `admin123`

## 👥 Équipe

- Najib Barbara
- Cyril Barrallon
- Neil Bonnetot
- Anis Younes
