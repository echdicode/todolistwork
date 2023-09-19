using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todolistwork.Core.Unit
{
    public static class ContentMail
    {
        public static string ResetPassword = "Mật khẩu mới của bạn là {0}. Hãy truy cập vào đường link {1} để xác nhận.";
        public static string Registration = "Mời bạn truy cập vào đường link {0} để xác nhận đăng kí.";
        public static string UrlToken = "https://localhost:7242/api/auth/authtoken/{0}?token={1}";
    }
}
