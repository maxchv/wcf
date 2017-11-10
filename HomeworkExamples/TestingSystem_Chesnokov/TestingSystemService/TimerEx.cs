namespace TestingSystemService
{
    public class TimerEx : System.Timers.Timer
    {
        public string Id { get; set; }

        public long TimeLeft { get; set; }

        public IServiceCallback Callback { get; set; }
    }
}