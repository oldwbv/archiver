using System;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Application = Microsoft.Office.Interop.Excel.Application;

namespace archiver
{
    // object - exporter of reports into excel application obj
    public class ReportExporter
    {
        Application _excel;
        int sessionCount;
        public ReportExporter()
        {
            sessionCount = 1;
            StartExcel();
        }

        public ReportExporter(int sessionCount)
        {
            StartExcel();
        }


        void StartExcel()
        {
            try
            {
                _excel = new Application();
                _excel.Visible = true;
                _excel.SheetsInNewWorkbook = 1;
                _excel.Workbooks.Add(Type.Missing);
                Fill("A1", "Средняя длина слова");
                Fill("B1", "");
                Fill("C1", "");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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
            Iteration+=2;
            Fill("A" + Iteration, session.SourceLength);
            Fill("B" + Iteration, session.EncodedLength);
            Fill("C" + Iteration, session.GetCompression());
            Fill("D" + Iteration, session.AverageWordLength);
        }

        public void Finish()
        {
            Workbook workbook = _excel.Workbooks[1];
            Sheets sheets = _excel.Worksheets;
            Worksheet sheet = sheets[1];

            ChartObjects chartObjects = sheet.ChartObjects();
            ChartObject chartObject = chartObjects.Add(10, 200, 500, 400);

            Range cells = sheet.Range["C1", "D"+sessionCount+1];
            cells.Select();
            Chart chart = chartObject.Chart;
            chart.SetSourceData(cells);
          
        }

    }
}
