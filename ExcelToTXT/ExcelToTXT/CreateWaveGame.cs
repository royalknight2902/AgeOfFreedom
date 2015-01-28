using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

using Excel = Microsoft.Office.Interop.Excel;

namespace ExcelToTXT
{
    class CreateWaveGame
    {
        //public CreateWaveGame()
        //{
        //    exportExcelToXml();
        //}

        public void exportExcelToXml()
        {
            string path = Directory.GetCurrentDirectory() + "\\Wave Game";
            foreach (string file in Directory.GetFiles(path))
            {
                if (file.EndsWith(".xlsx") || file.EndsWith(".xls"))
                {
                    string[] temp = file.ToString().Split('\\');
                    string name_file = temp[temp.Length - 1].Split('.')[0];
                    writeXml(file, name_file, path);
                }
            }
        }

        private void writeXml(string source_file, string name_file, string path_file)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Range xlRange;

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Open(source_file.ToString(), 0, true, 5, "", "", true,
                Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, false, false);

            // Create a new file in C:\\ dir
            XmlTextWriter textWriter = new XmlTextWriter(path_file + "\\" + name_file.ToString() + ".xml", null);
            // Opens the document
            textWriter.WriteStartDocument(true);

            textWriter.WriteStartElement("LevelGame");

            foreach (Excel.Worksheet xlWorkSheet in xlWorkBook.Worksheets)
            {
                xlRange = xlWorkSheet.UsedRange;
                string _szTemp = "";
                int _iTemp = 0;

                textWriter.WriteString("\n\t");
                textWriter.WriteStartElement("Map");

                _szTemp = (xlRange.Cells[2, 1] as Excel.Range).Value2.ToString();
                textWriter.WriteAttributeString("MapID", _szTemp);
                Console.WriteLine("MapID: " + _szTemp);

                _szTemp = (xlRange.Cells[2, 2] as Excel.Range).Value2.ToString();
                textWriter.WriteString("\n\t\t");
                textWriter.WriteElementString("Name", _szTemp);

                _szTemp = (xlRange.Cells[2, 3] as Excel.Range).Value2.ToString();
                textWriter.WriteString("\n\t\t");
                textWriter.WriteElementString("Money", _szTemp);

                _szTemp = (xlRange.Cells[2, 4] as Excel.Range).Value2.ToString();
                textWriter.WriteString("\n\t\t");
                textWriter.WriteElementString("Heart", _szTemp);

                _szTemp = (xlRange.Cells[2, 5] as Excel.Range).Value2.ToString();
                textWriter.WriteString("\n\t\t");
                textWriter.WriteElementString("StarTotal", _szTemp);

                _szTemp = (xlRange.Cells[2, 7] as Excel.Range).Value2.ToString();
                textWriter.WriteString("\n\t\t");
                textWriter.WriteElementString("TowerUsed", _szTemp);

                textWriter.WriteString("\n\t\t");
                textWriter.WriteStartElement("Waves");

                int _iTotalWave = int.Parse((xlRange.Cells[2, 6] as Excel.Range).Value2.ToString());
                Console.WriteLine("Total wave of this map: " + _iTotalWave);
                string _szEnemies = "";
                string _szLevels = "";
                string _szTimes = "";
                string _szNumber = "";
                for (int i = 0; i < _iTotalWave; i++)
                {
                    textWriter.WriteString("\n\t\t\t");
                    textWriter.WriteStartElement("Wave");

                    _szTemp = (xlRange.Cells[11 + i, 1] as Excel.Range).Value2.ToString();
                    textWriter.WriteAttributeString("WaveID", _szTemp);

                    _iTemp = int.Parse((xlRange.Cells[11 + i, 2] as Excel.Range).Value2.ToString());
                    if (_iTemp == 0)
                    {
                        textWriter.WriteAttributeString("hasBoss", "false");
                    }
                    else
                    {
                        textWriter.WriteAttributeString("hasBoss", "true");
                    }

                    _szTemp = (xlRange.Cells[11 + i, 3] as Excel.Range).Value2.ToString();
                    textWriter.WriteString("\n\t\t\t\t");
                    textWriter.WriteElementString("TimeWave", _szTemp);

                    _szTemp = (xlRange.Cells[11 + i, 4] as Excel.Range).Value2.ToString();
                    textWriter.WriteString("\n\t\t\t\t");
                    textWriter.WriteElementString("TimeEnemy", _szTemp);
                    textWriter.WriteString("\n\t\t\t\t");

                    _szLevels = (xlRange.Cells[11 + i, 5] as Excel.Range).Value2.ToString();
                    _szEnemies = (xlRange.Cells[11 + i, 6] as Excel.Range).Value2.ToString();
                    _szTimes = (xlRange.Cells[11 + i, 7] as Excel.Range).Value2.ToString();
                    _szNumber = (xlRange.Cells[11 + i, 8] as Excel.Range).Value2.ToString();
                    string[] _arrLevel = _szLevels.Split('-');
                    string[] _arrEnemies = _szEnemies.Split('-');
                    string[] _arrTimes = _szTimes.Split('-');
                    string[] _arrNumber = _szNumber.Split('-');
                    int _iLenght = _arrLevel.Length;
                    for (int j = 0; j < _iLenght; j++)
                    {
                        textWriter.WriteString("\n\t\t\t\t");
                        textWriter.WriteComment("Level " + _arrLevel[j]);
                        textWriter.WriteString("\n\t\t\t\t");
                        textWriter.WriteElementString("Enemy", _arrEnemies[j] + "-" + _arrNumber[j] + "-" + _arrTimes[j]);
                    }

                    textWriter.WriteString("\n\t\t\t");
                    // end element wave
                    textWriter.WriteEndElement();
                    textWriter.WriteString("\n\t\t\t");
                }

                textWriter.WriteString("\n\t\t");
                // end element Waves
                textWriter.WriteEndElement();

                textWriter.WriteString("\n\t");
                // end element Map
                textWriter.WriteEndElement();
                textWriter.WriteString("\n\t");

                xlWorkSheet.ClearArrows();
                releaseObject(xlWorkSheet);
            }

            textWriter.WriteString("\n");
            // end element LevelGame
            textWriter.WriteEndElement();
            // Ends the document.
            textWriter.WriteEndDocument();
            // close writer
            textWriter.Close();

            xlWorkBook.Close(true, null, null);
            xlApp.Quit();

            releaseObject(xlWorkBook);
            releaseObject(xlApp);

            Console.WriteLine("\nNhap enter de tiep tuc");
            Console.ReadLine();
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }

            catch (Exception ex)
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
