using AutoMapper;
using Common.DTO.CourseDTO;
using Common.DTO.LessonDTO;
using Common.DTO.LessonPdfDTO;
using Common.DTO.LessonVideoDTO;
using Common.DTO.QuizDTO;
using Common.DTO.UserDTO;
using Common.Entity;
using Microsoft.AspNetCore.Http.HttpResults;

namespace WEB_CLIENT
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<UserCreateDTO, User>()
                .ForMember(des => des.FullName, src => src.MapFrom(m => m.FullName.Trim()))
                .ForMember(des => des.Address, src => src.MapFrom(m => m.Address == null || m.Address.Trim().Length == 0 ? null : m.Address.Trim()));
            CreateMap<CourseCreateUpdateDTO, Course>()
                .ForMember(des => des.CourseName, src => src.MapFrom(m => m.CourseName.Trim()))
                .ForMember(des => des.Image, src => src.MapFrom(m => m.Image.Trim()))
                .ForMember(des => des.Description, src => src.MapFrom(m => m.Description == null || m.Description.Trim().Length == 0 ? null : m.Description.Trim()));
            CreateMap<LessonCreateUpdateDTO, Lesson>()
                .ForMember(des => des.LessonName, src => src.MapFrom(m => m.LessonName.Trim()));
            CreateMap<LessonPdfCreateUpdateDTO, LessonPdf>()
                .ForMember(des => des.Pdfname, src => src.MapFrom(m => m.Pdfname.Trim()));
            CreateMap<LessonVideoCreateUpdateDTO, LessonVideo>()
               .ForMember(des => des.VideoName, src => src.MapFrom(m => m.VideoName.Trim()));
            CreateMap<QuizCreateUpdateDTO, Quiz>()
              .ForMember(des => des.Question, src => src.MapFrom(m => m.Question.Trim()))
              .ForMember(des => des.Answer1, src => src.MapFrom(m => m.Answer1.Trim()))
              .ForMember(des => des.Answer2, src => src.MapFrom(m => m.Answer2.Trim()))
              .ForMember(des => des.Answer3, src => src.MapFrom(m => m.Answer3 == null ? null : m.Answer3.Trim()))
              .ForMember(des => des.Answer4, src => src.MapFrom(m => m.Answer4 == null ? null : m.Answer4.Trim()));

        }
    }
}
