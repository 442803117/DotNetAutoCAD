using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace ConsoleApplication1 {
/// <summary> 数据处理辅助类
/// </summary>
public static class DataUtils {
    /// <summary> 将DataTable中的某两列转换为Dictionary&lt;string, string&gt;
    /// </summary>
    /// <param name="dt">需要转换的DataTable</param>
    /// <param name="columnKey">DataTable中需要转换为Dictionary中的Key的列名</param>
    /// <param name="columnValue">DataTable中需要转换为Dictionary中的Value的列名</param>
    /// <returns></returns>
    public static Dictionary<string, string> DtToDic(this DataTable dt, string columnKey, string columnValue) {
        Dictionary<string, string> dic = new Dictionary<string, string>();
        foreach (DataRow dr in dt.Rows) {
            string key = dr[columnKey].ToString();
            string val = dr[columnValue].ToString();

            if (key.Length > 0 && !dic.Keys.Contains(key)) {
                dic.Add(key, val);
            }
        }

        return dic;
    }


    /// <summary> 将DataTable中的某列转换为List&lt;object&gt;
    /// </summary>
    /// <param name="dt">需要转换的DataTable</param>
    /// <param name="column">DataTable中需要转换为List的列名</param>
    /// <returns></returns>
    public static List<object> DtToList(this DataTable dt, string column) {
        List<object> li = new List<object>();
        foreach (DataRow dr in dt.Rows) {
            object val = dr[column];

            if (!li.Contains(val)) {
                li.Add(val);
            }
        }

        return li;
    }

    /// <summary> 将DataTable中的某列转换为List&lt;string&gt;
    /// </summary>
    /// <param name="dt">需要转换的DataTable</param>
    /// <param name="column">DataTable中需要转换为List的列名</param>
    /// <returns></returns>
    public static List<string> DtToStrList(this DataTable dt, string column) {
        List<string> li = new List<string>();
        foreach (DataRow dr in dt.Rows) {
            string val = dr[column].ToString();

            if (!li.Contains(val)) {
                li.Add(val);
            }
        }

        return li;
    }


    /// <summary> 将DataTable中的某列转换为List&lt;string&gt;转换后的string均为小写
    /// </summary>
    /// <param name="dt">需要转换的DataTable</param>
    /// <param name="column">DataTable中需要转换为List的列名</param>
    /// <returns></returns>
    public static List<string> DtToStrListLower(this DataTable dt, string column) {
        List<string> li = new List<string>();
        foreach (DataRow dr in dt.Rows) {
            string val = dr[column].ToString().ToLower();

            if (!li.Contains(val)) {
                li.Add(val);
            }
        }

        return li;
    }


    /// <summary> 将DataTable中的某列转换为List&lt;string&gt;转换后的string均为大写
    /// </summary>
    /// <param name="dt">需要转换的DataTable</param>
    /// <param name="column">DataTable中需要转换为List的列名</param>
    /// <returns></returns>
    public static List<string> DtToStrListUpper(this DataTable dt, string column) {
        List<string> li = new List<string>();
        foreach (DataRow dr in dt.Rows) {
            string val = dr[column].ToString().ToUpper();

            if (!li.Contains(val)) {
                li.Add(val);
            }
        }

        return li;
    }

    /// <summary> 将DataTable中的所有列名转换为大写形式;
    /// </summary>
    /// <param name="dt">需转换的DataTable</param>
    /// <returns></returns>
    public static void MakeDtColumnNameUpper(this DataTable dt) {
        if (dt == null) {
            return;
        }
        foreach (DataColumn dc in dt.Columns) {
            dc.ColumnName = dc.ColumnName.ToUpper();
        }
    }

    /// <summary> 将DataTable中的所有列名转换为小写形式;
    /// </summary>
    /// <param name="dt">需转换的DataTable</param>
    /// <returns></returns>
    public static void MakeDtColumnNameLower(this DataTable dt) {
        if (dt == null) {
            return;
        }
        foreach (DataColumn dc in dt.Columns) {
            dc.ColumnName = dc.ColumnName.ToLower();
        }
    }

    /// <summary> 布尔型转0和1[TRUE则return1,FALSE则return 0 ]
    /// </summary>
    /// <param name="b"></param>
    /// <returns></returns>
    public static int BoolToBit(bool b) {
        return b ? 1 : 0;
    }


    #region DataTableToModel

    /// <summary> 将DataTable第一行记录转为Model
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static TModel DataTableToModel<TModel>(DataTable dt) where TModel : new() {
        if (dt == null || dt.Rows.Count == 0) {
            return default(TModel);
        }

        TModel md = DataRowToModel<TModel>(dt.Rows[0]);

        return md;
    }


    public static class DataTableToListModel<T> where T : new() {
        public static IList<T> ConvertToModel(DataTable dt) {
            //定义集合
            IList<T> ts = new List<T>();
            T t = new T();
            string tempName = "";
            //获取此模型的公共属性
            PropertyInfo[] propertys = t.GetType().GetProperties();
            foreach (DataRow row in dt.Rows) {
                t = new T();
                foreach (PropertyInfo pi in propertys) {
                    tempName = pi.Name;
                    //检查DataTable是否包含此列
                    if (dt.Columns.Contains(tempName)) {
                        //判断此属性是否有set
                        if (!pi.CanWrite)
                            continue;
                        object value = row[tempName];
                        if (value != DBNull.Value)
                            pi.SetValue(t, value, null);
                    }
                }
                ts.Add(t);
            }
            return ts;
        }
    }

    /// <summary> 将DataTable转为List&lt;Model&gt;
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static List<TModel> DataTableToModelList<TModel>(DataTable dt) where TModel : new() {
        List<TModel> li = new List<TModel>();

        if (dt == null) {
            return li;
        }

        li.AddRange(from DataRow dr in dt.Rows select DataRowToModel<TModel>(dr));

        return li;
    }

    /// <summary> 将DataTable转为Dictionary&lt;string,Model&gt;
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <param name="dt"></param>
    /// <returns></returns>
    public static Dictionary<string, TModel> DataTableToModelDic<TModel>(DataTable dt) where TModel : new() {
        Dictionary<string, TModel> dic = new Dictionary<string, TModel>();

        if (dt == null) {
            return dic;
        }

        foreach (DataRow dr in dt.Rows) {
            TModel md = DataRowToModel<TModel>(dr);

            string mdName = md.ToString();
            if (!dic.Keys.Contains(mdName)) {
                dic.Add(mdName, md);
            }
        }

        return dic;
    }

    /// <summary> 将DataRow记录转为Model
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <param name="dr"></param>
    /// <returns></returns>
    public static TModel DataRowToModel<TModel>(DataRow dr) where TModel : new() {
        if (dr == null) {
            return default(TModel);
        }

        var fields = from j in typeof(TModel).GetProperties()
                     where dr.Table.Columns.Contains(j.Name)
                     select j;


        TModel model = new TModel();

        foreach (var f in fields) {
            object value = dr[f.Name];
            value = value == DBNull.Value ? null : value;

            try {
                f.SetValue(model, value, null);
            } catch(Exception ex) {
                // Log.Error(typeof(DataUtils), ex);
                if (value != null) {
                    object v = f.GetValue(model, null);

                    if (value is decimal && v is int) {
                        f.SetValue(model, ToInt(value.ToString(), 0), null);
                    }
                }

            }

        }

        return model;
    }

    #endregion

    public static int ToInt(this string strInt, int defaultValue) {
        int r;

        if (!Int32.TryParse(strInt, out r)) {
            r = defaultValue;
        }
        return r;
    }


    /// <summary>
    /// //两个结构一样的DT合并
    /// </summary>
    /// <param name="DataTable1"></param>
    /// <param name="DataTable2"></param>
    /// <returns></returns>
    public static DataTable CombineTable(DataTable DataTable1, DataTable DataTable2) {
        DataTable newDataTable = DataTable1.Clone();
        object[] obj = new object[newDataTable.Columns.Count];
        for (int i = 0; i < DataTable1.Rows.Count; i++) {
            DataTable1.Rows[i].ItemArray.CopyTo(obj, 0);
            newDataTable.Rows.Add(obj);
        }

        for (int i = 0; i < DataTable2.Rows.Count; i++) {
            DataTable2.Rows[i].ItemArray.CopyTo(obj, 0);
            newDataTable.Rows.Add(obj);
        }
        return newDataTable;
    }

    /// <summary> 为某个值列创建字符串列[仅对数值列有效，不要对数据量大的表进行此操作]
    /// </summary>
    /// <param name="dt"></param>
    /// <param name="column">列名</param>
    /// <param name="columnStr">字符列的名称</param>
    public static void CreateColumnStr(this DataTable dt, string column, string columnStr) {
        if (dt.Columns.Contains(column)) {
            var col = dt.Columns[column];
            if (col.DataType != typeof(string)) {
                if (!dt.Columns.Contains(columnStr)) {
                    var colNew = new DataColumn(columnStr, typeof(string));
                    dt.Columns.Add(colNew);
                }

                foreach (DataRow dr in dt.Rows) {
                    dr[columnStr] = dr[column].ToString();
                }


                dt.AcceptChanges();
            }
        }
    }

    /// <summary>复制一个DataRow</summary>
    /// <param name="dr"></param>
    /// <returns></returns>
    public static DataRow CloneDataRow(this DataRow dr) {
        DataTable dt = dr.Table.Clone();
        DataRow newDr = dt.Rows.Add();
        newDr.ItemArray = dr.ItemArray;
        return newDr;
    }

    /// <summary>将DataTable按照某列排序</summary>
    /// <param name="dt"></param>
    /// <param name="sortColumn"></param>
    public static DataTable SortDataTableByColumn(this DataTable dt, string sortColumn) {
        DataView dv = dt.DefaultView;
        dv.Sort = sortColumn;
        return  dv.ToTable();
    }

    /// <summary>
    /// 将一个DataRow中的所有列的内容赋给另一个DataRow的同名字段
    /// </summary>
    /// <param name="dr1"></param>
    /// <param name="dr2"> </param>
    /// <returns></returns>
    public static void AssignDataRowToDataRow(this DataRow dr1, DataRow dr2) {
        foreach (DataColumn dc1 in dr1.Table.Columns) {
            if (dr2.Table.Columns.Contains(dc1.ColumnName)) {
                dr2[dc1.ColumnName] = dr1[dc1.ColumnName];
            }
        }
    }

}
}