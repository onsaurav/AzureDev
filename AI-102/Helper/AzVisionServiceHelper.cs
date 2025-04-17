using AI_102.Model;
using Azure;
using Azure.AI.Vision.ImageAnalysis;
using System;

namespace AI_102.Helper
{
    public class AzVisionServiceHelper
    {
        private readonly string _endpoint;
        private readonly string _key;

        public AzVisionServiceHelper(IConfiguration config)
        {
            _endpoint = config["VISION_ENDPOINT"]
            ?? throw new ArgumentNullException("VISION_ENDPOINT is missing");

            _key = config["VISION_KEY"]
                ?? throw new ArgumentNullException("VISION_KEY is missing");
        }

        public async Task<Result> AnalyzeImage(string imageUrl)
        {
            try
            {
                if (string.IsNullOrEmpty(imageUrl))
                    imageUrl = "https://learn.microsoft.com/azure/ai-services/computer-vision/media/quickstarts/presentation.png";

                ImageAnalysisClient client = new ImageAnalysisClient(
                    new Uri(_endpoint),
                    new AzureKeyCredential(_key));

                ImageAnalysisResult result = await client.AnalyzeAsync(new Uri(imageUrl), VisualFeatures.Caption | VisualFeatures.Read,
                    new ImageAnalysisOptions { GenderNeutralCaption = true });

                List<LineText> LineTexts = new List<LineText>();

                foreach (DetectedTextBlock block in result.Read.Blocks)
                {
                    foreach (DetectedTextLine line in block.Lines)
                    {
                        LineText lineText = new LineText()
                        {
                            Text = line.Text,
                            BoundingPolygon = string.Join(" ", line.BoundingPolygon),
                            WordTexts = line.Words.Select(w => new WordText()
                            {
                                Text = w.Text,
                                BoundingPolygon = string.Join(" ", w.BoundingPolygon),
                                Confidence = w.Confidence
                            }).ToList()
                        };
                        LineTexts.Add(lineText);
                    }
                }

                return new Result()
                {
                    IsSuccessful = true,
                    Message = "Image analysis completed successfully",
                    Data = new VisionAnalysisResult()
                    {
                        Caption = new Caption()
                        {
                            Title = result.Caption.Text,
                            Confidence = result.Caption.Confidence
                        },
                        LineTexts = LineTexts
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
