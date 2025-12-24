CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `car_models` (
    `id` int NOT NULL AUTO_INCREMENT,
    `name` varchar(100) CHARACTER SET utf8mb4 NOT NULL,
    `drive_type` longtext CHARACTER SET utf8mb4 NOT NULL,
    `seat_count` int NOT NULL,
    `body_type` longtext CHARACTER SET utf8mb4 NOT NULL,
    `car_class` longtext CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_car_models` PRIMARY KEY (`id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `clients` (
    `id` int NOT NULL AUTO_INCREMENT,
    `driver_license_number` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    `full_name` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
    `birth_date` date NOT NULL,
    CONSTRAINT `PK_clients` PRIMARY KEY (`id`)
) CHARACTER SET=utf8mb4;

CREATE TABLE `model_generations` (
    `id` int NOT NULL AUTO_INCREMENT,
    `year` int NOT NULL,
    `engine_volume` double NOT NULL,
    `transmission_type` longtext CHARACTER SET utf8mb4 NOT NULL,
    `model_id` int NOT NULL,
    `rental_cost_per_hour` decimal(10,2) NOT NULL,
    CONSTRAINT `PK_model_generations` PRIMARY KEY (`id`),
    CONSTRAINT `FK_model_generations_car_models_model_id` FOREIGN KEY (`model_id`) REFERENCES `car_models` (`id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `cars` (
    `id` int NOT NULL AUTO_INCREMENT,
    `model_generation_id` int NOT NULL,
    `license_plate` varchar(20) CHARACTER SET utf8mb4 NOT NULL,
    `color` varchar(50) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_cars` PRIMARY KEY (`id`),
    CONSTRAINT `FK_cars_model_generations_model_generation_id` FOREIGN KEY (`model_generation_id`) REFERENCES `model_generations` (`id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE TABLE `rentals` (
    `id` int NOT NULL AUTO_INCREMENT,
    `car_id` int NOT NULL,
    `client_id` int NOT NULL,
    `rental_start` datetime(6) NOT NULL,
    `rental_hours` int NOT NULL,
    CONSTRAINT `PK_rentals` PRIMARY KEY (`id`),
    CONSTRAINT `FK_rentals_cars_car_id` FOREIGN KEY (`car_id`) REFERENCES `cars` (`id`) ON DELETE CASCADE,
    CONSTRAINT `FK_rentals_clients_client_id` FOREIGN KEY (`client_id`) REFERENCES `clients` (`id`) ON DELETE CASCADE
) CHARACTER SET=utf8mb4;

CREATE UNIQUE INDEX `IX_car_models_name` ON `car_models` (`name`);

CREATE UNIQUE INDEX `IX_cars_license_plate` ON `cars` (`license_plate`);

CREATE INDEX `IX_cars_model_generation_id` ON `cars` (`model_generation_id`);

CREATE UNIQUE INDEX `IX_clients_driver_license_number` ON `clients` (`driver_license_number`);

CREATE INDEX `IX_model_generations_model_id` ON `model_generations` (`model_id`);

CREATE INDEX `IX_rentals_car_id` ON `rentals` (`car_id`);

CREATE INDEX `IX_rentals_client_id` ON `rentals` (`client_id`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20251224015005_InitialCreate', '8.0.0');

COMMIT;

