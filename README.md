# Пункт проката автомобилей

## Описание проекта
Система управления пунктом проката автомобилей, разработанная для учета автомобилей, клиентов и операций аренды. Проект реализует доменную модель, API для управления данными и аналитические запросы.

## Технологический стек
- **Backend**: .NET 8.0
- **База данных**: MySQL
- **Мессенджер**: NATS
- **Тестирование**: xUnit

## Доменная модель

### Основные сущности

#### CarModel (Модель автомобиля)
- Название модели
- Тип привода (FWD, AWD, RWD)
- Количество посадочных мест
- Тип кузова (Sedan, SUV, Hatchback)
- Класс автомобиля (Economy, Business, Premium)

#### ModelGeneration (Поколение модели)
- Год выпуска
- Объем двигателя
- Тип коробки передач
- Стоимость аренды в час
- Связь с моделью автомобиля

#### Car (Автомобиль)
- Государственный номер
- Цвет
- Связь с поколением модели

#### Client (Клиент)
- Номер водительского удостоверения
- ФИО
- Дата рождения

#### Rental (Аренда)
- Дата и время начала аренды
- Продолжительность в часах
- Связь с автомобилем и клиентом

## API Endpoints

### Автомобили
- `GET /api/cars` - получить все автомобили
- `GET /api/cars/{id}` - получить автомобиль по ID
- `POST /api/cars` - создать новый автомобиль
- `PUT /api/cars/{id}` - обновить автомобиль
- `DELETE /api/cars/{id}` - удалить автомобиль

### Клиенты
- `GET /api/clients` - получить всех клиентов
- `GET /api/clients/{id}` - получить клиента по ID
- `POST /api/clients` - создать нового клиента
- `PUT /api/clients/{id}` - обновить клиента
- `DELETE /api/clients/{id}` - удалить клиента

### Аренды
- `GET /api/rentals` - получить все аренды
- `GET /api/rentals/{id}` - получить аренду по ID
- `POST /api/rentals` - создать новую аренду
- `DELETE /api/rentals/{id}` - удалить аренду

### Аналитические запросы
- `GET /api/analytics/clients-by-model/{modelName}` - клиенты по модели автомобиля
- `GET /api/analytics/rented-cars` - автомобили в аренде
- `GET /api/analytics/top-rented-cars/{count}` - топ арендуемых автомобилей
- `GET /api/analytics/rental-counts` - количество аренд по автомобилям
- `GET /api/analytics/top-clients/{count}` - топ клиентов по сумме аренд

## Юнит-тесты

### 1. Клиенты по модели автомобиля
```csharp
// Вывести информацию обо всех клиентах, которые брали в аренду автомобили 
// указанной модели, упорядочить по ФИО
var clients = rentals
    .Where(r => r.Car.ModelGeneration.Model.Name == targetModel)
    .Select(r => r.Client)
    .Distinct()
    .OrderBy(c => c.FullName)
    .ToList();
```

### 2. Автомобили в аренде
```csharp
// Вывести информацию об автомобилях, находящихся в аренде
var rentedCars = rentals
    .Where(r => r.RentalDate.AddHours(r.RentalHours) > DateTime.Now)
    .Select(r => r.Car)
    .Distinct()
    .ToList();
```

### 3. Топ арендуемых автомобилей
```csharp
// Вывести топ 5 наиболее часто арендуемых автомобилей
var topCars = rentals
    .GroupBy(r => r.Car)
    .Select(g => new { Car = g.Key, RentalCount = g.Count() })
    .OrderByDescending(x => x.RentalCount)
    .Take(5)
    .ToList();
```

### 4. Количество аренд по автомобилям
```csharp
// Для каждого автомобиля вывести число аренд
var carsWithRentalCount = cars
    .Select(car => new
    {
        Car = car,
        RentalCount = rentals.Count(r => r.CarId == car.Id)
    })
    .ToList();
```

### 5. Топ клиентов по сумме аренд
```csharp
// Вывести топ 5 клиентов по сумме аренды
var topClients = rentals
    .GroupBy(r => r.Client)
    .Select(g => new
    {
        Client = g.Key,
        TotalAmount = g.Sum(r => r.RentalHours * r.Car.ModelGeneration.RentalPricePerHour)
    })
    .OrderByDescending(x => x.TotalAmount)
    .Take(5)
    .ToList();
```

## Структура проекта

```
CarRental/
├── src/
│   ├── CarRental.API/          # Web API контроллеры
│   ├── CarRental.Domain/       # Доменная модель и сущности
│   └── CarRental.Data/         # Репозитории и сервисы
└── tests/
    └── CarRental.Domain.Tests/ # Юнит-тесты
```

## Запуск проекта

### Требования
- .NET 8.0 SDK
- MySQL Server
- NATS Server

### Установка и запуск
1. Клонировать репозиторий
2. Настроить connection strings в appsettings.json
3. Запустить MySQL и NATS
4. Выполнить команды:
```bash
dotnet restore
dotnet build
dotnet test
dotnet run --project src/CarRental.API
```

## Особенности реализации

- Чистая архитектура с разделением на Domain, Data и API слои
- In-memory репозитории для демонстрации
- LINQ запросы для аналитики
- Полное покрытие юнит-тестами
- RESTful API с DTO
- Поддержка асинхронных операций
