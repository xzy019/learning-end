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
    public class StudyRepository:BaseRepository<StudyInfo>,IStudyRepository
    {
        public async override Task<List<StudyInfo>> QueryAsync()
        {
            return await base.Context.Queryable<StudyInfo>()
                .Mapper(c => c.Writer, c => c.WriterID, c => c.Writer.ID)
                .ToListAsync();
        }
        public async override Task<List<StudyInfo>> QueryAsync(Expression<Func<StudyInfo, bool>> func)
        {
            return await base.Context.Queryable<StudyInfo>()
                .Where(func)
                .Mapper(c => c.Writer, c => c.WriterID, c => c.Writer.ID)
                .ToListAsync();
        }

        public async override Task<List<StudyInfo>> QueryAsync(int page, int size, RefAsync<int> total)
        {
            return await base.Context.Queryable<StudyInfo>()
                .Mapper(c => c.Writer, c => c.WriterID, c => c.Writer.ID)
                .ToPageListAsync(page, size, total);
        }

        public async override Task<List<StudyInfo>> QueryAsync(Expression<Func<StudyInfo, bool>> func, int page, int size, RefAsync<int> total)
        {
            return await base.Context.Queryable<StudyInfo>()
                .Where(func)
                .Mapper(c => c.Writer, c => c.WriterID, c => c.Writer.ID)
                .ToPageListAsync(page, size, total);
        }
    }
}
