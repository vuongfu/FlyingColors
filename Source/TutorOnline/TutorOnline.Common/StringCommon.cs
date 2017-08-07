using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TutorOnline.Common
{
    public static class UserCommonString
    {
        public const string Parent = "Phụ huynh";
        public const string Student = "Học sinh";
        public const string Tutor = "Gia sư";
        public const string PreTutor = "Ứng viên gia sư";
        public const string SysAdmin = "Quản lý hệ thống";
        public const string Supporter = "Hỗ trợ";
        public const string Accountant = "Kế toán";
        public const string Manager = "Quản lý";
        public const string EmailNotExist = "Email bạn nhập không tồn tại. Hãy kiểm tra và thử lại.";
        public const string EmailFrom = "tutoronlinefptu@gmail.com";
        public const string EmailPass = "tutoronlinefptu12345";
        public const string EmailSubject = "Tutor Online - Đặt lại password";
        public const string EmailBaseBodyPass = "Mật khẩu mới của bạn là: {0}. \n Xin hãy đăng nhập và đổi mật khẩu.";
        public const string SendEmailSuccess = "Mật khẩu đã được đặt lại. Xin hãy kiểm tra email của bạn.";
        public const string RequiredMess = "Xin hãy điền thông tin vào đây.";
    }

    public static class StatusString
    {
        public const string ScheduleFinished = "Đã hoàn thành";
        public const string ScheduleBooked = "Đã được đặt";
        public const string SchudulueCanceled = "Đã bị hủy";
        public const string SchudulueAvailabled = "Khả dụng";
    }
}
