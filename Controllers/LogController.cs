using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tailorShop.Models;
using Microsoft.EntityFrameworkCore;

namespace tailorShop.Controllers
{
    [Route("pritam/[controller]")]
    [ApiController]
    public class LogController : Controller
    {
        private readonly tailoreShopContext DbContext;

        public LogController(tailoreShopContext context)
        {
            DbContext = context;
        }

        //pritam/log/auth
        [HttpGet("auth")]
        public ActionResult<userData> Log(string userName, string password)
        {
            if((userName == null)||(password== null))
            {
                return NotFound();
            }

            var userdetails = (from u in DbContext.Users where u.Username == userName select new { u.Username, u.Password }).FirstOrDefault();

            if (userdetails == null)
            {
                return NotFound();
            }
            else
            {
                if (userdetails.Username != userName || userdetails.Password != password)
                {
                    return NotFound();
                }
            }

            //if user is deleted
            bool? isDeleted = DbContext.Users.Where(w => w.Username == userName).Select(s => s.User.IsDelete).FirstOrDefault();
            if (isDeleted == true)
            {
                return NotFound();
            }

            var logInId = DbContext.Users.Where(w => w.Username == userName && w.Password == password).Select(s => s.LogInid).FirstOrDefault();

            //if logInId is right
            if (logInId < 1)
            {
                return NotFound();
            }
            else
            {
                var resutrnable = (from u in DbContext.Users
                                   join ud in DbContext.UserDetails on u.UserId equals ud.UserId
                                   where u.LogInid == logInId
                                   join s in DbContext.Shops on ud.ShopId equals s.ShopId
                                   join t in DbContext.UserType on u.UserTypeId equals t.UserTypeId
                                   select new userData
                                   {
                                       UserId = ud.UserId,
                                       Name = ud.Name,
                                       EmailAddress = ud.EmailAddress,
                                       MobileNo = ud.MobileNo,
                                       IsOnWhatsApp = ud.IsOnWhatsApp,
                                       Birthdate = ud.Birthdate,
                                       Address = ud.Address,
                                       ShopName = s.ShopName,
                                       BranchName = s.BranchName,
                                       IsTestData = ud.IsTestData,
                                       UserType1 = t.UserType1

                                   }).FirstOrDefault();

                return resutrnable;

            }
        }

        //pritam/log/check
        [HttpGet("check")]
        public ActionResult<bool> CheckUsernameAvailable(string userName)

        {
            //if input is empty
            if (userName == null)
            {
                return false;
            }

            Users user = DbContext.Users.Where(w => w.Username == userName).FirstOrDefault();

            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //pritam/log/insertUserDetails
        [HttpPost("insertUserDetails")]
        public async Task<ActionResult<int>> PostUserDetails([FromBody] Register userDetails)
        {
            var newUser = new UserDetails()
            {
                Name = userDetails.Name,
                EmailAddress = userDetails.EmailAddress,
                MobileNo = userDetails.MobileNo,
                IsOnWhatsApp = userDetails.IsOnWhatsApp,
                Birthdate = userDetails.Birthdate.AddDays(1),
                Address = userDetails.Address,
                ShopId = userDetails.ShopId,
                CreatedOn = userDetails.CreatedOn.Value.AddDays(1),
                CreatedBy = userDetails.CreatedBy,
                UpdatedOn = userDetails.UpdatedOn,
                UpdatedBy = userDetails.UpdatedBy,
                IsTestData = userDetails.IsTestData,
                IsDelete = userDetails.IsDelete
            };

            DbContext.UserDetails.Add(newUser);
            await DbContext.SaveChangesAsync();

            var user = new UsersInsertable()
            {
                UserId = newUser.UserId,
                Username = userDetails.Username,
                Password = userDetails.Password,
                UserTypeId = userDetails.UserTypeId,
                QuestionId = userDetails.QuestionId,
                Answer = userDetails.Answer
            };


            return await PostUser(user);
        }

        //pritam/log/insertUser
        [HttpPost("insertUser")]
        public async Task<ActionResult<int>> PostUser([FromBody] UsersInsertable users)
        {
            var newUser = new Users()
            {
                UserId = users.UserId,
                Username = users.Username,
                Password = users.Password,
                UserTypeId = users.UserTypeId,
                QuestionId = users.QuestionId,
                Answer = users.Answer
            };

            DbContext.Users.Add(newUser);
            await DbContext.SaveChangesAsync();

            return newUser.LogInid;
        }
    }
}