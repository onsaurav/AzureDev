namespace AI_102.Model
{
    public class VisionAnalysisResult
    {
        public Caption Caption { get; set; } = new Caption();
        public List<LineText> LineTexts { get; set; } = new List<LineText>();
    }

    public class Caption
    {
        public string Title { get; set; }
        public float Confidence { get; set; }
    }

    public class LineText
    {
        public string Text { get; set; }
        public string BoundingPolygon { get; set; }
        public List<WordText> WordTexts { get; set; } = new List<WordText>();
    }

    public class WordText
    {
        public string Text { get; set; }
        public float Confidence { get; set; }
        public string BoundingPolygon { get; set; }
    }
}
