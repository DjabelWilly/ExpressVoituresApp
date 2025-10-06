# Express Voitures App

## Description

Application de gestion et vente de véhicules d'occasion.  
Le concept : acheter des voitures aux enchères, les réparer, puis les revendre avec un petit bénéfice. Le prix de vente est calculé en ajoutant 500 € au prix d’achat et au coût des réparations. L’objectif est de générer un profit grâce à un volume de ventes suffisant.

## Fonctionnalités

- Afficher les voitures de l’inventaire
- Ajouter de nouvelles voitures
- Modifier les annonces (photos, descriptions)
- Marquer les voitures comme non disponibles

## Accès et sécurité

- **Visualisation** : tout le monde peut voir les informations.
- **Édition** : seul le propriétaire peut modifier les informations.
- Les bonnes pratiques de sécurité doivent être suivies pour protéger le site.

## Design et cohérence

- Interface simple et fonctionnelle
- Uniformité des saisies (dates via calendrier, années de voiture valides entre 1990 et aujourd’hui)
- Champs correctement étiquetés (ex. : modèle dans champ modèle, marque dans champ marque)

## Accessibilité

- Suivi des recommandations WCAG pour une utilisation par le plus grand nombre
- Ergonomie et navigabilité optimales

## Technologies

- ASP .NET Core
- Entity Framework Core
- SQL Server
