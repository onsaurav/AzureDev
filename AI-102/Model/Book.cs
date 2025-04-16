namespace AI_102.Model
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Publication { get; set; }
        public DateOnly PublicationDate { get; set; }
        public string Language { get; set; }
        public double Price { get; set; }
    }
}
