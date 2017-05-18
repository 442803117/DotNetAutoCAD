using System.IO;
using System.Reflection;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Ribbon;
using Autodesk.Windows;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using RibbonButton = Autodesk.Windows.RibbonButton;
using RibbonControl = Autodesk.Windows.RibbonControl;
using RibbonPanelSource = Autodesk.Windows.RibbonPanelSource;


namespace DotNetAutoCAD.Command.Commands {
public class AddMenuItem {

    private const string MY_TAB_ID = "MY_TAB_ID";

    [CommandMethod("NetAddRibbon")]
    public void CreateRibbon() {
        RibbonControl ribCntrl = RibbonServices.RibbonPaletteSet.RibbonControl;
        RibbonTab tab = ribCntrl.Tabs.Where(q => q.Title.Equals("My custom tab")).FirstOrDefault();
        if (tab != null) {
            ribCntrl.Tabs.Remove(tab);
        }
        //can also be Autodesk.Windows.ComponentManager.Ribbon;
        //add the tab
        RibbonTab ribTab = new RibbonTab();
        ribTab.Title = "My custom tab";
        ribTab.Id = MY_TAB_ID;
        ribCntrl.Tabs.Add(ribTab);

        //create and add both panels
        //addPanel1(ribTab);
        addPanel2(ribTab);

        //set as active tab
        ribTab.IsActive = true;
    }

    private void addPanel2(RibbonTab ribTab) {
        //create the panel source
        RibbonPanelSource ribPanelSource = new RibbonPanelSource();
        ribPanelSource.Title = "Edit Registry";

        //create the panel
        RibbonPanel ribPanel = new RibbonPanel();
        ribPanel.Source = ribPanelSource;
        ribTab.Panels.Add(ribPanel);

        //create button1
        RibbonButton button = new RibbonButton();
        Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(@"DotNetAutoCAD.Command.Resource.button_green.png");
        ImageSourceConverter imageSourceConverter = new ImageSourceConverter();
        //ribButtonDrawCircle.Image
        button.LargeImage = (ImageSource)imageSourceConverter.ConvertFrom(stream);
        button.Text = "My Draw Circle";
        button.Orientation = Orientation.Vertical;
        button.Size = RibbonItemSize.Large;
        button.ShowText = true;
        button.ShowImage = true;
        //pay attention to the SPACE after the command name
        button.CommandParameter = "DrawCircle";
        button.CommandHandler = new MyCommandHandler();

        ribPanelSource.Items.Add(button);
    }

    private void addPanel1(RibbonTab ribTab) {
        //throw new NotImplementedException();
    }

}
}
