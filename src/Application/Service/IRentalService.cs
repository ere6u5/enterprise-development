using Application.Dto;

namespace Application.Service;

/// <summary>
/// Сервис для работы с арендой
/// </summary>
public interface IRentalService
{
    /// <summary>
    /// Создание новой аренды
    /// </summary>
    /// <param name="entity">Данные аренды</param>
    /// <param name="publishEvent">Опубликован ли ивент</param>
    /// <returns>Идентификатор созданной аренды</returns>
    public Task<int> CreateRentalAsync(RentalDto entity, bool publishEvent = true);
    
    /// <summary>
    /// Получение всех аренд
    /// </summary>
    /// <returns>Список аренд</returns>
    public Task<List<RentalResponseDto>> GetAllRentalsAsync();
    
    /// <summary>
    /// Получение аренды по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор аренды</param>
    /// <returns>Аренда</returns>
    public Task<RentalResponseDto?> GetRentalAsync(int id);
    
    /// <summary>
    /// Обновление аренды
    /// </summary>
    /// <param name="id">Идентификатор аренды</param>
    /// <param name="entity">Новые данные аренды</param>
    /// <returns>Обновленная аренда</returns>
    public Task<RentalResponseDto?> UpdateRentalAsync(int id, RentalDto entity);
    
    /// <summary>
    /// Удаление аренды
    /// </summary>
    /// <param name="id">Идентификатор аренды</param>
    /// <returns>True, если удаление прошло успешно</returns>
    public Task<bool> DeleteRentalAsync(int id);
    
    /// <summary>
    /// Вывести информацию обо всех клиентах, которые брали в аренду автомобили указанной модели, упорядочить по ФИО
    /// </summary>
    /// <param name="modelId">Идентификатор модели</param>
    /// <returns>Список клиентов</returns>
    public Task<List<ClientResponseDto>> GetClientsByModelAsync(int modelId);
    
    /// <summary>
    /// Вывести информацию об автомобилях, находящихся в аренде
    /// </summary>
    /// <returns>Список автомобилей</returns>
    public Task<List<CarResponseDto>> GetRentedCarsAsync();
    
    /// <summary>
    /// Вывести топ 5 наиболее часто арендуемых автомобилей
    /// </summary>
    /// <returns>Список автомобилей с количеством аренд</returns>
    public Task<List<CarRentalCountDto>> GetTop5MostRentedCarsAsync();
    
    /// <summary>
    /// Для каждого автомобиля вывести число аренд
    /// </summary>
    /// <returns>Список автомобилей с количеством аренд</returns>
    public Task<List<CarRentalCountDto>> GetRentalCountPerCarAsync();
    
    /// <summary>
    /// Вывести топ 5 клиентов по сумме аренды
    /// </summary>
    /// <returns>Список клиентов с суммой аренды</returns>
    public Task<List<ClientRentalSumDto>> GetTop5ClientsByRentalSumAsync();
}