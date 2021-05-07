using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace Learn.Model
{
    public class CommentInfo
    {
        [SugarColumn(IsPrimaryKey = true)]
        public int WriterID { get; set; }
        [SugarColumn(IsPrimaryKey = true)]
        public int NoteID { get; set; }
        public string Comment { get; set; } 

        [SugarColumn(IsIgnore = true)]
        public Writer Writer { get; set; }
        [SugarColumn(IsIgnore = true)]
        public NoteInfo NoteInfo { get; set; }
    }
}
