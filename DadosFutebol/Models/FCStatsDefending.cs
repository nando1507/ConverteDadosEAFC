namespace DadosFutebol.Models
{
    public class FCStatsDefending
    {
        public int Interceptions { get; set; }
        public int HeadingAccuracy { get; set; }
        public int DefAwareness { get; set; }
        public int StandingTackle { get; set; }
        public int SlidingTackle { get; set; }


        public override string ToString()
        {
            return $@"{nameof(Interceptions)}: {Interceptions}; {nameof(HeadingAccuracy)}: {HeadingAccuracy}; {nameof(DefAwareness)}: {DefAwareness}; {nameof(StandingTackle)}: {StandingTackle}; {nameof(SlidingTackle)}: {SlidingTackle}";
        }
    }
}
