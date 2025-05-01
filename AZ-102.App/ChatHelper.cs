using Azure;
using OpenAI.Chat;
using OpenAI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.AI.OpenAI;
using System.ClientModel.Primitives;

namespace AZ_102.App
{
    public  class ChatHelper
    {
        static Uri endpoint = new Uri("https://ai-onsaurav5099ai179341971329.openai.azure.com/");
        static string model = "gpt-4o";
        static string deploymentName = "kcb-0404-gpt-4o";
        static string apiKey = "";

        public static async Task ChatWithGpt4o(List<ChatMessage> messages)
        {
            AzureOpenAIClient azureClient = new(endpoint, new AzureKeyCredential(apiKey));
            ChatClient chatClient = azureClient.GetChatClient(deploymentName);

            var response = chatClient.CompleteChatStreaming(messages);

            foreach (StreamingChatCompletionUpdate update in response)
            {
                foreach (ChatMessageContentPart updatePart in update.ContentUpdate)
                {
                    System.Console.Write(updatePart.Text);
                }
            }
        }
    }
}