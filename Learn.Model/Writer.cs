using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace Learn.Model
{
    public class Writer:BaseID
    {
        [SugarColumn(ColumnDataType = "varchar(20)")]
        public string NickName { get; set; }
        [SugarColumn(ColumnDataType = "tinyint")]
        public Byte Sex { get; set; }
        [SugarColumn(ColumnDataType = "varchar(30)")]
        public string TelNumber { get; set; }
        [SugarColumn(ColumnDataType = "varchar(30)")]
        public string UserName { get; set; }
        public string LoginKey { get; set; }
    }
}
