using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularApp.Data;
using AngularApp.HelperClass;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
        public string GetGridData()
        {
            try
            {
                string mode = Convert.ToString(Request.Form["mode"]);
                ColumnConfig columnConfig = new ColumnConfig(mode, HttpContext);
                var aaData =  columnConfig.gridParams.GetData();
                return aaData;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}