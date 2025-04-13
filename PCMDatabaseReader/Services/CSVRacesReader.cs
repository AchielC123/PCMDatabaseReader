using PCMDatabaseReader.Models;

namespace PCMDatabaseReader.Services
{
    public static class CSVRacesReader
    {
        public static List<Race> Read(string filePath)
        {
            var races = new List<Race>();

            foreach (var line in File.ReadLines(filePath))
            {
                var parts = line.Split(',');

                try
                {
                    races.Add(new Race
                    {
                        RaceId = int.Parse(parts[1]),
                        RaceName = parts[2],
                        RaceCategory = RaceCatergoryMapper.Map(parts[6])
                    });
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