using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;
using Telegram.Bot.Types;

// токен из личного кабинета
string apiKey = "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";

// адрес api для взаимодействия с чат-ботом
string endpoint = "https://api.openai.com/v1/chat/completions";

// набор соообщений диалога с чат-ботом
List<Message> messages = new List<Message>();

// HttpClient для отправки сообщений
var httpClient =  new HttpClient();

// устанавливаем отправляемый в запросе токен
httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer{apiKey}");

while (true)
{
    // ввод сообщения пользователя
    Console.Write("User:");
    var userContent = Console.ReadLine();
    
    // если введенное сообщение имеет длину меньше 1 символа
    // то выходим из цикла и завершаем программу
    if (userContent is not {Length: > 0}) break;
    
    // формируем отправляемое сообщение
    var _message = new Message() { Role = "User", Content = userContent };
    
    // добавляем сообщение в список сообщений
    messages.Add(_message);
    
}

class Message
{
    [JsonPropertyName("role")] 
    public string Role { get; set; }= "";
    
    [JsonPropertyName("content")] 
    public string Content { get; set; } = "";
}

class Request
{
    [JsonPropertyName("model")] 
    public string ModelId { get; set; } = "";
    
    [JsonPropertyName("messages")] 
    public List<Message> Messages { get; set; } = new();
}