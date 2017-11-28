using System;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Application = Microsoft.Office.Interop.Excel.Application;

namespace archiver.MultiArchiving
{
    // object - exporter of reports into excel application obj
    public class ReportExporter
    {

        Application _excel;
        readonly int _sessionCount;

        public ReportExporter(string filename)
        {
            _sessionCount = 1;
            StartExcel(filename);
        }

        public ReportExporter(int sessionCount, string filename)
        {
            _sessionCount = sessionCount;
            StartExcel(filename);
        }


        void StartExcel(string filename)
        {
            try
            {
                _excel = new Application();
                _excel.Visible = true;
                _excel.SheetsInNewWorkbook = 1;
                _excel.Workbooks.Add(Type.Missing);
                Fill("A1", "Файл");
                Fill("B1", filename);
                Fill("A2", "Тип кодирования");
                Fill("B2", "Тип элемента");
                Fill("C2", "Длина элемента");
                Fill("D2", "Средняя длина элемента");
                Fill("E2", "Размер файла");
                Fill("F2", "Размер файла после сжатия");
                Fill("G2", "Компрессия");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // fill a cell 
        private void Fill(string cell, object value)
        {
            Workbook workbook = _excel.Workbooks[1];
            Sheets sheets = _excel.Worksheets;
            Worksheet sheet = sheets[1];
            Range newcells = sheet.Range[cell, Type.Missing];
            newcells.Value2 = value;
        }

        public void WriteExcel(Session session, int Iteration)
        {
            Workbook workbook = _excel.Workbooks[1];
            Sheets sheets = _excel.Worksheets;
            Worksheet sheet = sheets[1];
            Iteration += 3;
            Fill("A" + Iteration, session.GetCodingType());
            Fill("B" + Iteration, session.GetElementType());
            Fill("C" + Iteration, session.ElementLength);
            Fill("D" + Iteration, session.AverageElementLength);
            Fill("E" + Iteration, session.SourceLength);
            Fill("F" + Iteration, session.EncodedLength);
            Fill("G" + Iteration, session.GetCompression());
        }

        public void Finish()
        {

            Workbook workbook = _excel.Workbooks[1];
            Sheets sheets = _excel.Worksheets;
            Worksheet sheet = sheets[1];

            ChartObjects chartObjects = sheet.ChartObjects();
            ChartObject chartObject = chartObjects.Add(10, 200, 500, 400);
            Range cells = sheet.Range["C1", "D"+(_sessionCount+1)];
            cells.Select();
            Chart chart = chartObject.Chart;
            chart.ChartType = XlChartType.xlLine;
            chart.SetSourceData(cells);

        }
    }
}
