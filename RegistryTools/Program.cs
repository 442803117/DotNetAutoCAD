using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistryTools {
class Program {
    private static string[] cmdStrings = new[] {"-reg", "-unreg"};
    static  string[] langStrings = new string[] {"en", "cn"};
    static void Main(string[] args) {
        if (args.Length == 3) {
            if (!cmdStrings.Contains(args[0].ToLower())) {
                Console.WriteLine("输入的参数不正确！");
                PrintHelp();
                PrintEndInfo();
            } else {
                int version = 0;
                if (int.TryParse(args[1], out version)) {
                    if (langStrings.Contains(args[2].ToLower())) {
                        if (args[0].ToLower().Equals(cmdStrings[0])) {
                            Register.Reg(args[1], args[2],
                                         @"C:\Users\Administrator\Documents\Visual Studio 2015\Projects\DotNetAutoCAD\bin\Debug\NetCommands.dll");
                        } else {
                            Register.UnReg(args[1], args[2]);
                        }
                        PrintEndInfo();
                    } else {
                        Console.WriteLine("输入的CAD语言不正确！当前支持的CAD语言如下：");
                        Console.Write("en cn ");
                        Console.WriteLine("");
                        PrintHelp();
                        PrintEndInfo();
                    }
                } else {
                    Console.WriteLine("输入的CAD版本不正确！当前支持的CAD版本如下：");
                    foreach (int key in Register.cadVerDictionary.Keys) {
                        Console.Write(key.ToString().Substring(0, 4));
                        Console.Write(" ");
                        Console.Write(key.ToString().EndsWith("01")? "cn":"en");
                        Console.Write(" ");
                    }
                    Console.WriteLine("");
                    PrintHelp();
                    PrintEndInfo();
                }
            }
        } else {
            Console.WriteLine("输入的参数不正确！");
            PrintHelp();
            PrintEndInfo();
        }
    }

    static void PrintEndInfo() {
        Console.WriteLine("按回车键结束：");
        Console.ReadLine();
    }

    static void PrintHelp() {
        Console.WriteLine("-reg ver lang 注册插件 ver:CAD 版本 lang: en 英文版 cn 中文版");
        Console.WriteLine("eg. -reg 2017 en  注册CAD2017版插件");
        Console.WriteLine("-unreg version 卸载插件");
        Console.WriteLine("eg. -unreg 2017 en  卸载CAD2017版插件");
    }
}
}
