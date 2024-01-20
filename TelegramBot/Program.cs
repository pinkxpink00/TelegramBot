using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Telegram.Bot.Types;

// токен из личного кабинета
string apiKey = "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";
// адрес api для взаимодействия с чат-ботом
string endpoint = "https://api.openai.com/v1/chat/completions";
// набор соообщений диалога с чат-ботом
List<Message> messages = new List<Message>();

var httpClient =  new HttpClient();
httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer{apiKey}");