using System.ComponentModel;

namespace Common.Enums
{
    public enum UserInfo
    {
        [Description("Male")]
        Gender_Male,
        [Description("Female")]
        Gender_Female,
        [Description("Other")]
        Gender_Other,
        Phone_Length = 10,
        Max_Address_Length = 100,
        [Description("^[0-9]")]
        Format_Phone,
        [Description("^[a-zA-Z][a-zA-Z0-9]")]
        Format_Username,
        [Description(@"^[a-zA-Z][\w-]+@([\w]+.[\w]+|[\w]+.[\w]{2,}.[\w]{2,})")]
        Format_Email,
        Min_Length_Username = 6,
        Max_Length_Username = 50,
        [Description("https://i.pinimg.com/564x/31/ec/2c/31ec2ce212492e600b8de27f38846ed7.jpg")]
        Avatar
    }
}
