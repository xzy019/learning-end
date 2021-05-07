using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace Learn.Model
{
    public class StudyInfo:BaseID
    {
        public string StudyTime { get; set; }
        public string Type { get; set; }
        public int WriterID { get; set; }
        [SugarColumn(IsIgnore = true)]
        public Writer Writer { get; set; }
    }
}
