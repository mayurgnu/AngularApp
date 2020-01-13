using System.Collections.Generic;

namespace AngularApp.HelperClass
{
    public class DataTableAjaxPostModel
    {
        public string draw { get; set; }
        public string start { get; set; }
        public string length { get; set; }
        public List<Column> columns { get; set; }
        public Search search { get; set; }
        public List<Order> order { get; set; }
        public string FixClause { get; set; }
        public string mode { get; set; }


    }

    public class Column
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public Search search { get; set; }
        public string length { get; set; }
        
    }

    public class Search
    {
        public string value { get; set; }
        public string regex { get; set; }
    }

    public class Order
    {
        public string column { get; set; }
        public string dir { get; set; }
    }
}