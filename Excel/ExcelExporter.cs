using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OfficeOpenXml;
using System.Drawing;

namespace Library.Excel
{
    public class ExcelExporter
    {
        public MemoryStream GetExcel(List<string[]> data, string[] headers)
        {
            using (ExcelPackage excel = new ExcelPackage())
            {
                ExcelWorksheet sheet = excel.Workbook.Worksheets.Add("Gays");

                

                var headCells = sheet.Cells["A1:F1"];
                headCells.Style.Font.Bold = true;
                headCells.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                headCells.Style.Fill.BackgroundColor.SetColor(Color.Yellow);

                for (int i = 0; i < headers.Length; i++)
                {
                    sheet.Cells[1, i + 1].Value = headers[i];
                }

                for (int i = 0; i < data.Count; i++)
                {
                    for (int j = 0; j < data.FirstOrDefault().Length; j++)
                    {
                        sheet.Cells[i + 2, j + 1].Value = data[i][j];
                    }
                }

                sheet.Cells.AutoFitColumns();

                var fileStream = new MemoryStream();
                excel.SaveAs(fileStream);
                fileStream.Position = 0;

                return fileStream;
            }
               
        }

    }
}
