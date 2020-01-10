using Microsoft.AspNetCore.Http;

namespace AngularApp.HelperClass
{
    /// <summary>
    /// for configure all grids of system.
    /// </summary>
    public class ColumnConfig
    {
        public GridParams gridParams = new GridParams();

        /// <summary>
        /// init grid configuration
        /// </summary>
        /// <param name="mode">keyword for identify grid</param>
        public ColumnConfig(string mode, HttpContext _HTTPContext, string where = "")
        {
            gridParams._HTTPContext = _HTTPContext;
            gridParams.PageNumber = 1;
            gridParams.RecordPerPage = 10;
            // if (mode == "Role")
            // {
            //     gridParams.ColumnsName = "RoleId,Role,IsActive";
            //     gridParams.SortColumn = "RoleId";
            //     gridParams.SortOrder = "desc";
            //     gridParams.TableName = "RoleMaster";
            //     gridParams.WhereClause = " IsActive=1  ";
            //     gridParams.ExportedFileName = "RoleList";
            // }
            gridParams.ColumnsName = "RoleId,Role,IsActive";
            gridParams.SortColumn = "RoleId";
            gridParams.SortOrder = "desc";
            gridParams.TableName = "RoleMaster";
            gridParams.WhereClause = " IsActive=1  ";
            gridParams.ExportedFileName = "RoleList";
        }
    }
}