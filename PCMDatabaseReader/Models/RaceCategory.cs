using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace PCMDatabaseReader.Models
{
    public enum RaceCategory
    {
        [Display(Name = "Grand Tours:")]
        GrandTour,
        [Display(Name = "Monuments:")]
        Monument,
        [Display(Name = "World Tour Stage Races:")]
        WorldTourStageRace,
        [Display(Name = "World Tour One Day Races:")]
        WorldTourOneDayRace,
        [Display(Name = "2.Pro Stage Races:")]
        ProSeriesStageRace,
        [Display(Name = "1.Pro One Day Races:")]
        ProSeriesOneDayRace,
        [Display(Name = "2.1 Stage Races:")]
        DotOneStageRace,
        [Display(Name = "1.1 One Day Races:")]
        DotOneOneDayRace,
        [Display(Name = "2.2 Stage Races:")]
        DotTwoStageRace,
        [Display(Name = "1.2 One Day Races:")]
        DotTwoOneDayRace,
        [Display(Name = "U23 Stage Races:")]
        Under23StageRace,
        [Display(Name = "U23 One Day Races:")]
        Under23OneDayRace,
        [Display(Name = "U23 National Cup Stage Races:")]
        Under23NationalCupStageRace,
        [Display(Name = "U23 National Cup One Day Races:")]
        Under23NationalCupOneDayRace,
        [Display(Name = "National Championship RC:")]
        NationalChampionship,
        [Display(Name = "National Championship ITT:")]
        NationalChampionshipITT,
        [Display(Name = "World Championship RC:")]
        WorldChampionship,
        [Display(Name = "World Championship ITT:")]
        WorldChampionshipITT,
        [Display(Name = "Euro Championship RC:")]
        EuropeanChampionship,
        [Display(Name = "Euro Championship ITT:")]
        EuropeanChampionshipITT,
        [Display(Name = "Other:")]
        Other
    }

    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum value)
        {
            return value.GetType()
                .GetMember(value.ToString())
                .FirstOrDefault()?
                .GetCustomAttribute<DisplayAttribute>()?
                .Name ?? value.ToString();
        }
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