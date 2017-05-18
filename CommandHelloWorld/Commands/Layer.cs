
using System;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;

namespace DotNetAutoCAD.Command.Commands {
public class Layer {

    [CommandMethod("NetGetAllLayerName")]
    public void GetLayerName() {
        Database db = HostApplicationServices.WorkingDatabase;
        using (Transaction trans = db.TransactionManager.StartTransaction()) {
            LayerTable lt = (LayerTable)trans.GetObject(db.LayerTableId, OpenMode.ForRead);
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;

            foreach (ObjectId layerId in lt) {
                LayerTableRecord ltr = (LayerTableRecord)trans.GetObject(layerId, OpenMode.ForRead);
                ed.WriteMessage(ltr.Name);
                ed.WriteMessage(Environment.NewLine);
            }
            trans.Commit();
        }
    }
}
}
