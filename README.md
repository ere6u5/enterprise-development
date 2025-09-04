# CarRental - Система управления прокатом автомобилей

## Задание

В базе данных службы проката автомобилей содержится информация о парке транспортных средств, клиентах и аренде.

**Автомобиль** характеризуется поколением модели, госномером, цветом.
**Поколение модели** является справочником и содержит сведения о годе выпуска, объеме двигателя, типе коробки передач, модели. Для каждого поколения модели указывается стоимость аренды в час.
**Модель** является справочником и содержит сведения о названии модели, типе привода, числе посадочных мест, типе кузова, классе автомобиля.

**Клиент** характеризуется номером водительского удостоверения, ФИО, датой рождения.

При выдаче автомобиля клиенту фиксируется время выдачи и указывается время аренды в часах.

## Создание и структура проекта

### Инициализация решения
```bash
# Создание решения
dotnet new sln -n CarRental

# Создание проектов
dotnet new classlib -n CarRental.Domain -f net8.0
dotnet new classlib -n CarRental.Contracts -f net8.0
dotnet new classlib -n CarRental.Infrastructure -f net8.0
dotnet new xunit -n CarRental.Tests -f net8.0

# Добавление проектов в решение
dotnet sln add CarRental.Domain/CarRental.Domain.csproj
dotnet sln add CarRental.Contracts/CarRental.Contracts.csproj
dotnet sln add CarRental.Infrastructure/CarRental.Infrastructure.csproj
dotnet sln add CarRental.Tests/CarRental.Tests.csproj

# Настройка зависимостей между проектами
dotnet add CarRental.Contracts reference CarRental.Domain
dotnet add CarRental.Infrastructure reference CarRental.Domain CarRental.Contracts
dotnet add CarRental.Tests reference CarRental.Domain CarRental.Infrastructure
```

### Архитектура проекта

```
CarRental/
├── CarRental.Domain/ # Domain models and interfaces
├── CarRental.Contracts/ # DTOs and service contracts
├── CarRental.Infrastructure/ # Data access and seed data
├── CarRental.Tests/ # Unit tests
└── CarRental.sln # Solution file
```

## Модели данных

### CarModel (Модель автомобиля)
- `Id` - Идентификатор модели
- `Name` - Название модели
- `DriveType` - Тип привода (WheelDriveType)
- `SeatsCount` - Количество посадочных мест
- `BodyType` - Тип кузова (BodyType)
- `Class` - Класс автомобиля (CarClass)

### ModelGeneration (Поколение модели)
- `Id` - Идентификатор поколения
- `ModelId` - Идентификатор модели
- `Model` - Модель автомобиля
- `ReleaseYear` - Год выпуска
- `EngineVolume` - Объем двигателя
- `Transmission` - Тип коробки передач (TransmissionType)
- `RentalCostPerHour` - Стоимость аренды в час

### Car (Автомобиль)
- `Id` - Идентификатор автомобиля
- `GenerationId` - Идентификатор поколения
- `Generation` - Поколение модели
- `LicensePlate` - Госномер
- `Color` - Цвет

### Customer (Клиент)
- `Id` - Идентификатор клиента
- `DriverLicenseNumber` - Номер водительского удостоверения
- `FullName` - ФИО клиента
- `BirthDate` - Дата рождения

### Rental (Аренда)
- `Id` - Идентификатор аренды
- `CarId` - Идентификатор автомобиля
- `Car` - Арендованный автомобиль
- `CustomerId` - Идентификатор клиента
- `Customer` - Клиент
- `RentalStart` - Время начала аренды
- `RentalHours` - Продолжительность аренды в часах
- `RentalEnd` - Время окончания аренды (вычисляемое)

## Запуск проекта

### Требования
- .NET 8.0 SDK
- IDE (Visual Studio, VS Code, Rider)

### Сборка и тестирование
```bash
# Восстановление пакетов
dotnet restore

# Сборка решения
dotnet build

# Запуск тестов
dotnet test

# Запуск конкретного тестового проекта
dotnet test CarRental.Tests/CarRental.Tests.csproj
```


## Технологии

- **.NET 8.0** - платформа разработки
- **xUnit** - фреймворк для unit-тестирования
- **Bogus** - генератор тестовых данных
- **LINQ** - язык запросов к данным
