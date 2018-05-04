using BrainPunch.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BrainPunch.Controllers
{
    public class HomeController : Controller
    {
        DatabaseContext db = new DatabaseContext();

        public ActionResult Index()
        {
            var members = db.Members.ToList();
            return View();
        }


        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register([Bind(Include = "MemberID,FirstName,LastName,UserName,Password,ConfirmPassword")] Member member)
        {
            if (ModelState.IsValid)
            {
                //member.Password += Encrypt(member.Password);
                db.Members.Add(member);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Account()
        {
            if (Session["Login"] != null)
            {
                var member = (Member)Session["Login"];
                ViewBag.Schedules = db.GameSchedules;
                return View(member);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Login([Bind(Include = "UserName, Password")] Member member)
        {
            if (member != null)
            {
                var match = db.Members.Where(r => r.UserName == member.UserName).FirstOrDefault();
                if (match != null)
                {
                    if (match.Password == member.Password)
                    {
                        Session["Login"] = match;
                        return RedirectToAction("Account");
                    }
                    else
                    {
                        //Invalid Password
                        return RedirectToAction("Index");

                    }
                }
                else
                {
                    //Ivalid UserName
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public  string Encrypt(string content)
        {
            using (SHA512 hashManager = new SHA512Managed())
            {
                byte[] hash = hashManager.ComputeHash(Encoding.UTF8.GetBytes(content));
                return Convert.ToBase64String(hash);
            }
        }
    }
}