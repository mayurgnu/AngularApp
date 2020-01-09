using System;
using System.Collections.Generic;

namespace AngularApp.Data
{
    public partial class EmailTemplateMst
    {
        public int TemplateId { get; set; }
        public string Tag { get; set; }
        public string MailSubject { get; set; }
        public string MailContent { get; set; }
        public string FromEmail { get; set; }
        public string FromName { get; set; }
    }
}
