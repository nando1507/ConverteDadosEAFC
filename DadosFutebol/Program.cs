using DadosFutebol.Enum;
using DadosFutebol.Models;
using DadosFutebol.Models.DadosProjeto;
using System.Data;
using System.Reflection;
using System.Text;
using System.Text.Json;

string caminhoPaginas = $@"D:\Paginas EA FC 24\";
string arquivo = @"EAFC24_player_ratings_database{0}.htm";
DirectoryInfo dir = new DirectoryInfo(caminhoPaginas);
var Files = dir.GetFiles("*", SearchOption.TopDirectoryOnly);
string[] TagTabela = { "<section class=\"Section_section__KHQne dropMargins_margins___ATA0 Section_compact__9TZJq\">", "</section>" };
string classeTableRow = "<tr class=\"Table_row__zokjM\">";
string tr = "<tr";

List<string> tds = new List<string>();
List<FCStats> ListaJogadores = new List<FCStats>();
List<FCStatsArquivos> doc = new List<FCStatsArquivos>();

int rank = 1;
for (int i = 1; i < Files.Length + 1; i++)
{

    string consulta = string.Concat(caminhoPaginas, string.Format(arquivo, i));
    using (StreamReader sr = new StreamReader(consulta, Encoding.UTF8))
    {
        Console.WriteLine($@"{consulta}");
        string? leitura = sr.ReadToEnd();
        int indexIni = leitura.IndexOf(TagTabela[0]);
        int indexFim = leitura.IndexOf(TagTabela[1]);
        string extraiTabela = leitura.Substring(indexIni, indexFim);
        string[]? trs = extraiTabela.Split(classeTableRow);
        string[]? trsFiltred = trs.Where(w => w.StartsWith("<td")).ToArray();

        doc.Add(new FCStatsArquivos()
        {
            id = i,
            NomeArquivo = string.Format(arquivo, i),
            indexInicial = indexIni,
            indexFinal = indexFim,
            Tamanho = extraiTabela.Length,
            Tabela = extraiTabela
        });


        for (int ji = 0; ji < trsFiltred.Length; ji++)
        {
            string dado = trsFiltred[ji];

            int PlayerRank = rank;

            string ClasseNome = "Table_profileCellAnchor__VU0JH";
            string? PlayerName = dado.Substring(dado.IndexOf(ClasseNome), dado.Substring(dado.IndexOf(ClasseNome)).IndexOf("</a>"));
            PlayerName = PlayerName.Substring(PlayerName.LastIndexOf(">") + 1);

            Console.WriteLine($@"Rank: {rank,10} :: {PlayerName,100}");


            string ClasseNacionalidade = @"Picture_image__H5fmC";
            string? PlayerNationality = dado.Split(ClasseNacionalidade)[2];
            PlayerNationality = PlayerNationality.Substring(PlayerNationality.LastIndexOf("alt") + 5);
            PlayerNationality = PlayerNationality.Substring(0, PlayerNationality.IndexOf('"'));

            string? PlayerNationalityFlagUrl = dado.Split(ClasseNacionalidade)[2];
            PlayerNationalityFlagUrl = PlayerNationalityFlagUrl.Substring(PlayerNationalityFlagUrl.IndexOf("srcSet") + 8);
            PlayerNationalityFlagUrl = PlayerNationalityFlagUrl.Substring(0, PlayerNationalityFlagUrl.IndexOf("\"/>"));

            string? PlayerPhotoURL = dado.Split(ClasseNacionalidade)[1];
            PlayerPhotoURL = PlayerPhotoURL.Substring(PlayerPhotoURL.IndexOf("srcSet") + 8);
            PlayerPhotoURL = PlayerPhotoURL.Substring(0, PlayerPhotoURL.IndexOf("\"/>"));

            string? PlayerClub = dado.Split(ClasseNacionalidade)[4];
            PlayerClub = PlayerClub.Substring(PlayerClub.LastIndexOf("alt") + 5);
            PlayerClub = PlayerClub.Substring(0, PlayerClub.IndexOf('"'));

            string? PlayerClubFlagUrl = dado.Split(ClasseNacionalidade)[4];
            PlayerClubFlagUrl = PlayerClubFlagUrl.Substring(PlayerClubFlagUrl.IndexOf("srcSet") + 8);
            PlayerClubFlagUrl = PlayerClubFlagUrl.Substring(0, PlayerClubFlagUrl.IndexOf("\"/>"));

            string classePosicao = "Table_tag__3Mxk9 generated_utility3sm__0pg6W generated_utility1lg__ECKe_";
            string? PlayerPosition = dado.Substring(dado.IndexOf(classePosicao) + classePosicao.Length + 2);
            PlayerPosition = PlayerPosition.Substring(0, PlayerPosition.IndexOf("<"));

            string classeOverall = "Table_statCellValue____Twu";
            string[] stats = dado.Split(classeOverall);
            int PlayerOverall = int.Parse(stats[1].Substring(2, stats[1].IndexOf("<") - 2));
            int PlayerPace = int.Parse(stats[2].Substring(2, stats[2].IndexOf("<") - 2));
            int PlayerShooting = int.Parse(stats[3].Substring(2, stats[3].IndexOf("<") - 2));
            int PlayerPassing = int.Parse(stats[4].Substring(2, stats[4].IndexOf("<") - 2));
            int PlayerDribbling = int.Parse(stats[5].Substring(2, stats[5].IndexOf("<") - 2));
            int PlayerDefending = int.Parse(stats[6].Substring(2, stats[6].IndexOf("<") - 2));
            int PlayerPhysicality = int.Parse(stats[7].Substring(2, stats[7].IndexOf("<") - 2));

            string classePePreferido = "Attribute_attributeValue__lb_M5";
            string? PlayerPreferredFoot = dado.Split(classePePreferido)[2];
            PlayerPreferredFoot = PlayerPreferredFoot.Substring(2, PlayerPreferredFoot.IndexOf("<") - 2);

            string? PlayerAge = dado.Split(classePePreferido)[4];
            PlayerAge = PlayerAge.Substring(2, PlayerAge.IndexOf("<") - 2);


            string classeEstrela = "aria-label=";
            string weekfoot = dado.Substring(dado.IndexOf("WEAK FOOT"));
            weekfoot = weekfoot.Substring(weekfoot.IndexOf(classeEstrela) + classeEstrela.Length + 1, 1);

            int PlayerWeakFoot = int.Parse(weekfoot);

            string skillMoves = dado.Substring(dado.IndexOf("SKILL MOVES"));
            skillMoves = skillMoves.Substring(skillMoves.IndexOf(classeEstrela) + classeEstrela.Length + 1, 1);

            int PlayerSkillMoves = int.Parse(skillMoves);

            string? PlayerAttWorkRate = dado.Split(classePePreferido)[5];
            PlayerAttWorkRate = PlayerAttWorkRate.Substring(2, PlayerAttWorkRate.IndexOf("<") - 2);

            string? PlayerDefWorkRate = dado.Split(classePePreferido)[7];
            PlayerDefWorkRate = PlayerDefWorkRate.Substring(2, PlayerDefWorkRate.IndexOf("<") - 2);


            string classeEstilos = "DetailedView_detailsItem__ZwdcY";
            string[] estilos = dado.Split(classeEstilos);

            string classeEstilo = "IconAttribute_attribute__KTIK0 generated_utility2__1zAUs";

            List<FCStyles> fCStylesPlus = new List<FCStyles>();
            int ini = 0;
            int fim = 0;
            string urlEmEstilo = string.Empty;
            int plus = dado.IndexOf("url(#PlayStylesPlusLogo_svg__a)");
            if (plus > 0)
            {
                string[] estiloPlus = estilos[1].Split(classeEstilo);

                ini = estiloPlus[1].IndexOf("span") + 5;
                fim = estiloPlus[1].LastIndexOf("</span>");
                urlEmEstilo = estiloPlus[1].Split(ClasseNacionalidade)[0];
                urlEmEstilo = urlEmEstilo.Substring(urlEmEstilo.IndexOf("srcSet") + 8);
                urlEmEstilo = urlEmEstilo.Substring(0, urlEmEstilo.IndexOf("\"/>"));
                fCStylesPlus.Add(new FCStyles()
                {
                    StyleName = estiloPlus[1].Substring(ini, fim - ini),
                    StyleURLImg = urlEmEstilo
                });
            }
            else
            {

                //Console.ReadKey();
            }

            List<FCStyles> fCStyles = new List<FCStyles>();
            if (estilos.Length > 1 && dado.IndexOf("url(#PlayStylesLogo_svg__a)") > 0)
            {

                string[] estiloSimples = estilos[plus == -1 ? 1 : 2].Split(classeEstilo);
                for (int ik = 1; ik < estiloSimples.Length; ik++)
                {
                    ini = estiloSimples[ik].IndexOf("span") + 5;
                    fim = estiloSimples[ik].LastIndexOf("</span>");

                    urlEmEstilo = estiloSimples[ik].Split(ClasseNacionalidade)[0];
                    urlEmEstilo = urlEmEstilo.Substring(urlEmEstilo.IndexOf("srcSet") + 8);
                    urlEmEstilo = urlEmEstilo.Substring(0, urlEmEstilo.IndexOf("\"/>"));


                    string nomeEstilo = estiloSimples[ik].Substring(ini);
                    nomeEstilo = nomeEstilo.Substring(0, nomeEstilo.IndexOf("</span>"));

                    fCStyles.Add(new FCStyles()
                    {
                        StyleName = nomeEstilo,
                        StyleURLImg = urlEmEstilo
                    });
                }
            }
            else
            {
                //Console.ReadKey();
            }
            #region stats
            int GoalkeepingDiving = 0;
            int GoalkeepingHandling = 0;
            int GoalkeepingKicking = 0;
            int GoalkeepingPositioning = 0;
            int GoalkeepingReflexes = 0;
            int PaceAcceleration = 0;
            int PaceSprintSpeed = 0;
            int ShootPositioning = 0;
            int ShootFinishing = 0;
            int ShootShotPower = 0;
            int ShootLongShots = 0;
            int ShootVolleys = 0;
            int ShootPenalties = 0;
            int PassingVision = 0;
            int PassingCrossing = 0;
            int PassingFreeKickAccuracy = 0;
            int PassingShotPassing = 0;
            int PassingLongPassing = 0;
            int PassingCurve = 0;
            int DribblingAgility = 0;
            int DribblingBalance = 0;
            int DribblingReactions = 0;
            int DribblingBallControl = 0;
            int DribblingDribbling = 0;
            int DribblingComposure = 0;
            int DefendingInterceptions = 0;
            int DefendingHeadingAccuracy = 0;
            int DefendingDefAwareness = 0;
            int DefendingStandingTackle = 0;
            int DefendingSlidingTackle = 0;
            int PhysicalityJumping = 0;
            int PhysicalityStamina = 0;
            int PhysicalityStrength = 0;
            int PhysicalityAggression = 0;
            #endregion

            if (PlayerPosition == "GK")
            {
                GoalkeepingDiving = TratamentoStats(FCEnumStats.GKDiving, dado);
                GoalkeepingHandling = TratamentoStats(FCEnumStats.GKHandling, dado);
                GoalkeepingKicking = TratamentoStats(FCEnumStats.GKHandling, dado);
                GoalkeepingPositioning = TratamentoStats(FCEnumStats.GKPositioning, dado);
                GoalkeepingReflexes = TratamentoStats(FCEnumStats.GKReflexes, dado);
                PaceAcceleration = TratamentoStats(FCEnumStats.Acceleration, dado);
                PaceSprintSpeed = TratamentoStats(FCEnumStats.SprintSpeed, dado);
                ShootPositioning = TratamentoStats(FCEnumStats.Positioning, dado);
                ShootFinishing = TratamentoStats(FCEnumStats.Finishing, dado);
                ShootShotPower = TratamentoStats(FCEnumStats.ShotPower, dado);
                ShootLongShots = TratamentoStats(FCEnumStats.LongShots, dado);
                ShootVolleys = TratamentoStats(FCEnumStats.Volleys, dado);
                ShootPenalties = TratamentoStats(FCEnumStats.Penalties, dado);
                PassingVision = TratamentoStats(FCEnumStats.Vision, dado);
                PassingCrossing = TratamentoStats(FCEnumStats.Crossing, dado);
                PassingFreeKickAccuracy = TratamentoStats(FCEnumStats.FreeKickAccuracy, dado);
                PassingShotPassing = TratamentoStats(FCEnumStats.ShotPassing, dado);
                PassingLongPassing = TratamentoStats(FCEnumStats.LongPassing, dado);
                PassingCurve = TratamentoStats(FCEnumStats.Curve, dado);
                DribblingAgility = TratamentoStats(FCEnumStats.Agility, dado);
                DribblingBalance = TratamentoStats(FCEnumStats.Balance, dado);
                DribblingReactions = TratamentoStats(FCEnumStats.Reactions, dado);
                DribblingBallControl = TratamentoStats(FCEnumStats.BallControl, dado);
                DribblingDribbling = TratamentoStats(FCEnumStats.Dribbling, dado);
                DribblingComposure = TratamentoStats(FCEnumStats.Composure, dado);
                DefendingInterceptions = TratamentoStats(FCEnumStats.Interceptions, dado);
                DefendingHeadingAccuracy = TratamentoStats(FCEnumStats.HeadingAccuracy, dado);
                DefendingDefAwareness = TratamentoStats(FCEnumStats.DefAwareness, dado);
                DefendingStandingTackle = TratamentoStats(FCEnumStats.StandingTackle, dado);
                DefendingSlidingTackle = TratamentoStats(FCEnumStats.SlidingTackle, dado);
                PhysicalityJumping = TratamentoStats(FCEnumStats.Jumping, dado);
                PhysicalityStamina = TratamentoStats(FCEnumStats.Stamina, dado);
                PhysicalityStrength = TratamentoStats(FCEnumStats.Strength, dado);
                PhysicalityAggression = TratamentoStats(FCEnumStats.Aggression, dado);
            }
            else
            {
                PaceAcceleration = TratamentoStats(FCEnumStats.Acceleration, dado);
                PaceSprintSpeed = TratamentoStats(FCEnumStats.SprintSpeed, dado);
                ShootPositioning = TratamentoStats(FCEnumStats.Positioning, dado);
                ShootFinishing = TratamentoStats(FCEnumStats.Finishing, dado);
                ShootShotPower = TratamentoStats(FCEnumStats.ShotPower, dado);
                ShootLongShots = TratamentoStats(FCEnumStats.LongShots, dado);
                ShootVolleys = TratamentoStats(FCEnumStats.Volleys, dado);
                ShootPenalties = TratamentoStats(FCEnumStats.Penalties, dado);
                PassingVision = TratamentoStats(FCEnumStats.Vision, dado);
                PassingCrossing = TratamentoStats(FCEnumStats.Crossing, dado);
                PassingFreeKickAccuracy = TratamentoStats(FCEnumStats.FreeKickAccuracy, dado);
                PassingShotPassing = TratamentoStats(FCEnumStats.ShotPassing, dado);
                PassingLongPassing = TratamentoStats(FCEnumStats.LongPassing, dado);
                PassingCurve = TratamentoStats(FCEnumStats.Curve, dado);
                DribblingAgility = TratamentoStats(FCEnumStats.Agility, dado);
                DribblingBalance = TratamentoStats(FCEnumStats.Balance, dado);
                DribblingReactions = TratamentoStats(FCEnumStats.Reactions, dado);
                DribblingBallControl = TratamentoStats(FCEnumStats.BallControl, dado);
                DribblingDribbling = TratamentoStats(FCEnumStats.Dribbling, dado);
                DribblingComposure = TratamentoStats(FCEnumStats.Composure, dado);
                DefendingInterceptions = TratamentoStats(FCEnumStats.Interceptions, dado);
                DefendingHeadingAccuracy = TratamentoStats(FCEnumStats.HeadingAccuracy, dado);
                DefendingDefAwareness = TratamentoStats(FCEnumStats.DefAwareness, dado);
                DefendingStandingTackle = TratamentoStats(FCEnumStats.StandingTackle, dado);
                DefendingSlidingTackle = TratamentoStats(FCEnumStats.SlidingTackle, dado);
                PhysicalityJumping = TratamentoStats(FCEnumStats.Jumping, dado);
                PhysicalityStamina = TratamentoStats(FCEnumStats.Stamina, dado);
                PhysicalityStrength = TratamentoStats(FCEnumStats.Strength, dado);
                PhysicalityAggression = TratamentoStats(FCEnumStats.Aggression, dado);
            }


            ListaJogadores.Add(new FCStats()
            {
                PlayerRank = PlayerRank,
                PlayerName = PlayerName,
                PlayerNationality = PlayerNationality,
                PlayerNationalityFlagUrl = PlayerNationalityFlagUrl,
                PlayerPhotoURL = PlayerPhotoURL,
                PlayerClub = PlayerClub,
                PlayerClubFlagUrl = PlayerClubFlagUrl,
                PlayerPosition = PlayerPosition,
                //overall e outros status
                PlayerOverall = PlayerOverall,
                PlayerPace = PlayerPace,
                PlayerShooting = PlayerShooting,
                PlayerPassing = PlayerPassing,
                PlayerDribbling = PlayerDribbling,
                PlayerDefending = PlayerDefending,
                PlayerPhysicality = PlayerPhysicality,
                //linha 2
                PlayerPreferredFoot = PlayerPreferredFoot,
                PlayerAge = PlayerAge,
                PlayerAttWorkRate = PlayerAttWorkRate,
                PlayerDefWorkRate = PlayerDefWorkRate,
                PlayerSkillMoves = PlayerSkillMoves,
                PlayerWeakFoot = PlayerWeakFoot,
                PlayStyles = fCStyles,
                PlayStylesPlus = fCStylesPlus,
                StatsPace = new FCStatsPace()
                {
                    Acceleration = PaceAcceleration,
                    SprintSpeed = PaceSprintSpeed
                },
                StatsShooting = new FCStatsShooting()
                {
                    Positioning = ShootPositioning,
                    Finishing = ShootFinishing,
                    ShotPower = ShootShotPower,
                    LongShots = ShootLongShots,
                    Volleys = ShootVolleys,
                    Penalties = ShootPenalties
                },
                StatsPassing = new FCStatsPassing()
                {
                    Vision = PassingVision,
                    Crossing = PassingCrossing,
                    FreeKickAccuracy = PassingFreeKickAccuracy,
                    ShotPassing = PassingShotPassing,
                    LongPassing = PassingLongPassing,
                    Curve = PassingCurve
                },
                StatsDribbling = new FCStatsDribbling()
                {
                    Agility = DribblingAgility,
                    Balance = DribblingBalance,
                    Reactions = DribblingReactions,
                    BallControl = DribblingBallControl,
                    Dribbling = DribblingDribbling,
                    Composure = DribblingComposure
                },
                StatsDefending = new FCStatsDefending()
                {
                    DefAwareness = DefendingInterceptions,
                    HeadingAccuracy = DefendingHeadingAccuracy,
                    Interceptions = DefendingDefAwareness,
                    SlidingTackle = DefendingStandingTackle,
                    StandingTackle = DefendingSlidingTackle
                },
                statsPhysicality = new FCStatsPhysicality()
                {
                    Jumping = PhysicalityJumping,
                    Stamina = PhysicalityStamina,
                    Strength = PhysicalityStrength,
                    Aggression = PhysicalityAggression,
                },
                statsGoalkeeping = new FCStatsGoalkeeping()
                {
                    Diving = GoalkeepingDiving,
                    Handling = GoalkeepingHandling,
                    Kicking = GoalkeepingKicking,
                    Positioning = GoalkeepingPositioning,
                    Reflexes = GoalkeepingReflexes,
                }
            });

            sr.Dispose();
            sr.Close();
            rank++;
        }
    }

}

var ListaSaida = ListToDataTable(ListaJogadores);
Console.WriteLine($@"Exportando dados em CSV");
ToCSV(@"c:\temp\EAFCStats.csv", ";", ListaSaida);
Console.WriteLine($@"Exportando dados em JS)NJ");
ToJson(@"c:\temp\EAFCStats.Json", ListaJogadores);


void ToJson<T>(string NomeArquivo, List<T> Parametros)
{
    using (StreamWriter sw = new StreamWriter(NomeArquivo, false, Encoding.UTF8))
    {
        string json = JsonSerializer.Serialize(Parametros);
        sw.AutoFlush = true;
        sw.Write(json);
        sw.Flush();
    }
}

void ToCSV(string NomeArquivo, string Delimitador, DataTable dt)
{
    using (StreamWriter sw = new StreamWriter(NomeArquivo, false, Encoding.UTF8))
    {
        sw.AutoFlush = true;
        //header
        StringBuilder builderHeader = new StringBuilder();
        string[] columnNames = dt.Columns.Cast<DataColumn>().Select(s => s.ColumnName).ToArray();

        builderHeader.AppendLine(string.Join(Delimitador, columnNames));
        sw.WriteLine(builderHeader.ToString());

        //corpo
        StringBuilder builderBody = new StringBuilder();
        foreach (DataRow row in dt.Rows)
        {
            builderBody.AppendLine(string.Join(Delimitador, row.ItemArray));
        }
        sw.Write(builderBody.ToString());
        sw.Flush();
        sw.Close();
    }
}

string DicionarioStats(FCEnumStats index)
{
    Dictionary<FCEnumStats, string> DicionarioStats = new Dictionary<FCEnumStats, string>() {

        { FCEnumStats.Acceleration, "Acceleration" },
        { FCEnumStats.SprintSpeed, "Sprint Speed" },
        { FCEnumStats.Positioning, "Positioning" },
        { FCEnumStats.Finishing, "Finishing" },
        { FCEnumStats.ShotPower, "Shot Power" },
        { FCEnumStats.LongShots, "Long Shots" },
        { FCEnumStats.Volleys, "Volleys" },
        { FCEnumStats.Penalties, "Penalties" },
        { FCEnumStats.Vision, "Vision" },
        { FCEnumStats.Crossing, "Crossing" },
        { FCEnumStats.FreeKickAccuracy, "Free Kick Accuracy" },
        { FCEnumStats.ShotPassing, "Shot Passing" },
        { FCEnumStats.LongPassing, "Long Passing" },
        { FCEnumStats.Curve, "Curve" },
        { FCEnumStats.Agility, "Agility" },
        { FCEnumStats.Balance, "Balance" },
        { FCEnumStats.Reactions, "Reactions" },
        { FCEnumStats.BallControl, "Ball Control" },
        { FCEnumStats.Dribbling, "Dribbling" },
        { FCEnumStats.Composure, "Composure" },
        { FCEnumStats.Interceptions, "Interceptions" },
        { FCEnumStats.HeadingAccuracy, "Heading Accuracy" },
        { FCEnumStats.DefAwareness, "Def Awareness" },
        { FCEnumStats.StandingTackle, "Standing Tackle" },
        { FCEnumStats.SlidingTackle, "Sliding Tackle" },
        { FCEnumStats.Jumping, "Jumping" },
        { FCEnumStats.Stamina, "Stamina" },
        { FCEnumStats.Strength, "Strength" },
        { FCEnumStats.Aggression, "Aggression" },
        { FCEnumStats.GKDiving, "GK Diving" },
        { FCEnumStats.GKHandling, "GK Handling" },
        { FCEnumStats.GKKicking, "GK Kicking" },
        { FCEnumStats.GKPositioning, "GK Positioning" },
        { FCEnumStats.GKReflexes, "GK Reflexes" }
    };

    return DicionarioStats[index];
}

int TratamentoStats(FCEnumStats index, string dado)
{
    string stat = string.Empty;
    string tratamento = string.Empty;
    stat = DicionarioStats(index);
    tratamento = dado.Substring(dado.IndexOf(stat) + stat.Length);
    tratamento = tratamento.Substring(tratamento.IndexOf(">") + 1);
    tratamento = tratamento.Substring(0, tratamento.IndexOf("<"));

    return int.Parse(tratamento);
}

DataTable ListToDataTable<T>(List<T> list)
{
    DataTable dt = new DataTable();
    foreach (PropertyInfo info in typeof(T).GetProperties())
    {
        dt.Columns.Add(new DataColumn(info.Name, info.PropertyType));
    }
    foreach (T t in list)
    {
        DataRow row = dt.NewRow();
        foreach (PropertyInfo info in typeof(T).GetProperties())
        {
            row[info.Name] = info.GetValue(t, null);
        }
        dt.Rows.Add(row);
    }
    return dt;
}


Console.ReadKey();