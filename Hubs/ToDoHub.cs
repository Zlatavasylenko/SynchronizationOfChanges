using Microsoft.AspNetCore.SignalR; // Підключення бібліотеки SignalR для реалізації хабів
using Microsoft.Extensions.Logging; // Підключення бібліотеки для ведення журналу

namespace WebApplication1.Hubs
{
    // Оголошення класу ToDoHub, який наслідується від Hub
    public class ToDoHub : Hub
    {
        private readonly ILogger<ToDoHub> _logger; // Змінна для ведення журналу

        // Конструктор класу, який приймає ILogger<ToDoHub> для ведення журналу
        public ToDoHub(ILogger<ToDoHub> logger) =>
            _logger = logger; // Ініціалізація змінної ведення журналу

        // Метод для відправлення оновлень задачі
        public async Task SendTaskUpdate(string taskId)
        {
            var startTime = DateTime.Now; // Запис початкового часу

            _logger.LogInformation("Task update received"); // Логування отримання оновлення задачі

            // Відправлення оновлення всім підключеним клієнтам
            await Clients.All.SendAsync("ReceiveTaskUpdate", taskId);

            _logger.LogInformation("Task update sent to all clients"); // Логування відправлення оновлення клієнтам

            var endTime = DateTime.Now; // Запис кінцевого часу
            var elapsedTime = endTime - startTime; // Обчислення часу виконання

            // Логування часу, що знадобився для виконання методу
            _logger.LogInformation($"SendTaskUpdate took {elapsedTime.TotalMilliseconds} ms");
        }
    }
}
