using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ConsoleApplication2 {
class Program {
    static void Main(string[] args) {
        Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(@"ConsoleApplication2.Resource.button_green.png");
        ImageSourceConverter imageSourceConverter = new ImageSourceConverter();
        ImageSource src = (ImageSource)imageSourceConverter.ConvertFrom(stream);
        Console.WriteLine(src.Height);

        Console.ReadLine();
    }
}
}
