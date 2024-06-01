﻿using DataAccess.DTO;
using DataAccess.DTO.UserDTO;

namespace WEB_CLIENT.Services.IService
{
    public interface IProfileService
    {
        Task<ResponseDTO<Dictionary<string, object>?>> Index(Guid UserID);
        Task<ResponseDTO<Dictionary<string, object>?>> Index(Guid UserID, ProfileDTO DTO, string valueImg);
    }
}
