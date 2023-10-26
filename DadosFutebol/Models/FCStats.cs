namespace DadosFutebol.Models
{
    public class FCStats
    {
        public int PlayerRank { get; set; }
        public string PlayerName { get; set; }
        public string PlayerPhotoURL { get; set; }
        public string PlayerNationality { get; set; }
        public string PlayerNationalityFlagUrl { get; set; }
        public string PlayerAge { get; set; }
        public int PlayerWeakFoot { get; set; }
        public int PlayerSkillMoves { get; set; }
        public string PlayerAttWorkRate { get; set; }
        public string PlayerDefWorkRate { get; set; }
        public string PlayerPreferredFoot { get; set; }
        public string PlayerClub { get; set; }
        public string PlayerClubFlagUrl { get; set; }
        public string PlayerPosition { get; set; }
        public int PlayerOverall { get; set; }
        public int PlayerPace { get; set; }
        public int PlayerShooting { get; set; }
        public int PlayerPassing { get; set; }
        public int PlayerDribbling { get; set; }
        public int PlayerDefending { get; set; }
        public int PlayerPhysicality { get; set; }
        public List<FCStyles> PlayStylesPlus { get; set; }
        public List<FCStyles> PlayStyles { get; set; }
        public FCStatsPace StatsPace { get; set; }
        public FCStatsShooting StatsShooting { get; set; }
        public FCStatsPassing StatsPassing { get; set; }
        public FCStatsDribbling StatsDribbling { get; set; }
        public FCStatsDefending StatsDefending { get; set; }
        public FCStatsPhysicality statsPhysicality { get; set; }
        public FCStatsGoalkeeping? statsGoalkeeping { get; set; }

        public override string ToString()
        {
            return $@"
                {nameof(PlayerRank)}: {PlayerRank};  
                {nameof(PlayerName)}: {PlayerName}; 
                {nameof(PlayerPhotoURL)}: {PlayerPhotoURL}; 
                {nameof(PlayerNationality)}: {PlayerNationality}; 
                {nameof(PlayerNationalityFlagUrl)}: {PlayerNationalityFlagUrl}; 
                {nameof(PlayerAge)}: {PlayerAge}; 
                {nameof(PlayerWeakFoot)}: {PlayerWeakFoot}; 
                {nameof(PlayerSkillMoves)}: {PlayerSkillMoves}; 
                {nameof(PlayerAttWorkRate)}: {PlayerAttWorkRate}; 
                {nameof(PlayerDefWorkRate)}: {PlayerDefWorkRate}; 
                {nameof(PlayerPreferredFoot)}: {PlayerPreferredFoot}; 
                {nameof(PlayerClub)}: {PlayerClub}; 
                {nameof(PlayerClubFlagUrl)}: {PlayerClubFlagUrl}; 
                {nameof(PlayerPosition)}: {PlayerPosition}; 
                {nameof(PlayerOverall)}: {PlayerOverall}; 
                {nameof(PlayerPace)}: {PlayerPace}; 
                {nameof(PlayerShooting)}: {PlayerShooting}; 
                {nameof(PlayerPassing)}: {PlayerPassing}; 
                {nameof(PlayerDribbling)}: {PlayerDribbling}; 
                {nameof(PlayerDefending)}: {PlayerDefending}; 
                {nameof(PlayerPhysicality)}: {PlayerPhysicality}; 
                {nameof(PlayStylesPlus)}: [{PlayStylesPlus.Select(s => new { w = string.Join(";", s.StyleName + ":" + s.StyleURLImg) }).ToString()}];
                {nameof(PlayStyles)}: [{PlayStyles.Select(s => new { w = string.Join(";", s.StyleName + ":" + s.StyleURLImg) }).ToString()}];
                {nameof(StatsPace)}: {StatsPace};
                {nameof(StatsShooting)}: {StatsShooting};
                {nameof(StatsPassing)}: {StatsPassing};
                {nameof(StatsDribbling)}: {StatsDribbling};
                {nameof(StatsDefending)}: {StatsDefending};
                {nameof(statsPhysicality)}: {statsPhysicality};
                {nameof(statsGoalkeeping)}: {statsGoalkeeping};
";

        }

    }
}
