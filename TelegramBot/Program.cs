using System;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

namespace TelegramBot
{
    class Program
    {
        // Это клиент для работы с Telegram Bot API, который позволяет отправлять сообщения, управлять ботом, подписываться на обновления и многое другое.
        private static ITelegramBotClient _telegramBotClient;
        
        // Это объект с настройками работы бота. Здесь мы будем указывать, какие типы Update мы будем получать, Timeout бота и так далее.
        private static ReceiverOptions _receiverOptions;

        static async Task Main()
        {
            // Присваиваем нашей переменной значение, в параметре передаем Token, полученный от BotFather
            _telegramBotClient = new TelegramBotClient("<6952143006:AAGnQWEsAu_hgNyAfIiiK_dPzlPmy6rEn-U>");

            // Также присваем значение настройкам бота
            _receiverOptions = new ReceiverOptions()
            {
                // Тут указываем типы получаемых Update`ов, о них подробнее расказано тут https://core.telegram.org/bots/api#update
                AllowedUpdates = new []
                { // Сообщения (текст, фото/видео, голосовые/видео сообщения и т.д.)
                  UpdateType.Message  
                },
                // Параметр, отвечающий за обработку сообщений, пришедших за то время, когда ваш бот был оффлайн
                // True - не обрабатывать, False (стоит по умолчанию) - обрабаывать
                ThrowPendingUpdates = false,
            };

            using var cts = new CancellationTokenSource();
            
            // UpdateHander - обработчик приходящих Update`ов
            // ErrorHandler - обработчик ошибок, связанных с Bot API
            _telegramBotClient.StartReceiving(UpdateHandler,ErrorHandler,cts.Token,_receiverOptions);// Запускаем бота
            
            var infoAboutBot = async _telegramBotClient.GetMeAsync(); // put info in var about our bot 
            Console.WriteLine($"{infoAboutBot.FirstName} get start");

            await Task.Delay(-1); //create infinity delay, for our bot working without timerest

        }

    }
}

