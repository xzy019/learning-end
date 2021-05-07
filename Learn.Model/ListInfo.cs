using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace Learn.Model
{
    public class ListInfo:BaseID
    {
        [SugarColumn(ColumnDataType = "varchar(30)")]
        public string Title { get; set; }
        [SugarColumn(ColumnDataType = "tinyint")]
        public Byte Level { get; set; }
        [SugarColumn(ColumnDataType = "tinyint")]
        public Byte Complete { get; set; }
        public int WriterID { get; set; }
        [SugarColumn(IsIgnore = true)]
        public Writer Writer { get; set; }
    }
}
