using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;

namespace Learn.Model
{
    public class BaseID
    {
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
        public int ID { get; set; }
    }
}
