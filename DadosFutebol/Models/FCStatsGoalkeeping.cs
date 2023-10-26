namespace DadosFutebol.Models
{
    public class FCStatsGoalkeeping
    {
        public int Diving { get; set; }
        public int Handling { get; set; }
        public int Kicking { get; set; }
        public int Positioning { get; set; }
        public int Reflexes { get; set; }

        public override string ToString()
        {
            return $@"{nameof(Diving)}: {Diving}; {nameof(Handling)}: {Handling}; {nameof(Kicking)}: {Kicking}; {nameof(Positioning)}: {Positioning}; {nameof(Reflexes)}: {Reflexes}";
        }
    }
}
