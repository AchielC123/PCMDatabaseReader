using ClosedXML.Excel;
using PCMDatabaseReader.Models;

class Program
{
    static void Main()
    {
        // From WorldDB 2025 I think
        string filePath = "PCM2024Races.csv";  // Path to your .csv file

        string outputExcelPath = filePath.Substring(0, filePath.IndexOf(".csv")) + ".xlsx";

        if (!File.Exists(filePath))
        {
            Console.WriteLine("CSV file not found!");
            return;
        }

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
                    //Date = DateTime.Parse(parts[2], CultureInfo.InvariantCulture),
                    RaceCategory = RaceCatergoryMapper.Map(parts[6])
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing line: {line}");
                Console.WriteLine(ex.Message);
            }
        }

        var groupedRaces = races
           .OrderBy(r => r.RaceName) // Sort by raceName
           .GroupBy(r => r.RaceCategory) // Group by raceCategory
           .OrderBy(g => g.Key); // Sort by enum value

        using var workbook = new XLWorkbook();

        var worksheet = workbook.Worksheets.Add(filePath.Substring(0, filePath.IndexOf(".csv")));

        var columnNumber = 1;

        foreach (var group in groupedRaces)
        {
            //Headers
            worksheet.Range(1, columnNumber, 1, columnNumber + 1).Merge();
            worksheet.Cell(1, columnNumber).Value = group.First().RaceCategory.ToString();
            worksheet.Cell(1, columnNumber).Style.Fill.BackgroundColor = XLColor.Red;
            worksheet.Cell(1, columnNumber).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            worksheet.Cell(1, columnNumber).Style.Font.Bold = true;

            //Races
            int row = 2;
            foreach (var race in group)
            {
                worksheet.Cell(row, columnNumber).Value = race.RaceName;
                worksheet.Cell(row, columnNumber).Style.Fill.BackgroundColor = XLColor.Red;
                worksheet.Column(columnNumber).AdjustToContents();

                worksheet.Cell(row, columnNumber + 1).Value = 0;
                worksheet.Column(columnNumber + 1).Width = 10;
                
                row++;
            }
            columnNumber += 2;
        }

        workbook.SaveAs(outputExcelPath);
        Console.WriteLine($"Excel file saved to {outputExcelPath}");
    }  
}