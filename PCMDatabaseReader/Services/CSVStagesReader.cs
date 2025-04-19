using PCMDatabaseReader.Models;

namespace PCMDatabaseReader.Services
{
    public class CSVStagesReader
    {
        public static List<Race> AddDatesToRacesListFromStagesCSV(List<Race> races, string filePath)
        {
            foreach (var line in File.ReadLines(filePath))
            {
                var parts = line.Split(',');

                try
                {
                    var datePart = parts[5];
                    var date = new DateOnly(
                        year: int.Parse(datePart.Substring(0, 4)),
                        month: int.Parse(datePart.Substring(4, 2)),
                        day: int.Parse(datePart.Substring(6, 2))
                    );

                    var test = races.FirstOrDefault(x => x.RaceId == int.Parse(parts[2])).RaceDate;

                    if (races.FirstOrDefault(x => x.RaceId == int.Parse(parts[2])).RaceDate > date || races.FirstOrDefault(x => x.RaceId == int.Parse(parts[2])).RaceDate is null)
                    {
                        races.FirstOrDefault(x => x.RaceId == int.Parse(parts[2])).RaceDate = date;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error parsing line: {line}");
                    Console.WriteLine(ex.Message);
                }
            }
            return races;
        }
    }
}