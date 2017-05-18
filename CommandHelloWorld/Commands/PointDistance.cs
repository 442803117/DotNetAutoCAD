using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;

namespace DotNetAutoCAD.Command.Commands {
public class PointDistance {

    [CommandMethod("NetSelectPoint")]
    public void SelectAPoint() {
        //实例化一个 PromptPointOptions类用来设置提示字符串和其他的一些控制提示
        PromptPointOptions prPointOptions = new PromptPointOptions("请选择一个点：");
        PromptPointResult prPointRes;
        // 实例化一个Editor类，使用GetPoint方法返回
        Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        prPointRes = ed.GetPoint(prPointOptions);
        if (prPointRes.Status != PromptStatus.OK) {
            ed.WriteMessage("选择错误！");
        } else {
            ed.WriteMessage("选择的点为：" + prPointRes.Value.ToString());
        }
    }

    [CommandMethod("NetGetDistance")]
    public void GetDistance() {
        PromptDistanceOptions prDistOptions = new PromptDistanceOptions("计算两点距离，请选择第一个点：");
        PromptDoubleResult prDistRes;
        Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
        prDistRes = ed.GetDistance(prDistOptions);
        if (prDistRes.Status != PromptStatus.OK) {
            ed.WriteMessage("选择错误！");
        } else {
            ed.WriteMessage("两点的距离为：" + prDistRes.Value.ToString());
        }
    }
}
}
