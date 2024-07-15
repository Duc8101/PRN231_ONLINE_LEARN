using Common.Entity;
using DataAccess.Model.DBContext;

namespace DataAccess.Model.DAO
{
    public class DAOLessonPDF : BaseDAO
    {
        public DAOLessonPDF(MyDbContext context) : base(context)
        {

        }

        public List<LessonPdf> getListPDF()
        {
            return _context.LessonPdfs.Where(p => p.IsDeleted == false).ToList();
        }

        public void CreatePDF(LessonPdf PDF)
        {
            _context.LessonPdfs.Add(PDF);
            _context.SaveChanges();
        }

        public LessonPdf? getPDF(int pdfId , Guid lessonId)
        {
            return _context.LessonPdfs.SingleOrDefault(p => p.Pdfid == pdfId && p.IsDeleted == false && p.LessonId == lessonId);
        }

        public void UpdatePDF(LessonPdf PDF)
        {
            _context.LessonPdfs.Update(PDF);
            _context.SaveChanges();
        }

        public void DeletePDF(LessonPdf PDF)
        {
            PDF.IsDeleted = true;
            UpdatePDF(PDF);
        }
    }
}
