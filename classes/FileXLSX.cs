using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Practice.classes
{
    class FileXLSX : IFileHelper
    {
        public void Export(string[] data, string path)
        {
            try
            {
                using (var excelPackage = new ExcelPackage())
                {
                    excelPackage.Workbook.Properties.Author = "Манукян Артем";
                    excelPackage.Workbook.Properties.Title = "Экспорт входных данных";
                    excelPackage.Workbook.Properties.Subject = "Игры с природой";
                    excelPackage.Workbook.Properties.Created = DateTime.Now;

                    var worksheet = excelPackage.Workbook.Worksheets.Add("Лист 1");
                    for (int i = 1; i <= data.Length; i++)
                    {
                        var line = data[i-1].Split(' ');
                        for (int j = 1; j <= line.Length; j++)
                        {
                            worksheet.Cells[i, j].Value = line[j - 1];
                        }
                    }
                    var fileInfo = new FileInfo(path);
                    excelPackage.SaveAs(fileInfo);
                }
            }
            catch (Exception)
            {
                throw new Exception("Некоректный путь");
            }   
        }

        public string[] Import(string path)
        {
            var file = new FileInfo(path);
            try
            {
                using (var excelPackage = new ExcelPackage(file))
                {
                    ExcelWorksheet workSheet = excelPackage.Workbook.Worksheets[0];
                    var rowCnt = workSheet.Dimension.End.Row;
                    var colCnt = workSheet.Dimension.End.Column;
                    int[,] m = new int[colCnt, rowCnt];
                    string[] result = new string[colCnt];
                    for (int i = 0; i < colCnt; i++)
                    {
                        result[i] = workSheet.Cells[i+1, 1].Value.ToString();
                        for (int j = 1; j < rowCnt; j++)
                        {
                            result[i] = result[i] + " " + workSheet.Cells[i+1, j+1].Value.ToString();
                        }
                    }
                    return result;
                }
            }
            catch (Exception)
            {
                throw new Exception("Некоректный файл");
            }
            
        }
    }
}
