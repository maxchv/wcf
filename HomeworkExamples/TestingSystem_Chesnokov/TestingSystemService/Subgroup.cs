using System.Collections.Generic;

namespace TestingSystemService
{
    // Подгруппа
    public class Subgroup
    {
        // ИД подгруппы
        public string Id { get; set; }

        // Вопросы
        public List<Question> Questions { get; set; } = new List<Question>();

        // коллбек
        public IServiceCallback Callback { get; set; }

        // таймер
        public TimerEx Timer { get; set; }
    }
}