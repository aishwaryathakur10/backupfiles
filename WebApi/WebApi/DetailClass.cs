using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApi.Models;

namespace WebApi.Models
{
    public partial class DetailClass
    {
        public string prev_url { get; set; }

        public string next_url { get; set; }
       public List<StudentTable> studentList { get; set; }
    }
}