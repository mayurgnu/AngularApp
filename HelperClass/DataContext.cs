using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace AngularApp.HelperClass
{
    public class ConnectionStrings
    {
        /// <summary>
        /// Declared for to get & set database connnection string
        /// </summary>
        /// <value></value>
        public string Default { get; set; }

        /// <summary>
        /// Declared for to get & set logging database connnection string
        /// </summary>
        /// <value></value>
        public string StrDBConnLog { get; set; }
    }
}