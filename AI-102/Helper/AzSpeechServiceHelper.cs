using AI_102.Model;
using Azure;
using Azure.AI.Vision.ImageAnalysis;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech;
using System;

namespace AI_102.Helper
{
    public class AzSpeechServiceHelper
    {
        private readonly string _endpoint;
        private readonly string _key;

        public AzSpeechServiceHelper(IConfiguration config)
        {
            _endpoint = config["SPEECH_ENDPOINT"]
            ?? throw new ArgumentNullException("SPEECH_ENDPOINT is missing");

            _key = config["SPEECH_KEY"]
                ?? throw new ArgumentNullException("SPEECH_KEY is missing");
        }

        public async Task<Result> RecognizSpeechFromFile(string fileName)
        {
            try
            {
                if (string.IsNullOrEmpty(fileName))
                {
                    return new Result()
                    {
                        IsSuccessful = false,
                        Message = "Audio file name is empty or null"
                    };
                }

                string filePath = $"Audio/{fileName.Trim()}";

                var speechConfig = SpeechConfig.FromEndpoint(new Uri(_endpoint), _key);

                using var audioConfig = AudioConfig.FromWavFileInput(filePath);
                using var speechRecognizer = new SpeechRecognizer(speechConfig, audioConfig);

                var speechRecognitionResult = await speechRecognizer.RecognizeOnceAsync();

                return new Result()
                {
                    IsSuccessful = true,
                    Message = "Recognizd speech successfully from file",
                    Data = speechRecognitionResult
                };
            }
            catch (Exception ex) {
                return new Result()
                {
                    IsSuccessful = false,
                    Message = ex.Message
                };
            }
        }

    }
}
