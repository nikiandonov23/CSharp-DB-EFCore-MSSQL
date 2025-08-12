using P02_FootballBetting.Data;

namespace P02_FootballBetting
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Db Creation started...");



            try
            {
                using FootballBettingContext dbContext = new FootballBettingContext();
                Console.WriteLine("SUCCESS :)");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}
