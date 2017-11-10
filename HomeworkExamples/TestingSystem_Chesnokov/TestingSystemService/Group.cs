using System.Collections.Generic;

namespace TestingSystemService
{
    public class Group
    {
        // ИД группы
        public string Id { get; set; }

        // Подгруппы
        public List<Subgroup> Subgroups = new List<Subgroup>();
    }
}