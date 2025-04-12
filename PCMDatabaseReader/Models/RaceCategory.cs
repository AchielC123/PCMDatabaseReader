namespace PCMDatabaseReader.Models
{
    public enum RaceCategory
    {
        GrandTour,
        Monument,
        WorldTourStageRace,
        WorldTourOneDayRace,
        ProSeriesStageRace,
        ProSeriesOneDayRace,
        DotOneStageRace,
        DotOneOneDayRace,
        DotTwoStageRace,
        DotTwoOneDayRace,
        Under23StageRace,
        Under23OneDayRace,
        Under23NationalCupStageRace,
        Under23NationalCupOneDayRace,
        NationalChampionship,
        NationalChampionshipITT,
        WorldChampionship,
        WorldChampionshipITT,
        EuropeanChampionship,
        EuropeanChampionshipITT,
        Other
    }

    public class RaceCatergoryMapper
    {
        public static RaceCategory Map(string raceCateGory)
        {
            return raceCateGory switch
            {
                "CWTGTFrance" => RaceCategory.GrandTour,
                "CWTGTAutres" => RaceCategory.GrandTour,

                "CWTMajeures" => RaceCategory.Monument,

                "CWTAutresToursA" => RaceCategory.WorldTourStageRace,
                "CWTAutresToursB" => RaceCategory.WorldTourStageRace,
                "CWTAutresToursC" => RaceCategory.WorldTourStageRace,

                "CWTAutresClasA" => RaceCategory.WorldTourOneDayRace,
                "CWTAutresClasB" => RaceCategory.WorldTourOneDayRace,
                "CWTAutresClasC" => RaceCategory.WorldTourOneDayRace,

                "Cont2HC" => RaceCategory.ProSeriesStageRace,
                "Cont1HC" => RaceCategory.ProSeriesOneDayRace,

                "Cont21" => RaceCategory.DotOneStageRace,
                "Cont11" => RaceCategory.DotOneOneDayRace,

                "Cont22" => RaceCategory.DotTwoStageRace,
                "Cont12" => RaceCategory.DotTwoOneDayRace,

                "Cont22U" => RaceCategory.Under23StageRace,
                "Cont12U" => RaceCategory.Under23OneDayRace,

                "U23_2NCup" => RaceCategory.Under23NationalCupStageRace,
                "U23_1NCup" => RaceCategory.Under23NationalCupOneDayRace,

                "EuropeanChampionship" => RaceCategory.EuropeanChampionship,
                "EuropeanChampionshipITT" => RaceCategory.EuropeanChampionshipITT,

                "WorldChampionship" => RaceCategory.WorldChampionship,
                "WorldChampionshipITT" => RaceCategory.WorldChampionshipITT,

                "NationalChampionship" => RaceCategory.NationalChampionship,
                "NationalChampionshipITT" => RaceCategory.NationalChampionshipITT,

                _ => RaceCategory.Other
            };
        }
    }
}