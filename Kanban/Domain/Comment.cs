namespace Domain
{
    public class Comment
    {
        public IExecutor Author { get; }
        public string Message { get; }
        
        public Comment(IExecutor author, string message)
        {
            Author = author;
            Message = message;
        }
    }
}