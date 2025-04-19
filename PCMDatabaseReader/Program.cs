using PCMDatabaseReader.Services;

class Program
{
    static void Main()
    {
        // From WorldDB 2025 I think
        string filePathRaces = "Data/PCM2024Races.csv";  // Path to your .csv file
        // Needed to sort the races on date
        string filePathStages = "Data/PCM2024Stages.csv";  // Path to your .csv file

        string outputExcelPath = "BlancoPalmares.xlsx";

        if (!File.Exists(filePathRaces) || !File.Exists(filePathStages))
        {
            Console.WriteLine("CSV file not found!");
            return;
        }

        var races = CSVRacesReader.Read(filePathRaces);

        races = CSVStagesReader.AddDatesToRacesListFromStagesCSV(races, filePathStages);

        ExcelRacesGenerator.ExportToExcel(races, outputExcelPath);

        Console.WriteLine($"Excel file saved to {outputExcelPath}");
    }
}