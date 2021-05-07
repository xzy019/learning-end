using Learn.IRepository;
using Learn.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Learn.Repository
{
    public class ListRepository:BaseRepository<ListInfo>,IListRepository
    {
        public async override Task<List<ListInfo>> QueryAsync()
        {
            return await base.Context.Queryable<ListInfo>()
                .Mapper(c => c.Writer, c => c.WriterID, c => c.Writer.ID)
                .ToListAsync();
        }
        public async override Task<List<ListInfo>> QueryAsync(Expression<Func<ListInfo, bool>> func)
        {
            return await base.Context.Queryable<ListInfo>()
                .Where(func)
                .Mapper(c => c.Writer, c => c.WriterID, c => c.Writer.ID)
                .ToListAsync();
        }

        public async override Task<List<ListInfo>> QueryAsync(int page, int size, RefAsync<int> total)
        {
            return await base.Context.Queryable<ListInfo>()
                .Mapper(c => c.Writer, c => c.WriterID, c => c.Writer.ID)
                .ToPageListAsync(page, size, total);
        }

        public async override Task<List<ListInfo>> QueryAsync(Expression<Func<ListInfo, bool>> func, int page, int size, RefAsync<int> total)
        {
            return await base.Context.Queryable<ListInfo>()
                .Where(func)
                .Mapper(c => c.Writer, c => c.WriterID, c => c.Writer.ID)
                .ToPageListAsync(page, size, total);
        }
    }
}
