using PCMDatabaseReader.Services;

class Program
{
    static void Main()
    {
        // From WorldDB 2025 I think
        string filePathRaces = "Data/PCM2026Races.csv";  // Path to your .csv file
        // Needed to sort the races on date
        string filePathStages = "Data/PCM2026Stages.csv";  // Path to your .csv file

        string outputExcelPath = "BlancoPalmares2026.xlsx";

        if (!File.Exists(filePathRaces) || !File.Exists(filePathStages))
        {
            Console.WriteLine("CSV file not found!");
            return;
        }

        var races = CSVRacesReader.Read(filePathRaces);

        Console.WriteLine($"Races {races}");
        Console.WriteLine($"Races Count {races.Count}");

        races = CSVStagesReader.AddDatesToRacesListFromStagesCSV(races, filePathStages);

        Console.WriteLine($"Races {races}");
        Console.WriteLine($"Races Count {races.Count}");

        ExcelRacesGenerator.ExportToExcel(races, outputExcelPath);

        Console.WriteLine($"Excel file saved to {outputExcelPath}");
    }
}