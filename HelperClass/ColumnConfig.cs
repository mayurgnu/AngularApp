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
        public ColumnConfig(DataTableAjaxPostModel model, string where = "")
        {
            if(model.mode == "Role"){
                gridParams.AjaxPostModel = model;
                gridParams.PageNumber = 1;
                gridParams.RecordPerPage = 10;
                gridParams.ColumnsName = "RoleId,Role,IsActive,(CASE WHEN IsActive = 1 THEN 'green' else 'red' END) as Clr";
                gridParams.SortColumn = "RoleId";
                gridParams.SortOrder = "desc";
                gridParams.TableName = "RoleMaster";
                gridParams.WhereClause = " 1=1  ";
                gridParams.ExportedFileName = "RoleList";
            }
        }
    }
}