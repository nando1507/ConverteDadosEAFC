using System.Security.Cryptography.X509Certificates;

namespace DadosFutebol.Models
{
    public class FCStatsShooting
    {
        public int Positioning { get; set; }
        public int Finishing { get; set; }
        public int ShotPower { get; set; }
        public int LongShots { get; set; }
        public int Volleys { get; set; }
        public int Penalties { get; set; }

        public override string ToString()
        {
            return $@"{nameof(Positioning)}: {Positioning}; {nameof(Finishing)}: {Finishing}; {nameof(ShotPower)}: {ShotPower}; {nameof(LongShots)}: {LongShots}; {nameof(Volleys)}: {Volleys}; {nameof(Penalties)}: {Penalties}";
        }
    }
}
