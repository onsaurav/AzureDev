using AI_102.Model;
using Azure;
using Azure.AI.TextAnalytics;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;

namespace AI_102.Helper
{
    public class AzLanguageServiceHelper
    {
        private readonly string _endpoint;
        private readonly string _key;

        public AzLanguageServiceHelper(IConfiguration config)
        {
            _endpoint = config["AZURE_LANGUAGE_ENDPOINT"]
            ?? throw new ArgumentNullException("AZURE_LANGUAGE_ENDPOINT is missing");

            _key = config["AZURE_LANGUAGE_KEY"]
                ?? throw new ArgumentNullException("AZURE_LANGUAGE_KEY is missing");
        }

        public async Task<Result> GetLanguage(string text)
        {
            try
            {
                Uri endpoint = new(_endpoint);
                AzureKeyCredential credential = new(_key);
                TextAnalyticsClient client = new(endpoint, credential);
                
                Response<DetectedLanguage> response = await client.DetectLanguageAsync(text);
                DetectedLanguage language = response.Value;

                return new Result()
                {
                    IsSuccessful = true,
                    Message = "Language detected successfully",
                    Data = new
                    {
                        Language = language.Name,
                        ConfidenceScore = language.ConfidenceScore,
                        ISO6391Name = language.Iso6391Name
                    }
                };                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Message: {ex.Message}");
                return new Result() {
                    IsSuccessful = false,
                    Message = ex.Message
                };
            }
        }

        public async Task<Result> IdentifyComplaints(List<string> batchedDocuments)
        {
            try
            {
                Uri endpoint = new Uri(_endpoint);
                AzureKeyCredential credential = new AzureKeyCredential(_key);
                TextAnalyticsClient client = new TextAnalyticsClient(endpoint, credential);

                AnalyzeSentimentOptions options = new() { IncludeOpinionMining = true };
                Response<AnalyzeSentimentResultCollection> response = await client.AnalyzeSentimentBatchAsync(batchedDocuments, options: options);
                AnalyzeSentimentResultCollection reviews = response.Value;

                Dictionary<string, int> complaints = GetComplaints(reviews);
                string majorComplaint = complaints.Count > 0 ? complaints.Aggregate((l, r) => l.Value > r.Value ? l : r).Key : "None";

                return new Result()
                {
                    IsSuccessful = true,
                    Message = "Complaint analysis completed",
                    Data = new
                    {
                        MajorComplaint = majorComplaint,
                        AllComplaints = complaints
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

        private Dictionary<string, int> GetComplaints(AnalyzeSentimentResultCollection reviews)
        {
            Dictionary<string, int> complaints = new();
            foreach (AnalyzeSentimentResult review in reviews)
            {
                foreach (SentenceSentiment sentence in review.DocumentSentiment.Sentences)
                {
                    foreach (SentenceOpinion opinion in sentence.Opinions)
                    {
                        if (opinion.Target.Sentiment == TextSentiment.Negative)
                        {
                            complaints.TryGetValue(opinion.Target.Text, out int value);
                            complaints[opinion.Target.Text] = value + 1;
                        }
                    }
                }
            }
            return complaints;
        }
    }
}
