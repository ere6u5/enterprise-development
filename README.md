# Пункт проката автомобилей

## Описание проекта
Система управления пунктом проката автомобилей, разработанная для учета автомобилей, клиентов и операций аренды. Проект реализует доменную модель, API для управления данными и аналитические запросы. Архитектура построена по принципам чистой архитектуры с разделением на слои Domain, Application, Infrastructure и API.

## Технологический стек
- **Backend**: .NET 8.0, ASP.NET Core
- **Архитектура**: Clean Architecture, Repository Pattern
- **API**: RESTful Web API, Swagger/OpenAPI
- **Тестирование**: xUnit, In-Memory репозитории
- **ORM**: Entity Framework Core (для этапа 3)
- **База данных**: In-Memory коллекции (этап 1-2), MySQL (этап 3)
- **Мессенджер**: NATS (для будущей интеграции)
- **Оркестрация**: Aspire (для этапа 3)

## Доменная модель

### Основные сущности

#### CarModel (Модель автомобиля) - справочник
- `Id` - Идентификатор модели
- `Name` - Название модели (Toyota Camry, BMW X5, и т.д.)
- `DriveType` - Тип привода (FrontWheelDrive, RearWheelDrive, AllWheelDrive)
- `SeatCount` - Количество посадочных мест
- `BodyType` - Тип кузова (Sedan, SUV, Hatchback, Coupe, и т.д.)
- `CarClass` - Класс автомобиля (Economy, Comfort, Business, Premium, Sport)

#### ModelGeneration (Поколение модели) - справочник
- `Id` - Идентификатор поколения
- `Year` - Год выпуска
- `EngineVolume` - Объем двигателя
- `TransmissionType` - Тип коробки передач (Manual, Automatic, Robotic, CVT)
- `ModelId` - Ссылка на модель автомобиля
- `Model` - Навигационное свойство к модели
- `RentalCostPerHour` - Стоимость аренды в час

#### Car (Автомобиль)
- `Id` - Идентификатор автомобиля
- `ModelGenerationId` - Ссылка на поколение модели
- `ModelGeneration` - Навигационное свойство к поколению модели
- `LicensePlate` - Государственный номер
- `Color` - Цвет автомобиля

#### Client (Клиент)
- `Id` - Идентификатор клиента
- `DriverLicenseNumber` - Номер водительского удостоверения
- `FullName` - ФИО клиента
- `BirthDate` - Дата рождения

#### Rental (Аренда) - контракт
- `Id` - Идентификатор аренды
- `CarId` - Ссылка на автомобиль
- `Car` - Навигационное свойство к автомобилю
- `ClientId` - Ссылка на клиента
- `Client` - Навигационное свойство к клиенту
- `RentalStart` - Время выдачи автомобиля
- `RentalHours` - Время аренды в часах

## Архитектура проекта

### Слои приложения

#### 1. Domain Layer (Доменный слой)
- **Entities**: Бизнес-сущности (CarModel, ModelGeneration, Car, Client, Rental)
- **Enums**: Перечисления (CarDriveType, BodyType, CarClass, TransmissionType)
- **Repositories**: Интерфейсы репозиториев (IRepository<T>)
- **Seeder**: Тестовые данные (DataSeeder)

#### 2. Application Layer (Слой приложения)
- **DTO**: Data Transfer Objects для входа/выхода
- **Services**: Бизнес-логика и обработка данных
- **Interfaces**: Контракты сервисов

#### 3. Infrastructure Layer (Слой инфраструктуры)
- **Infrastructure.InMemory**: In-Memory реализации репозиториев (для этапа 1-2)
- **Infrastructure.Db**: Entity Framework реализации (для этапа 3)

#### 4. API Layer (Слой представления)
- **Controllers**: REST API контроллеры
- **Program.cs**: Конфигурация приложения
- **AnalyticController**: Специальный контроллер для аналитических запросов

#### 5. Tests Layer (Слой тестирования)
- **Unit Tests**: xUnit тесты для бизнес-логики и аналитических запросов
- **Test Fixtures**: Тестовые данные и конфигурации

#### 6. Hosting Layer (Слой оркестрации)
- **AppHost**: Aspire оркестратор для запуска приложения и БД (для этапа 3)
- **ServiceDefaults**: Конфигурация по умолчанию для Aspire

## API Endpoints

### Базовые CRUD операции

#### CarModel (Модели автомобилей)
```
GET    /CarModel                 - Получить все модели
GET    /CarModel/{id}            - Получить модель по ID
POST   /CarModel                 - Создать новую модель
PUT    /CarModel/{id}            - Обновить модель
DELETE /CarModel/{id}            - Удалить модель
```

#### ModelGeneration (Поколения моделей)
```
GET    /ModelGeneration                 - Получить все поколения
GET    /ModelGeneration/{id}            - Получить поколение по ID
POST   /ModelGeneration                 - Создать новое поколение
PUT    /ModelGeneration/{id}            - Обновить поколение
DELETE /ModelGeneration/{id}            - Удалить поколение
```

#### Car (Автомобили)
```
GET    /Car                 - Получить все автомобили
GET    /Car/{id}            - Получить автомобиль по ID
POST   /Car                 - Создать новый автомобиль
PUT    /Car/{id}            - Обновить автомобиль
DELETE /Car/{id}            - Удалить автомобиль
```

#### Client (Клиенты)
```
GET    /Client                 - Получить всех клиентов
GET    /Client/{id}            - Получить клиента по ID
POST   /Client                 - Создать нового клиента
PUT    /Client/{id}            - Обновить клиента
DELETE /Client/{id}            - Удалить клиента
```

#### Rental (Аренды)
```
GET    /Rental                 - Получить все аренды
GET    /Rental/{id}            - Получить аренду по ID
POST   /Rental                 - Создать новую аренду
PUT    /Rental/{id}            - Обновить аренду
DELETE /Rental/{id}            - Удалить аренду
```

### Аналитические запросы (Unit-тесты)

#### AnalyticController
```
GET    /api/Analytic/clients-by-model/{modelId}        - Клиенты по модели (упорядочены по ФИО)
GET    /api/Analytic/rented-cars                       - Автомобили в аренде
GET    /api/Analytic/top5-most-rented-cars             - Топ 5 арендуемых автомобилей
GET    /api/Analytic/rental-count-per-car              - Количество аренд по автомобилям
GET    /api/Analytic/top5-clients-by-rental-sum        - Топ 5 клиентов по сумме аренды
```

## Структура проекта

```
CarRental/
├── CarRental.sln                 # Файл решения
├── src/
│   ├── Domain/                   # Доменный слой
│   │   ├── Entities/             # Сущности (CarModel, ModelGeneration, Car, Client, Rental)
│   │   ├── Enums/                # Перечисления (CarDriveType, BodyType, CarClass, TransmissionType)
│   │   ├── Repositories/         # Интерфейсы репозиториев
│   │   ├── Seeder/               # Тестовые данные
│   │   └── Domain.csproj         # Проект Domain
│   │
│   ├── Application/              # Слой приложения
│   │   ├── DTO/                  # Data Transfer Objects
│   │   ├── Service/              # Сервисы и интерфейсы
│   │   └── Application.csproj    # Проект Application
│   │
│   ├── Infrastructure.InMemory/  # In-Memory инфраструктура
│   │   ├── Repositories/         # In-Memory реализации репозиториев
│   │   └── Infrastructure.InMemory.csproj
│   │
│   ├── Infrastructure.Db/        # Database инфраструктура (для этапа 3)
│   │   ├── Repositories/         # Entity Framework репозитории
│   │   ├── Migrations/           # Миграции базы данных
│   │   ├── AppDbContext.cs       # Контекст базы данных
│   │   └── Infrastructure.Db.csproj
│   │
│   ├── Api/                      # Web API слой
│   │   ├── Controllers/          # API контроллеры
│   │   ├── Program.cs            # Конфигурация приложения
│   │   ├── appsettings.json      # Конфигурационные файлы
│   │   └── Api.csproj            # Проект API
│   │
│   ├── Tests/                    # Тестовый слой
│   │   ├── CarRentalTests.cs     # Юнит-тесты
│   │   ├── CarRentalRepoTests.cs # Тесты репозиториев
│   │   ├── CarRentalFixture.cs   # Тестовые данные
│   │   └── Tests.csproj          # Проект тестов
│   │
│   ├── AppHost/                  # Оркестратор (для этапа 3)
│   │   ├── AppHost.cs            # Конфигурация оркестратора
│   │   └── AppHost.csproj        # Проект оркестратора
│   │
│   └── ServiceDefaults/          # Сервисы по умолчанию для Aspire
│       ├── Extensions.cs         # Методы расширения
│       └── ServiceDefaults.csproj
```

## Запуск проекта

### Этап 1: In-Memory хранилище (лабораторная 1)

#### Требования
- .NET 8.0 SDK или новее
- IDE (Visual Studio, VS Code, Rider)

#### Установка и запуск
1. Клонировать репозиторий
2. Восстановить зависимости:
```bash
dotnet restore
```
3. Собрать решение:
```bash
dotnet build
```
4. Запустить тесты:
```bash
cd src/Tests
dotnet test
```
5. Запустить API:
```bash
cd src/Api
dotnet run
```
6. Открыть Swagger UI в браузере: `http://localhost:5212/swagger`

### Этап 2: REST API (лабораторная 2)

#### Изменения по сравнению с этапом 1
- Реализованы все CRUD операции через REST API
- Добавлен AnalyticController для аналитических запросов
- Включен Swagger для документации API
- Сохранение данных в памяти (In-Memory коллекции)

#### Проверка работы API
1. Используйте Swagger UI для тестирования endpoints
2. Протестируйте все CRUD операции
3. Проверьте аналитические запросы через `/api/Analytic/*`
4. Убедитесь, что данные сохраняются в памяти во время работы приложения

### Этап 3: ORM и база данных (лабораторная 3)

#### Требования
- .NET 8.0 SDK
- Docker Desktop (для запуска MySQL через Aspire)
- Aspire (устанавливается автоматически)

#### Изменения по сравнению с этапом 2
- Добавлен Infrastructure.Db проект с Entity Framework
- Созданы миграции для создания таблиц в БД
- Настроен Aspire для оркестрации приложения и БД
- Реализованы DbRepository вместо InMemoryRepository
- Настроено первичное заполнение базы данных (Data Seeding)

#### Запуск с базой данных
1. Запустить AppHost проект:
```bash
cd src/AppHost
dotnet run
```
2. Aspire запустит:
   - Приложение API
   - MySQL базу данных
   - Dashboard для мониторинга
3. Проверить работу приложения через Swagger
4. Убедиться, что данные сохраняются в MySQL

## Особенности реализации

### Архитектурные паттерны
- **Clean Architecture**: Разделение на слои Domain, Application, Infrastructure, API
- **Repository Pattern**: Абстракция доступа к данным через интерфейсы IRepository<T>
- **Dependency Injection**: Внедрение зависимостей через конструкторы
- **DTO Pattern**: Отдельные классы для передачи данных между слоями
- **Service Layer**: Бизнес-логика инкапсулирована в сервисах

### Принципы проектирования
- **SOLID**: Принципы проектирования классов и интерфейсов
- **Separation of Concerns**: Разделение ответственности между слоями
- **DRY**: Повторное использование кода
- **YAGNI**: Реализация только необходимой функциональности

### Тестирование
- **xUnit**: Фреймворк для юнит-тестов
- **In-Memory репозитории**: Изоляция тестов от внешних зависимостей
- **Test Fixtures**: Переиспользование тестовых данных
- **Аналитические тесты**: Проверка всех бизнес-требований

### Документация
- **XML комментарии**: Подробная документация для всех классов и методов
- **Swagger/OpenAPI**: Автоматическая генерация документации API
- **README**: Полное описание проекта и инструкции по запуску

## Дальнейшее развитие

### Планируемые улучшения
1. **Аутентификация и авторизация**: JWT токены, ролевая модель
2. **Пагинация и фильтрация**: Для списков сущностей
3. **Кэширование**: Redis для повышения производительности
4. **Фоновая обработка**: Hangfire для фоновых задач
5. **NATS интеграция**: Асинхронная коммуникация между сервисами
6. **Графический интерфейс**: Angular/React фронтенд
7. **Мониторинг**: Prometheus + Grafana для метрик
8. **Логирование**: Structured logging с Elasticsearch

### Масштабирование
1. **Микросервисная архитектура**: Разделение на сервисы моделей, аренд, клиентов
2. **Контейнеризация**: Docker для всех компонентов
3. **Оркестрация**: Kubernetes для управления контейнерами
4. **Балансировка нагрузки**: NGINX/Traefik
5. **База данных**: Репликация и шардинг MySQL
