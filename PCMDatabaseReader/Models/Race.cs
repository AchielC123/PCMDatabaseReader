namespace PCMDatabaseReader.Models
{
    public class Race
    {
        public int RaceId { get; set; }

        public string RaceName { get; set; }

        public DateOnly? RaceDate { get; set; }

        public RaceCategory RaceCategory { get; set; }
    }
}