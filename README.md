# WebjarProj API

This repository contains the source code and documentation for the WebjarProj API. The API provides functionalities for managing products, addons, features, and images.

## Endpoints

### Addons

- `GET /api/addons`: Get all addons.
- `GET /api/addons/{id}`: Get addon by ID.
- `POST /api/addons`: Create a new addon.
- `PUT /api/addons/{id}`: Update addon name and price.
- `DELETE /api/addons/{id}`: Delete an addon.

### Features

- `GET /api/features`: Get all features.
- `GET /api/features/{id}`: Get feature by ID.
- `POST /api/features`: Create a new feature.
- `PUT /api/features/{id}`: Update feature name and value.
- `DELETE /api/features/{id}`: Delete a feature.

### Images

- `POST /api/images`: Get base64 image and save it as "ImagePath.PNG".

### Products

- `GET /api/products`: Get all products. You can pass many optional parameters to filter the results.
- `GET /api/products/{id}`: Get product by ID. You can add a list of addon IDs to calculate the total price with addons.
- `POST /api/products`: Create a new product.
- `PUT /api/products/{id}`: Update a product by ID.
- `DELETE /api/products/{id}`: Delete a product and its dependencies from the database by ID.

## Database Migrations

The repository includes database migrations using Entity Framework Core. The following migrations are available:

- `create_database`: Creates the initial database schema.

## License

This project is licensed under the [MIT License](LICENSE).
