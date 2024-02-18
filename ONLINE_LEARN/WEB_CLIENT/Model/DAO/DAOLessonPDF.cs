using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace WEB_CLIENT.Model.DAO
{
    public class DAOLessonPDF : MyDbContext
    {
        public async Task<List<LessonPdf>> getList()
        {
            return await LessonPdfs.Where(p => p.IsDeleted == false).ToListAsync();
        }

        public async Task CreatePDF(LessonPdf pdf)
        {
            await LessonPdfs.AddAsync(pdf);
            await SaveChangesAsync();
        }

        public async Task<LessonPdf?> getPDF(int PdfID, Guid LessonID)
        {
            return await LessonPdfs.SingleOrDefaultAsync(p => p.Pdfid == PdfID && p.IsDeleted == false && p.LessonId == LessonID);
        }

        public async Task UpdatePDF(LessonPdf pdf)
        {
            LessonPdfs.Update(pdf);
            await SaveChangesAsync();
        }

        public async Task DeletePDF(LessonPdf pdf)
        {
            pdf.IsDeleted = true;
            await UpdatePDF(pdf);
        }
    }
}
