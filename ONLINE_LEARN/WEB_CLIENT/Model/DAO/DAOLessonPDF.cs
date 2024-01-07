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

        public async Task CreatePDF(LessonPdf pdf)
        {
            await context.LessonPdfs.AddAsync(pdf);
            await context.SaveChangesAsync();
        }

        public async Task<LessonPdf?> getPDF(int PdfID, Guid LessonID)
        {
            return await context.LessonPdfs.SingleOrDefaultAsync(p => p.Pdfid == PdfID && p.IsDeleted == false && p.LessonId == LessonID);
        }

        public async Task UpdatePDF(LessonPdf pdf)
        {
            context.LessonPdfs.Update(pdf);
            await context.SaveChangesAsync();
        }

        public async Task DeletePDF(LessonPdf pdf)
        {
            pdf.IsDeleted = true;
            await UpdatePDF(pdf);
        }
    }
}
