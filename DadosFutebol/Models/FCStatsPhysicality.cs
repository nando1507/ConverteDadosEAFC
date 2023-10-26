namespace DadosFutebol.Models
{
    public class FCStatsPhysicality
    {
        public int Jumping { get; set; }
        public int Stamina { get; set; }
        public int Strength { get; set; }
        public int Aggression { get; set; }

        public override string ToString()
        {
            return $@"{nameof(Jumping)}: {Jumping}; {nameof(Stamina)}: {Stamina}; {nameof(Strength)}: {Strength}; {nameof(Aggression)}: {Aggression}";
        }
    }
}
