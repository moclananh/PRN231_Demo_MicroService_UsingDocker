using System;
using System.Collections.Generic;

#nullable disable

namespace CustomerAPI.DataAccess
{
    public partial class Customer
    {
        public int CusId { get; set; }
        public string CusName { get; set; }
        public string CusAddress { get; set; }
    }
}
