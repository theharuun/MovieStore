namespace MovieStore.Services
{
    public class ConsoleLogger : IloggerService
    {
        public void Write(string message)
        {
            Console.WriteLine("[Console Logger ] - " + message);
        }
    }
}
