﻿using DataAccess.DTO;
using DataAccess.DTO.UserDTO;
using DataAccess.Entity;
using DataAccess.Model;
using DataAccess.Model.IDAO;
using System.Net;
using WEB_CLIENT.Services.IService;

namespace WEB_CLIENT.Services.Service
{
    public class LoginService : ILoginService
    {
        private readonly ICommonDAO<User> _daoUser;

        public LoginService(ICommonDAO<User> daoUser)
        {
            _daoUser = daoUser;
        }

        public async Task<ResponseDTO<User?>> Index(Guid UserID)
        {
            try
            {
                User? user = await _daoUser.GetById(UserID);
                if (user == null)
                {
                    return new ResponseDTO<User?>(null, "Not found user", (int)HttpStatusCode.NotFound);
                }
                return new ResponseDTO<User?>(user, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<User?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ResponseDTO<User?>> Index(LoginDTO DTO)
        {
            try
            {
                User? user = await _daoUser.Get(u => u.Username == DTO.Username && u.IsDeleted == false);
                if (user == null || DTO.Password == null || string.Compare(user.Password, UserUtil.HashPassword(DTO.Password), false) != 0)
                {
                    return new ResponseDTO<User?>(null, "Username or password incorrect", (int)HttpStatusCode.Conflict);
                }
                return new ResponseDTO<User?>(user, string.Empty);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<User?>(null, ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}