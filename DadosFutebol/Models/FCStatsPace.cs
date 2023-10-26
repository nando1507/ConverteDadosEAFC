namespace DadosFutebol.Models
{
    public class FCStatsPace
    {
        public int Acceleration { get; set; }
        public int SprintSpeed { get; set; }

        public override string ToString()
        {
            return $@"{nameof(Acceleration)}: {Acceleration}; {nameof(SprintSpeed)}: {SprintSpeed}";
        }
    }
}
