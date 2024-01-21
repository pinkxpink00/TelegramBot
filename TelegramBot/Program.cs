using System.Net.Http.Json;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;
using System.Net.Http;


// токен из личного кабинета
string apiKey = "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";

// адрес api для взаимодействия с чат-ботом
string endpoint = "https://api.openai.com/v1/chat/completions";

// набор соообщений диалога с чат-ботом
List<Message> messages = new List<Message>();

// HttpClient для отправки сообщений
var httpClient =  new HttpClient();

// устанавливаем отправляемый в запросе токен
httpClient.DefaultRequestHeaders.Add("Authorization",$"Bearer {apiKey}");

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

    var requestDate = new Request()
    {
        ModelId = "gpt-3.5-turbo-instruct",
        Messages = messages
    };
    using var response = await httpClient.PostAsJsonAsync(endpoint, requestDate);

    ResponseData? responseData = await response.Content.ReadFromJsonAsync<ResponseData>();
    
    var choices = responseData?.Choices ?? new List<Choise>();
    if (choices.Count == 0)
    {
        Console.WriteLine("No choices were returned by the API");
        continue;
    }
    var choice = choices[0];
    var responseMessage = choice.Message;
    // добавляем полученное сообщение в список сообщений
    messages.Add(responseMessage);
    var responseText = responseMessage.Content.Trim();
    Console.WriteLine($"ChatGPT: {responseText}");
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

class ResponseData
{
    [JsonPropertyName("id")] 
    public string Id { get; set; } = "";

    [JsonPropertyName("object")]
    public string Object { get; set; } = "";
    
    [JsonPropertyName("created")]
    public ulong Created { get; set; }

    [JsonPropertyName("choices")] 
    public List<Choise> Choices { get; set; } = new();

    [JsonPropertyName("usage")] 
    public Usage Usage { get; set; } = new();

}

class Choise
{
    [JsonPropertyName("index")]
    public int Index { get; set; }

    [JsonPropertyName("message")] 
    public Message Message { get; set; } = new();

    [JsonPropertyName("finish_reason")] 
    public string FinishReason { get; set; } = "";
}

class Usage
{
    [JsonPropertyName("promt_tokens")]
    public int PromptTokens { get; set; }
    
    [JsonPropertyName("completion_tokens")]
    public int CompletionTokens { get; set; }
    
    [JsonPropertyName("total_tokens")]
    public int TotalTokens { get; set; }
}