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
    public class CommentRepository:BaseRepository<CommentInfo>,ICommentRepository
    {
        public async override Task<List<CommentInfo>> QueryAsync()
        {
            return await base.Context.Queryable<CommentInfo>()
                .Mapper(c => c.Writer, c => c.WriterID, c => c.Writer.ID)
                .Mapper(c => c.NoteInfo, c=> c.NoteID, c=>c.NoteInfo.ID)
                .ToListAsync();
        }
        public async override Task<List<CommentInfo>> QueryAsync(Expression<Func<CommentInfo, bool>> func)
        {
            return await base.Context.Queryable<CommentInfo>()
                .Where(func)
                .Mapper(c => c.Writer, c => c.WriterID, c => c.Writer.ID)
                .Mapper(c => c.NoteInfo, c => c.NoteID, c => c.NoteInfo.ID)
                .ToListAsync();
        }

        public async override Task<List<CommentInfo>> QueryAsync(int page, int size, RefAsync<int> total)
        {
            return await base.Context.Queryable<CommentInfo>()
                .Mapper(c => c.Writer, c => c.WriterID, c => c.Writer.ID)
                .Mapper(c => c.NoteInfo, c => c.NoteID, c => c.NoteInfo.ID)
                .ToPageListAsync(page, size, total);
        }

        public async override Task<List<CommentInfo>> QueryAsync(Expression<Func<CommentInfo, bool>> func, int page, int size, RefAsync<int> total)
        {
            return await base.Context.Queryable<CommentInfo>()
                .Where(func)
                .Mapper(c => c.Writer, c => c.WriterID, c => c.Writer.ID)
                .Mapper(c => c.NoteInfo, c => c.NoteID, c => c.NoteInfo.ID)
                .ToPageListAsync(page, size, total);
        }
    }
}
