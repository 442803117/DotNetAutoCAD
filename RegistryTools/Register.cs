using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace RegistryTools {

public class Register {

    public static void Reg(int ver, string location) {
        string path = string.Empty;
        cadVerDictionary.TryGetValue(ver, out path);
        RegApp("SOFTWARE\\Autodesk\\AutoCAD\\" + path, location);
    }

    public static void Reg(string ver, string lang, string location) {

        int version = Int32.Parse(ver + (lang.ToLower().Equals("cn") ? "00" : "01"));
        string path = string.Empty;
        cadVerDictionary.TryGetValue(version, out path);
        RegApp("SOFTWARE\\Autodesk\\AutoCAD\\" + path, location);
    }

    //注册程序
    private static void RegApp(string keypath, string Location) {
        try {
            RegistryKey appkey = RegistryHelper.GetRegistryKey().OpenSubKey(keypath + "\\Applications", true); //打开Applications
            RegistryKey rk = appkey.CreateSubKey("DoitTools");
            rk.SetValue("DESCRIPTION", "初始化.NET程序", RegistryValueKind.String);
            rk.SetValue("LOADCTRLS", 2, RegistryValueKind.DWord);
            rk.SetValue("LOADER", Location, RegistryValueKind.String);
            rk.SetValue("MANAGED", 1, RegistryValueKind.DWord);
            appkey.Close();
            Console.WriteLine("注册成功");
        } catch (Exception ex) {
            Console.WriteLine("注册失败：" + ex.Message);
        }
    }

    public static void UnReg(string ver, string lang) {
        int version = Int32.Parse(ver + (lang.ToLower().Equals("cn") ? "00" : "01"));
        string path = string.Empty;
        cadVerDictionary.TryGetValue(version, out path);
        UnRegApp("SOFTWARE\\Autodesk\\AutoCAD\\" + path);
    }

    public static void UnReg(int ver) {
        string path = string.Empty;
        cadVerDictionary.TryGetValue(ver, out path);
        UnRegApp("SOFTWARE\\Autodesk\\AutoCAD\\" + path);
    }

    private static void UnRegApp(string keypath) { //卸载
        try {
            RegistryKey appkey = Registry.LocalMachine.OpenSubKey(keypath + "\\Applications", true);//打开Applications
            appkey.DeleteSubKeyTree("DoitTools");
            Console.WriteLine("卸载成功");
        } catch (Exception ex) {
            Console.WriteLine("卸载失败：" + ex.Message);
        }
    }

    public static Dictionary<int, string> cadVerDictionary = new Dictionary<int, string> {
        {200600, "R16.2\\ACAD-4001:804"}, //2006中文版
        {200601, "R16.2\\ACAD-4001:409"},//2006英文版
        {200700, "R17.0\\ACAD-5001:804"},//2007中文版
        {200701, "R17.0\\ACAD-5001:409"},//2007英文版
        {200800, "R17.1\\ACAD-6001:804"},//2008中文版
        {200801, "R17.1\\ACAD-6001:409"},//2008英文版
        {200900, "R17.2\\ACAD-7001:804"},//2009中文版
        {200901, "R17.2\\ACAD-7001:409"},//2009英文版
        {201000, "R18.0\\ACAD-8001:804"},//2010中文版
        {201001, "R18.0\\ACAD-8001:409"},//2010英文版
        {201700, "R21.0\\ACAD-0001:804"},//2017中文版
        {201701, "R21.0\\ACAD-0001:409"}//2017英文版
    };
}
}
