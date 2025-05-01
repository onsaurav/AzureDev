using Azure;
using OpenAI.Chat;
using OpenAI;
using AZ_102.App;

Console.WriteLine("My bot");

Console.WriteLine("Start chatting with GPT-4o. Type 'exit' to quit.");

List<ChatMessage> messages = new List<ChatMessage>()
{
    new SystemChatMessage("You are a helpful assistant.")
};

while (true)
{
    Console.Write("\nYou: ");
    var userInput = Console.ReadLine();

    if (string.Equals(userInput, "exit", StringComparison.OrdinalIgnoreCase))
        break;

    messages.Add(new UserChatMessage(userInput));

    ChatHelper.ChatWithGpt4o(messages).Wait();
}

Console.WriteLine("Goodbye!");