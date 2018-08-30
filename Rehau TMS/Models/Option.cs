using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rehau_TMS.Models
{
    public class Option
    {
        public int OptionId { get; set; }
        public string Name { get; set; }
        public int ToolsModelStateId { get; set; }
    }
}