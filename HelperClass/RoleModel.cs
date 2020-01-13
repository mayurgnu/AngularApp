using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AngularApp.HelperClass
{
    public class RoleModel
    {
        /// <summary>
        /// Unique role name 
        /// </summary>
        [Required(ErrorMessage = "Role is required.")]
        public string Role { get; set; }

        /// <summary>
        /// Unique id associated with role
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Indicates role is active or not
        /// </summary>
        public bool IsActive { get; set; }
    }
}