using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    gender = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    CourseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryID = table.Column<int>(type: "int", nullable: false),
                    CreatorID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.CourseID);
                    table.ForeignKey(
                        name: "FK_Course_Category_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Category",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Course_User_CreatorID",
                        column: x => x.CreatorID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EnrollCourse",
                columns: table => new
                {
                    CourseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrollCourse", x => new { x.CourseID, x.StudentID });
                    table.ForeignKey(
                        name: "FK_EnrollCourse_Course_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Course",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnrollCourse_User_StudentID",
                        column: x => x.StudentID,
                        principalTable: "User",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Lesson",
                columns: table => new
                {
                    LessonID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LessonName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CourseID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LessonNo = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lesson", x => x.LessonID);
                    table.ForeignKey(
                        name: "FK_Lesson_Course_CourseID",
                        column: x => x.CourseID,
                        principalTable: "Course",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LessonPDF",
                columns: table => new
                {
                    PDFID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PDFName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePDF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LessonID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonPDF", x => x.PDFID);
                    table.ForeignKey(
                        name: "FK_LessonPDF_Lesson_LessonID",
                        column: x => x.LessonID,
                        principalTable: "Lesson",
                        principalColumn: "LessonID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LessonVideo",
                columns: table => new
                {
                    VideoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VideoName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FileVideo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LessonID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonVideo", x => x.VideoID);
                    table.ForeignKey(
                        name: "FK_LessonVideo_Lesson_LessonID",
                        column: x => x.LessonID,
                        principalTable: "Lesson",
                        principalColumn: "LessonID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Quiz",
                columns: table => new
                {
                    QuestionID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LessonID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Answer1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Answer4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AnswerCorrect = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quiz", x => x.QuestionID);
                    table.ForeignKey(
                        name: "FK_Quiz_Lesson_LessonID",
                        column: x => x.LessonID,
                        principalTable: "Lesson",
                        principalColumn: "LessonID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Result",
                columns: table => new
                {
                    LessonID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    score = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Result", x => new { x.LessonID, x.StudentID });
                    table.ForeignKey(
                        name: "FK_Result_Lesson_LessonID",
                        column: x => x.LessonID,
                        principalTable: "Lesson",
                        principalColumn: "LessonID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Result_User_StudentID",
                        column: x => x.StudentID,
                        principalTable: "User",
                        principalColumn: "ID");
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "ID", "CreatedAt", "IsDeleted", "Name", "UpdateAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(812), false, "Graphic Design", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(821) },
                    { 2, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(824), false, "Software Technology", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(824) },
                    { 3, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(825), false, "Soft Skills", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(826) },
                    { 4, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(827), false, "Foreign Language", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(827) },
                    { 5, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(828), false, "Business", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(829) }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "ID", "address", "CreatedAt", "email", "FullName", "gender", "image", "IsDeleted", "password", "phone", "RoleName", "UpdateAt", "username" },
                values: new object[,]
                {
                    { new Guid("043bbe38-8350-4253-8ca3-518183b99f07"), "005 Prairie Rose Point", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(918), "bkervin4@gmail.com", "Nguyen Minh Duc", "Female", "https://hinhnen123.com/?attachment_id=405", false, "c1d0e46fdeb2b72758a6a5bd5eecf2622ff8b84a8964c8e9687c6c05c9f474b5", "5541282702", "Student", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(918), "MinhDuc" },
                    { new Guid("1ebb6d27-ac61-4d57-bf17-0f3dace682f6"), null, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(936), "valdred3@gmail.com", "Virge Aldred", "Female", "./img/team-3.jpg", false, "64ea093e4806662796ecdd757ca39a7d45dbdceb5d44857a7ab235d19d5709b7", "2934629124", "Teacher", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(936), "Virge" },
                    { new Guid("3496dbaf-5685-49ef-9ebb-5fcd2f5bc1ab"), null, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(933), "tcordobes2@gmail.com", "Terrell Cordobes", "Male", "./img/team-2.jpg", false, "64ea093e4806662796ecdd757ca39a7d45dbdceb5d44857a7ab235d19d5709b7", "7908661977", "Teacher", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(934), "Terrell" },
                    { new Guid("450c1e80-9a94-4d31-bc71-f1e2102161cd"), "11007 Cherokee Drive", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(924), "ngaitone6@gmail.com", "Nicky Gaitone", "Female", "https://i.pinimg.com/564x/08/51/e6/0851e61234e5341c687dbb716158e320.jpg", false, "c1d0e46fdeb2b72758a6a5bd5eecf2622ff8b84a8964c8e9687c6c05c9f474b5", "7583151589", "Student", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(925), "Nicky" },
                    { new Guid("467db315-2dc2-408d-a82b-423453f17c5f"), null, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(927), "KirkNelson@gmail.com", "Kirk Nelson", "Female", "https://s3.amazonaws.com/cms-assets.tutsplus.com/uploads/users/8/profiles/18494/profileImage/KirkHeadShot.jpg", false, "64ea093e4806662796ecdd757ca39a7d45dbdceb5d44857a7ab235d19d5709b7", "6481628081", "Teacher", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(927), "Kirk" },
                    { new Guid("59e86453-0a1d-4366-ad9a-e427e5a9280d"), null, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(943), "fiadjrf8@gmail.com", "Admin", "Male", "./img/team-1.jpg", false, "70db85967ceb5ab1d79060fe0e2fc536f02ca747086564989953385fe58cab7f", "9661231236", "Admin", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(944), "Admin" },
                    { new Guid("5acd3021-5833-4d13-ba53-f0844918885b"), "7683 Ruskin Avenue", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(896), "oparagreen0@gmail.com", "Nguyen Thi Thu", "Male", "./img/carousel-2.jpg", false, "c1d0e46fdeb2b72758a6a5bd5eecf2622ff8b84a8964c8e9687c6c05c9f474b5", "0984739845", "Student", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(896), "ThuThu" },
                    { new Guid("5d676de5-8c12-4c10-b6a8-60259b401e3c"), null, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(930), "uayliffe1@gmail.com", "Ulises Ayliffe", "Female", "./img/team-1.jpg", false, "64ea093e4806662796ecdd757ca39a7d45dbdceb5d44857a7ab235d19d5709b7", "6511678528", "Teacher", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(930), "Ulises" },
                    { new Guid("66f31e35-01a5-4322-880a-e489face55cf"), "0341 Everett Court", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(901), "kfleet1@gmail.com", "Nguyen Anh Tuan", "Male", "https://i.pinimg.com/564x/68/cb/39/68cb398abe7964a7e9eb9f1e9e0da8a6.jpg", false, "c1d0e46fdeb2b72758a6a5bd5eecf2622ff8b84a8964c8e9687c6c05c9f474b5", "6298446654", "Student", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(902), "AnhTuan" },
                    { new Guid("78a9948b-7541-431c-aa64-3a50df21d17d"), "7023 Algoma Street", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(905), "fellcock2@gmail.com", "Chu Quang Quan", "Female", "https://i.pinimg.com/736x/95/0b/21/950b21a6422cf2f2db7579c6494d4acb.jpg", false, "c1d0e46fdeb2b72758a6a5bd5eecf2622ff8b84a8964c8e9687c6c05c9f474b5", "8851738015", "Student", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(905), "QuangQuan" },
                    { new Guid("cf370fe9-bb5a-4f92-aa98-711514a0cc27"), "943 Heath Pass", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(915), "phatherell3@gmail.com", "Ta Nhat Anh", "Female", "https://pdp.edu.vn/wp-content/uploads/2021/05/hinh-anh-avatar-nam-1-600x600.jpg", false, "c1d0e46fdeb2b72758a6a5bd5eecf2622ff8b84a8964c8e9687c6c05c9f474b5", "9306711406", "Student", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(915), "NhatAnh" },
                    { new Guid("e3e16a39-9a00-44d1-91fc-e2a829fd5457"), null, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(941), "twalbrun4@gmail.com", "Tommy Walbrun", "Male", "https://img.lovepik.com/free-png/20210919/lovepik-male-teacher-teaching-png-image_400770642_wh1200.png", false, "64ea093e4806662796ecdd757ca39a7d45dbdceb5d44857a7ab235d19d5709b7", "9661305299", "Teacher", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(941), "Tommy" },
                    { new Guid("feab0a86-aada-4ec3-94fd-d24bc0a0e3de"), "4 Aberg Drive", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(921), "rrushworth5@gmail.com", "Nguyen Minh Vuong", "Male", "https://demoda.vn/wp-content/uploads/2022/04/avatar-cap-doi-chibi-lang-man-ve-nu.jpg", false, "c1d0e46fdeb2b72758a6a5bd5eecf2622ff8b84a8964c8e9687c6c05c9f474b5", "9544569704", "Student", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(922), "VuongDepTrai" }
                });

            migrationBuilder.InsertData(
                table: "Course",
                columns: new[] { "CourseID", "CategoryID", "CourseName", "CreatedAt", "CreatorID", "description", "image", "IsDeleted", "UpdateAt" },
                values: new object[,]
                {
                    { new Guid("167ef6df-86f0-4d6c-a177-ea4e1e0d0519"), 2, "CertNexus Certified Ethical Emerging Technologist Professional Certificate", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(976), new Guid("5d676de5-8c12-4c10-b6a8-60259b401e3c"), "Learners will create a portfolio of assets highlighting their skills as ethical leaders. The assets consist of written documents and video communications required of ethical leaders, including Op-Ed articles, risk management reports, strategy memos, media releases, and video press briefings.Learners who complete the Honors projects will also author an industry feature article; a recommendation memo for the most appropriate ethical framework to guide an organization or a project; an Algorithmic Impact Assessment; a change management presentation to a Board of Directors; and a strategic business document such as a cost-benefit analysis, design plan, or business continuity plan.", "http://img.youtube.com/vi/3OJOEo6MTaI/hqdefault.jpg", false, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(976) },
                    { new Guid("19b3226c-14d2-429b-abd5-72acb775dfaf"), 3, "Skill Play Game", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(995), new Guid("3496dbaf-5685-49ef-9ebb-5fcd2f5bc1ab"), "You can master all the skills in league of legends wild rift with the instructional tips learned from this course", "https://cdn.sforum.vn/sforum/wp-content/uploads/2022/01/thum-960x540.jpg", false, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(995) },
                    { new Guid("24f4fa75-27a7-4fb1-ad34-57a30e38c6c7"), 5, "Social Media Marketing", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(984), new Guid("467db315-2dc2-408d-a82b-423453f17c5f"), "In a 2018 survey of businesses, Buffer found that only 29% had effective social media marketing programs.    A recent survey of consumers by Tomoson found 92% of consumers trust recommendations from other people over brand content, 70% found consumer reviews to be their second most trusted source, 47% read blogs developed by influencers and experts to discover new trends and new ideas and 35% used blogs to discover new products and services.  Also, 20% of women who used social considered products promoted by bloggers they knew.   Today, businesses and consumers use social media to make their purchase decisions.", "https://social.vn/wp-content/uploads/2020/11/Social-Media-Marketing.jpg", false, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(985) },
                    { new Guid("281bc679-9d3d-4276-b8af-dab12a7cbbf3"), 1, "Working With Color Color in Adobe Photoshop", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(967), new Guid("467db315-2dc2-408d-a82b-423453f17c5f"), "Any artist will tell you that the use of color is a major component of the design process, regardless of the medium. Digital art and photography are no exceptions. Color can be powerful and evocative, but only if you know how to use it properly. In this course you will learn about the basics of color theory. We will then take a look at the different ways Photoshop handles colors using color modes, and what the advantages and disadvantages are. After that we will use this knowledge to assemble a colorful and vibrant digital scene, using several different techniques for controlling color in Photoshop.", "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTTtAJzLDx1VGYsdC9Mg1rqYkhswW8kQ4dLRw&usqp=CAU", false, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(967) },
                    { new Guid("296cc407-5430-405b-aa5f-876e7f7e7105"), 3, "Learning How to Learn : Powerful mental tools to help you master tough subjects", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(979), new Guid("3496dbaf-5685-49ef-9ebb-5fcd2f5bc1ab"), "Although living brains are very complex, this module uses metaphor and analogy to help simplify matters. You will discover several fundamentally different modes of thinking, and how you can use these modes to improve your learning. You will also be introduced to a tool for tackling procrastination, be given some practical information about memory, and discover surprisingly useful insights about learning and sleep.", "https://s3-ap-southeast-1.amazonaws.com/images.spiderum.com/sp-images/ecc93e90203511ec9c8281a41a921537.png", false, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(979) },
                    { new Guid("5c9df98e-a4a3-4881-89db-969373b152d9"), 4, "Better Business Writing in English", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(982), new Guid("1ebb6d27-ac61-4d57-bf17-0f3dace682f6"), "Learning outcomes: After this module, you will be able to develop your personal voice and increase your accuracy, and appropriateness in written English, and produce a written document which displays your personal voice.", "https://technicalwriterhq.com/wp-content/uploads/2022/02/41-Business-Writing-Course.jpg", false, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(982) },
                    { new Guid("764aeffd-3132-4b07-ac0b-539baaba3f33"), 5, "Design JSP", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(997), new Guid("467db315-2dc2-408d-a82b-423453f17c5f"), "good", "https://wiki.tino.org/wp-content/uploads/2021/04/servlet-jsp-tutorial.png", false, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(998) },
                    { new Guid("a4647afa-c2b7-49df-b13b-99577acef413"), 2, "Object Oriented Programming in C++", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(988), new Guid("5d676de5-8c12-4c10-b6a8-60259b401e3c"), "Object-Oriented-Programming is an object-based programming method to find the essence of the problem. C++ OOP course helps programmers learn programming techniques where all logic", "https://res.cloudinary.com/practicaldev/image/fetch/s--rPvSn38T--/c_imagga_scale,f_auto,fl_progressive,h_420,q_auto,w_1000/https://dev-to-uploads.s3.amazonaws.com/i/h2917prj7ldo0ow5x5ih.png", false, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(988) },
                    { new Guid("db861b08-b768-486f-a0c1-d097ecac8301"), 2, "Basic Cross-Platform Application Programming With .NET", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(992), new Guid("5d676de5-8c12-4c10-b6a8-60259b401e3c"), ".NET Core is a free and open source framework for developing cross-platform applications targeting Windows, Linux and macOS. It is capable of running applications on devices, the cloud and the IoT. It supports four cross-platform scenarios: ASP.NET Core Web apps, command line apps, libraries and Web APIs. The recently released .NET Core 3 (preview bits) supports Windows rendering forms like WinForms, WPF and UWP, but only on Windows.", "https://coodesh.com/blog/wp-content/uploads/2021/11/Artigo-148-scaled.jpg", false, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(992) }
                });

            migrationBuilder.InsertData(
                table: "EnrollCourse",
                columns: new[] { "CourseID", "StudentID", "CreatedAt", "IsDeleted", "UpdateAt" },
                values: new object[,]
                {
                    { new Guid("24f4fa75-27a7-4fb1-ad34-57a30e38c6c7"), new Guid("78a9948b-7541-431c-aa64-3a50df21d17d"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1019), false, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1019) },
                    { new Guid("281bc679-9d3d-4276-b8af-dab12a7cbbf3"), new Guid("5acd3021-5833-4d13-ba53-f0844918885b"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1016), false, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1017) },
                    { new Guid("296cc407-5430-405b-aa5f-876e7f7e7105"), new Guid("78a9948b-7541-431c-aa64-3a50df21d17d"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1021), false, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1021) }
                });

            migrationBuilder.InsertData(
                table: "Lesson",
                columns: new[] { "LessonID", "CourseID", "CreatedAt", "IsDeleted", "LessonName", "LessonNo", "UpdateAt" },
                values: new object[,]
                {
                    { new Guid("161b0a99-2b92-4138-8ede-78ba850fe38a"), new Guid("24f4fa75-27a7-4fb1-ad34-57a30e38c6c7"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1095), false, "Most Important Metrics to Observe", 2, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1095) },
                    { new Guid("1a13fdec-8f09-46cf-a810-1446104bed6b"), new Guid("5c9df98e-a4a3-4881-89db-969373b152d9"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1085), false, "3 Key Parts of a Proposal", 3, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1085) },
                    { new Guid("1e540a42-2699-4dc0-9107-116ce5d61760"), new Guid("281bc679-9d3d-4276-b8af-dab12a7cbbf3"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1044), false, "Working With Color", 4, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1044) },
                    { new Guid("3479ea5a-7f81-40e0-a740-f32a6d7d765f"), new Guid("296cc407-5430-405b-aa5f-876e7f7e7105"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1072), false, "Procrastination, Memory, and Sleep", 3, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1072) },
                    { new Guid("37578122-2acd-4922-817c-43956b2f7cdc"), new Guid("5c9df98e-a4a3-4881-89db-969373b152d9"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1089), false, "Connect Ideas & Sentences", 5, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1090) },
                    { new Guid("384750a1-a9d1-43ce-b50f-a4eb59d34018"), new Guid("24f4fa75-27a7-4fb1-ad34-57a30e38c6c7"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1097), false, "Building a Successful Social Marketing Program", 3, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1097) },
                    { new Guid("45565d13-9f49-428b-818d-781325adce83"), new Guid("5c9df98e-a4a3-4881-89db-969373b152d9"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1087), false, "Write Better Sentences", 4, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1087) },
                    { new Guid("4ee5f7f6-9cb1-4f5a-808e-c1c187a8018d"), new Guid("167ef6df-86f0-4d6c-a177-ea4e1e0d0519"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1057), false, "Data and Privacy", 4, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1058) },
                    { new Guid("5a34da1f-2442-4a14-9d5e-36c5d68f9f97"), new Guid("281bc679-9d3d-4276-b8af-dab12a7cbbf3"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1103), false, "Summary", 6, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1104) },
                    { new Guid("5ac7e921-3cc5-4c05-9bd3-6f3366e26128"), new Guid("24f4fa75-27a7-4fb1-ad34-57a30e38c6c7"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1099), false, "Sustaining Your Social Programs", 4, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1100) },
                    { new Guid("62e2318f-31a0-45a9-ab53-d409c580cf37"), new Guid("167ef6df-86f0-4d6c-a177-ea4e1e0d0519"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1055), false, "Artificial Intelligence Fundamentals", 3, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1055) },
                    { new Guid("69360c6e-8b32-4ae7-b380-7da17c4311ec"), new Guid("167ef6df-86f0-4d6c-a177-ea4e1e0d0519"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1053), false, "Data Science Fundamentals", 2, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1053) },
                    { new Guid("6d51a90e-dc43-416a-98f0-b316e136cc5e"), new Guid("281bc679-9d3d-4276-b8af-dab12a7cbbf3"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1041), false, "Color Modes", 3, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1041) },
                    { new Guid("7e3a7d4e-5fb3-4cb8-96ce-a1bf717e11d1"), new Guid("24f4fa75-27a7-4fb1-ad34-57a30e38c6c7"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1091), false, "Security, Privacy & Governance Concerns", 1, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1092) },
                    { new Guid("862751b6-5553-421b-90e1-f525e80793b9"), new Guid("24f4fa75-27a7-4fb1-ad34-57a30e38c6c7"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1101), false, "Why is listening critical to your social programs?", 5, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1102) },
                    { new Guid("8f34a42c-d129-419f-970a-75ca6ffd9de2"), new Guid("5c9df98e-a4a3-4881-89db-969373b152d9"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1080), false, "Perosonal Voice", 1, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1080) },
                    { new Guid("94ce2729-6da3-426a-986b-cf6a42952621"), new Guid("5c9df98e-a4a3-4881-89db-969373b152d9"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1082), false, "Sentence Types", 2, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1082) },
                    { new Guid("9f18c5f8-8cac-4d10-90f7-96b73bb309e3"), new Guid("281bc679-9d3d-4276-b8af-dab12a7cbbf3"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1046), false, "Tips and Tricks", 5, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1046) },
                    { new Guid("a0894f77-3189-437b-84c4-b14f0c847708"), new Guid("281bc679-9d3d-4276-b8af-dab12a7cbbf3"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1039), false, "Basic Color Theory", 2, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1039) },
                    { new Guid("a64aa0a0-58ad-4463-9845-b63e5380b34d"), new Guid("167ef6df-86f0-4d6c-a177-ea4e1e0d0519"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1061), false, "Legal Concepts Related to Data-Driven Technologies", 5, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1061) },
                    { new Guid("b7415797-7844-41e2-8a43-8b4f2da977cb"), new Guid("296cc407-5430-405b-aa5f-876e7f7e7105"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1063), false, "Introduction", 1, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1063) },
                    { new Guid("ba5b6542-7fed-4167-8e9f-2971de4162a9"), new Guid("296cc407-5430-405b-aa5f-876e7f7e7105"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1075), false, "Summary", 4, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1076) },
                    { new Guid("c2312f2a-ee3c-47a8-b1a1-249d38511009"), new Guid("296cc407-5430-405b-aa5f-876e7f7e7105"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1078), false, "Optional Further Readings and Interviews", 5, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1078) },
                    { new Guid("c40742c0-7bd4-42d0-9802-17e58a9f7db1"), new Guid("296cc407-5430-405b-aa5f-876e7f7e7105"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1069), false, "Forcused versus Diffuse Thinking", 2, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1070) },
                    { new Guid("cf46cac0-1da7-4b1d-8ddf-f4a5876e14c0"), new Guid("281bc679-9d3d-4276-b8af-dab12a7cbbf3"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1035), false, "Introduction", 1, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1035) },
                    { new Guid("e69c3ef5-6cb1-411e-a380-333e7b09623a"), new Guid("167ef6df-86f0-4d6c-a177-ea4e1e0d0519"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1050), false, "Overview", 1, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1051) }
                });

            migrationBuilder.InsertData(
                table: "LessonPDF",
                columns: new[] { "PDFID", "CreatedAt", "FilePDF", "IsDeleted", "LessonID", "PDFName", "UpdateAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1123), "Get More from the Georgia Tech Language Institute.pdf", false, new Guid("1a13fdec-8f09-46cf-a810-1446104bed6b"), "Get More from the Georgia Tech Language Institute", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1123) },
                    { 2, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1126), "Reading.pdf", false, new Guid("94ce2729-6da3-426a-986b-cf6a42952621"), "Reading", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1126) },
                    { 3, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1127), "Farewell and Hello.pdf", false, new Guid("37578122-2acd-4922-817c-43956b2f7cdc"), "Farewell and Hello", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1128) },
                    { 4, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1129), "Welcome and Course Information.pdf", false, new Guid("b7415797-7844-41e2-8a43-8b4f2da977cb"), "Welcome and Course Information", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1129) },
                    { 5, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1131), "Written Communication.pdf", false, new Guid("45565d13-9f49-428b-818d-781325adce83"), "Written Communication", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1131) },
                    { 6, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1133), "Get to Know Your Classmates.pdf", false, new Guid("c40742c0-7bd4-42d0-9802-17e58a9f7db1"), "Get to Know Your Classmates", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1133) },
                    { 7, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1134), "Focused versus Diffuse Thinking.pdf", false, new Guid("3479ea5a-7f81-40e0-a740-f32a6d7d765f"), "Focused versus Diffuse Thinking", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1135) },
                    { 8, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1136), "A Posting about Anxiety.pdf", false, new Guid("ba5b6542-7fed-4167-8e9f-2971de4162a9"), "A Posting about Anxiety", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1136) },
                    { 9, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1138), "Chunking.pdf", false, new Guid("c2312f2a-ee3c-47a8-b1a1-249d38511009"), "Chunking", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1138) },
                    { 10, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1140), "Ethical Considerations for Data Science.pdf", false, new Guid("69360c6e-8b32-4ae7-b380-7da17c4311ec"), "Ethical Considerations for Data Science", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1140) },
                    { 11, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1142), "Benefits of Ethical Data Science.pdf", false, new Guid("62e2318f-31a0-45a9-ab53-d409c580cf37"), "Benefits of Ethical Data Science", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1142) },
                    { 12, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1143), "A Day in the Life of an Ethical Data Scientist.pdf", false, new Guid("69360c6e-8b32-4ae7-b380-7da17c4311ec"), "A Day in the Life of an Ethical Data Scientis", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1143) },
                    { 13, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1145), "How to Teach Artificial Intelligence Some Common Sense.pdf", false, new Guid("4ee5f7f6-9cb1-4f5a-808e-c1c187a8018d"), "How to Teach Artificial Intelligence Some Common Sense", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1145) },
                    { 14, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1146), "Ethical Considerations for AI.pdf", false, new Guid("a64aa0a0-58ad-4463-9845-b63e5380b34d"), "Ethical Considerations for AI", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1147) },
                    { 15, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1152), "How AI detectives are cracking open the black box of deep learning.pdf", false, new Guid("4ee5f7f6-9cb1-4f5a-808e-c1c187a8018d"), "How AI detectives are cracking open the black box of deep learning", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1152) },
                    { 16, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1153), "Chunking.pdf", false, new Guid("5a34da1f-2442-4a14-9d5e-36c5d68f9f97"), "Reading Summary", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1154) }
                });

            migrationBuilder.InsertData(
                table: "LessonVideo",
                columns: new[] { "VideoID", "CreatedAt", "FileVideo", "IsDeleted", "LessonID", "UpdateAt", "VideoName" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1171), "1.Introduction.mp4", false, new Guid("cf46cac0-1da7-4b1d-8ddf-f4a5876e14c0"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1172), "Introduction" },
                    { 2, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1174), "2.1.The Color Wheel.mp4", false, new Guid("a0894f77-3189-437b-84c4-b14f0c847708"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1174), "The Color Wheel" },
                    { 3, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1176), "2.2.Warm vs Cool.mp4", false, new Guid("a0894f77-3189-437b-84c4-b14f0c847708"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1176), "Warm vs Cool" },
                    { 4, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1177), "2.3.Color Schemes.mp4", false, new Guid("a0894f77-3189-437b-84c4-b14f0c847708"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1178), "Color Schemes" },
                    { 5, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1179), "2.4.Hue, Sturation and Linghtness.mp4", false, new Guid("a0894f77-3189-437b-84c4-b14f0c847708"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1180), "Hue, Sturation and Linghtness" },
                    { 6, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1181), "2.5.When Colors Collide.mp4", false, new Guid("a0894f77-3189-437b-84c4-b14f0c847708"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1182), "When Colors Collide" },
                    { 7, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1183), "3.1.RGB.mp4", false, new Guid("6d51a90e-dc43-416a-98f0-b316e136cc5e"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1183), "RGB" },
                    { 8, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1185), "3.2.CMYK.mp4", false, new Guid("6d51a90e-dc43-416a-98f0-b316e136cc5e"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1185), "CMYK" },
                    { 9, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1186), "3.3.LAB.mp4", false, new Guid("6d51a90e-dc43-416a-98f0-b316e136cc5e"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1187), "LAB" },
                    { 10, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1188), "4.1.Scene Planning.mp4", false, new Guid("1e540a42-2699-4dc0-9107-116ce5d61760"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1189), "Scene Planning" },
                    { 11, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1190), "4.2.Controlling Color With the Hue_Saturation Adjustment Layer.mp4", false, new Guid("1e540a42-2699-4dc0-9107-116ce5d61760"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1190), "Controlling Color With the Hue/Saturation Adjustment Layer" },
                    { 12, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1192), "4.3.Controlling Color With Blending Modes.mp4", false, new Guid("1e540a42-2699-4dc0-9107-116ce5d61760"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1192), "Controlling Color With Blending Modes" },
                    { 13, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1193), "4.4.Controlling Color With Gradient Maps.mp4", false, new Guid("1e540a42-2699-4dc0-9107-116ce5d61760"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1194), "Controlling Color With Gradient Maps" },
                    { 14, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1195), "5.1.GUI Color Wheel.mp4", false, new Guid("9f18c5f8-8cac-4d10-90f7-96b73bb309e3"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1195), "GUI Color Wheel" },
                    { 15, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1197), "5.2.Adobe Color Themes.mp4", false, new Guid("9f18c5f8-8cac-4d10-90f7-96b73bb309e3"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1197), "Adobe Color Themes" },
                    { 16, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1198), "5.3.Color Look-Up Tables (CLUT).mp4", false, new Guid("9f18c5f8-8cac-4d10-90f7-96b73bb309e3"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1199), "Color Look-Up Tables (CLUT)" },
                    { 17, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1200), "Understanding Data Protection.mp4", false, new Guid("7e3a7d4e-5fb3-4cb8-96ce-a1bf717e11d1"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1200), "Understanding Data Protection" },
                    { 18, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1203), "Security, Privacy, and Governance Pt. 1.mp4", false, new Guid("7e3a7d4e-5fb3-4cb8-96ce-a1bf717e11d1"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1203), "Security, Privacy, and Governance Pt. 1" },
                    { 19, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1204), "Managing Social Programs.mp4", false, new Guid("161b0a99-2b92-4138-8ede-78ba850fe38a"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1205), "Managing Social Programs" },
                    { 20, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1206), "Identifying Social Successes.mp4", false, new Guid("161b0a99-2b92-4138-8ede-78ba850fe38a"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1206), "Identifying Social Successes" },
                    { 21, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1208), "A New Model for Marketers.mp4", false, new Guid("161b0a99-2b92-4138-8ede-78ba850fe38a"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1208), "A New Model for Marketers" },
                    { 22, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1209), "Measuring Engagement.mp4", false, new Guid("161b0a99-2b92-4138-8ede-78ba850fe38a"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1210), "Measuring Engagement" },
                    { 23, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1211), "Finding Relevant Performance Metrics.mp4", false, new Guid("161b0a99-2b92-4138-8ede-78ba850fe38a"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1211), "Finding Relevant Performance Metrics" },
                    { 24, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1212), "Performance Funnels and KPIs.mp4", false, new Guid("161b0a99-2b92-4138-8ede-78ba850fe38a"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1213), "Performance Funnels and KPIs" },
                    { 25, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1214), "Developing Your Budget.mp4", false, new Guid("384750a1-a9d1-43ce-b50f-a4eb59d34018"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1214), "Developing Your Budget" },
                    { 26, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1216), "Justification Metrics.mp4", false, new Guid("384750a1-a9d1-43ce-b50f-a4eb59d34018"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1216), "Justification Metrics" },
                    { 27, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1217), "Calculating Performance Metrics.mp4", false, new Guid("384750a1-a9d1-43ce-b50f-a4eb59d34018"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1218), "Calculating Performance Metrics" },
                    { 28, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1219), "Program Testing.mp4", false, new Guid("5ac7e921-3cc5-4c05-9bd3-6f3366e26128"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1219), "Program Testing" },
                    { 29, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1221), "Program Management.mp4", false, new Guid("5ac7e921-3cc5-4c05-9bd3-6f3366e26128"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1221), "Program Management" },
                    { 30, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1222), "Learn More with Medill IMC.mp4", false, new Guid("5ac7e921-3cc5-4c05-9bd3-6f3366e26128"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1223), "Learn More with Medill IMC" },
                    { 31, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1224), "Moore's Law and the 3 Accelerations that changed business forever.mp4", false, new Guid("862751b6-5553-421b-90e1-f525e80793b9"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1224), "Moore Law and the 3 Accelerations that changed business forever" },
                    { 32, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1226), "Thomas Friedman on the 3 Accelerations [book link in resources].mp4", false, new Guid("862751b6-5553-421b-90e1-f525e80793b9"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1226), "Thomas Friedman on the 3 Accelerations [book link in resources]" },
                    { 33, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1227), "Using Social Data.mp4", false, new Guid("862751b6-5553-421b-90e1-f525e80793b9"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1228), "Using Social Data" },
                    { 34, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1230), "Social Data Flows from a Single Source.mp4", false, new Guid("862751b6-5553-421b-90e1-f525e80793b9"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1230), "Social Data Flows from a Single Source" },
                    { 35, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1232), "CEET Specialization Introduction.mp4", false, new Guid("e69c3ef5-6cb1-411e-a380-333e7b09623a"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1232), "CEET Specialization Introduction" },
                    { 36, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1233), "Promote the Ethical Use of Data-Driven Technologies Course Introduction.mp4", false, new Guid("e69c3ef5-6cb1-411e-a380-333e7b09623a"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1234), "Promote the Ethical Use of Data-Driven Technologies Course Introduction" },
                    { 37, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1235), "Course Welcome & Success Tips.mp4", false, new Guid("e69c3ef5-6cb1-411e-a380-333e7b09623a"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1235), "Course Welcome & Success Tips" },
                    { 38, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1237), "Ethics Make a Difference in Emerging Technologies.mp4", false, new Guid("e69c3ef5-6cb1-411e-a380-333e7b09623a"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1237), "Ethics Make a Difference in Emerging Technologies" },
                    { 39, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1238), "Big Data.mp4", false, new Guid("69360c6e-8b32-4ae7-b380-7da17c4311ec"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1239), "Big Data" },
                    { 40, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1240), "Working with Big Data.mp4", false, new Guid("69360c6e-8b32-4ae7-b380-7da17c4311ec"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1240), "Working with Big Data" },
                    { 41, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1242), "Data Analytics.mp4", false, new Guid("69360c6e-8b32-4ae7-b380-7da17c4311ec"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1242), "Data Analytics" },
                    { 42, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1243), "Data Science Pipeline.mp4", false, new Guid("69360c6e-8b32-4ae7-b380-7da17c4311ec"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1244), "Data Science Pipeline" },
                    { 43, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1245), "Artificial Intelligence.mp4", false, new Guid("62e2318f-31a0-45a9-ab53-d409c580cf37"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1245), "Artificial Intelligence" },
                    { 44, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1246), "Narrow AI.mp4", false, new Guid("62e2318f-31a0-45a9-ab53-d409c580cf37"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1247), "Narrow AI" },
                    { 45, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1248), "General AI and Superintelligence.mp4", false, new Guid("62e2318f-31a0-45a9-ab53-d409c580cf37"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1248), "General AI and Superintelligence" },
                    { 46, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1250), "Ambient Intelligence and IoT.mp4", false, new Guid("62e2318f-31a0-45a9-ab53-d409c580cf37"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1250), "Ambient Intelligence and IoT" },
                    { 47, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1251), "Data Privacy.mp4", false, new Guid("4ee5f7f6-9cb1-4f5a-808e-c1c187a8018d"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1252), "Data Privacy" },
                    { 48, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1253), "PII.mp4", false, new Guid("4ee5f7f6-9cb1-4f5a-808e-c1c187a8018d"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1253), "PII" },
                    { 49, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1254), "Privacy Risks in IoT Ambient Intelligence Technologies.mp4", false, new Guid("4ee5f7f6-9cb1-4f5a-808e-c1c187a8018d"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1255), "Privacy Risks in IoT/Ambient Intelligence Technologies" },
                    { 50, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1256), "Privacy Protection through Individual Authorization.mp4", false, new Guid("4ee5f7f6-9cb1-4f5a-808e-c1c187a8018d"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1257), "Privacy Protection through Individual Authorization" },
                    { 51, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1258), "Legal Terminology Responsibility, Accountability, and Liability.mp4", false, new Guid("a64aa0a0-58ad-4463-9845-b63e5380b34d"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1258), "Legal Terminology: Responsibility, Accountability, and Liability" },
                    { 52, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1259), "Technology Contract Types.mp4", false, new Guid("a64aa0a0-58ad-4463-9845-b63e5380b34d"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1260), "Technology Contract Types" },
                    { 53, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1261), "Smart Contracts.mp4", false, new Guid("a64aa0a0-58ad-4463-9845-b63e5380b34d"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1261), "Smart Contracts" },
                    { 54, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1263), "Introduction to the Focused and Diffuse Modes.mp4", false, new Guid("b7415797-7844-41e2-8a43-8b4f2da977cb"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1263), "Introduction to the Focused and Diffuse Modes" },
                    { 55, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1264), "Terrence Sejnowski and Barbara Oakley--Introduction to the Course Structure.mp4", false, new Guid("c40742c0-7bd4-42d0-9802-17e58a9f7db1"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1265), "Terrence Sejnowski and Barbara Oakley--Introduction to the Course Structure" },
                    { 56, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1266), "Using the Focused and Diffuse Modes--Or, a Little Dali will do You.mp4", false, new Guid("c40742c0-7bd4-42d0-9802-17e58a9f7db1"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1266), "Using the Focused and Diffuse Modes--Or, a Little Dali will do You" },
                    { 57, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1268), "What is Learning.mp4", false, new Guid("c40742c0-7bd4-42d0-9802-17e58a9f7db1"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1268), "What is Learning?" },
                    { 58, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1269), "A Procrastination Preview.mp4", false, new Guid("3479ea5a-7f81-40e0-a740-f32a6d7d765f"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1270), "A Procrastination Preview" },
                    { 59, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1271), "Practice Makes Permanent.mp4", false, new Guid("3479ea5a-7f81-40e0-a740-f32a6d7d765f"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1271), "Practice Makes Permanent" },
                    { 60, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1273), "Introduction to Memory.mp4", false, new Guid("3479ea5a-7f81-40e0-a740-f32a6d7d765f"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1273), "Introduction to Memory" },
                    { 61, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1274), "The Importance of Sleep in Learning.mp4", false, new Guid("3479ea5a-7f81-40e0-a740-f32a6d7d765f"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1275), "The Importance of Sleep in Learning" },
                    { 62, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1276), "Summary video for Module 1.mp4", false, new Guid("ba5b6542-7fed-4167-8e9f-2971de4162a9"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1276), "Summary" },
                    { 63, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1278), "Excitement About Whats Next! MaryAnne Nestor Gives Special Hints.mp4", false, new Guid("ba5b6542-7fed-4167-8e9f-2971de4162a9"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1278), "Excitement About Whats Next! MaryAnne Nestor Gives Special Hints" },
                    { 64, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1279), "Introduction to Renaissance Learning and Unlocking Your Potential.mp4", false, new Guid("c2312f2a-ee3c-47a8-b1a1-249d38511009"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1280), "Introduction to Renaissance Learning and Unlocking Your Potential" },
                    { 65, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1281), "How to Become a Better Learner.mp4", false, new Guid("c2312f2a-ee3c-47a8-b1a1-249d38511009"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1281), "How to Become a Better Learner" },
                    { 66, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1287), "Create a Lively Visual Metaphor or Analogy.mp4", false, new Guid("c2312f2a-ee3c-47a8-b1a1-249d38511009"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1287), "Create a Lively Visual Metaphor or Analogy" },
                    { 67, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1289), "Intro to Course.mp4", false, new Guid("8f34a42c-d129-419f-970a-75ca6ffd9de2"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1289), "Intro to Course" },
                    { 68, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1290), "Personal Voice.mp4", false, new Guid("8f34a42c-d129-419f-970a-75ca6ffd9de2"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1291), "Personal Voice" },
                    { 69, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1292), "Sentence Types Part 1.mp4", false, new Guid("94ce2729-6da3-426a-986b-cf6a42952621"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1292), "Sentence Types Part 1" },
                    { 70, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1293), "Strong Sentences Part 1 Verb Tense & Parallel Structure.mp4", false, new Guid("94ce2729-6da3-426a-986b-cf6a42952621"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1294), "Strong Sentences Part 1: Verb Tense & Parallel Structure" },
                    { 71, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1295), "Strong Sentences Part 2  Subject-Verb Agreement.mp4", false, new Guid("94ce2729-6da3-426a-986b-cf6a42952621"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1295), "Strong Sentences Part 2: Subject-Verb Agreement" },
                    { 72, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1297), "3 Key Parts of a Proposal.mp4", false, new Guid("1a13fdec-8f09-46cf-a810-1446104bed6b"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1297), "3 Key Parts of a Proposal" },
                    { 73, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1299), "How to Connect Ideas & Sentences.mp4", false, new Guid("1a13fdec-8f09-46cf-a810-1446104bed6b"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1299), "How to Connect Ideas & Sentences" },
                    { 74, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1301), "How to Write a Process.mp4", false, new Guid("1a13fdec-8f09-46cf-a810-1446104bed6b"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1301), "How to Write a Process" },
                    { 75, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1302), "No Need for Genius Envy.mp4", false, new Guid("45565d13-9f49-428b-818d-781325adce83"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1303), "No Need for Genius Envy" },
                    { 76, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1304), "Organize, Write, and Design Effective Slides.mp4", false, new Guid("45565d13-9f49-428b-818d-781325adce83"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1304), "Organize, Write, and Design Effective Slides" },
                    { 77, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1305), "How to Use Articles and Count Non-count Nouns.mp4", false, new Guid("37578122-2acd-4922-817c-43956b2f7cdc"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1306), "How to Use Articles and Count/Non-count Nouns" },
                    { 78, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1307), "Change Your Thoughts, Change Your Life.mp4", false, new Guid("37578122-2acd-4922-817c-43956b2f7cdc"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1307), "Change Your Thoughts, Change Your Life" },
                    { 79, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1309), "Working with Big Data.mp4", false, new Guid("5a34da1f-2442-4a14-9d5e-36c5d68f9f97"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1309), "Summary" },
                    { 80, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1310), "Privacy Protection through Individual Authorization.mp4", false, new Guid("5a34da1f-2442-4a14-9d5e-36c5d68f9f97"), new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1311), "Summary2" }
                });

            migrationBuilder.InsertData(
                table: "Quiz",
                columns: new[] { "QuestionID", "Answer1", "Answer2", "Answer3", "Answer4", "AnswerCorrect", "CreatedAt", "IsDeleted", "LessonID", "Question", "UpdateAt" },
                values: new object[,]
                {
                    { new Guid("106a92b5-6cbf-4bca-b7e1-5805d4108e46"), "The contingent nature of academic knowledge.", "The need for evidence to support opinions.", "The use of research to produce knowledge.", "The importance of independent learning.", 1, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1447), false, new Guid("c2312f2a-ee3c-47a8-b1a1-249d38511009"), "\"Knowledge is soon changed, then lost in the mist, an echo half-heard.\" - Gene Wolfe. What quality of academic culture does this relate to?", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1447) },
                    { new Guid("110bedcb-c6ab-4183-9e9c-86f53808cd30"), "Cultural record", "Scholarly record", "Public record", "Police record", 2, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1459), false, new Guid("3479ea5a-7f81-40e0-a740-f32a6d7d765f"), "Which record is an article on positive topological entropy in the journal Annals of Mathematics part of?", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1459) },
                    { new Guid("120248fb-cafd-4182-b27a-ac7cbba2e1dd"), "Primary source", "Secondary source", "Tertiary source", "Quaternary source", 3, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1462), false, new Guid("c40742c0-7bd4-42d0-9802-17e58a9f7db1"), "Is a first year visual art textbook called Introduction to Art History an example of a primary source, secondary source, or tertiary source?", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1463) },
                    { new Guid("182155d0-ae4b-4d84-9db8-606d6187f599"), "Determine your technology costs.", "Create performance funnels and establish Key Performance Indicators (KPIs).", "Develop your staffing costs.", "All correct", 4, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1363), false, new Guid("384750a1-a9d1-43ce-b50f-a4eb59d34018"), "Which of the following steps are involved in developing your budget?", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1364) },
                    { new Guid("1a1ed860-c5d9-4908-b62d-da3ba40dee59"), " solitude, computers, civilians, subjects, state, politics", " surveillance, computers, inhabitants, dwellers, state, politics", "surveillance, computers, civilians, subjects, state, politics", "surveillance, machines, civilians, subjects, state, politics", 3, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1454), false, new Guid("ba5b6542-7fed-4167-8e9f-2971de4162a9"), "Consider the following question. \"You have zero privacy anyway. Get over it\" (Scott McNealey, 1999). What is privacy? How have changes in technology made privacy an issue for citizens and governments? Another important step in developing a search strategy is to develop a set of search terms using synonyms of key words in the question. Choose the best set of synonyms of key terms from the groups below.", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1454) },
                    { new Guid("1c184b87-32b7-4042-84e1-8dfd0d112066"), "Themes/Topics", "Intentions", "Sentiment", "Categories", 3, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1385), false, new Guid("862751b6-5553-421b-90e1-f525e80793b9"), "Which of the following text analytics terms is used to describe how people on social “feel” about a topic?", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1385) },
                    { new Guid("1de01586-5ed5-4cb1-b0d6-4ffabc7ebd6b"), "A efficient manager plans an meeting every month", "An efficient manager plans an meeting every month", " An efficient manager plans a meeting every month.", "An employee in this store works late every night.", 3, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1387), false, new Guid("37578122-2acd-4922-817c-43956b2f7cdc"), "Which sentence uses the article a/an correctly?", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1387) },
                    { new Guid("27038430-f322-4462-a59a-67cf249e469a"), "privacy AND technology AND civil*", "privacy AND technology AND (civilians OR citizens OR subjects)", "privacy OR technology AND civilian", "privacy AND technology NOT government", 2, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1456), false, new Guid("3479ea5a-7f81-40e0-a740-f32a6d7d765f"), "\"You have zero privacy anyway. Get over it\" (Scott McNealey, 1999). What is privacy? How have changes in technology made privacy an issue for citizens and governments? A student wants to research the effects of technology on privacy for citizens. What is the best combination of search terms below?", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1457) },
                    { new Guid("2cf7aa37-1a18-43a2-9a5a-78b22987fc50"), "Social pyramid", "Maximizing market share", "Return on investment", "Maturity modeling", 2, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1371), false, new Guid("5ac7e921-3cc5-4c05-9bd3-6f3366e26128"), "Testing different types of media to determine the ones which reach your target markets in an efficient and effective way is known as ______?", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1372) },
                    { new Guid("326e5dce-dc4b-445c-8c1c-f860d3ce2b8a"), "Abstract", "Introduction", "Methodology", "Conclusion", 3, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1482), false, new Guid("a64aa0a0-58ad-4463-9845-b63e5380b34d"), "Which of these parts of an academic journal article would you probably NOT read in great detail?", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1482) },
                    { new Guid("32a1ee97-d5f5-4e29-ac0b-dc2718c182ec"), "The academic field youre studying.", "How easy it is to access.", "How fine-grained you want your information to be.", "What youre looking for", 4, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1469), false, new Guid("b7415797-7844-41e2-8a43-8b4f2da977cb"), "What is the most important thing that determines where you look for information sources, according to Pat Norman in lesson 2.3?", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1470) },
                    { new Guid("3a889699-1905-407b-a9d1-2187dbd21ec7"), "Yasuo_2013_robot surgery", "Noda et al_2013_robot surgery", "20170511_sociology of technology assignment", "journal.pone.0054116.PDF", 2, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1476), false, new Guid("b7415797-7844-41e2-8a43-8b4f2da977cb"), "You would like to save the PLos One article on robot surgery to your computer. The article citation is: Noda Y, Ida Y, Tanaka S, Toyama T, Roggia MF, et al. (2013) Impact of Robotic Assistance on Precision of Vitreoretinal Surgical Procedures. PLoS ONE 8(1): e54116. doi:10.1371/journal.pone.0054116. According to lesson 3.3a, what would be the best file name to use?", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1476) },
                    { new Guid("3f2395e0-4173-4b77-9f06-cd965b7674dd"), "to behave ethically", "to not cheat", "to not become involved in political scandals", "to conduct qualitative research", 1, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1442), false, new Guid("c2312f2a-ee3c-47a8-b1a1-249d38511009"), "According to the lecture, universities are not just trying to train you for a job role, but are also trying to prepare you ___", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1443) },
                    { new Guid("4284db06-fbf4-475f-9424-a8f495f6d215"), "The MROI is a negative number", "The MROI is 50% or better. ", "The MROI is over 100%", "The MROI is zero, meaning break even.", 3, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1366), false, new Guid("384750a1-a9d1-43ce-b50f-a4eb59d34018"), "What indicates success has been achieved when calculating the marketing return on investment (MROI) of a program?", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1366) },
                    { new Guid("497a4df8-bd75-4126-ab6f-a85a3044623a"), "Did you meet the owner of the car you want to buy?", "Did you meet an owner of a car you want to buy?", "Did you meet the owner of  a car you want to buy?", null, 1, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1390), false, new Guid("37578122-2acd-4922-817c-43956b2f7cdc"), "Which sentence uses the articles a/an or the correctly?", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1390) },
                    { new Guid("4f428e01-9701-497f-8608-8fa12e0f93eb"), "problem,solution,qualifications", "problem,title,solution", "title, solution, qualifications", "solution, problem, qualifications", 1, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1431), false, new Guid("8f34a42c-d129-419f-970a-75ca6ffd9de2"), "Complete the sentences to describe the 3 parts of a proposal. State the _____________________. Provide the _____________________. Highlight your _____________________.", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1432) },
                    { new Guid("54cba684-de5c-428b-96a6-6360cbf9a356"), "snippet", "SocialGist", "document", "API", 1, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1380), false, new Guid("862751b6-5553-421b-90e1-f525e80793b9"), "A paragraph within a blog article would be considered a/an:", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1380) },
                    { new Guid("5601391c-5e88-4a3b-9e6c-919908ebcccc"), "Overall engagement rate", "A measure of how appealing your post is", "A measure of viral reach or how much your content has inspired your social audience to share it", "Number of comments or replies per post", 3, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1358), false, new Guid("161b0a99-2b92-4138-8ede-78ba850fe38a"), "The metric for Amplification measures what?", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1359) },
                    { new Guid("5bca62e4-73d6-443b-aaf9-252c35601758"), "Honesty", "Trust", "Fairness", "Responsibility", 3, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1445), false, new Guid("c2312f2a-ee3c-47a8-b1a1-249d38511009"), "\"Academic communities of integrity value the interactive, cooperative, participatory nature of learning. They honor, value, and consider diverse opinions and ideas. ... In academic environments of integrity, even those who disagree on facts share ... reverence for knowledge and the methods by which it is obtained.\" (International Centre for Academic Integrity , 2014, pg 24).Which of the academic values is this referring to?", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1445) },
                    { new Guid("69b2a6c5-9653-40b7-8002-a550020fe3cd"), "Understanding academic culture in its entirety, without being divided into parts.", "Behaving ethically and responsibly at university.", "Being honest and not cheating.", "Displaying the core values of academic culture - honesty, trust, fairness, respect and responsibility - in class.", 2, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1439), false, new Guid("c2312f2a-ee3c-47a8-b1a1-249d38511009"), "What is academic integrity?", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1439) },
                    { new Guid("6f771561-c2b0-451e-9c41-cc02623723f9"), "Pronouns", "Transition words", "key words and phrases", "logical order", 2, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1411), false, new Guid("1a13fdec-8f09-46cf-a810-1446104bed6b"), "We add ____________________________ to connect sentences.", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1411) },
                    { new Guid("817c3293-28be-4d5a-a576-794cad3cf2c2"), "technology OR curriculum AND change", "technology OR curriculum change", "technology AND \"curriculum change\"", "technology OR curriculum OR change", 3, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1471), false, new Guid("b7415797-7844-41e2-8a43-8b4f2da977cb"), "\"It is important to remember that educational software, like textbooks, is only one tool in the learning process. Neither can be a substitute for well-trained teachers, leadership, and parental involvement\" (Keith Krueger). How important is educational technology? A student wants to research the importance of technology in curriculum change. What would be the best combination of search terms below?", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1472) },
                    { new Guid("864bb08d-055e-4f76-9e29-f5ce825a30e4"), "Promotions", "Direct Response", "Social", "All Correct", 4, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1349), false, new Guid("161b0a99-2b92-4138-8ede-78ba850fe38a"), "Which of the following would be included in a fully integrated marketing plan", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1349) },
                    { new Guid("8a6d9ed1-8482-4228-aa18-953d1a9654cb"), "an application programming interface.", "any writing that is generated by a word processor.", "a portion of an entry containing important analysis information.", "any single entry or posting on any type of social media.", 4, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1377), false, new Guid("862751b6-5553-421b-90e1-f525e80793b9"), "In social Big Data a “document” refers to:", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1378) },
                    { new Guid("9e44a4d5-1152-4f00-9d67-90abfe063256"), "Simple Present", "Simple Past", "Simple Future", "Simple base", 2, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1418), false, new Guid("1a13fdec-8f09-46cf-a810-1446104bed6b"), "Which tense would mainly be used for the following business document. Year-end progress report", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1419) },
                    { new Guid("9eaa6117-1ba9-4bed-a278-ac30ec3cc8dd"), "pronouns", "transition words", "key words and phrases", "lavie", 1, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1406), false, new Guid("45565d13-9f49-428b-818d-781325adce83"), "We use consistent ___________________ to connect sentences.", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1406) },
                    { new Guid("9f598add-42bc-440d-81b7-b445bfa562e9"), "Our cars is always available.", "Our car are always available.", "Our cars are always available.", "Our Table are oke", 3, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1427), false, new Guid("94ce2729-6da3-426a-986b-cf6a42952621"), "Choose the sentence with subject verb agreement.", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1427) },
                    { new Guid("a4eee1dd-6632-44f5-a3d5-398ea891f1a0"), "Performance funnels track the price per click", "Performance funnels track the number of to clicks that result in shares.", "Performance funnels track a company’s success in securing customer data and performance behavior.", "Performance funnels track the consumer journey from total market to product purchase.", 4, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1361), false, new Guid("161b0a99-2b92-4138-8ede-78ba850fe38a"), "What are performance funnels?", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1361) },
                    { new Guid("a8eb92e3-cb72-48ed-a976-cdb90b040531"), "Progressive profiling involves an exchange of value between a company and consumer, such as name and email from the consumer resulting in a free white paper from the company.", "Progressive profiling involves interaction between a company and government and territories, and the varying laws that differ from place to place", "Progressive profiling involves a government or territory monitoring companies on social based on their size and worth", "Progressive profiling involves a company taking user behavior and information, running analytics on it, and selling leads to another company.", 1, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1342), false, new Guid("7e3a7d4e-5fb3-4cb8-96ce-a1bf717e11d1"), "In discussing security, privacy, and governance, Seth Redmore discussed “progressive profiling.” Which of the following statements is an attributes of “progressive profiling”? ", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1343) },
                    { new Guid("b1c2f809-fe58-4a77-84c6-e915cd52d60d"), "advices", "one advice", "some advice", null, 2, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1392), false, new Guid("37578122-2acd-4922-817c-43956b2f7cdc"), "I have a problem and need __________________.", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1393) },
                    { new Guid("b62fd673-50ea-496b-a9b3-773b2681155d"), "Reactors", "Influencers", "Protectors", "Representatives", 2, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1347), false, new Guid("7e3a7d4e-5fb3-4cb8-96ce-a1bf717e11d1"), "Which of the following members of an organization are security professionals who already have an open dialogue with the Board or CEO on data security?", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1347) },
                    { new Guid("b99fe0f8-02f2-4072-bf06-dead33421f58"), "The design was completed by our team on time.", "Our team completed the design on time.", "On time,  the design was completed", "So your readers know you care about them", 2, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1437), false, new Guid("8f34a42c-d129-419f-970a-75ca6ffd9de2"), "Which sentence shows active writing?", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1437) },
                    { new Guid("b9c20958-ac50-4cd2-b02f-1daf91a1c93f"), "BUS1002", "2018_Quarter 2", "Caroline Ng", "Business Communications", 2, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1478), false, new Guid("a64aa0a0-58ad-4463-9845-b63e5380b34d"), "You have a collection of course notes from your course in Business Communications (BUS1002) from the first year of your undergraduate course in Business Management. You took the course in the second quarter of 2018 and your lecturer was Caroline Ng. What would be the best name for the top level folder in your university folder hierarchy for this information", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1479) },
                    { new Guid("ba7e6847-950e-4aaf-9b7e-20fe8cfca80a"), "Customer and employee enjoy eating in the restaurant on a third floor. ", "Customers and employees enjoy eating in the restaurant on the third floor.", "Customer and employee enjoy eating in the restaurant on the third floor.", null, 3, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1399), false, new Guid("45565d13-9f49-428b-818d-781325adce83"), "Which sentence uses articles correctly?", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1399) },
                    { new Guid("bac0311a-c83e-4112-880b-331529c8f8dc"), "They are too general.", "They are difficult to read because of the expert vocabulary used.", " They are shorter than textbooks, and therefore contain less information.", "They are available on subscription only.", 2, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1452), false, new Guid("ba5b6542-7fed-4167-8e9f-2971de4162a9"), "Academic journal articles allow us to enter the discussions that define our academic field. However, what is a DISADVANTAGE of academic journal articles?", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1452) },
                    { new Guid("be300174-6cc3-4a9c-b406-043b0470ec71"), "Focus on your own story, and write informally", "Focus on your readers, write actively and be positive and sincere.", "Focus on your accomplishments, write actively", "Focus on your accomplishments, write actively and be polite.", 2, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1434), false, new Guid("8f34a42c-d129-419f-970a-75ca6ffd9de2"), "How can you develop personal voice when you write in English?", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1434) },
                    { new Guid("c00e8d5b-9223-4871-b9dc-38dcc4b0b417"), "Clarity", "Scholarly purpose", "Audience", "Objectivity", 1, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1474), false, new Guid("b7415797-7844-41e2-8a43-8b4f2da977cb"), "Which of the following is NOT a criterion used to judge the credibility of sources?", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1474) },
                    { new Guid("c29449d9-e0bc-4871-a54e-a21a7f28b518"), "logical order", "key words and phrases", "pronouns", "lahase", 2, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1408), false, new Guid("1a13fdec-8f09-46cf-a810-1446104bed6b"), "We repeat ___________________ to connect sentences.", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1409) },
                    { new Guid("c3a1a204-fdc4-450c-b497-a6d56fff5086"), "We use the simple base form of a verb and a simple subject.", "We use the simple base form of a verb and a simple subject.", "We use the simple base form of the verb after the subject. ", "We use the simple base form of the verb at the beginning of the sentence. We do not write the subject.", 4, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1403), false, new Guid("45565d13-9f49-428b-818d-781325adce83"), "How do we write imperatives?", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1404) },
                    { new Guid("c4291c87-3939-4279-b97e-8cf84d05231e"), "Abstract, Introduction, Methodology, Results, Discussion, Conclusion, References", "Abstract, Introduction, Results, Methodology , Discussion, Conclusion, References", "Abstract, Introduction, Body, Conclusion, References", "Abstract, Introduction, Body, References, Conclusion", 2, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1467), false, new Guid("c40742c0-7bd4-42d0-9802-17e58a9f7db1"), "Which of these options is the usual structure of a humanities journal article?", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1467) },
                    { new Guid("c533f50b-1586-4520-ba82-dbf8869fead9"), "Abstract", "Introduction", "Methodology", "Results", 4, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1465), false, new Guid("c40742c0-7bd4-42d0-9802-17e58a9f7db1"), "In which part of an academic journal article would you encounter the findings or outcomes of the research that was conducted?", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1465) },
                    { new Guid("cdf8659d-abcf-41af-983e-f0faa2c505f5"), "Matching subjects with verb", "Using the same word forms", "Talking about time", "Take care", 2, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1424), false, new Guid("94ce2729-6da3-426a-986b-cf6a42952621"), "What is parallel structure?", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1425) },
                    { new Guid("d060c796-c2ba-4093-8ccf-4c5bb069fe17"), "A lot of the management live in the city", "All of the progress helps us attract new business.", "None of the equipment work.", null, 2, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1401), false, new Guid("45565d13-9f49-428b-818d-781325adce83"), "Which sentence uses expressions of quantity correctly?", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1401) },
                    { new Guid("d3fa4bf9-bbbf-43cd-8850-1f323b594625"), "The Customer Decision Journey", "Loyalty Loop", "AIDA Model, or “The Funnel”", "Customer Engagement Engine", 4, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1355), false, new Guid("161b0a99-2b92-4138-8ede-78ba850fe38a"), "What is a present-day framework that explains the current engagement ecosystem, including insight into how social engagement can play an active role?", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1356) },
                    { new Guid("d645ca11-064d-478f-9014-b20320856d75"), "Measuring return on investment", "Measuring the cost for a click", "Cost per complete view", "Raw number or views or shares.", 3, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1353), false, new Guid("161b0a99-2b92-4138-8ede-78ba850fe38a"), "When using metrics to measure success in social, which of the following would be most appropriate for measuring the success of the content syndication of a video?", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1353) },
                    { new Guid("dccf0342-db46-487e-98bb-49c8c6f12cc6"), "Simple Present", "Simple Past", "Simple Future", "Simple base", 3, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1422), false, new Guid("1a13fdec-8f09-46cf-a810-1446104bed6b"), "Which tense would mainly be used for the following business document. Vision Statement", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1422) },
                    { new Guid("e0abab8c-52d9-42cb-8ef9-1341e7d28ea0"), "Time management", "Collaboration", "Noticing how your field structures knowledge.", "Study skills", 1, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1449), false, new Guid("ba5b6542-7fed-4167-8e9f-2971de4162a9"), "Consider the following situation. In Jeong Woo s first semester at university he often forgot to get his course readings done in time, and had to ask for an extension twice for his assignments as he couldnt complete them. In his second semester he became a lot more aware of when he needed to have completed readings and assignments, which resulted in better grades and greater contributions to tutorials. Which survival skill mentioned in lesson 1.3 does this refer to?", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1450) },
                    { new Guid("e0b6c499-5d76-4b22-b8f0-9355d107985d"), "Reduce the breadth or reach of the program.", "Reduce the scope of the original plan.", "Reduce the timing of the program.", "All correct", 4, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1375), false, new Guid("5ac7e921-3cc5-4c05-9bd3-6f3366e26128"), "Which of the following methods assist is developing a pilot project?", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1375) },
                    { new Guid("f32d0eab-a122-4f93-a5a1-b9947d419077"), "Pilots help identify the risks of the program. ", "It’s a way to convince skeptical senior management to at least try the program to see its benefits.", "A pilot program allows you to gauge action performance and make adjustments to the program before you commit to a plan.", "All correct", 4, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1369), false, new Guid("5ac7e921-3cc5-4c05-9bd3-6f3366e26128"), "What are the benefits of a pilot program for a social marketing strategy?", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1369) },
                    { new Guid("f86e3b0c-b31f-4221-b599-6512e9f75846"), "Testimonials from satisfied customers", "Restatement of the problem and the solution", "Important facts and figures that highlight your qualifications", "Provides the solution to the client", 3, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1429), false, new Guid("8f34a42c-d129-419f-970a-75ca6ffd9de2"), "What should the final section of a proposal include?", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1430) },
                    { new Guid("fab33c05-7652-4598-82a8-ac1d5d898f68"), "Employee in this store works late every night.", "An employees in this store work late every night.", "An employee every night.", null, 1, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1396), false, new Guid("37578122-2acd-4922-817c-43956b2f7cdc"), "Which sentence uses articles correctly?", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1397) },
                    { new Guid("fc71ed8c-8bda-4707-88a2-9d87ccee5224"), "Generating discussion about social media", "To extract social data in real-time from a social site.", "To standardize the connection of learning systems with external service tools.", "Microblogging within social networks.", 2, new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1382), false, new Guid("862751b6-5553-421b-90e1-f525e80793b9"), "APIs (automatic programming interface) are used for what purpose?", new DateTime(2024, 2, 26, 15, 56, 52, 607, DateTimeKind.Local).AddTicks(1383) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Course_CategoryID",
                table: "Course",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Course_CreatorID",
                table: "Course",
                column: "CreatorID");

            migrationBuilder.CreateIndex(
                name: "IX_EnrollCourse_StudentID",
                table: "EnrollCourse",
                column: "StudentID");

            migrationBuilder.CreateIndex(
                name: "IX_Lesson_CourseID",
                table: "Lesson",
                column: "CourseID");

            migrationBuilder.CreateIndex(
                name: "IX_LessonPDF_LessonID",
                table: "LessonPDF",
                column: "LessonID");

            migrationBuilder.CreateIndex(
                name: "IX_LessonVideo_LessonID",
                table: "LessonVideo",
                column: "LessonID");

            migrationBuilder.CreateIndex(
                name: "IX_Quiz_LessonID",
                table: "Quiz",
                column: "LessonID");

            migrationBuilder.CreateIndex(
                name: "IX_Result_StudentID",
                table: "Result",
                column: "StudentID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnrollCourse");

            migrationBuilder.DropTable(
                name: "LessonPDF");

            migrationBuilder.DropTable(
                name: "LessonVideo");

            migrationBuilder.DropTable(
                name: "Quiz");

            migrationBuilder.DropTable(
                name: "Result");

            migrationBuilder.DropTable(
                name: "Lesson");

            migrationBuilder.DropTable(
                name: "Course");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
