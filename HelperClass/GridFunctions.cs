using System;
using System.Data;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace AngularApp.HelperClass
{
    public class GridFunctions
    {
        private readonly HttpContext _HttpContext;
        private readonly DataTableAjaxPostModel _AjaxPostModel;

        public GridFunctions(DataTableAjaxPostModel AjaxPostModel)
        {
            _AjaxPostModel = AjaxPostModel;
        }

        /// <summary>
        /// for get the column names
        /// </summary>
        /// <returns></returns>
        public string GetColumns()
        {
            StringBuilder columns = new StringBuilder();
            for (int i = 0; i< _AjaxPostModel.columns.Count ; i++)
            {
                if (!string.IsNullOrEmpty(_AjaxPostModel.columns[i].data))
                {
                    string c = Convert.ToString(_AjaxPostModel.columns[i].data);
                    columns.Append(columns.ToString()?.Length == 0 ? c : "," + c);
                }
                else
                {
                    break;
                }
            }
            // if (!string.IsNullOrEmpty(_AjaxPostModel.columns))
            //     columns.Append(Convert.ToString(_HttpContext.Request.Form["Columns"]));
            return columns.ToString();
        }

        /// <summary>
        /// for get sorted column name
        /// </summary>
        /// <param name="defaultColName"></param>
        /// <returns></returns>
        public string GetSortColumn(string defaultColName)
        {
            if (!string.IsNullOrEmpty(_AjaxPostModel.order[0].column))
            {
                string index = Convert.ToString(_AjaxPostModel.order[0].column);
                string ColName = Convert.ToString(_AjaxPostModel.columns[0].data);
                if (string.IsNullOrEmpty(ColName))
                {
                    try
                    {
                        string[] columns = Convert.ToString(_AjaxPostModel.columns).Split(',');
                        if (columns.Length > index.ToInt())
                            ColName = columns[index.ToInt()].Split(" [")[0];
                        else
                            ColName = defaultColName;
                    }
                    catch (Exception)
                    {
                        ColName = defaultColName;
                    }
                }
                // if (_HttpContext.Request.Form["mode"].ToString() == "TeeTime" && ColName == "OnDate")
                //     return "CONVERT(Date,OnDate,103)";
                return ColName;
            }
            else
            {
                return defaultColName;
            }
        }

        /// <summary>
        /// for get order of sorted column
        /// </summary>
        /// <returns></returns>
        public string GetSortOrder()
        {
            if (!string.IsNullOrEmpty(_AjaxPostModel.order[0].dir))
            {
                string order = Convert.ToString(_AjaxPostModel.order[0].dir);
                if (string.IsNullOrEmpty(order))
                    order = "asc";
                return order;
            }
            else
            {
                return "asc";
            }
        }

        /// <summary>
        /// for set where clause(global serach)
        /// </summary>
        /// <param name="w"></param>
        /// <returns>returns where clause</returns>
        public string GetWhereClause(string w = "")
        {
            string where = w;
            if (!string.IsNullOrEmpty(_AjaxPostModel.FixClause))
            {
                string fix = Convert.ToString(_AjaxPostModel.FixClause);

                //'%'%' To '%''%' | '%te'st%' To '%te''st%'
                const string pattern = @"(%)(\w*)(')(\w*)(%)";
                fix = System.Text.RegularExpressions.Regex.Replace(fix, pattern, "$1$2''$4$5");

                if (fix != "")
                    where += where?.Length == 0 ? fix : " AND (" + fix + ")";
            }
            if (!string.IsNullOrEmpty(_AjaxPostModel.search.value))
            {
                string val = Convert.ToString(_AjaxPostModel.search.value);
                if (val != "")
                {
                    val = val.Replace("'", "''");
                    StringBuilder whereforall = new StringBuilder();
                    string[] columns = GetColumns().Split(',');
                    foreach (string col in columns)
                    {
                        if (!string.Equals(col, "rownumber", StringComparison.OrdinalIgnoreCase))
                            whereforall.Append(whereforall.ToString()?.Length == 0 ? col + " LIKE N'%" + val + "%'" : " OR " + col + " LIKE N'%" + val + "%'");
                    }
                    where += where?.Length == 0 ? "(" + whereforall + ")" : " AND (" + whereforall + ")";
                }
            }
            return where;
        }

        /// <summary>
        /// for get current page number
        /// </summary>
        /// <returns>returns current page number</returns>
        public int GetPageNumber()
        {
            if (!string.IsNullOrEmpty(_AjaxPostModel.start))
                return Convert.ToString(_AjaxPostModel.start)?.Length == 0 ? -1 : Convert.ToInt32(Convert.ToString(_AjaxPostModel.start));
            else
                return 1;
        }

        /// <summary>
        /// for get total rows per page
        /// </summary>
        /// <returns>returns total no of data as per _HttpContext.Request.Form["length"] parameter</returns>
        public int GetRecordPerPage()
        {
            if (!string.IsNullOrEmpty(_AjaxPostModel.length))
                return Convert.ToInt32(Convert.ToString(_AjaxPostModel.length));
            else
                return 10;
        }

        /// <summary>
        /// get data
        /// </summary>
        /// <param name="oGrid">grid configuration object</param>
        /// <returns>returns datatable</returns>
        public DataTable GetDataTable(GridParams oGrid)
        {
            DatabaseHelper dbHelper = new DatabaseHelper(ConfigurationSettings.Default);
            dbHelper.AddParameter("@TableName", oGrid.TableName);
            dbHelper.AddParameter("@ColumnsName", oGrid.ColumnsName);
            dbHelper.AddParameter("@SortOrder", GetSortOrder());
            dbHelper.AddParameter("@SortColumn", GetSortColumn(oGrid.SortColumn));
            dbHelper.AddParameter("@PageNumber", GetPageNumber());
            dbHelper.AddParameter("@RecordPerPage", GetRecordPerPage());
            dbHelper.AddParameter("@WhereClause", GetWhereClause(oGrid.WhereClause));
            DataSet ds = dbHelper.ExecuteDataSet("GetDataForGridWeb", CommandType.StoredProcedure);
            dbHelper.Dispose();
            return ds.Tables[0];
        }

        /// <summary>
        /// return json as a datatable response
        /// </summary>
        /// <param name="oGrid">grid configuration object</param>
        /// <returns>retuns datatable as json response</returns>
        public string GetJson(GridParams oGrid,DataTableAjaxPostModel AjaxPostModel)
        {
            DataTable dt = GetDataTable(oGrid);
            return dt.GetJsonForDataTableJS(AjaxPostModel);
        }

        /// <summary>
        /// export data to specific format
        /// </summary>
        /// <param name="oGrid">grid configuration object</param>
        public void Export(GridParams oGrid)
        {
            DataTable dt = GetDataTable(oGrid);

            oGrid.ExportedColumns = Convert.ToString(_HttpContext.Request.Form["Columns"]).Replace("null,", "");
            if (string.Equals(Convert.ToString(_HttpContext.Request.Form["mode"]), "banner"
, StringComparison.OrdinalIgnoreCase)
                || string.Equals(Convert.ToString(_HttpContext.Request.Form["mode"]), "category"
, StringComparison.OrdinalIgnoreCase)
                || string.Equals(Convert.ToString(_HttpContext.Request.Form["mode"]), "warrantymaster"
, StringComparison.OrdinalIgnoreCase)
                || string.Equals(Convert.ToString(_HttpContext.Request.Form["mode"]), "eventmaster", StringComparison.OrdinalIgnoreCase))
            {
                var columnList = Convert.ToString(_HttpContext.Request.Form["Columns"]).Split(',').ToList();
                for (int i = 0; i < columnList.Count; i++)
                {
                    if (columnList[i].Contains("photo", StringComparison.OrdinalIgnoreCase) || columnList[i].Contains("prebanner", StringComparison.OrdinalIgnoreCase) || columnList[i].Contains("photo", StringComparison.OrdinalIgnoreCase) || columnList[i].Contains("purchaseproof", StringComparison.OrdinalIgnoreCase))
                    {
                        columnList.RemoveAt(i);
                    }
                }
                oGrid.ExportedColumns = string.Join(",", columnList);
            }
            string[] c = oGrid.ExportedColumns.Split(',');
            string[] s = oGrid.ExportedColumns.Split(',');
            for (int i = 0; i < c.Length; i++)
            {
                c[i] = c[i].Split(' ')[0];
            }

            DataTable dtTemp = dt.Copy();
            int j = 0;
            foreach (DataColumn dc in dtTemp.Columns)
            {
                if (!c.Contains(dc.ColumnName))
                {
                    dt.Columns.Remove(dc.ColumnName);
                }
                else
                {
                    dt.Columns[s[j].Split(' ')[0]].SetOrdinal(j);
                    dt.Columns[s[j].Split(' ')[0]].ColumnName = s[j].Split('[').Length > 1 ? s[j].Split('[')[1].Replace("]", "") : s[j];
                    j++;
                }
            }
            if (dt.Columns.Contains("TotalRows"))
                dt.Columns.Remove("TotalRows");
            string type = Convert.ToString(_HttpContext.Request.Form["type"]);

            if (string.Equals(type, "excel", StringComparison.OrdinalIgnoreCase))
            {
                dt.ExportToExcel(oGrid.ExportedFileName);
            }
            else if (string.Equals(type, "word", StringComparison.OrdinalIgnoreCase))
            {
                dt.ExportToWord(oGrid.ExportedFileName);
            }
            else if (string.Equals(type, "pdf", StringComparison.OrdinalIgnoreCase))
            {
                dt.ExportToPdf(oGrid.ExportedFileName);
            }
            else if (string.Equals(type, "csv", StringComparison.OrdinalIgnoreCase))
            {
                dt.ExportToCSV(oGrid.ExportedFileName);
            }
            else
            {
                dt.ExportToExcel(oGrid.ExportedFileName);
            }
        }
    }
}