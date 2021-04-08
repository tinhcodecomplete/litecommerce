using LiteCommerce.Admin;
using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Remoting;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace LiteCommerce.Admin.Controllers
{
    /// <summary>
    /// Giao diện quản lý Account
    /// </summary>
    [Authorize]
    public class AccountController : Controller
    {
        /// <summary>
        /// return view
        /// </summary>
        /// <returns></returns>
        // GET: Account
        public ActionResult Index()
        {
            WebUserData userData = User.GetUserData();
            Employee data = EmployeeBLL.Employee_GetByEmail(userData.UserID);
            return View(data);
            
        }   
        /// <summary>
        /// Đổi mật khẩu
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public ActionResult ChangePassword()
        {
            return View();
        }
        /// <summary>
        /// Doi mat khau
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult ChangePassword(UserAccount model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.UserID))
                {
                    ModelState.AddModelError("", "email trống!");
                }
                else
                {
                    UserAccount user = UserAccountBLL.UserAccount_GetUser(model.UserID);
                    string password = user.Password;
                    if (string.IsNullOrEmpty(model.Password))
                    {
                        ModelState.AddModelError("", "password trống!");
                    }
                    else
                    {
                        if (Md5HashCode.GetMd5Hash(model.Password) != password)
                        {
                            ModelState.AddModelError("", "mật khẩu sai!");
                        }
                        else
                        {
                            if (model.Password == model.NewPassword)
                            {
                                ModelState.AddModelError("", "mật khẩu mới bạn vừa nhập trùng với mật khẩu cũ!");
                            }
                            else
                            {
                                if (model.NewPassword != model.ReNewPassword)
                                {
                                    ModelState.AddModelError("", "Mật khẩu nhập lại không đúng!");
                                }
                            }
                        }
                    }
                }
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                ViewBag.alert = "";
                bool editPass = UserAccountBLL.UserAccount_ChangePassword(model.UserID, model.NewPassword);
                if (editPass == true)
                {
                    ViewBag.alert = "Update password success";
                }
                else
                {
                    ViewBag.alert = "Update password Failuer";
                }
                return RedirectToAction("Index", "Dashboard");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("err", ex.Message);
                return View(model);
            }
        }
        /// <summary>
        /// Đăng xuất
        /// </summary>
        /// <returns></returns>
        public ActionResult SignOut()
        {

            FormsAuthentication.SignOut();
             //Response.Redirect("Login");
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Login"); //quay lai, khong view
        }
        /// <summary>
        /// Xử lý login
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Login(String email = "", String password = "")
        {
            if (Request.HttpMethod == "GET")
            {
                return View();
            }
            else
            {

                var userAccount = UserAccountBLL.Authorize(email, password, UserAccountTypes.Employee);
                if (userAccount != null)
                {
                    WebUserData cookieData = new Admin.WebUserData()
                    {
                        UserID = userAccount.UserID,
                        FullName = userAccount.FullName,
                        Photo = userAccount.Photo,
                        GroupName = userAccount.GroupName,
                        LoginTime = DateTime.Now,
                        SessionID = Session.SessionID,
                        ClientIP = Request.UserHostAddress
                    };
                    FormsAuthentication.SetAuthCookie(cookieData.ToCookieString(), false);
                   
                    return RedirectToAction("Index", "Dashboard");



                }
                else
                {

                    ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng!");
                    ViewBag.Email = email;
                    return View();
                }




            }
        }
        /// <summary>
        /// kiem tra email co ton tai khong
        /// </summary>
        /// <param name="emailID"></param>
        /// <returns></returns>
        [NonAction]
        public bool IsEmailExist(string emailID)
        {
            UserAccount account = UserAccountBLL.UserAccount_GetUser(emailID);
            if (account != null)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// gui mail reset pass or verify account
        /// </summary>
        /// <param name="emailID"></param>
        /// <param name="activationCode"></param>
        /// <param name="emailFor"></param>
        [NonAction]
        public void SendVerificationLinkEmail(string emailID, string activationCode, string emailFor = "VerifyAccount")
        {
            var verifyUrl = "/Account/ResetPassword?ResetCode=" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("tinhphule@gmail.com", "Forgot Password");
            var toEmail = new MailAddress(emailID);
            var fromEmailPassword = "enteryourmailpasswordhere";
            // link https://www.google.com/settings/security/lesssecureapps

            string subject = "";
            string body = "";
            if (emailFor == "VerifyAccount")
            {
                subject = "Tai khoan cua ban da duoc tao!";

                body = "<br/><br/>We are excited to tell you that your Dotnet Awesome account is" +
                   " successfully created. Please click on the below link to verify your account" +
                   " <br/><br/><a href='" + link + "'>" + link + "</a> ";
            }
            else
            {
                subject = "Forgot your LiteCommerce password";

                body = "<br/><br/>Click vào link bên dưới để đến trang reset password" +
                   " <br/><br/><a href='" + link + "'>" + link + "</a> ";
            }


            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                smtp.Send(message);
        }
        /// <summary>
        /// quen mat kau
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ForgotPwd()
        {
            return View();
        }
        /// <summary>
        /// quen mat khau
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public ActionResult ForgotPwd(UserAccount model)
        {
            if (string.IsNullOrEmpty(model.UserID))
            {
                ModelState.AddModelError("email", "Bạn chưa nhập tài khoản!");
            }
            if (IsEmailExist(model.UserID) == true)
            {
                ModelState.AddModelError("", "Tài khoản này không có hoặc không tồn tại trong hệ thống. Hãy kiểm tra xem " +
                    "bạn đã nhập đúng địa tài khoản chưa?");
            }
            if (!ModelState.IsValid)
            {
                return View();
            }

            string message = "";

            UserAccount account = UserAccountBLL.UserAccount_GetUser(model.UserID);


            if (account.UserID != null)
            {

                string resetCode = Guid.NewGuid().ToString();
                SendVerificationLinkEmail(account.UserID, resetCode, "ResetPassword");
                account.ResetPasswordCode = resetCode;
                UserAccountBLL.UserAccount_AddResetCode(model.UserID, resetCode);
                message = "đã gửi đường dẫn reset Password đến địa chỉ email của bạn. vui lòng kiểm tra hộp thư đến để lấy lại mật khẩu!";
            }
            else
            {
                message = "account not found";
            }

            ViewBag.message = message;
            return View();
        }
        /// <summary>
        /// reset password
        /// </summary>
        /// <param name="ResetCode"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ResetPassword(string ResetCode)
        {

            UserAccount user = UserAccountBLL.UserAccount_GetUserByResetPasswordCode(ResetCode);

            if (user != null)
            {
                ResetPasswordModel model = new ResetPasswordModel();
                model.ResetCode = ResetCode;
                return View(model);

            }
            else
            {
               return  RedirectToAction("NotFound");
            }


        }
        /// <summary>
        /// reset password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            if(model.NewPassword.Length < 8)
            {
                ModelState.AddModelError("", "mật khẩu phái có ít nhất 8 ký tự!");
            }
            else
            {
                if(model.NewPassword != model.ConfirmPassword)
                {
                    ModelState.AddModelError("", "mật khẩu nhập lại không đúng!");
                }
                else
                {
                    var message = "";
                    UserAccount user = UserAccountBLL.UserAccount_GetUserByResetPasswordCode(model.ResetCode);

                    if (user != null)
                    {
                        UserAccountBLL.UserAccount_ChangePassword(user.UserID, model.NewPassword);
                        UserAccountBLL.UserAccount_AddResetCode(user.UserID, "");
                        return RedirectToAction("Index");

                    }
                    else
                    {
                        message = "fail";
                    }
                    ViewBag.message = message;
                }
            }
            
           
            return View(model);
        }
        /// <summary>
        /// thay doi thong tin user
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ChangeInfo(Employee data)
        {
           
            if (data != null)
            {
               
                Employee getEmployee = EmployeeBLL.Employee_Get(data.EmployeeID);
                
                data.Password = getEmployee.Password;
                data.Notes = getEmployee.Notes;
                data.GroupNames = getEmployee.GroupNames;
                data.Email = getEmployee.Email;
                bool editUser = EmployeeBLL.Employee_Update(data);
            }
            return RedirectToAction("Index");
        }
        /// <summary>
        /// upload anh
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public string ProcessUpload(HttpPostedFileBase file)
        {
            file.SaveAs(Server.MapPath("~/Images/" + file.FileName));
            return "/Images/" + file.FileName;
        }

    }
}