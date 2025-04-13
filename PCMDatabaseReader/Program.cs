using PCMDatabaseReader.Services;

class Program
{
    static void Main()
    {
        // From WorldDB 2025 I think
        string filePath = "PCM2024Races.csv";  // Path to your .csv file

        string outputExcelPath = filePath.Replace(".csv", ".xlsx");

        if (!File.Exists(filePath))
        {
            Console.WriteLine("CSV file not found!");
            return;
        }

        var races = CSVRacesReader.Read(filePath);

        ExcelRacesGenerator.ExportToExcel(races, outputExcelPath, "PCM2024Races");

        Console.WriteLine($"Excel file saved to {outputExcelPath}");
    }
}