using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Threading.Tasks;

namespace ExcelToTXT
{
    class Program
    {
        static void Main(string[] args)
        {
            TowerInfo t = new TowerInfo();
            EnemyInfo e = new EnemyInfo();
            CreateWaveGame w = new CreateWaveGame();
            Dragon d = new Dragon();
            TowerPassive tp = new TowerPassive();
            HouseDragon hd = new HouseDragon();

            while (true)
            {

                Console.WriteLine("Lua chon tool:");
                Console.WriteLine("0 - Exit");
                Console.WriteLine("1 - Enemy info");
                Console.WriteLine("2 - Tower Info");
                Console.WriteLine("3 - Wave info");
                Console.WriteLine("4 - Dragon");
                Console.WriteLine("5 - TowerPassive");
                Console.WriteLine("6 - HouseDragon");
                Console.WriteLine("10 - All");

                string s = Console.ReadLine();

                while (s != "0" && s != "1" && s != "2" && s != "3" && s != "4" && s != "5" && s != "6" && s != "10")
                {
                    Console.WriteLine("Hay chon lai, ban chon khong dung");
                    s = Console.ReadLine();
                }

                int temp = int.Parse(s);
                switch (temp)
                {
                    case 0:
                        return;
                    case 1:
                        e.exportExcelToTxt();
                        break;
                    case 2:
                        t.exportExcelToTxt();
                        break;
                    case 3:
                        w.exportExcelToXml();
                        break;
                    case 4:
                        d.exportExcelToTxt();
                        break;
                    case 5:
                        tp.exportExcelToTxt();
                        break;
                    case 6:
                        hd.exportExcelToTxt();
                        break;
                    case 10:
                        e.exportExcelToTxt();
                        t.exportExcelToTxt();
                        w.exportExcelToXml();
                        d.exportExcelToTxt();
                        tp.exportExcelToTxt();
                        hd.exportExcelToTxt();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}