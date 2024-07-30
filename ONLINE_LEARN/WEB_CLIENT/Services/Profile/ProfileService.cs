﻿using Common.Base;
using Common.DTO.UserDTO;
using Common.Entity;
using Common.Enums;
using DataAccess.Model.DAO;
using DataAccess.Model.Helper;
using System.Net;

namespace WEB_CLIENT.Services.Profile
{
    public class ProfileService : IProfileService
    {
        private readonly DAOUser _daoUser;
        public ProfileService(DAOUser daoUser)
        {
            _daoUser = daoUser;
        }

        public ResponseBase<Dictionary<string, object>?> Index(Guid userId)
        {
            try
            {
                User? user = _daoUser.getUser(userId);
                if (user == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>("Not found user", (int)HttpStatusCode.NotFound);
                }
                List<string> list = UserHelper.Genders;
                Dictionary<string, object> data = new Dictionary<string, object>()
                {
                    {"user", user },
                    {"list", list },
                };
                return new ResponseBase<Dictionary<string, object>?>(data);
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object>?>(ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }
        }

        public ResponseBase<Dictionary<string, object>?> Index(Guid userId, ProfileDTO DTO, string valueImg)
        {
            try
            {
                User? user = _daoUser.getUser(userId);
                if (user == null)
                {
                    return new ResponseBase<Dictionary<string, object>?>("Not found user", (int)HttpStatusCode.NotFound);
                }
                List<string> list = UserHelper.Genders;
                user.FullName = DTO.FullName == null || DTO.FullName.Trim().Length == 0 ? "" : DTO.FullName.Trim();
                user.Phone = DTO.Phone;
                user.Image = DTO.Image == null || DTO.Image.Trim().Length == 0 ? valueImg : "/img/" + DTO.Image.Trim();
                user.Address = DTO.Address == null || DTO.Address.Trim().Length == 0 ? null : DTO.Address.Trim();
                user.Email = DTO.Email.Trim();
                user.Gender = DTO.Gender.Trim();
                Dictionary<string, object> data = new Dictionary<string, object>()
                {
                    {"user", user },
                    {"list", list },
                };
                if (DTO.FullName == null || DTO.FullName.Trim().Length == 0)
                {
                    return new ResponseBase<Dictionary<string, object>?>(data, "You have to input your name", (int)HttpStatusCode.Conflict);
                }
                if (DTO.Phone != null && DTO.Phone.Length != (int)UserInfo.Phone_Length)
                {
                    return new ResponseBase<Dictionary<string, object>?>(data, "Phone must be " + (int)UserInfo.Phone_Length + " numbers", (int)HttpStatusCode.Conflict);
                }
                if (DTO.Address != null && DTO.Address.Trim().Length > (int)UserInfo.Max_Address_Length)
                {
                    return new ResponseBase<Dictionary<string, object>?>(data, "Address max " + (int)UserInfo.Max_Address_Length + " characters", (int)HttpStatusCode.Conflict);
                }
                if (_daoUser.isExist(userId , user.Email))
                {
                    return new ResponseBase<Dictionary<string, object>?>(data, "Email existed", (int)HttpStatusCode.Conflict);
                }
                user.UpdateAt = DateTime.Now;
                _daoUser.UpdateUser(user);
                data["user"] = user;
                return new ResponseBase<Dictionary<string, object>?>(data, "Update successful");
            }
            catch (Exception ex)
            {
                return new ResponseBase<Dictionary<string, object>?>(ex.Message + " " + ex, (int)HttpStatusCode.InternalServerError);
            }

        }
    }
}
