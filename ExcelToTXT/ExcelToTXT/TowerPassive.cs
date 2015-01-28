using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace ExcelToTXT
{
    class TowerPassive
    {
        public void exportExcelToTxt()
        {
            string path = Directory.GetCurrentDirectory() + "\\TowerPassive";
            foreach (string file in Directory.GetFiles(path))
            {
                if (file.EndsWith(".xlsx") || file.EndsWith(".xls"))
                {
                    string[] temp = file.ToString().Split('\\');
                    string name_file = temp[temp.Length - 1].Split('.')[0];
                    writeTXT(file, name_file, path);
                }
            }
        }

        private void writeTXT(string source_file, string name_file, string path_file)
        {
            Excel.Application xlApp;
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            Excel.Range range;

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Open(source_file.ToString(), 0, true, 5, "", "", true,
                Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, false, false);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            range = xlWorkSheet.UsedRange;

            TextWriter tw = new StreamWriter(path_file + "\\" + name_file.ToString() + ".txt");
            string temp = "";

            int rows = range.Rows.Count;
            int col = int.Parse((range.Cells[2, 1] as Excel.Range).Value2.ToString());

            int col_start = int.Parse((range.Cells[4, 1] as Excel.Range).Value2.ToString());
            int col_end = col_start + col - 1;
            // header for txt
            for (int i = 1; i <= col; i++)
            {
                if (i == col)
                    temp += (range.Cells[1, i + col_start] as Excel.Range).Value2.ToString();
                else
                    temp += (range.Cells[1, i + col_start] as Excel.Range).Value2.ToString() + ";";
            }
            Console.WriteLine(temp);
            tw.WriteLine(temp);
            temp = "";

            for (int i = 2; i <= rows; i++)
            {
                for (int j = 1; j <= col; j++)
                {
                    if (j == 10)
                    {
                        float coin = float.Parse((range.Cells[i, j + col_start] as Excel.Range).Value2.ToString());
                        if (coin - (int)coin >= 0.5f)
                        {
                            temp += ((int)coin + 1).ToString() + ";";
                        }
                        else
                        {
                            temp += ((int)coin).ToString() + ";";
                        }
                    }
                    else if (j == col)
                        temp += (range.Cells[i, j + col_start] as Excel.Range).Value2.ToString();
                    else
                        temp += (range.Cells[i, j + col_start] as Excel.Range).Value2.ToString() + ";";
                }
                Console.WriteLine(temp);
                tw.WriteLine(temp);
                temp = "";
            }
            tw.Close();
            xlWorkSheet.ClearArrows();
            xlWorkBook.Close(true, null, null);
            xlApp.Quit();
            releaseObject(xlWorkSheet);
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
