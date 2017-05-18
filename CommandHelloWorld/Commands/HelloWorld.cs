using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;

namespace DotNetAutoCAD.Command.Commands {
public class HelloWorld {
    [CommandMethod("NetHelloWorld")]
    public void NetHelloWorld() {
        Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        ed.WriteMessage("Hello World!!!!");
    }
}
}
