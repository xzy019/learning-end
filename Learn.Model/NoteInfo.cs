using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace Learn.Model
{
    public class NoteInfo:BaseID
    {
        [SugarColumn(ColumnDataType = "varchar(30)")]
        public string Title { get; set; }
        [SugarColumn(ColumnDataType = "text")]
        public string Content { get; set; }
        public DateTime Time { get; set; }
        public string Tag { get; set; }
        public int LikeNumber { get; set; }
        public int WriterID { get; set; }

        //类型,不映射到数据库
        [SugarColumn(IsIgnore = true)]
        public Writer Writer { get; set; }
    }
}
