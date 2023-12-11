using DataAccess.Const;
using DataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace WEB_CLIENT.Model
{
    public class InsertData
    {
        private static readonly MyDbContext context = new MyDbContext();
        public static async Task Insert()
        {
            List<Category> listCategory = await InsertCategory();
            List<User> listUser = await InsertUser();
            List<Course> listCourse = await InsertCourse(listUser, listCategory);
            await InsertEnrollCourse(listCourse, listUser);
        }
        private static async Task InsertEnrollCourse(List<Course> listCourse, List<User> listUser)
        {
            if(listCourse.Count > 0 && listUser.Count > 0)
            {
                if (await context.EnrollCourses.AnyAsync() == false)
                {
                    List<EnrollCourse> listEnroll = new List<EnrollCourse>()
                    {
                        new EnrollCourse() { CourseId = listCourse[0].CourseId, StudentId = listUser[0].Id, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        new EnrollCourse() { CourseId = listCourse[4].CourseId, StudentId = listUser[2].Id, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        new EnrollCourse() { CourseId = listCourse[2].CourseId, StudentId = listUser[2].Id, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                    };
                    await context.EnrollCourses.AddRangeAsync(listEnroll);
                    await context.SaveChangesAsync();
                }
            }
        }
        private static async Task<List<Course>> InsertCourse(List<User> listUser, List<Category> listCategory)
        {
            List<Course> listCourse = new List<Course>();
            if(listUser.Count > 0 && listCategory.Count > 0)
            {
                if (await context.Courses.AnyAsync() == false)
                {
                    listCourse = new List<Course>()
                    {
                         /*1*/new Course() {CourseId = Guid.NewGuid() , CourseName = "Working With Color Color in Adobe Photoshop", Image = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTTtAJzLDx1VGYsdC9Mg1rqYkhswW8kQ4dLRw&usqp=CAU", CategoryId = listCategory[0].Id, CreatorId = listUser[7].Id, Description = "Any artist will tell you that the use of color is a major component of the design process, regardless of the medium. Digital art and photography are no exceptions. Color can be powerful and evocative, but only if you know how to use it properly. In this course you will learn about the basics of color theory. We will then take a look at the different ways Photoshop handles colors using color modes, and what the advantages and disadvantages are. After that we will use this knowledge to assemble a colorful and vibrant digital scene, using several different techniques for controlling color in Photoshop.", CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                         /*2*/new Course() {CourseId = Guid.NewGuid() , CourseName = "CertNexus Certified Ethical Emerging Technologist Professional Certificate", Image = "http://img.youtube.com/vi/3OJOEo6MTaI/hqdefault.jpg", CategoryId = listCategory[1].Id, CreatorId = listUser[8].Id, Description = "Learners will create a portfolio of assets highlighting their skills as ethical leaders. The assets consist of written documents and video communications required of ethical leaders, including Op-Ed articles, risk management reports, strategy memos, media releases, and video press briefings.Learners who complete the Honors projects will also author an industry feature article; a recommendation memo for the most appropriate ethical framework to guide an organization or a project; an Algorithmic Impact Assessment; a change management presentation to a Board of Directors; and a strategic business document such as a cost-benefit analysis, design plan, or business continuity plan.", CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                         /*3*/new Course() {CourseId = Guid.NewGuid() , CourseName = "Learning How to Learn : Powerful mental tools to help you master tough subjects", Image = "https://s3-ap-southeast-1.amazonaws.com/images.spiderum.com/sp-images/ecc93e90203511ec9c8281a41a921537.png", CategoryId = listCategory[2].Id, CreatorId = listUser[9].Id, Description = "Although living brains are very complex, this module uses metaphor and analogy to help simplify matters. You will discover several fundamentally different modes of thinking, and how you can use these modes to improve your learning. You will also be introduced to a tool for tackling procrastination, be given some practical information about memory, and discover surprisingly useful insights about learning and sleep.", CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                         /*4*/new Course() {CourseId = Guid.NewGuid() , CourseName = "Better Business Writing in English", Image = "https://technicalwriterhq.com/wp-content/uploads/2022/02/41-Business-Writing-Course.jpg", CategoryId = listCategory[3].Id, CreatorId = listUser[10].Id, Description = "Learning outcomes: After this module, you will be able to develop your personal voice and increase your accuracy, and appropriateness in written English, and produce a written document which displays your personal voice.", CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                         /*5*/new Course() {CourseId = Guid.NewGuid() , CourseName = "Social Media Marketing", Image = "https://social.vn/wp-content/uploads/2020/11/Social-Media-Marketing.jpg", CategoryId = listCategory[4].Id, CreatorId = listUser[7].Id, Description = "In a 2018 survey of businesses, Buffer found that only 29% had effective social media marketing programs.    A recent survey of consumers by Tomoson found 92% of consumers trust recommendations from other people over brand content, 70% found consumer reviews to be their second most trusted source, 47% read blogs developed by influencers and experts to discover new trends and new ideas and 35% used blogs to discover new products and services.  Also, 20% of women who used social considered products promoted by bloggers they knew.   Today, businesses and consumers use social media to make their purchase decisions.", CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                         /*6*/new Course() {CourseId = Guid.NewGuid() , CourseName = "Object Oriented Programming in C++", Image = "https://res.cloudinary.com/practicaldev/image/fetch/s--rPvSn38T--/c_imagga_scale,f_auto,fl_progressive,h_420,q_auto,w_1000/https://dev-to-uploads.s3.amazonaws.com/i/h2917prj7ldo0ow5x5ih.png", CategoryId = listCategory[1].Id, CreatorId = listUser[8].Id, Description = "Object-Oriented-Programming is an object-based programming method to find the essence of the problem. C++ OOP course helps programmers learn programming techniques where all logic", CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                         /*7*/new Course() {CourseId = Guid.NewGuid() , CourseName = "Basic Cross-Platform Application Programming With .NET", Image = "https://coodesh.com/blog/wp-content/uploads/2021/11/Artigo-148-scaled.jpg", CategoryId = listCategory[1].Id, CreatorId = listUser[8].Id, Description = ".NET Core is a free and open source framework for developing cross-platform applications targeting Windows, Linux and macOS. It is capable of running applications on devices, the cloud and the IoT. It supports four cross-platform scenarios: ASP.NET Core Web apps, command line apps, libraries and Web APIs. The recently released .NET Core 3 (preview bits) supports Windows rendering forms like WinForms, WPF and UWP, but only on Windows.", CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                         /*8*/new Course() {CourseId = Guid.NewGuid() , CourseName = "Skill Play Game", Image = "https://cdn.sforum.vn/sforum/wp-content/uploads/2022/01/thum-960x540.jpg", CategoryId = listCategory[2].Id, CreatorId = listUser[9].Id, Description = "You can master all the skills in league of legends wild rift with the instructional tips learned from this course", CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                         /*9*/new Course() {CourseId = Guid.NewGuid() , CourseName = "Design JSP", Image = "https://wiki.tino.org/wp-content/uploads/2021/04/servlet-jsp-tutorial.png", CategoryId = listCategory[4].Id, CreatorId = listUser[7].Id, Description = "good", CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                    };
                    await context.Courses.AddRangeAsync(listCourse);
                    await context.SaveChangesAsync();
                }
            }   
            return listCourse;
        }
        private static async Task<List<User>> InsertUser()
        {
            List<User> list = new List<User>();
            if (await context.Users.AnyAsync() == false)
            {
                list = new List<User>()
                {
                    /*1*/new User(){ Id = Guid.NewGuid(), FullName = "Nguyen Thi Thu", Image = "./img/carousel-2.jpg", Phone = "0984739845", Address = "7683 Ruskin Avenue", Email = "oparagreen0@gmail.com", Gender = UserConst.GENDER_MALE, Username = "ThuThu", Password = "c1d0e46fdeb2b72758a6a5bd5eecf2622ff8b84a8964c8e9687c6c05c9f474b5", RoleName = UserConst.ROLE_STUDENT, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                    /*2*/new User(){ Id = Guid.NewGuid(), FullName = "Nguyen Anh Tuan",Image = "https://i.pinimg.com/564x/68/cb/39/68cb398abe7964a7e9eb9f1e9e0da8a6.jpg", Phone = "6298446654", Address = "0341 Everett Court", Email = "kfleet1@gmail.com", Gender = UserConst.GENDER_MALE, Username = "AnhTuan", Password = "c1d0e46fdeb2b72758a6a5bd5eecf2622ff8b84a8964c8e9687c6c05c9f474b5", RoleName = UserConst.ROLE_STUDENT, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                    /*3*/new User(){ Id = Guid.NewGuid(), FullName = "Chu Quang Quan", Image = "https://i.pinimg.com/736x/95/0b/21/950b21a6422cf2f2db7579c6494d4acb.jpg", Phone = "8851738015", Address = "7023 Algoma Street", Email = "fellcock2@gmail.com", Gender = UserConst.GENDER_FEMALE, Username = "QuangQuan", Password = "c1d0e46fdeb2b72758a6a5bd5eecf2622ff8b84a8964c8e9687c6c05c9f474b5", RoleName = UserConst.ROLE_STUDENT, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                    /*4*/new User(){ Id = Guid.NewGuid(), FullName = "Ta Nhat Anh", Image = "https://pdp.edu.vn/wp-content/uploads/2021/05/hinh-anh-avatar-nam-1-600x600.jpg", Phone = "9306711406", Address = "943 Heath Pass", Email = "phatherell3@gmail.com", Gender = UserConst.GENDER_FEMALE, Username = "NhatAnh", Password = "c1d0e46fdeb2b72758a6a5bd5eecf2622ff8b84a8964c8e9687c6c05c9f474b5", RoleName = UserConst.ROLE_STUDENT, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                    /*5*/new User(){ Id = Guid.NewGuid(), FullName = "Nguyen Minh Duc",Image = "https://hinhnen123.com/?attachment_id=405", Phone = "5541282702", Address = "005 Prairie Rose Point", Email = "bkervin4@gmail.com", Gender = UserConst.GENDER_FEMALE, Username = "MinhDuc", Password = "c1d0e46fdeb2b72758a6a5bd5eecf2622ff8b84a8964c8e9687c6c05c9f474b5", RoleName = UserConst.ROLE_STUDENT, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                    /*6*/new User(){ Id = Guid.NewGuid(), FullName = "Nguyen Minh Vuong",Image = "https://demoda.vn/wp-content/uploads/2022/04/avatar-cap-doi-chibi-lang-man-ve-nu.jpg", Phone = "9544569704", Address = "4 Aberg Drive", Email = "rrushworth5@gmail.com", Gender = UserConst.GENDER_MALE, Username = "VuongDepTrai", Password = "c1d0e46fdeb2b72758a6a5bd5eecf2622ff8b84a8964c8e9687c6c05c9f474b5", RoleName = UserConst.ROLE_STUDENT, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                    /*7*/new User(){ Id = Guid.NewGuid(), FullName = "Nicky Gaitone", Image = "https://i.pinimg.com/564x/08/51/e6/0851e61234e5341c687dbb716158e320.jpg", Phone = "7583151589", Address = "11007 Cherokee Drive", Email = "ngaitone6@gmail.com", Gender = UserConst.GENDER_FEMALE, Username = "Nicky", Password = "c1d0e46fdeb2b72758a6a5bd5eecf2622ff8b84a8964c8e9687c6c05c9f474b5", RoleName = UserConst.ROLE_STUDENT, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                    /*8*/new User(){ Id = Guid.NewGuid(), FullName = "Kirk Nelson", Image = "https://s3.amazonaws.com/cms-assets.tutsplus.com/uploads/users/8/profiles/18494/profileImage/KirkHeadShot.jpg", Phone = "6481628081", Address = null, Email = "KirkNelson@gmail.com", Gender = UserConst.GENDER_FEMALE, Username = "Kirk", Password = "64ea093e4806662796ecdd757ca39a7d45dbdceb5d44857a7ab235d19d5709b7", RoleName = UserConst.ROLE_TEACHER, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                    /*9*/new User(){ Id = Guid.NewGuid(), FullName = "Ulises Ayliffe",Image = "./img/team-1.jpg", Phone = "6511678528", Address = null, Email = "uayliffe1@gmail.com", Gender = UserConst.GENDER_FEMALE, Username = "Ulises", Password = "64ea093e4806662796ecdd757ca39a7d45dbdceb5d44857a7ab235d19d5709b7", RoleName = UserConst.ROLE_TEACHER, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                    /*10*/new User(){ Id = Guid.NewGuid(), FullName = "Terrell Cordobes",Image = "./img/team-2.jpg", Phone = "7908661977", Address = null, Email = "tcordobes2@gmail.com", Gender = UserConst.GENDER_MALE, Username = "Terrell", Password = "64ea093e4806662796ecdd757ca39a7d45dbdceb5d44857a7ab235d19d5709b7", RoleName = UserConst.ROLE_TEACHER, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                    /*11*/new User(){ Id = Guid.NewGuid(), FullName = "Virge Aldred",Image = "./img/team-3.jpg", Phone = "2934629124", Address = null, Email = "valdred3@gmail.com", Gender = UserConst.GENDER_FEMALE, Username = "Virge", Password = "64ea093e4806662796ecdd757ca39a7d45dbdceb5d44857a7ab235d19d5709b7", RoleName = UserConst.ROLE_TEACHER, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                    /*12*/new User(){ Id = Guid.NewGuid(), FullName = "Tommy Walbrun",Image = "https://img.lovepik.com/free-png/20210919/lovepik-male-teacher-teaching-png-image_400770642_wh1200.png", Phone = "9661305299", Address = null, Email = "twalbrun4@gmail.com", Gender = UserConst.GENDER_MALE, Username = "Tommy", Password = "64ea093e4806662796ecdd757ca39a7d45dbdceb5d44857a7ab235d19d5709b7", RoleName = UserConst.ROLE_TEACHER, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                    /*13*/new User(){ Id = Guid.NewGuid(), FullName = "Admin",Image = "./img/team-1.jpg", Phone = "9661231236", Address = null, Email = "fiadjrf8@gmail.com", Gender = UserConst.GENDER_MALE, Username = "Admin", Password = "70db85967ceb5ab1d79060fe0e2fc536f02ca747086564989953385fe58cab7f", RoleName = UserConst.ROLE_ADMIN, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                };
                await context.Users.AddRangeAsync(list);
                await context.SaveChangesAsync();
            }
            return list;
        }
        private static async Task<List<Category>> InsertCategory()
        {
            List<Category> listCategory = new List<Category>();
            if (await context.Categories.AnyAsync() == false)
            {
                listCategory = new List<Category>()
                {
                    new Category(){ Name = "Graphic Design", CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                    new Category(){ Name = "Software Technology", CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                    new Category(){ Name = "Soft Skills", CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                    new Category(){ Name = "Foreign Language", CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                    new Category(){ Name = "Business", CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                };
                await context.Categories.AddRangeAsync(listCategory);
                await context.SaveChangesAsync();
                listCategory = await context.Categories.ToListAsync();
            }
            return listCategory;
        }
    }
}
