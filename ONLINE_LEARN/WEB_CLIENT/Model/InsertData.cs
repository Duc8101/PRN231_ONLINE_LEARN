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
            List<Lesson> listLesson = await InsertLesson(listCourse);
            await InsertLessonPDF(listLesson);
            await InsertLessonVideo(listLesson);
        }
        private static async Task InsertLessonVideo(List<Lesson> listLesson)
        {
            if (listLesson.Count > 0)
            {
                if (await context.LessonVideos.AnyAsync() == false)
                {
                    List<LessonVideo> listVideo = new List<LessonVideo>()
                    {
                        /*1*/ new LessonVideo() {VideoName = "Introduction", FileVideo = "1.Introduction.mp4", LessonId = listLesson[0].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*2*/ new LessonVideo() {VideoName = "The Color Wheel", FileVideo = "2.1.The Color Wheel.mp4", LessonId = listLesson[1].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*3*/ new LessonVideo() {VideoName = "Warm vs Cool", FileVideo = "2.2.Warm vs Cool.mp4", LessonId = listLesson[1].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*4*/ new LessonVideo() {VideoName = "Color Schemes", FileVideo = "2.3.Color Schemes.mp4", LessonId = listLesson[1].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*5*/ new LessonVideo() {VideoName = "Hue, Sturation and Linghtness", FileVideo = "2.4.Hue, Sturation and Linghtness.mp4", LessonId = listLesson[1].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*6*/ new LessonVideo() {VideoName = "When Colors Collide", FileVideo = "2.5.When Colors Collide.mp4", LessonId = listLesson[1].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*7*/ new LessonVideo() {VideoName = "RGB", FileVideo = "3.1.RGB.mp4", LessonId = listLesson[2].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*8*/ new LessonVideo() {VideoName = "CMYK", FileVideo = "3.2.CMYK.mp4", LessonId = listLesson[2].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*9*/ new LessonVideo() {VideoName = "LAB", FileVideo = "3.3.LAB.mp4", LessonId = listLesson[2].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*10*/ new LessonVideo() {VideoName = "Scene Planning", FileVideo = "4.1.Scene Planning.mp4", LessonId = listLesson[3].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*11*/ new LessonVideo() {VideoName = "Controlling Color With the Hue/Saturation Adjustment Layer", FileVideo = "4.2.Controlling Color With the Hue_Saturation Adjustment Layer.mp4", LessonId = listLesson[3].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*12*/ new LessonVideo() {VideoName = "Controlling Color With Blending Modes", FileVideo = "4.3.Controlling Color With Blending Modes.mp4", LessonId = listLesson[3].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*13*/ new LessonVideo() {VideoName = "Controlling Color With Gradient Maps", FileVideo = "4.4.Controlling Color With Gradient Maps.mp4", LessonId = listLesson[3].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*14*/ new LessonVideo() {VideoName = "GUI Color Wheel", FileVideo = "5.1.GUI Color Wheel.mp4", LessonId = listLesson[4].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*15*/ new LessonVideo() {VideoName = "Adobe Color Themes", FileVideo = "5.2.Adobe Color Themes.mp4", LessonId = listLesson[4].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*16*/ new LessonVideo() {VideoName = "Color Look-Up Tables (CLUT)", FileVideo = "5.3.Color Look-Up Tables (CLUT).mp4", LessonId = listLesson[4].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*17*/ new LessonVideo() {VideoName = "Understanding Data Protection", FileVideo = "Understanding Data Protection.mp4", LessonId = listLesson[20].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*18*/ new LessonVideo() {VideoName = "Security, Privacy, and Governance Pt. 1", FileVideo = "Security, Privacy, and Governance Pt. 1.mp4", LessonId = listLesson[20].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*19*/ new LessonVideo() {VideoName = "Managing Social Programs", FileVideo = "Managing Social Programs.mp4", LessonId = listLesson[21].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*20*/ new LessonVideo() {VideoName = "Identifying Social Successes", FileVideo = "Identifying Social Successes.mp4", LessonId = listLesson[21].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*21*/ new LessonVideo() {VideoName = "A New Model for Marketers", FileVideo = "A New Model for Marketers.mp4", LessonId = listLesson[21].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*22*/ new LessonVideo() {VideoName = "Measuring Engagement", FileVideo = "Measuring Engagement.mp4", LessonId = listLesson[21].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*23*/ new LessonVideo() {VideoName = "Finding Relevant Performance Metrics", FileVideo = "Finding Relevant Performance Metrics.mp4", LessonId = listLesson[21].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*24*/ new LessonVideo() {VideoName = "Performance Funnels and KPIs", FileVideo = "Performance Funnels and KPIs.mp4", LessonId = listLesson[21].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*25*/ new LessonVideo() {VideoName = "Developing Your Budget", FileVideo = "Developing Your Budget.mp4", LessonId = listLesson[22].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*26*/ new LessonVideo() {VideoName = "Justification Metrics", FileVideo = "Justification Metrics.mp4", LessonId = listLesson[22].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*27*/ new LessonVideo() {VideoName = "Calculating Performance Metrics", FileVideo = "Calculating Performance Metrics.mp4", LessonId = listLesson[22].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*28*/ new LessonVideo() {VideoName = "Program Testing", FileVideo = "Program Testing.mp4", LessonId = listLesson[23].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*29*/ new LessonVideo() {VideoName = "Program Management", FileVideo = "Program Management.mp4", LessonId = listLesson[23].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*30*/ new LessonVideo() {VideoName = "Learn More with Medill IMC", FileVideo = "Learn More with Medill IMC.mp4", LessonId = listLesson[23].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*31*/ new LessonVideo() {VideoName = "Moore Law and the 3 Accelerations that changed business forever", FileVideo = "Moore's Law and the 3 Accelerations that changed business forever.mp4", LessonId = listLesson[24].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*32*/ new LessonVideo() {VideoName = "Thomas Friedman on the 3 Accelerations [book link in resources]", FileVideo = "Thomas Friedman on the 3 Accelerations [book link in resources].mp4", LessonId = listLesson[24].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*33*/ new LessonVideo() {VideoName = "Using Social Data", FileVideo = "Using Social Data.mp4", LessonId = listLesson[24].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*34*/ new LessonVideo() {VideoName = "Social Data Flows from a Single Source", FileVideo = "Social Data Flows from a Single Source.mp4", LessonId = listLesson[24].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*35*/ new LessonVideo() {VideoName = "CEET Specialization Introduction", FileVideo = "CEET Specialization Introduction.mp4", LessonId = listLesson[5].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*36*/ new LessonVideo() {VideoName = "Promote the Ethical Use of Data-Driven Technologies Course Introduction", FileVideo = "Promote the Ethical Use of Data-Driven Technologies Course Introduction.mp4", LessonId = listLesson[5].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*37*/ new LessonVideo() {VideoName = "Course Welcome & Success Tips", FileVideo = "Course Welcome & Success Tips.mp4", LessonId = listLesson[5].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*38*/ new LessonVideo() {VideoName = "Ethics Make a Difference in Emerging Technologies", FileVideo = "Ethics Make a Difference in Emerging Technologies.mp4", LessonId = listLesson[5].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*39*/ new LessonVideo() {VideoName = "Big Data", FileVideo = "Big Data.mp4", LessonId = listLesson[6].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*40*/ new LessonVideo() {VideoName = "Working with Big Data", FileVideo = "Working with Big Data.mp4", LessonId = listLesson[6].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*41*/ new LessonVideo() {VideoName = "Data Analytics", FileVideo = "Data Analytics.mp4", LessonId = listLesson[6].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*42*/ new LessonVideo() {VideoName = "Data Science Pipeline", FileVideo = "Data Science Pipeline.mp4", LessonId = listLesson[6].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*43*/ new LessonVideo() {VideoName = "Artificial Intelligence", FileVideo = "Artificial Intelligence.mp4", LessonId = listLesson[7].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*44*/ new LessonVideo() {VideoName = "Narrow AI", FileVideo = "Narrow AI.mp4", LessonId = listLesson[7].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*45*/ new LessonVideo() {VideoName = "General AI and Superintelligence", FileVideo = "General AI and Superintelligence.mp4", LessonId = listLesson[7].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*46*/ new LessonVideo() {VideoName = "Ambient Intelligence and IoT", FileVideo = "Ambient Intelligence and IoT.mp4", LessonId = listLesson[7].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*47*/ new LessonVideo() {VideoName = "Data Privacy", FileVideo = "Data Privacy.mp4", LessonId = listLesson[8].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*48*/ new LessonVideo() {VideoName = "PII", FileVideo = "PII.mp4", LessonId = listLesson[8].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*49*/ new LessonVideo() {VideoName = "Privacy Risks in IoT/Ambient Intelligence Technologies", FileVideo = "Privacy Risks in IoT Ambient Intelligence Technologies.mp4", LessonId = listLesson[8].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*50*/ new LessonVideo() {VideoName = "Privacy Protection through Individual Authorization", FileVideo = "Privacy Protection through Individual Authorization.mp4", LessonId = listLesson[8].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*51*/ new LessonVideo() {VideoName = "Legal Terminology: Responsibility, Accountability, and Liability", FileVideo = "Legal Terminology Responsibility, Accountability, and Liability.mp4", LessonId = listLesson[9].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*52*/ new LessonVideo() {VideoName = "Technology Contract Types", FileVideo = "Technology Contract Types.mp4", LessonId = listLesson[9].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*53*/ new LessonVideo() {VideoName = "Smart Contracts", FileVideo = "Smart Contracts.mp4", LessonId = listLesson[9].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*54*/ new LessonVideo() {VideoName = "Introduction to the Focused and Diffuse Modes", FileVideo = "Introduction to the Focused and Diffuse Modes.mp4", LessonId = listLesson[10].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*55*/ new LessonVideo() {VideoName = "Terrence Sejnowski and Barbara Oakley--Introduction to the Course Structure", FileVideo = "Terrence Sejnowski and Barbara Oakley--Introduction to the Course Structure.mp4", LessonId = listLesson[11].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*56*/ new LessonVideo() {VideoName = "Using the Focused and Diffuse Modes--Or, a Little Dali will do You", FileVideo = "Using the Focused and Diffuse Modes--Or, a Little Dali will do You.mp4", LessonId = listLesson[11].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*57*/ new LessonVideo() {VideoName = "What is Learning?", FileVideo = "What is Learning.mp4", LessonId = listLesson[11].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*58*/ new LessonVideo() {VideoName = "A Procrastination Preview", FileVideo = "A Procrastination Preview.mp4", LessonId = listLesson[12].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*59*/ new LessonVideo() {VideoName = "Practice Makes Permanent", FileVideo = "Practice Makes Permanent.mp4", LessonId = listLesson[12].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*60*/ new LessonVideo() {VideoName = "Introduction to Memory", FileVideo = "Introduction to Memory.mp4", LessonId = listLesson[12].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*61*/ new LessonVideo() {VideoName = "The Importance of Sleep in Learning", FileVideo = "The Importance of Sleep in Learning.mp4", LessonId = listLesson[12].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*62*/ new LessonVideo() {VideoName = "Summary", FileVideo = "Summary video for Module 1.mp4", LessonId = listLesson[13].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*63*/ new LessonVideo() {VideoName = "Excitement About Whats Next! MaryAnne Nestor Gives Special Hints", FileVideo = "Excitement About Whats Next! MaryAnne Nestor Gives Special Hints.mp4", LessonId = listLesson[13].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*64*/ new LessonVideo() {VideoName = "Introduction to Renaissance Learning and Unlocking Your Potential", FileVideo = "Introduction to Renaissance Learning and Unlocking Your Potential.mp4", LessonId = listLesson[14].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*65*/ new LessonVideo() {VideoName = "How to Become a Better Learner", FileVideo = "How to Become a Better Learner.mp4", LessonId = listLesson[14].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*66*/ new LessonVideo() {VideoName = "Create a Lively Visual Metaphor or Analogy", FileVideo = "Create a Lively Visual Metaphor or Analogy.mp4", LessonId = listLesson[14].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*67*/ new LessonVideo() {VideoName = "Intro to Course", FileVideo = "Intro to Course.mp4", LessonId = listLesson[15].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*68*/ new LessonVideo() {VideoName = "Personal Voice", FileVideo = "Personal Voice.mp4", LessonId = listLesson[15].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*69*/ new LessonVideo() {VideoName = "Sentence Types Part 1", FileVideo = "Sentence Types Part 1.mp4", LessonId = listLesson[16].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*70*/ new LessonVideo() {VideoName = "Strong Sentences Part 1: Verb Tense & Parallel Structure", FileVideo = "Strong Sentences Part 1 Verb Tense & Parallel Structure.mp4", LessonId = listLesson[16].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*71*/ new LessonVideo() {VideoName = "Strong Sentences Part 2: Subject-Verb Agreement", FileVideo = "Strong Sentences Part 2  Subject-Verb Agreement.mp4", LessonId = listLesson[16].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*72*/ new LessonVideo() {VideoName = "3 Key Parts of a Proposal", FileVideo = "3 Key Parts of a Proposal.mp4", LessonId = listLesson[17].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*73*/ new LessonVideo() {VideoName = "How to Connect Ideas & Sentences", FileVideo = "How to Connect Ideas & Sentences.mp4", LessonId = listLesson[17].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*74*/ new LessonVideo() {VideoName = "How to Write a Process", FileVideo = "How to Write a Process.mp4", LessonId = listLesson[17].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*75*/ new LessonVideo() {VideoName = "No Need for Genius Envy", FileVideo = "No Need for Genius Envy.mp4", LessonId = listLesson[18].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*76*/ new LessonVideo() {VideoName = "Organize, Write, and Design Effective Slides", FileVideo = "Organize, Write, and Design Effective Slides.mp4", LessonId = listLesson[18].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*77*/ new LessonVideo() {VideoName = "How to Use Articles and Count/Non-count Nouns", FileVideo = "How to Use Articles and Count Non-count Nouns.mp4", LessonId = listLesson[19].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*78*/ new LessonVideo() {VideoName = "Change Your Thoughts, Change Your Life", FileVideo = "Change Your Thoughts, Change Your Life.mp4", LessonId = listLesson[19].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*79*/ new LessonVideo() {VideoName = "Summary", FileVideo = "Working with Big Data.mp4", LessonId = listLesson[25].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false },
                        /*80*/ new LessonVideo() {VideoName = "Sumary2", FileVideo = "Privacy Protection through Individual Authorization.mp4", LessonId = listLesson[25].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false }
                    };
                    await context.LessonVideos.AddRangeAsync(listVideo);
                    await context.SaveChangesAsync();
                }
            }
        }
        private static async Task InsertLessonPDF(List<Lesson> listLesson)
        {
            if(listLesson.Count > 0)
            {
                if(await context.LessonPdfs.AnyAsync() == false)
                {
                    List<LessonPdf> listPDF = new List<LessonPdf>()
                    {
                        /*1*/new LessonPdf() {Pdfname = "Get More from the Georgia Tech Language Institute", FilePdf = "Get More from the Georgia Tech Language Institute.pdf", LessonId = listLesson[17].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                        /*2*/new LessonPdf() {Pdfname = "Reading", FilePdf = "Reading.pdf", LessonId = listLesson[16].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                        /*3*/new LessonPdf() {Pdfname = "Farewell and Hello", FilePdf = "Farewell and Hello.pdf", LessonId = listLesson[19].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                        /*4*/new LessonPdf() {Pdfname = "Welcome and Course Information", FilePdf = "Welcome and Course Information.pdf", LessonId = listLesson[10].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                        /*5*/new LessonPdf() {Pdfname = "Written Communication", FilePdf = "Written Communication.pdf", LessonId = listLesson[18].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                        /*6*/new LessonPdf() {Pdfname = "Get to Know Your Classmates", FilePdf = "Get to Know Your Classmates.pdf", LessonId = listLesson[11].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                        /*7*/new LessonPdf() {Pdfname = "Focused versus Diffuse Thinking", FilePdf = "Focused versus Diffuse Thinking.pdf", LessonId = listLesson[12].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                        /*8*/new LessonPdf() {Pdfname = "A Posting about Anxiety", FilePdf = "A Posting about Anxiety.pdf", LessonId = listLesson[13].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                        /*9*/new LessonPdf() {Pdfname = "Chunking", FilePdf = "Chunking.pdf", LessonId = listLesson[14].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                        /*10*/new LessonPdf() {Pdfname = "Ethical Considerations for Data Science", FilePdf = "Ethical Considerations for Data Science.pdf", LessonId = listLesson[6].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                        /*11*/new LessonPdf() {Pdfname = "Benefits of Ethical Data Science", FilePdf = "Benefits of Ethical Data Science.pdf", LessonId = listLesson[7].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                        /*12*/new LessonPdf() {Pdfname = "A Day in the Life of an Ethical Data Scientis", FilePdf = "A Day in the Life of an Ethical Data Scientist.pdf", LessonId = listLesson[6].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                        /*13*/new LessonPdf() {Pdfname = "How to Teach Artificial Intelligence Some Common Sense", FilePdf = "How to Teach Artificial Intelligence Some Common Sense.pdf", LessonId = listLesson[8].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                        /*14*/new LessonPdf() {Pdfname = "Ethical Considerations for AI", FilePdf = "Ethical Considerations for AI.pdf", LessonId = listLesson[9].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                        /*15*/new LessonPdf() {Pdfname = "How AI detectives are cracking open the black box of deep learning", FilePdf = "How AI detectives are cracking open the black box of deep learning.pdf", LessonId = listLesson[8].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                        /*16*/new LessonPdf() {Pdfname = "Reading Summary", FilePdf = "Chunking.pdf", LessonId = listLesson[25].LessonId, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                    };
                    await context.LessonPdfs.AddRangeAsync(listPDF);
                    await context.SaveChangesAsync();
                }
            }
        }
        private static async Task<List<Lesson>> InsertLesson(List<Course> listCourse)
        {
            List<Lesson> listLesson = new List<Lesson>();
            if(listCourse.Count > 0)
            {
                if(await context.Lessons.AnyAsync() == false)
                {
                    listLesson = new List<Lesson>()
                    {
                        /*1*/ new Lesson() {LessonId = Guid.NewGuid() , LessonName = "Introduction" , CourseId = listCourse[0].CourseId, LessonNo = 1, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                        /*2*/ new Lesson() {LessonId = Guid.NewGuid() , LessonName = "Basic Color Theory" , CourseId = listCourse[0].CourseId, LessonNo = 2, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                        /*3*/ new Lesson() {LessonId = Guid.NewGuid() , LessonName = "Color Modes" , CourseId = listCourse[0].CourseId, LessonNo = 3, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                        /*4*/ new Lesson() {LessonId = Guid.NewGuid() , LessonName = "Working With Color" , CourseId = listCourse[0].CourseId, LessonNo = 4, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                        /*5*/ new Lesson() {LessonId = Guid.NewGuid() , LessonName = "Tips and Tricks" , CourseId = listCourse[0].CourseId, LessonNo = 5, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                        /*6*/ new Lesson() {LessonId = Guid.NewGuid() , LessonName = "Overview" , CourseId = listCourse[1].CourseId, LessonNo = 1, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                        /*7*/ new Lesson() {LessonId = Guid.NewGuid() , LessonName = "Data Science Fundamentals" , CourseId = listCourse[1].CourseId, LessonNo = 2, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                        /*8*/ new Lesson() {LessonId = Guid.NewGuid() , LessonName = "Artificial Intelligence Fundamentals" , CourseId = listCourse[1].CourseId, LessonNo = 3, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                        /*9*/ new Lesson() {LessonId = Guid.NewGuid() , LessonName = "Data and Privacy" , CourseId = listCourse[1].CourseId, LessonNo = 4, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                        /*10*/ new Lesson() {LessonId = Guid.NewGuid() , LessonName = "Legal Concepts Related to Data-Driven Technologies" , CourseId = listCourse[1].CourseId, LessonNo = 5, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                        /*11*/ new Lesson() {LessonId = Guid.NewGuid() , LessonName = "Introduction" , CourseId = listCourse[2].CourseId, LessonNo = 1, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                        /*12*/ new Lesson() {LessonId = Guid.NewGuid() , LessonName = "Forcused versus Diffuse Thinking" , CourseId = listCourse[2].CourseId, LessonNo = 2, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                        /*13*/ new Lesson() {LessonId = Guid.NewGuid() , LessonName = "Procrastination, Memory, and Sleep" , CourseId = listCourse[2].CourseId, LessonNo = 3, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                        /*14*/ new Lesson() {LessonId = Guid.NewGuid() , LessonName = "Summary" , CourseId = listCourse[2].CourseId, LessonNo = 4, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                        /*15*/ new Lesson() {LessonId = Guid.NewGuid() , LessonName = "Optional Further Readings and Interviews" , CourseId = listCourse[2].CourseId, LessonNo = 5, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                        /*16*/ new Lesson() {LessonId = Guid.NewGuid() , LessonName = "Perosonal Voice" , CourseId = listCourse[3].CourseId, LessonNo = 1, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                        /*17*/ new Lesson() {LessonId = Guid.NewGuid() , LessonName = "Sentence Types" , CourseId = listCourse[3].CourseId, LessonNo = 2, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                        /*18*/ new Lesson() {LessonId = Guid.NewGuid() , LessonName = "3 Key Parts of a Proposal" , CourseId = listCourse[3].CourseId, LessonNo = 3, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                        /*19*/ new Lesson() {LessonId = Guid.NewGuid() , LessonName = "Write Better Sentences" , CourseId = listCourse[3].CourseId, LessonNo = 4, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                        /*20*/ new Lesson() {LessonId = Guid.NewGuid() , LessonName = "Connect Ideas & Sentences" , CourseId = listCourse[3].CourseId, LessonNo = 5, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                        /*21*/ new Lesson() {LessonId = Guid.NewGuid() , LessonName = "Security, Privacy & Governance Concerns" , CourseId = listCourse[4].CourseId, LessonNo = 1, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                        /*22*/ new Lesson() {LessonId = Guid.NewGuid() , LessonName = "Most Important Metrics to Observe" , CourseId = listCourse[4].CourseId, LessonNo = 2, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                        /*23*/ new Lesson() {LessonId = Guid.NewGuid() , LessonName = "Building a Successful Social Marketing Program" , CourseId = listCourse[4].CourseId, LessonNo = 3, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                        /*24*/ new Lesson() {LessonId = Guid.NewGuid() , LessonName = "Sustaining Your Social Programs" , CourseId = listCourse[4].CourseId, LessonNo = 4, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                        /*25*/ new Lesson() {LessonId = Guid.NewGuid() , LessonName = "Why is listening critical to your social programs?" , CourseId = listCourse[4].CourseId, LessonNo = 5, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                        /*26*/ new Lesson() {LessonId = Guid.NewGuid() , LessonName = "Summary" , CourseId = listCourse[0].CourseId, LessonNo = 6, CreatedAt = DateTime.Now, UpdateAt = DateTime.Now, IsDeleted = false},
                    };
                    await context.Lessons.AddRangeAsync(listLesson);
                    await context.SaveChangesAsync();
                }      
            }
            return listLesson;
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
