namespace DadosFutebol.Models
{
    public class FCStatsPassing
    {
        public int Vision { get; set; }
        public int Crossing { get; set; }
        public int FreeKickAccuracy { get; set; }
        public int ShotPassing { get; set; }
        public int LongPassing { get; set; }
        public int Curve { get; set; }
        public override string ToString()
        {
            return $@"{nameof(Vision)}: {Vision}; {nameof(Crossing)}: {Crossing}; {nameof(FreeKickAccuracy)}: {FreeKickAccuracy}; {nameof(ShotPassing)}: {ShotPassing}; {nameof(LongPassing)}: {LongPassing}; {nameof(Curve)}: {Curve}";
        }
    }
}
