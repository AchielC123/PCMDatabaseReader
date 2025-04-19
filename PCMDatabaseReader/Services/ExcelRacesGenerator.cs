using ClosedXML.Excel;
using PCMDatabaseReader.Models;

namespace PCMDatabaseReader.Services
{
    /// <summary>
    /// Provides functionality to generate and export race data to an Excel file.
    /// </summary>
    public static class ExcelRacesGenerator
    {
        /// <summary>
        /// Exports a list of races to an Excel file with specified options for filtering and formatting.
        /// </summary>
        /// <param name="races">The list of races to export.</param>
        /// <param name="outputPath">The file path where the Excel file will be saved.</param>
        /// <param name="worksheetName">The name of the worksheet in the Excel file.</param>
        /// <param name="includeNationalChampionships">
        /// A boolean indicating whether to include national championship races in the export.
        /// </param>
        /// <param name="includeUnder23Races">
        /// A boolean indicating whether to include under-23 category races in the export.
        /// </param>
        public static void ExportToExcel(List<Race> races, string outputPath, string worksheetName = "PCM2024Races", bool includeNationalChampionships = true, bool includeUnder23Races = true)
        {
            if (!includeNationalChampionships)
            {
                races = races.Where(x => x.RaceCategory != RaceCategory.NationalChampionship &&
                                x.RaceCategory != RaceCategory.NationalChampionshipITT)
                            .ToList();
            }

            if (!includeUnder23Races)
            {
                races = races.Where(x => x.RaceCategory != RaceCategory.Under23OneDayRace &&
                                x.RaceCategory != RaceCategory.Under23StageRace &&
                                x.RaceCategory != RaceCategory.Under23NationalCupOneDayRace &&
                                x.RaceCategory != RaceCategory.Under23NationalCupStageRace)
                    .ToList();
            }

            // Sort and Group races
            var groupedRaces = races
                .OrderBy(r => r.RaceDate)
                .GroupBy(r => r.RaceCategory)
                .OrderBy(g => g.Key);

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add(worksheetName);

            var columnNumber = 1;
            var rowNumberChampionships = 10;

            foreach (var group in groupedRaces)
            {
                // Championships in first column under Grand Tours
                if (group.Key == RaceCategory.WorldChampionship || group.Key == RaceCategory.WorldChampionshipITT
                    || group.Key == RaceCategory.EuropeanChampionship || group.Key == RaceCategory.EuropeanChampionshipITT)
                {
                    // Header title for Championships
                    if (rowNumberChampionships == 10)
                    {
                        worksheet.Range(rowNumberChampionships, 1, rowNumberChampionships, 2).Merge();
                        worksheet.Cell(rowNumberChampionships, 1).Value = "Championships:";
                        worksheet.Cell(rowNumberChampionships, 1).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        worksheet.Cell(rowNumberChampionships, 1).Style.Fill.BackgroundColor = XLColor.Red;
                        worksheet.Cell(rowNumberChampionships, 1).Style.Font.Bold = true;
                    }

                    rowNumberChampionships++;

                    worksheet.Cell(rowNumberChampionships, 1).Value = group.First().RaceName;
                    worksheet.Cell(rowNumberChampionships, 1).Style.Fill.BackgroundColor = XLColor.Red;
                    worksheet.Cell(rowNumberChampionships, 2).Value = 0;
                    worksheet.Cell(rowNumberChampionships, 2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    worksheet.Column(2).Width = 5;

                    // Conditional formatting for victories, when greater than 0 -> green
                    worksheet.Range(worksheet.Cell(rowNumberChampionships, 1).Address, worksheet.Cell(rowNumberChampionships, 1).Address)
                        .AddConditionalFormat()
                        .WhenIsTrue($"{worksheet.Cell(rowNumberChampionships, 2).Address.ToStringFixed()} > 0")
                        .Fill.SetBackgroundColor(XLColor.Green);

                    worksheet.Column(1).AdjustToContents();
                    continue;
                }

                //Headers
                worksheet.Range(1, columnNumber, 1, columnNumber + 1).Merge();
                worksheet.Cell(1, columnNumber).Value = group.First().RaceCategory.GetDisplayName();
                worksheet.Cell(1, columnNumber).Style.Fill.BackgroundColor = XLColor.Red;
                worksheet.Cell(1, columnNumber).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                worksheet.Cell(1, columnNumber).Style.Font.Bold = true;

                //Races
                int row = 2;
                foreach (var race in group)
                {
                    var raceNameCell = worksheet.Cell(row, columnNumber);
                    var victoryCell = worksheet.Cell(row, columnNumber + 1);

                    raceNameCell.Value = race.RaceName;
                    //Default background red -> zero victories
                    raceNameCell.Style.Fill.BackgroundColor = XLColor.Red;

                    // Conditional formatting for victories, when greater than 0 -> green
                    worksheet.Range(raceNameCell.Address, raceNameCell.Address)
                        .AddConditionalFormat()
                        .WhenIsTrue($"{victoryCell.Address.ToStringFixed()} > 0")
                        .Fill.SetBackgroundColor(XLColor.Green);

                    victoryCell.Value = 0;
                    victoryCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    worksheet.Column(columnNumber).AdjustToContents();
                    worksheet.Column(columnNumber + 1).Width = 5;
                    row++;
                }
                columnNumber += 2;
            }
            workbook.SaveAs(outputPath);
        }
    }
}