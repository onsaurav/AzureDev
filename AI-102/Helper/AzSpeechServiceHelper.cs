using AI_102.Model;
using Azure;
using Azure.AI.Vision.ImageAnalysis;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.CognitiveServices.Speech;
using System;
using static System.Net.Mime.MediaTypeNames;

namespace AI_102.Helper
{
    public class AzSpeechServiceHelper
    {
        private readonly string _endpoint;
        private readonly string _key;
        private readonly string _region;

        public AzSpeechServiceHelper(IConfiguration config)
        {
            _endpoint = config["SPEECH_ENDPOINT"]
            ?? throw new ArgumentNullException("SPEECH_ENDPOINT is missing");

            _key = config["SPEECH_KEY"]
                ?? throw new ArgumentNullException("SPEECH_KEY is missing");

            _region = config["SPEECH_REGION"]
               ?? throw new ArgumentNullException("SPEECH_REGION is missing");
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
            catch (Exception ex)
            {
                return new Result()
                {
                    IsSuccessful = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Result> SpeakThisText(string text, string outputFilePath)
        {
            Result result = new Result() { IsSuccessful = false };

            try
            {
                if (string.IsNullOrEmpty(text))
                {
                    result.Message = "Text is empty or null";
                    return result;
                }

                var speechConfig = SpeechConfig.FromSubscription(_key, _region);
                speechConfig.SpeechSynthesisVoiceName = "en-US-AvaMultilingualNeural";

                using (var speechSynthesizer = new SpeechSynthesizer(speechConfig))
                {
                    var speechSynthesisResult = await speechSynthesizer.SpeakTextAsync(text);
                    result = new Result()
                    {
                        IsSuccessful = true,
                        Message = $"Speech synthesized for text: [{text}]",
                        Data = speechSynthesisResult
                    };

                    string filePath = $"C:\\temp\\recording\\{outputFilePath}";
                    File.WriteAllBytes(filePath, speechSynthesisResult.AudioData);
                }
            }
            catch (Exception ex)
            {
                result = new Result()
                {
                    IsSuccessful = false,
                    Message = ex.Message
                };
            }

            return result;
        }
    }
}
