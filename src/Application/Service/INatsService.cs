namespace Application.Service;

/// <summary>
/// Интерфейс для службы обмена сообщениями NATS
/// </summary>
public interface INatsService
{
    /// <summary>
    /// Публикует событие создания аренды
    /// </summary>
    /// <param name="rentalId">ID аренды</param>
    /// <param name="carId">ID автомобиля</param>
    /// <param name="clientId">ID клиента</param>
    /// <param name="rentalStart">Время начала аренды</param>
    /// <param name="rentalHours">Продолжительность аренды в часах</param>
    public Task PublishRentalCreatedAsync(int rentalId, int carId, int clientId, DateTime rentalStart, int rentalHours);
    
    /// <summary>
    /// Публикует событие окончания аренды
    /// </summary>
    /// <param name="rentalId">ID аренды</param>
    /// <param name="endTime">Время окончания</param>
    public Task PublishRentalEndedAsync(int rentalId, DateTime endTime);
}