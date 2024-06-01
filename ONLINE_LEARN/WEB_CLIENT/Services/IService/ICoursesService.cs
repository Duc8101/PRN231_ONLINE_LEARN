﻿using DataAccess.DTO;

namespace WEB_CLIENT.Services.IService
{
    public interface ICoursesService
    {
        Task<ResponseDTO<Dictionary<string, object?>?>> Index(int? CategoryID, string? properties, string? flow, int? page, string? userId);
        Task<ResponseDTO<Dictionary<string, object?>?>> Detail(Guid CourseID, string? userId);
        Task<ResponseDTO<Dictionary<string, object?>?>> EnrollCourse(Guid CourseID, Guid UserID);
        Task<ResponseDTO<Dictionary<string, object>?>> LearnCourse(Guid CourseID, Guid UserID, string? video /* file video */, string? name /*video name or pdf name*/, string? PDF /*file PDF */, Guid? LessonID, int? VideoID, int? PDFID);
    }
}