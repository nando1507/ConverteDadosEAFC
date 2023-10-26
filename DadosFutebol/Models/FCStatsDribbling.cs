namespace DadosFutebol.Models
{
    public class FCStatsDribbling
    {
        public int Agility { get; set; }
        public int Balance { get; set; }
        public int Reactions { get; set; }
        public int BallControl { get; set; }
        public int Dribbling { get; set; }
        public int Composure { get; set; }

        public override string ToString()
        {
            return $@"{nameof(Agility)}: {Agility}; {nameof(Balance)}: {Balance}; {nameof(Reactions)}: {Reactions}; {nameof(BallControl)}: {BallControl}; {nameof(Dribbling)}: {Dribbling}; {nameof(Composure)}: {Composure}";
        }
    }
}
