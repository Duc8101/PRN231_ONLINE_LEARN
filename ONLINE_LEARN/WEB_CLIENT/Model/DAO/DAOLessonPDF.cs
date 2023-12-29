using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace WEB_CLIENT.Model.DAO
{
    public class DAOLessonPDF : BaseDAO
    {
        public async Task<List<LessonPdf>> getList()
        {
            return await context.LessonPdfs.Where(p => p.IsDeleted == false).ToListAsync();
        }
    }
}
