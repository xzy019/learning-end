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
    public class NoteRepository:BaseRepository<NoteInfo>,INoteRepository
    {
        public async override Task<List<NoteInfo>> QueryAsync()
        {
            return await base.Context.Queryable<NoteInfo>()
                .Mapper(c => c.Writer, c => c.WriterID, c => c.Writer.ID)
                .ToListAsync();
        }
        public async override Task<List<NoteInfo>> QueryAsync(Expression<Func<NoteInfo, bool>> func)
        {
            return await base.Context.Queryable<NoteInfo>()
                .Where(func)
                .Mapper(c => c.Writer, c => c.WriterID, c => c.Writer.ID)
                .ToListAsync();
        }

        public async override Task<List<NoteInfo>> QueryAsync(int page, int size, RefAsync<int> total)
        {
            return await base.Context.Queryable<NoteInfo>()
                .Mapper(c => c.Writer, c => c.WriterID, c => c.Writer.ID)
                .ToPageListAsync(page, size, total);
        }

        public async override Task<List<NoteInfo>> QueryAsync(Expression<Func<NoteInfo, bool>> func, int page, int size, RefAsync<int> total)
        {
            return await base.Context.Queryable<NoteInfo>()
                .Where(func)
                .Mapper(c => c.Writer, c => c.WriterID, c => c.Writer.ID)
                .ToPageListAsync(page, size, total);
        }
    }
}
