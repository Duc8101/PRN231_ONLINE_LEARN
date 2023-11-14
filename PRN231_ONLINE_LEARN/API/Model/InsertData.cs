using DataAccess.Const;
using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace API.Model
{
    public class InsertData
    {
        private static readonly MyDbContext context = new MyDbContext();
        public static async Task Insert()
        {
            List<Category> listCategory = await InsertCategory();

        }

        private static async Task<List<User>> InsertUser()
        {
            List<User> list = new List<User>();
            if (await context.Users.AnyAsync() == false)
            {
                list = new List<User>()
                {
                    new User(){ Id = Guid.NewGuid(), FullName = "Nguyen Thi Thu", Phone = "0984739845", Address = "7683 Ruskin Avenue", Email = "oparagreen0@gmail.com", Gender = UserConst.GENDER_MALE, Username = "ThuThu", Password = "", RoleName = UserConst.ROLE_STUDENT, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                    new User(){ Id = Guid.NewGuid(), FullName = "Nguyen Anh Tuan", Phone = "6298446654", Address = "0341 Everett Court", Email = "kfleet1@gmail.com", Gender = UserConst.GENDER_MALE, Username = "AnhTuan", Password = "", RoleName = UserConst.ROLE_STUDENT, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                    new User(){ Id = Guid.NewGuid(), FullName = "Chu Quang Quan", Phone = "8851738015", Address = "7023 Algoma Street", Email = "fellcock2@gmail.com", Gender = UserConst.GENDER_FEMALE, Username = "QuangQuan", Password = "", RoleName = UserConst.ROLE_STUDENT, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                    new User(){ Id = Guid.NewGuid(), FullName = "Ta Nhat Anh", Phone = "9306711406", Address = "943 Heath Pass", Email = "phatherell3@gmail.com", Gender = UserConst.GENDER_FEMALE, Username = "NhatAnh", Password = "", RoleName = UserConst.ROLE_STUDENT, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                    new User(){ Id = Guid.NewGuid(), FullName = "Nguyen Minh Duc", Phone = "5541282702", Address = "005 Prairie Rose Point", Email = "bkervin4@gmail.com", Gender = UserConst.GENDER_FEMALE, Username = "MinhDuc", Password = "", RoleName = UserConst.ROLE_STUDENT, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                    new User(){ Id = Guid.NewGuid(), FullName = "Nguyen Minh Vuong", Phone = "9544569704", Address = "4 Aberg Drive", Email = "rrushworth5@gmail.com", Gender = UserConst.GENDER_MALE, Username = "VuongDepTrai", Password = "", RoleName = UserConst.ROLE_STUDENT, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                    new User(){ Id = Guid.NewGuid(), FullName = "Nicky Gaitone", Phone = "7583151589", Address = "11007 Cherokee Drive", Email = "ngaitone6@gmail.com", Gender = UserConst.GENDER_FEMALE, Username = "Nicky", Password = "", RoleName = UserConst.ROLE_STUDENT, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                    new User(){ Id = Guid.NewGuid(), FullName = "Kirk Nelson", Phone = "6481628081", Address = null, Email = "KirkNelson@gmail.com", Gender = UserConst.GENDER_FEMALE, Username = "Kirk", Password = "", RoleName = UserConst.ROLE_TEACHER, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                    new User(){ Id = Guid.NewGuid(), FullName = "Ulises Ayliffe", Phone = "6511678528", Address = null, Email = "uayliffe1@gmail.com", Gender = UserConst.GENDER_FEMALE, Username = "Ulises", Password = "", RoleName = UserConst.ROLE_TEACHER, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                    new User(){ Id = Guid.NewGuid(), FullName = "Terrell Cordobes", Phone = "7908661977", Address = null, Email = "tcordobes2@gmail.com", Gender = UserConst.GENDER_MALE, Username = "Terrell", Password = "", RoleName = UserConst.ROLE_TEACHER, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                    new User(){ Id = Guid.NewGuid(), FullName = "Virge Aldred", Phone = "2934629124", Address = null, Email = "valdred3@gmail.com", Gender = UserConst.GENDER_FEMALE, Username = "Virge", Password = "", RoleName = UserConst.ROLE_TEACHER, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                    new User(){ Id = Guid.NewGuid(), FullName = "Tommy Walbrun", Phone = "9661305299", Address = null, Email = "twalbrun4@gmail.com", Gender = UserConst.GENDER_MALE, Username = "Tommy", Password = "", RoleName = UserConst.ROLE_TEACHER, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                    new User(){ Id = Guid.NewGuid(), FullName = "Admin", Phone = "9661231236", Address = null, Email = "fiadjrf8@gmail.com", Gender = UserConst.GENDER_MALE, Username = "Admin", Password = "", RoleName = UserConst.ROLE_ADMIN, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                };
            }
            return list;
        }
        private static async Task<List<Category>> InsertCategory()
        {
            List<Category> list = new List<Category>();
            if (await context.Categories.AnyAsync() == false)
            {
                list = new List<Category>()
                {
                    new Category(){ Name = "Graphic Design", CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                    new Category(){ Name = "Software Technology", CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                    new Category(){ Name = "Soft Skills", CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                    new Category(){ Name = "Foreign Language", CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                    new Category(){ Name = "Business", CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                };
                await context.Categories.AddRangeAsync(list);
                await context.SaveChangesAsync();
            }
            return list;
        }
    }
}
