using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;
using Application = Autodesk.AutoCAD.ApplicationServices.Application;
using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;

namespace DotNetAutoCAD.Command.Commands {
public class DocumentOp {

    [CommandMethod("NetNewDrawing", CommandFlags.Session)]
    public static void NewDrawing() {
        // 指定使用的样板，如果这个样板没找到，
        // 就使用默认设置
        string strTemplatePath = "acad.dwt";
        DocumentCollection acDocMgr = Application.DocumentManager;
        Document acDoc = acDocMgr.Add(strTemplatePath);
        acDocMgr.MdiActiveDocument = acDoc;
    }

    [CommandMethod("NetOpenDrawing", CommandFlags.Session)]
    public static void OpenDrawing() {
        OpenFileDialog openFileDialog = new OpenFileDialog();
        openFileDialog.Filter = "CAD文件|*.dwg";
        openFileDialog.Multiselect = false;
        if (openFileDialog.ShowDialog() == DialogResult.OK) {
            string strFileName = openFileDialog.FileName;
            DocumentCollection acDocMgr = Application.DocumentManager;
            if (File.Exists(strFileName)) {
                acDocMgr.Open(strFileName, false);
            } else {
                acDocMgr.MdiActiveDocument.Editor.WriteMessage("File " + strFileName +
                        " does not exist.");
            }
        }
    }

    [CommandMethod("NetSaveActiveDrawing")]
    public static void SaveActiveDrawing() {
        Document acDoc = Application.DocumentManager.MdiActiveDocument;
        string strDWGName = acDoc.Name;
        object obj = Application.GetSystemVariable("DWGTITLED");
        //图形命名了吗？0-没呢
        //if (System.Convert.ToInt16(obj) == 0) {
        // 如果图形使用了默认名 (Drawing1、Drawing2等)，
        // 就提供一个新文件名
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filter = "CAD文件|*.dwg";
        saveFileDialog.FileName = strDWGName;
        if (saveFileDialog.ShowDialog() == DialogResult.OK) {
            acDoc.Database.SaveAs(saveFileDialog.FileName, true, DwgVersion.Current,
                                  acDoc.Database.SecurityParameters);
        }
        //}
    }
}
}
