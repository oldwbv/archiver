using System;
using Microsoft.Office.Interop.Excel;
using Application = Microsoft.Office.Interop.Excel.Application;

namespace archiver.MultiArchiving
{
    // object - exporter of reports into excel application obj
    public class ReportExporter
    {
        private Application _excel;
        private readonly int _sessionCount;

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

        private void StartExcel(string filename)
        {
                _excel = new Application();
                _excel.Visible = false;
                _excel.SheetsInNewWorkbook = 1;
                _excel.Workbooks.Add(Type.Missing);
                Fill(1, 1, "Файл");
                Fill(2, 1, filename);
                Fill(1, 2, "Тип кодирования");                //a
                Fill(2, 2, "Тип элемента");                   //b
                Fill(3, 2, "Длина элемента");                 //c
                Fill(4, 2, "Средняя длина элемента");         //d
                Fill(5, 2, "Размер исходного файла");         //e
                Fill(6, 2, "Размер закодированного текста");  //f
                Fill(7, 2, "Размер metadata");                //g
                Fill(8, 2, "Размер файла после сжатия");      //h
                Fill(9, 2, "Компрессия");                     //i
                Fill(10, 2, "Чистая компрессия");              //j
                Fill(11, 2, "Время затрачено, мс");            //k
        }

        // fill a cell 
        private void Fill(string cell, object value) // write 'text' into cell 'A1'' is Fill("A1", "text") 
        {
            Sheets sheets = _excel.Worksheets;
            Worksheet sheet = sheets[1];
            Range newcells = sheet.Range[cell, Type.Missing];
            newcells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            newcells.VerticalAlignment = XlVAlign.xlVAlignCenter;
            newcells.Value2 = value;

        }
        // fill a cell 
        private void Fill(int letter, int number, object value) //  write 'text' into cell 'A1' is Fill(1, 1, "text")
        {
            var newcells = _excel.Sheets[1].Cells[number, letter];
            //Range newcells = sheet.Range[letter, number, Type.Missing];
            newcells.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            newcells.VerticalAlignment = XlVAlign.xlVAlignCenter;
            newcells.Value2 = value;
        }

        public void WriteExcel(Session session, int iteration)
        {
            iteration += 3;
            Fill(1, iteration, session.GetCodingType()); // тип кодирования
            Fill(2, iteration, session.GetElementType()); // тип элемента
            Fill(3, iteration, session.ElementLength); // длина 
            Fill(4, iteration, session.AverageElementLength);
            Fill(5, iteration, session.SourceLength);
            Fill(6, iteration, session.DestinationLength-session.InfoLength);
            Fill(7, iteration, session.InfoLength);
            Fill(8, iteration, session.DestinationLength);
            Fill(9, iteration, session.GetCompression());
            Fill(10, iteration, session.GetPureCompression());
            Fill(11, iteration, session.TimeSpent.TotalMilliseconds);
        }

        public void Finish()
        {
            Sheets sheets = _excel.Worksheets;
            Worksheet sheet = sheets[1];
            Range cells = _excel.Sheets[1].Range["A1", "Z3"];
            cells.Columns.AutoFit();
            Range r = sheet.Cells[11, 1];
           
            AddDiagram(sheet, "Средняя длина слова", 3, 4);
            AddDiagram(sheet, "Длина закодированного текста", 3, 6);
            AddDiagram(sheet, "Чистая компрессия", 3, 10);
            /*cells = _excel.Union(
                 sheet.Range["C3", "C" + (_sessionCount / 4 + 2)],
                 sheet.Range["D3", "D" + (_sessionCount / 4 + 2)],
                 sheet.Range["C" + (_sessionCount / 4 + 2), "C" + (_sessionCount / 2 + 2)],
                 sheet.Range["D" + (_sessionCount / 4 + 2), "D" + (_sessionCount / 2 + 2)],
                 sheet.Range["C" + (_sessionCount / 2 + 2), "C" + (_sessionCount * 3 / 4 + 2)],
                 sheet.Range["D" + (_sessionCount / 2 + 2), "D" + (_sessionCount * 3 / 4 + 2)],
                 sheet.Range["C" + (_sessionCount * 3 / 4 + 2), "C" + (_sessionCount + 2)],
                 sheet.Range["D" + (_sessionCount * 3 / 4 + 2), "D" + (_sessionCount + 2)]
                 );*/
            //cells.Select();



            //seriesCollection.Add(wordLen);

            //SeriesCollection seriesCollection = chart.SeriesCollection();
            //Series first = seriesCollection.Item(1);
            // first.Name = "1111";
            //first.XValues = 

            //chart.SetSourceData(cells, PlotBy:XlRowCol.xlRows);
            _excel.Visible = true;
        }

        Range reverseCell(Worksheet sheet, int column, int number)
        {
            return sheet.Cells[number, column];
        }
        private void AddDiagram(Worksheet sheet, string title, int firstCol, int secondCol)
        {
            ChartObjects chartObjects = sheet.ChartObjects();
            ChartObject wordChartObject = chartObjects.Add(0, 0, 1600, 800);
            Chart wordChart = wordChartObject.Chart;
            wordChart.HasTitle = true;
            wordChart.ChartTitle.Text = title;
            
            SeriesCollection seriesCollection = wordChart.SeriesCollection();
            Series tempSeria = seriesCollection.NewSeries();
            int off = _sessionCount / 4;
            int offset = off + 2;
            tempSeria.XValues = sheet.Range[
                reverseCell(sheet, firstCol, 3),
                reverseCell(sheet, firstCol, offset)
                ];
            tempSeria.Values = sheet.Range[
                reverseCell(sheet, secondCol, 3),
                reverseCell(sheet, secondCol, offset)
                ];
            tempSeria.Name = "Блок, позиционный";
            tempSeria.ChartType = XlChartType.xlLineMarkers;

            tempSeria = seriesCollection.NewSeries();
            offset++;
            int offset2 = 2 * off + 2;
            tempSeria.XValues = sheet.Range[
                reverseCell(sheet, firstCol, offset),
                reverseCell(sheet, firstCol, offset2)
                ];
            tempSeria.Values = sheet.Range[
                reverseCell(sheet, secondCol, offset),
                reverseCell(sheet, secondCol, offset2)
                ];
            tempSeria.Name = "Блок, оптимальный";
            tempSeria.ChartType = XlChartType.xlLineMarkers;

            tempSeria = seriesCollection.NewSeries();
            offset2++;
            offset = 3 * off + 2;
            tempSeria.XValues = sheet.Range[
                reverseCell(sheet, firstCol, offset2),
                reverseCell(sheet, firstCol, offset)
                ];
            tempSeria.Values = sheet.Range[
                reverseCell(sheet, secondCol, offset2), 
                reverseCell(sheet, secondCol, offset)
                ];
            tempSeria.Name = "L-грам, позиционный";
            tempSeria.ChartType = XlChartType.xlLineMarkers;

            tempSeria = seriesCollection.NewSeries();
            offset++;
            offset2 = 4 * off + 2;
            tempSeria.XValues = sheet.Range[
                reverseCell(sheet, firstCol, offset),
                reverseCell(sheet, firstCol, offset2)
                ];
            tempSeria.Values = sheet.Range[
                reverseCell(sheet, secondCol, offset),
                reverseCell(sheet, secondCol, offset2)
                ];
            tempSeria.Name = "L-грам, оптимальный";
            tempSeria.ChartType = XlChartType.xlLineMarkers;

            ((Axis)wordChart.Axes(XlAxisType.xlCategory)).HasTitle = true;
            ((Axis) wordChart.Axes(XlAxisType.xlCategory)).AxisTitle.Text = sheet.Cells[2, firstCol].Value2.ToString();
            ((Axis)wordChart.Axes(XlAxisType.xlValue)).HasTitle = true;
            ((Axis) wordChart.Axes(XlAxisType.xlValue)).AxisTitle.Text = sheet.Cells[2, secondCol].Value2.ToString();
            wordChart.Location(XlChartLocation.xlLocationAsNewSheet, title);
        }

        ~ReportExporter()
        {
            _excel.DisplayAlerts = false;
            _excel.Workbooks.Close();
            _excel.Quit();
        }
    }
}
