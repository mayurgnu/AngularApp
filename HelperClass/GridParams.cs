using Microsoft.AspNetCore.Http;
using System.Data;

namespace AngularApp.HelperClass
{
    public class GridParams
    {
        public HttpContext _HTTPContext;

        /// <summary>
        /// name of table. incase of join (tblA inner join tblB ON tblA.columnA=tblB.columnB)
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Column names which will be required for page
        /// </summary>
        public string ColumnsName { get; set; }

        /// <summary>
        /// default sort column
        /// </summary>
        public string SortColumn { get; set; }

        /// <summary>
        /// default sort order
        /// </summary>
        public string SortOrder { get; set; }

        /// <summary>
        /// default page number
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// default total rows per page
        /// </summary>
        public int RecordPerPage { get; set; }

        /// <summary>
        /// where cluase for grid query
        /// </summary>
        public string WhereClause { get; set; }

        /// <summary>
        /// column list for export file
        /// </summary>
        public string ExportedColumns { get; set; }

        /// <summary>
        /// exported file name
        /// </summary>
        public string ExportedFileName { get; set; }

        /// <summary>
        /// Gets data based on parameters passed as(GridParams._HTTPContext )
        /// </summary>
        /// <returns>return json as a datatable response</returns>
        public string GetData()
        {
            GridFunctions oGrid = new GridFunctions(_HTTPContext);
            return oGrid.GetJson(this);
        }

        /// <summary>
        /// Gets datatable based on parameters passed as(GridParams._HTTPContext )
        /// </summary>
        /// <returns>return datatable as response</returns>
        public DataTable GetDataTable()
        {
            GridFunctions oGrid = new GridFunctions(_HTTPContext);
            return oGrid.GetDataTable(this);
        }

        /// <summary>
        /// Gets Griddata based on parameters passed as(GridParams._HTTPContext) and 
        /// Exports excel of those data 
        /// </summary>
        public void ExportData()
        {
            GridFunctions oGrid = new GridFunctions(_HTTPContext);
            oGrid.Export(this);
        }
    }
}