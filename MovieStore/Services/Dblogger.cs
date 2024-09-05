namespace MovieStore.Services
{
    public class DbLogger : IloggerService
    {
        public void Write(string message)
        {
            Console.WriteLine("[DB Logger ] - " + message);
        }
    }
}
