namespace Application.Service;

/// <summary>
/// Interface for NATS messaging service
/// </summary>
public interface INatsService
{
    /// <summary>
    /// Интерфейс для службы обмена сообщениями NATS
    /// </summary>
    /// <param name="rentalId">Аренда ID</param>
    /// <param name="carId">Машины ID</param>
    /// <param name="clientId">ID Клиента</param>
    /// <param name="rentalStart">Время начала аренды</param>
    /// <param name="rentalHours">Продолжительность аренды в часах</param>
    public Task PublishRentalCreatedAsync(int rentalId, int carId, int clientId, DateTime rentalStart, int rentalHours);
    
    /// <summary>
    /// Публикуется событие окончания аренды
    /// </summary>
    /// <param name="rentalId">Идентификатор аренды</param>
    /// <param name="endTime">Время окончания</param>
    public Task PublishRentalEndedAsync(int rentalId, DateTime endTime);
}