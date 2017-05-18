using System.Windows;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Ribbon;
using Autodesk.AutoCAD.Runtime;
using DotNetAutoCAD.Command.Commands;
using Application = Autodesk.AutoCAD.ApplicationServices.Core.Application;

namespace DotNetAutoCAD.Command {
public class Init : IExtensionApplication {
    public void Initialize() {
        RibbonServices.RibbonPaletteSetCreated += RibbonServices_RibbonPaletteSetCreated;
        //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //ed.WriteMessage("Initialize ....");
    }

    private void RibbonServices_RibbonPaletteSetCreated(object sender, System.EventArgs e) {
        RibbonServices.RibbonPaletteSet.RibbonControl.Loaded += RibbonControl_Loaded;
        //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //ed.WriteMessage("RibbonPaletteSetCreated ....");
    }

    private void RibbonControl_Loaded(object sender, RoutedEventArgs e) {
        AddMenuItem addMenuItem = new AddMenuItem();//Class_Initialize类就是创建菜单的那个
        addMenuItem.CreateRibbon();
        //Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        //ed.WriteMessage("RibbonControlLoaded ....");
    }

    public void Terminate() {
    }
}
}
