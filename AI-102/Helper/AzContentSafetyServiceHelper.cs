using AI_102.Model;
using Azure;
using Azure.AI.ContentSafety;
using Azure.AI.Vision.ImageAnalysis;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System;
using System.Reflection;

namespace AI_102.Helper
{
    public class AzContentSafetyServiceHelper
    {
        private readonly string _endpoint;
        private readonly string _key;

        public AzContentSafetyServiceHelper(IConfiguration config)
        {
            _endpoint = config["CONTENT_SAFETY_ENDPOINT"]
           ?? throw new ArgumentNullException("CONTENT_SAFETY_ENDPOINT is missing");

            _key = config["CONTENT_SAFETY_KEY"]
                ?? throw new ArgumentNullException("CONTENT_SAFETY is missing"); 
        }

        public async Task<Result> AnalyzeTextContentSafety(string text)
        {
            try
            {
                ContentSafetyClient client = new ContentSafetyClient(new Uri(_endpoint), new AzureKeyCredential(_key));

                var request = new AnalyzeTextOptions(text);

                Response<AnalyzeTextResult> response = client.AnalyzeText(request); 

                return new Result()
                {
                    IsSuccessful = true,
                    Message = "Content Safety checking completed successfully",
                    Data = new
                    {
                        Hate = (response.Value.CategoriesAnalysis.FirstOrDefault(a => a.Category == TextCategory.Hate)?.Severity ?? 0),
                        SelfHarm = (response.Value.CategoriesAnalysis.FirstOrDefault(a => a.Category == TextCategory.SelfHarm)?.Severity ?? 0),
                        Sexual = (response.Value.CategoriesAnalysis.FirstOrDefault(a => a.Category == TextCategory.Sexual)?.Severity ?? 0),
                        Violence = (response.Value.CategoriesAnalysis.FirstOrDefault(a => a.Category == TextCategory.Violence)?.Severity ?? 0)
                    }
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Message: {ex.Message}");
                return new Result()
                {
                    IsSuccessful = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Result> AnalyzeImageContentSafety(string imageFileName)
        {
            try
            {
                string datapath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Public", "Images", imageFileName);
                ContentSafetyImageData image = new ContentSafetyImageData(BinaryData.FromBytes(File.ReadAllBytes(datapath)));

                ContentSafetyClient client = new ContentSafetyClient(new Uri(_endpoint), new AzureKeyCredential(_key));

                var request = new AnalyzeImageOptions(image);

                Response<AnalyzeImageResult> response = client.AnalyzeImage(request);

                return new Result()
                {
                    IsSuccessful = true,
                    Message = "Content Safety checking completed successfully",
                    Data = new
                    {
                        Hate = (response.Value.CategoriesAnalysis.FirstOrDefault(a => a.Category == ImageCategory.Hate)?.Severity ?? 0),
                        SelfHarm = (response.Value.CategoriesAnalysis.FirstOrDefault(a => a.Category == ImageCategory.SelfHarm)?.Severity ?? 0),
                        Sexual = (response.Value.CategoriesAnalysis.FirstOrDefault(a => a.Category == ImageCategory.Sexual)?.Severity ?? 0),
                        Violence = (response.Value.CategoriesAnalysis.FirstOrDefault(a => a.Category == ImageCategory.Violence)?.Severity ?? 0)
                    }
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Message: {ex.Message}");
                return new Result()
                {
                    IsSuccessful = false,
                    Message = ex.Message
                };
            }
        }
    }
}
