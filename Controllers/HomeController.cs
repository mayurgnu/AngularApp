using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularApp.Data;
using AngularApp.HelperClass;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AngularApp.HelperClass;
using Microsoft.EntityFrameworkCore;

namespace AngularApp.Controllers
{
    [ApiController]
    //[Route("[controller]")]
    [Route("/api/home")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BoonSiewContext _context;
        public HomeController(ILogger<HomeController> logger,BoonSiewContext context)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("Roles")]
        public IEnumerable<RoleMaster> GetRoleList()
        {
            return _context.RoleMaster.ToList();
        }
        [HttpGet]
        [Route("RoleById/{id}")]
        public RoleMaster GetRoleById(int id=0)
        {
            var obj = _context.RoleMaster.FirstOrDefault(x=>x.RoleId == id);
            return obj;
        }

        [HttpPost]
        [Route("GetRoles")]
        public IEnumerable<RoleMaster> GetRoles()
        {
            return _context.RoleMaster.ToList();
        }

        #region Common Grid Methods
        /// <summary>
        /// Common method to Get Data From Database and Display, Search, Sort, And Paginate data in DataTable Grid 
        /// </summary>
        /// <returns>Returns Data According to Search,Sort and Pagination criteria</returns>
        [HttpPost]
        [Route("GetGridData")]
        public string GetGridData(DataTableAjaxPostModel model)
        {
            try
            {
                //string mode = data.draw;//// Convert.ToString(Request.Form["mode"]);
                ColumnConfig columnConfig = new ColumnConfig(model);
                var aaData =  columnConfig.gridParams.GetData();
                return aaData;
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        #endregion


        /// <summary>
        /// To save new or update existing role records
        /// </summary>
        /// <typeparamref name="model">role object posted from client will bounded to model</typeparamref>
        /// <returns>Returns json result with success = true or false and ReturnMsg and (ManageRole.cshtml as partialview if there is an error failure)</returns>
        [HttpPost]
        [Route("ManageRole")]
        public IActionResult ManageRole(RoleMaster model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.IsActive = true;
                    if (model.RoleId == 0)
                    {
                        if (_context.RoleMaster.Where(x => x.Role.Trim() == model.Role.Trim() && x.IsActive).Count() == 0)
                        {
                            _context.Add(model);
                            _context.SaveChanges();
                            return Ok(new { success = "true", ReturnMsg = "Role saved successfully."});
                        }
                        else
                        {
                            return Ok(new { success = "false", ReturnMsg = "Role already exist."});
                        }
                    }
                    else
                    {
                        if (_context.RoleMaster.Where(x => x.RoleId != model.RoleId &&  x.Role.Trim() == model.Role.Trim() && x.IsActive).ToList().Count() == 0)
                        {
                            _context.Entry(model).State = EntityState.Modified;
                            _context.SaveChanges();
                            return Ok(new { success = "true", ReturnMsg = "Role updated successfully.", PartialviewContent = "" });
                        }
                        else
                        {
                            return Ok(new { success = "false", ReturnMsg = "Role already exist."});
                        }
                    }
                }
                else
                {
                    string _message = string.Join(Environment.NewLine, ModelState.Values
                                               .SelectMany(x => x.Errors)
                                               .Select(x => x.ErrorMessage));
                    return Ok(new { success = "false", ReturnMsg = _message});
                }
            }
            catch (Exception ex)
            {
                return Ok(new { success = "false", ReturnMsg = ex.Message});
            }
        }
    }
    public class DataTableParam
    {
        public string Mode { get; set; }
        public string SortDirection { get; set; }

    }
}