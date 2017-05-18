using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1 {
class Program {
    static void Main(string[] args) {
        DataTable dt = new DataTable("Table_AX");
        dt.Columns.Add("Name", Type.GetType("System.String"));
        dt.Columns.Add("Sex", Type.GetType("System.String"));
        dt.Columns.Add("Age", Type.GetType("System.Int32"));
        for (int i = 1; i <= 100000; i++) {
            dt.Rows.Add("张三" + i, "男", 20);
        }
        Console.WriteLine("DataTable RowCount：{0}", dt.Rows.Count);
        Stopwatch sw = new Stopwatch();
        sw.Start();

        List<User>   users = new List<User>();
        users = (from row in dt.AsEnumerable()
                 orderby row.Field<string>("Name")
        select new User {
            //Name = Convert.ToString(row["Name"]),
            //Sex = row.Field<string>("Sex"),
            //Age = row.Field<Int32>("Age")
            Name = Convert.ToString(row["Name"]),
            Sex = Convert.ToString(row["Sex"]),
            Age = Convert.ToInt32(row["Age"])
        }).ToList<User>();
        sw.Stop();
        TimeSpan ts1 = sw.Elapsed;
        Console.WriteLine("LinQ总共花费{0}ms.", ts1.TotalMilliseconds);

        sw.Start();
        List<User> users2 = DataUtils.DataTableToModelList<User>(dt);
        sw.Stop();
        TimeSpan ts2 = sw.Elapsed;
        Console.WriteLine("反射总共花费{0}ms.", ts2.TotalMilliseconds);

        Console.ReadLine();
    }
}

class User {
    public string Name {
        set;
        get;
    }

    public string Sex {
        set;
        get;
    }

    public int Age {
        set;
        get;
    }
}
}
