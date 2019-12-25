using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tailorShop.Models;

namespace tailorShop.Controllers
{
    [Route("pritam/[controller]")]
    [ApiController]
    public class MasterDataController : Controller
    {
        private readonly tailoreShopContext _context;

        public MasterDataController(tailoreShopContext context)
        {
            _context = context;
        }

        // GET: pritam/masterdata/security
        [HttpGet("security")]
        public async Task<ActionResult<IEnumerable<Security>>> GetSecurity()
        {
            return await _context.Security.ToListAsync();
        }

		// GET: pritam/masterdata/shop
		[HttpGet("shop")]
		public async Task<ActionResult<IEnumerable<Shops>>> GetShop()
		{
			return await _context.Shops.ToListAsync();
		}

        // GET: pritam/masterdata/laxumiPooja
        [HttpGet("laxumiPooja")]
        public ActionResult<DateTime> GetLaxumiPooja(decimal year)
        {
            return _context.LaxumiPooja.Where(w => w.Year == year).Select(s => s.Date).FirstOrDefault();
        }

        // GET: pritam/masterdata/checkUserName
        [HttpGet("checkUserName")]
        public ActionResult<bool> CheckUserName(string userName)
        {
            var user = _context.Users.Where(w => w.Username == userName).FirstOrDefault();

            if (user != null)
            {
                if (user.Username == userName)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // GET: pritam/masterdata/getQuestion
        [HttpGet("getQuestion")]
        public ActionResult<Security> GetQuestion(string userName)
        {
            var questionId = _context.Users.Where(w => w.Username == userName).Select(s => s.QuestionId).FirstOrDefault();
            if (questionId > 0)
            {
                return _context.Security.Where(w => w.QuestionId == questionId).FirstOrDefault();
            }
            else
            {
                return NotFound();
            }
        }

        // GET: pritam/masterdata/checkAnswer
        [HttpGet("checkAnswer")]
        public ActionResult<bool> CheckAnswer(string userName, string answer)
        {
            var ans = _context.Users.Where(w => w.Username == userName).Select(s => s.Answer).FirstOrDefault();

            if (ans != null)
            {
                if(ans.ToString() == answer)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        // GET: pritam/masterdata/clothCame
        [HttpGet("clothCame")]
        public ActionResult<ClothDeatils> GetClothCame(DateTime lastYear, DateTime thisYear, bool isTestData)
        {
            var shirtCount = (from ub in _context.UpperBody
                              join w in _context.Work on ub.WorkId equals w.WorkId
                              join ud in _context.UserDetails on w.UserId equals ud.UserId
                              where w.CreatedOn >= lastYear && w.CreatedOn < thisYear && ub.ClothName == "Shirt" && ud.IsDelete== false && ud.IsTestData== isTestData
                              select new
                              {
                                  ub.WorkId

                              }).Count();

            var safariShirtCount = (from ub in _context.UpperBody
                                  join w in _context.Work on ub.WorkId equals w.WorkId
                                    join ud in _context.UserDetails on w.UserId equals ud.UserId
                                    where w.CreatedOn >= lastYear && w.CreatedOn < thisYear && ub.ClothName == "Safari Shirt" && ud.IsDelete == false && ud.IsTestData == isTestData
                                    select new
                                  {
                                      ub.WorkId

                                  }).Count();

            var pantCount = (from ub in _context.LowerBody
                              join w in _context.Work on ub.WorkId equals w.WorkId
                             join ud in _context.UserDetails on w.UserId equals ud.UserId
                             where w.CreatedOn >= lastYear && w.CreatedOn < thisYear && ub.ClothName == "Pant" && ud.IsDelete == false && ud.IsTestData == isTestData
                             select new
                              {
                                  ub.WorkId

                              }).Count();

            var safariPantCount = (from ub in _context.LowerBody
                                    join w in _context.Work on ub.WorkId equals w.WorkId
                                   join ud in _context.UserDetails on w.UserId equals ud.UserId
                                   where w.CreatedOn >= lastYear && w.CreatedOn < thisYear && ub.ClothName == "Safari Pant" && ud.IsDelete == false && ud.IsTestData == isTestData
                                   select new
                                    {
                                        ub.WorkId

                                    }).Count();

            var lowerOther= (from ub in _context.LowerBody
                             join w in _context.Work on ub.WorkId equals w.WorkId
                             join ud in _context.UserDetails on w.UserId equals ud.UserId
                             where w.CreatedOn >= lastYear && w.CreatedOn < thisYear && ub.ClothName != "Safari Pant" && ub.ClothName != "Pant" && ud.IsDelete == false && ud.IsTestData == isTestData
                             select new
                             {
                                 ub.WorkId

                             }).Count();

            var upperOther = (from ub in _context.UpperBody
                              join w in _context.Work on ub.WorkId equals w.WorkId
                              join ud in _context.UserDetails on w.UserId equals ud.UserId
                              where w.CreatedOn >= lastYear && w.CreatedOn < thisYear && ub.ClothName != "Shirt" && ub.ClothName != "Safari Shirt" && ud.IsDelete == false && ud.IsTestData == isTestData
                              select new
                              {
                                  ub.WorkId

                              }).Count();

            var all = new ClothDeatils {
                Shirt= shirtCount,
                Pant= pantCount,
                SafariShirt= safariShirtCount,
                SafariPant= safariPantCount,
                Other= upperOther+ lowerOther
            };
            

            return all;
        }

        // GET: pritam/masterdata/clothOut
        [HttpGet("clothOut")]
        public ActionResult<ClothDeatils> GetClothOut(DateTime lastYear, DateTime thisYear, bool isTestData)
        {
            var shirtCount = (from ub in _context.UpperBody
                              join w in _context.Work on ub.WorkId equals w.WorkId
                              join ud in _context.UserDetails on w.UserId equals ud.UserId
                              where w.CreatedOn >= lastYear && w.CreatedOn < thisYear && ub.Status == 6 && ub.ClothName == "shirt" && ud.IsDelete == false && ud.IsTestData == isTestData
                              select new
                              {
                                  ub.WorkId

                              }).Count();

            var safariShirtCount = (from ub in _context.UpperBody
                                    join w in _context.Work on ub.WorkId equals w.WorkId
                                    join ud in _context.UserDetails on w.UserId equals ud.UserId
                                    where w.CreatedOn >= lastYear && w.CreatedOn < thisYear && ub.Status == 6 && ub.ClothName == "Safari Shirt" && ud.IsDelete == false && ud.IsTestData == isTestData
                                    select new
                                    {
                                        ub.WorkId

                                    }).Count();

            var pantCount = (from ub in _context.LowerBody
                             join w in _context.Work on ub.WorkId equals w.WorkId
                             join ud in _context.UserDetails on w.UserId equals ud.UserId
                             where w.CreatedOn >= lastYear && w.CreatedOn < thisYear && ub.Status == 6 && ub.ClothName == "Pant" && ud.IsDelete == false && ud.IsTestData == isTestData
                             select new
                             {
                                 ub.WorkId

                             }).Count();

            var safariPantCount = (from ub in _context.LowerBody
                                   join w in _context.Work on ub.WorkId equals w.WorkId
                                   join ud in _context.UserDetails on w.UserId equals ud.UserId
                                   where w.CreatedOn >= lastYear && w.CreatedOn < thisYear && ub.Status == 6 && ub.ClothName == "Safari Pant" && ud.IsDelete == false && ud.IsTestData == isTestData
                                   select new
                                   {
                                       ub.WorkId

                                   }).Count();

            var lowerOther = (from ub in _context.LowerBody
                              join w in _context.Work on ub.WorkId equals w.WorkId
                              join ud in _context.UserDetails on w.UserId equals ud.UserId
                              where w.CreatedOn >= lastYear && w.CreatedOn < thisYear && ub.Status == 6 && ub.ClothName != "Safari Pant" && ub.ClothName != "Pant" && ud.IsDelete == false && ud.IsTestData == isTestData
                              select new
                              {
                                  ub.WorkId

                              }).Count();

            var upperOther = (from ub in _context.UpperBody
                              join w in _context.Work on ub.WorkId equals w.WorkId
                              join ud in _context.UserDetails on w.UserId equals ud.UserId
                              where w.CreatedOn >= lastYear && w.CreatedOn < thisYear && ub.Status == 6 && ub.ClothName != "Shirt" && ub.ClothName != "Safari Shirt" && ud.IsDelete == false && ud.IsTestData == isTestData
                              select new
                              {
                                  ub.WorkId

                              }).Count();

            var all = new ClothDeatils
            {
                Shirt = shirtCount,
                Pant = pantCount,
                SafariShirt = safariShirtCount,
                SafariPant = safariPantCount,
                Other = upperOther + lowerOther
            };


            return all;
        }

        // GET: pritam/masterdata/userType
        [HttpGet("userType")]
        public ActionResult<IEnumerable<UserType>> GetUserTypes()
        {
            return (from u in _context.UserType
                    where u.UserTypeId != 0
                    select new UserType
                    {
                        UserTypeId = u.UserTypeId,
                        UserType1= u.UserType1

                    }).Distinct().ToList();
        }

        // GET: pritam/masterdata/allUser
        [HttpGet("allUser")]
        public ActionResult<IEnumerable<AllUsers>> GetAllUser(bool isTestUser, string shopName)
        {
            return (from ud in _context.UserDetails
                    join s in _context.Shops on ud.ShopId equals s.ShopId
                    where ud.UserId != 1 && ud.IsTestData == isTestUser && ud.IsDelete == false && s.ShopName == shopName
                    select new AllUsers
                    {
                        UserId = ud.UserId,
                        Name = ud.Name,
                        MobileNo = ud.MobileNo,

                    }).Distinct().ToList();
        }

        // GET: pritam/masterdata/allWorks
        [HttpGet("allWorks")]
        public ActionResult<IEnumerable<AllWorks>> GetWork(bool isTestUser, string shopName)
        {
            return (from w in _context.Work
                    join ud in _context.UserDetails on w.UserId equals ud.UserId
                    join s in _context.Shops on ud.ShopId equals s.ShopId
                    where ud.IsTestData == isTestUser && ud.IsDelete == false && s.ShopName == shopName
                    select new AllWorks
                    {
                        WorkId = w.WorkId,
                        Name = ud.Name,
                        MobileNo = ud.MobileNo,

                    }).Distinct().ToList();
        }

        // GET: pritam/masterdata/allEmployees
        [HttpGet("allEmployees")]
        public ActionResult<IEnumerable<AllUsers>> GetAllEmployees(bool isTestUser, string shopName)
        {
            return (from u in _context.Users
                    join ud in _context.UserDetails on u.UserId equals ud.UserId
                    join s in _context.Shops on ud.ShopId equals s.ShopId
                    where u.UserTypeId != 0 && ud.IsTestData == isTestUser && ud.IsDelete == false && s.ShopName== shopName && u.UserTypeId== 3
                    select new AllUsers
                    {
                        UserId = ud.UserId,
                        Name = ud.Name,
                        MobileNo = ud.MobileNo,

                    }).Distinct().ToList();
        }

        // GET: pritam/masterdata/perticularUser
        [HttpGet("perticularUser")]
        public ActionResult<SendUserDetails> GetPerticularUser(int id)
        {
            if(id< 1)
            {
                return NotFound();
            }
            else
            {
                return (from u in _context.UserDetails
                        where u.UserId == id
                        select new SendUserDetails
                        {
                            UserId= u.UserId,
                            Name= u.Name,
                            EmailAddress= u.EmailAddress,
                            MobileNo= u.MobileNo,
                            IsOnWhatsApp= u.IsOnWhatsApp,
                            Birthdate= u.Birthdate,
                            Address= u.Address,
                            ShopId= u.ShopId

                        }).FirstOrDefault();
            }
        }

        // GET: pritam/masterdata/fastCard
        [HttpGet("fastCard")]
        public ActionResult<IEnumerable<RestData>> GetFastCard(bool isTestUser, bool day)
        {
            DateTime days = DateTime.Now;

            if(day)
            {
                var all = (from w in _context.Work
                           join ud in _context.UserDetails on w.UserId equals ud.UserId
                           where ud.IsTestData == isTestUser && ud.IsDelete == false && w.CreatedOn == days
                           join s in _context.Shops on ud.ShopId equals s.ShopId
                           select new RestData
                           {
                               workId = w.WorkId,
                               name = ud.Name,
                               shop = s.ShopName,
                               branch = s.BranchName,
                               deliveryDate = w.DeliveryDate

                           }).ToList();

                return all;
            }
            else
            {
                days= days.AddDays(-1);

                var all = (from w in _context.Work
                           join ud in _context.UserDetails on w.UserId equals ud.UserId
                           where ud.IsTestData == isTestUser && ud.IsDelete == false && w.CreatedOn == days
                           join s in _context.Shops on ud.ShopId equals s.ShopId
                           select new RestData
                           {
                               workId = w.WorkId,
                               name = ud.Name,
                               shop = s.ShopName,
                               branch = s.BranchName,
                               deliveryDate = w.DeliveryDate

                           }).ToList();

                return all;
            }
            
        }

        // GET: pritam/masterdata/upper
        [HttpGet("upper")]
        public ActionResult<IEnumerable<Cloths>> GetTodayUpperCloths(bool isTestUser, bool day)
        {
            DateTime days = DateTime.Now;
            
            if(day)
            {
                var all = (from w in _context.Work
                           join ub in _context.UpperBody on w.WorkId equals ub.WorkId
                           join ud in _context.UserDetails on w.UserId equals ud.UserId
                           where ud.IsTestData == isTestUser && ud.IsDelete == false && w.CreatedOn == days
                           select new Cloths
                           {
                               workId = w.WorkId,
                               cloth = ub.ClothName,

                           }).ToList();
                return all;
            }
            else
            {
                days= days.AddDays(-1);

                var all = (from w in _context.Work
                           join ub in _context.UpperBody on w.WorkId equals ub.WorkId
                           join ud in _context.UserDetails on w.UserId equals ud.UserId
                           where ud.IsTestData == isTestUser && ud.IsDelete == false && w.CreatedOn == days
                           select new Cloths
                           {
                               workId = w.WorkId,
                               cloth = ub.ClothName,

                           }).ToList();

                return all;
            }
            
        }

        // GET: pritam/masterdata/lower
        [HttpGet("lower")]
        public ActionResult<IEnumerable<Cloths>> GetTodayLowerCloths(bool isTestUser, bool day)
        {
            DateTime days = DateTime.Now;

            if(day)
            {
                var all = (from w in _context.Work
                           join lb in _context.LowerBody on w.WorkId equals lb.WorkId
                           join ub in _context.UserDetails on w.UserId equals ub.UserId
                           where ub.IsTestData == isTestUser && ub.IsDelete == false && w.CreatedOn == days
                           select new Cloths
                           {
                               workId = w.WorkId,
                               cloth = lb.ClothName,

                           }).ToList();

                return all;
            }
            else
            {
                days= days.AddDays(-1);

                var all = (from w in _context.Work
                           join lb in _context.LowerBody on w.WorkId equals lb.WorkId
                           join ub in _context.UserDetails on w.UserId equals ub.UserId
                           where ub.IsTestData == isTestUser && ub.IsDelete == false && w.CreatedOn == days
                           select new Cloths
                           {
                               workId = w.WorkId,
                               cloth = lb.ClothName,

                           }).ToList();

                return all;
            }
            
        }

        // GET: pritam/masterdata/customer
        [HttpGet("customer")]
        public ActionResult<Customer> GetCustome(bool isTestUser, string searchByName, int searchByUserId, bool searchType)
        {
            if (searchType)
            {
                var all = (from ud in _context.UserDetails
                           join s in _context.Shops on ud.ShopId equals s.ShopId
                           where ud.UserId != 1 && ud.IsTestData == isTestUser && ud.IsDelete == false && ud.Name == searchByName
                           select new 
                           {
                               UserId= ud.UserId,
                               Name= ud.Name,
                               EmailAddress= ud.EmailAddress,
                               MobileNo= ud.MobileNo,
                               IsOnWhatsApp= ud.IsOnWhatsApp,
                               Birthdate= ud.Birthdate,
                               Address= ud.Address,
                               CreatedOn= ud.CreatedOn,
                               CreatedBy= ud.CreatedBy,
                               UpdatedOn= ud.UpdatedOn,
                               UpdatedBy= ud.UpdatedBy,
                               ShopName = s.ShopName,
                               BranchName= s.BranchName

                           }).FirstOrDefault();

                if(all!= null)
                {
                    var newAll = new Customer
                    {
                        UserId = all.UserId,
                        Name = all.Name,
                        EmailAddress = all.EmailAddress,
                        MobileNo = all.MobileNo,
                        IsOnWhatsApp = all.IsOnWhatsApp,
                        Birthdate = all.Birthdate,
                        Address = all.Address,
                        CreatedOn = all.CreatedOn,
                        CreatedBy = GetName(all.CreatedBy),
                        UpdatedOn = all.UpdatedOn,
                        UpdatedBy = GetName(all.UpdatedBy),
                        ShopName = all.ShopName,
                        BranchName = all.BranchName
                    };

                    return newAll;
                }
                else
                {
                    return NotFound();
                }

                
            }
            else
            {
                var all= (from ud in _context.UserDetails
                         join s in _context.Shops on ud.ShopId equals s.ShopId
                          where ud.UserId != 1 && ud.IsTestData == isTestUser && ud.IsDelete == false && ud.UserId == searchByUserId
                          select new 
                         {
                             UserId = ud.UserId,
                             Name = ud.Name,
                             EmailAddress = ud.EmailAddress,
                             MobileNo = ud.MobileNo,
                             IsOnWhatsApp = ud.IsOnWhatsApp,
                             Birthdate = ud.Birthdate,
                             Address = ud.Address,
                             CreatedOn = ud.CreatedOn,
                             CreatedBy = ud.CreatedBy,
                             UpdatedOn = ud.UpdatedOn,
                             UpdatedBy = ud.UpdatedBy,
                             ShopName = s.ShopName,
                             BranchName = s.BranchName

                         }).FirstOrDefault();


                if (all != null)
                {
                    var newAll = new Customer
                    {
                        UserId = all.UserId,
                        Name = all.Name,
                        EmailAddress = all.EmailAddress,
                        MobileNo = all.MobileNo,
                        IsOnWhatsApp = all.IsOnWhatsApp,
                        Birthdate = all.Birthdate,
                        Address = all.Address,
                        CreatedOn = all.CreatedOn,
                        CreatedBy = GetName(all.CreatedBy),
                        UpdatedOn = all.UpdatedOn,
                        UpdatedBy = GetName(all.UpdatedBy),
                        ShopName = all.ShopName,
                        BranchName = all.BranchName
                    };

                    return newAll;
                }
                else
                {
                    return NotFound();
                }
            }
        }

        // GET: pritam/masterdata/works
        [HttpGet("works")]
        public ActionResult<IEnumerable<WorkReturn>> GetWorks(bool isTestUser, int userId)
        {
            DateTime today = DateTime.Now;

            DateTime startDate = _context.LaxumiPooja.Where(w => w.Year == (today.Year - 1)).Select(s => s.Date).FirstOrDefault();
            DateTime endDate = _context.LaxumiPooja.Where(w => w.Year == (today.Year)).Select(s => s.Date).FirstOrDefault();

            var all = (from ud in _context.UserDetails
                       join w in _context.Work on ud.UserId equals w.UserId
                       where ud.IsTestData == isTestUser && ud.IsDelete == false && w.DeliveryDate>= startDate && w.DeliveryDate< endDate && w.UserId== userId
                       select new
                       {
                           UserId = w.UserId,
                           WorkId = w.WorkId,
                           CreatedOn = w.CreatedOn,
                           CreatedBy = w.CreatedBy,
                           UpdatedOn = w.UpdatedOn,
                           UpdatedBy = w.UpdatedBy,
                           DeliveredOn = w.DeliveredOn,
                           DeliveryDate = w.DeliveryDate,
                           Advance = w.Advance

                       }).ToList();

            if (all.Count!= 0)
            {
                List<WorkReturn> newAll = new List<WorkReturn>();

                all.ForEach(obj =>
                {
                    var b = new WorkReturn
                    {
                        UserId = GetName(obj.UserId),
                        WorkId = obj.WorkId,
                        CreatedOn = obj.CreatedOn,
                        CreatedBy = GetName(obj.CreatedBy),
                        UpdatedOn = obj.UpdatedOn,
                        UpdatedBy = GetName(obj.UpdatedBy),
                        DeliveredOn = obj.DeliveredOn,
                        DeliveryDate = obj.DeliveryDate,
                        Advance = obj.Advance
                    };

                    newAll.Add(b);
                });

                return newAll;
            }
            else
            {
                return NotFound();
            }
        }

        // GET: pritam/masterdata/work
        [HttpGet("work")]
        public ActionResult<WorkReturn> GetWork(bool isTestUser, int workId)
        {
            var all = (from ud in _context.UserDetails
                        join w in _context.Work on ud.UserId equals w.UserId 
                        where ud.UserId!= 1 && ud.IsTestData == isTestUser && ud.IsDelete == false && w.WorkId == workId
                        select new
                        {
                            UserId = w.UserId,
                            WorkId= w.WorkId,
                            CreatedOn= w.CreatedOn,
                            CreatedBy= w.CreatedBy,
                            UpdatedOn= w.UpdatedOn,
                            UpdatedBy= w.UpdatedBy,
                            DeliveredOn= w.DeliveredOn,
                            DeliveryDate= w.DeliveryDate,
                            Advance= w.Advance

                        }).FirstOrDefault();

            if (all != null)
            {
                var newAll = new WorkReturn
                {
                    UserId = GetName(all.UserId),
                    WorkId = all.WorkId,
                    CreatedOn = all.CreatedOn,
                    CreatedBy = GetName(all.CreatedBy),
                    UpdatedOn = all.UpdatedOn,
                    UpdatedBy = GetName(all.UpdatedBy),
                    DeliveredOn = all.DeliveredOn,
                    DeliveryDate = all.DeliveryDate,
                    Advance = all.Advance
                };

                return newAll;
            }
            else
            {
                return NotFound();
            }
        }

        // GET: pritam/masterdata/upperComplete
        [HttpGet("upperComplete")]
        public ActionResult<IEnumerable<UpperComplete>> GetupperComplete(bool isTestUser, int workId)
        {
            

            var all = (from ub in _context.UpperBody
                        join w in _context.Work on ub.WorkId equals w.WorkId
                        join ud in _context.UserDetails on w.UserId equals ud.UserId
                        where ud.IsTestData == isTestUser && ud.IsDelete == false && ub.WorkId == workId
                        select new 
                        {
                            WorkId= ub.WorkId,
                            ClothId= ub.ClothId,
                            ClothName= ub.ClothName,
                            Height= ub.Height,
                            Front= ub.Front,
                            Collar= ub.Collar,
                            Shoulder= ub.Shoulder,
                            Sleeve= ub.Sleeve,
                            SleeveWidth= ub.SleeveWidth,
                            Cuff= ub.Cuff,
                            Note= ub.Note,
                            Status= UIFrendlyStatus(ub.Status),
                            Price = ub.Price,
                            AssignTo= ub.AssignTo,
                            PaidTo= ub.PaidTo,
                            FeedbackId= ub.FeedbackId

                        }).ToList();

            if (all.Count!= 0)
            {
                List<UpperComplete> newAll = new List<UpperComplete>();

                all.ForEach(obj =>
                {
                    var feedback = _context.Feedback.Where(w => w.FeedbackId == obj.FeedbackId).FirstOrDefault();

                    if(feedback!= null)
                    {
                        var b = new UpperComplete
                        {
                            WorkId = obj.WorkId,
                            ClothId = obj.ClothId,
                            ClothName = obj.ClothName,
                            Height = obj.Height,
                            Front = obj.Front,
                            Collar = obj.Collar,
                            Shoulder = obj.Shoulder,
                            Sleeve = obj.Sleeve,
                            SleeveWidth = obj.SleeveWidth,
                            Cuff = obj.Cuff,
                            Note = obj.Note,
                            Status = obj.Status,
                            Price = obj.Price,
                            AssignTo = GetName(obj.AssignTo),
                            PaidTo = obj.PaidTo,
                            Fit = feedback.Fit,
                            Style = feedback.Style,
                            Quality = feedback.Quality,
                            Feedback1 = feedback.Feedback1,
                            FeedbackId = feedback.FeedbackId
                        };

                        newAll.Add(b);
                    }
                    else
                    {
                        var b = new UpperComplete
                        {
                            WorkId = obj.WorkId,
                            ClothId = obj.ClothId,
                            ClothName = obj.ClothName,
                            Height = obj.Height,
                            Front = obj.Front,
                            Collar = obj.Collar,
                            Shoulder = obj.Shoulder,
                            Sleeve = obj.Sleeve,
                            SleeveWidth = obj.SleeveWidth,
                            Cuff = obj.Cuff,
                            Note = obj.Note,
                            Status = obj.Status,
                            Price = obj.Price,
                            AssignTo = GetName(obj.AssignTo),
                            PaidTo = obj.PaidTo,
                            Fit = 0,
                            Style = 0,
                            Quality = 0,
                            Feedback1 = "",
                            FeedbackId = 0
                        };

                        newAll.Add(b);
                    }
                    

                    
                });

                return newAll;
            }
            else
            {
                return NotFound();
            }
  
        }

        // GET: pritam/masterdata/lowerComplete
        [HttpGet("lowerComplete")]
        public ActionResult<IEnumerable<LowerComplete>> GetlowerComplete(bool isTestUser, int workId)
        {
            var all = (from ub in _context.LowerBody
                       join w in _context.Work on ub.WorkId equals w.WorkId
                       join ud in _context.UserDetails on w.UserId equals ud.UserId
                       where ud.IsTestData == isTestUser && ud.IsDelete == false && ub.WorkId == workId
                       select new 
                       {
                           WorkId = ub.WorkId,
                           ClothId = ub.ClothId,
                           ClothName = ub.ClothName,
                           Height = ub.Height,
                           Knee = ub.Knee,
                           Bottom = ub.Bottom,
                           Waist = ub.Waist,
                           Seat = ub.Seat,
                           Thigh = ub.Thigh,
                           Underline = ub.Underline,
                           Note = ub.Note,
                           Status = UIFrendlyStatus(ub.Status),
                           Price = ub.Price,
                           AssignTo = ub.AssignTo,
                           PaidTo = ub.PaidTo,
                           FeedbackId= ub.FeedbackId

                       }).ToList();

            if (all.Count != 0)
            {
                List<LowerComplete> newAll = new List<LowerComplete>();

                all.ForEach(obj =>
                {
                    var feedback = _context.Feedback.Where(w => w.FeedbackId == obj.FeedbackId).FirstOrDefault();

                    if (feedback != null)
                    {
                        var b = new LowerComplete
                        {
                            WorkId = obj.WorkId,
                            ClothId = obj.ClothId,
                            ClothName = obj.ClothName,
                            Height = obj.Height,
                            Knee = obj.Knee,
                            Bottom = obj.Bottom,
                            Waist = obj.Waist,
                            Seat = obj.Seat,
                            Thigh = obj.Thigh,
                            Underline = obj.Underline,
                            Note = obj.Note,
                            Status = obj.Status,
                            Price = obj.Price,
                            AssignTo = GetName(obj.AssignTo),
                            PaidTo = obj.PaidTo,
                            Fit = feedback.Fit,
                            Style = feedback.Style,
                            Quality = feedback.Quality,
                            Feedback1 = feedback.Feedback1,
                            FeedbackId = feedback.FeedbackId
                        };

                        newAll.Add(b);
                    }
                    else
                    {
                        var b = new LowerComplete
                        {
                            WorkId = obj.WorkId,
                            ClothId = obj.ClothId,
                            ClothName = obj.ClothName,
                            Height = obj.Height,
                            Knee = obj.Knee,
                            Bottom = obj.Bottom,
                            Waist = obj.Waist,
                            Seat = obj.Seat,
                            Thigh = obj.Thigh,
                            Underline = obj.Underline,
                            Note = obj.Note,
                            Status = obj.Status,
                            Price = obj.Price,
                            AssignTo = GetName(obj.AssignTo),
                            PaidTo = obj.PaidTo,
                            Fit = 0,
                            Style = 0,
                            Quality = 0,
                            Feedback1 = "",
                            FeedbackId = 0
                        };

                        newAll.Add(b);

                    }
                });

                return newAll;
            }
            else
            {
                return NotFound();
            }

        }

        // GET: pritam/masterdata/name
        [HttpGet("name")]
        public string GetName(int? userId)
        {
            if(userId== null)
            {
                return "";
            }
            else
            {
                return _context.UserDetails.Where(w => w.UserId == userId).Select(s => s.Name).FirstOrDefault();
            }
        }

        // GET: pritam/masterdata/upperWorkTask
        [HttpGet("upperWorkTask")]
        public ActionResult<IEnumerable<UpperTask>> GetUpperWorkTask(string ShopName, int Days, Boolean isTestData, int? AssignTo)
        {
            DateTime taskBefore = DateTime.Now.AddDays(Days);

            if(AssignTo== 0)
            {
                var all = (from ud in _context.UserDetails
                           join w in _context.Work on ud.UserId equals w.UserId
                           join ub in _context.UpperBody on w.WorkId equals ub.WorkId
                           join s in _context.Shops on ud.ShopId equals s.ShopId
                           where ud.IsTestData == isTestData && ud.IsDelete == false && w.DeliveryDate <= taskBefore && ub.Status == 2 || ub.Status == 3 && s.ShopName == ShopName
                           select new UpperTask
                           {
                               WorkId= w.WorkId,
                               Name= ud.Name,
                               DeliveryDate= w.DeliveryDate,
                               ShopName= s.ShopName,
                               BranchName= s.BranchName,
                               ClothId= ub.ClothId,
                               ClothName= ub.ClothName,
                               Height= ub.Height,
                               Front= ub.Front,
                               Collar= ub.Collar,
                               Shoulder= ub.Shoulder,
                               Sleeve= ub.Sleeve,
                               SleeveWidth= ub.SleeveWidth,
                               Cuff= ub.Cuff,
                               Note= ub.Note,
                               Status= UIFrendlyStatus(ub.Status),
                               Price= ub.Price,
                               AssignTo= null,
                               PaidTo= ub.PaidTo,
                               FeedbackId= ub.FeedbackId

                            }).ToList();

                return all;
            }
            else
            {
                var all = (from ud in _context.UserDetails
                           join w in _context.Work on ud.UserId equals w.UserId
                           join ub in _context.UpperBody on w.WorkId equals ub.WorkId
                           join ud1 in _context.UserDetails on ub.AssignTo equals ud1.UserId
                           join s in _context.Shops on ud.ShopId equals s.ShopId
                           where ud.IsTestData == isTestData && ud.IsDelete == false && ub.Status == 4 && ub.AssignTo== AssignTo
                           select new UpperTask
                           {
                               WorkId = w.WorkId,
                               Name = ud.Name,
                               DeliveryDate = w.DeliveryDate,
                               ShopName = s.ShopName,
                               BranchName = s.BranchName,
                               ClothId = ub.ClothId,
                               ClothName = ub.ClothName,
                               Height = ub.Height,
                               Front = ub.Front,
                               Collar = ub.Collar,
                               Shoulder = ub.Shoulder,
                               Sleeve = ub.Sleeve,
                               SleeveWidth = ub.SleeveWidth,
                               Cuff = ub.Cuff,
                               Note = ub.Note,
                               Status = UIFrendlyStatus(ub.Status),
                               Price = ub.Price,
                               AssignTo = ud1.Name,
                               PaidTo = ub.PaidTo,
                               FeedbackId = ub.FeedbackId

                           }).ToList();

                return all;
            } 
        }

        // GET: pritam/masterdata/lowerWorkTask
        [HttpGet("lowerWorkTask")]
        public ActionResult<IEnumerable<LowerTask>> GetLowerWorkTask(string ShopName, int Days , Boolean isTestData, int? AssignTo)
        {
            DateTime taskBefore = DateTime.Now.AddDays(Days);

            if (AssignTo== 0)
            {
                var all = (from ud in _context.UserDetails
                           join w in _context.Work on ud.UserId equals w.UserId
                           join ub in _context.LowerBody on w.WorkId equals ub.WorkId
                           join s in _context.Shops on ud.ShopId equals s.ShopId
                           where ud.IsTestData == isTestData && ud.IsDelete == false && w.DeliveryDate <= taskBefore && ub.Status == 2 || ub.Status == 3 && s.ShopName == ShopName
                           select new LowerTask
                           {
                               WorkId = w.WorkId,
                               Name = ud.Name,
                               DeliveryDate = w.DeliveryDate,
                               ShopName = s.ShopName,
                               BranchName = s.BranchName,
                               ClothId = ub.ClothId,
                               ClothName = ub.ClothName,
                               Height = ub.Height,
                               Knee = ub.Knee,
                               Bottom = ub.Bottom,
                               Waist = ub.Waist,
                               Seat = ub.Seat,
                               Thigh = ub.Thigh,
                               Underline = ub.Underline,
                               Note = ub.Note,
                               Status = UIFrendlyStatus(ub.Status),
                               Price = ub.Price,
                               AssignTo = null,
                               PaidTo = ub.PaidTo,
                               FeedbackId = ub.FeedbackId

                           }).ToList();

                return all;
            }
            else
            {
                var all = (from ud in _context.UserDetails
                           join w in _context.Work on ud.UserId equals w.UserId
                           join ub in _context.LowerBody on w.WorkId equals ub.WorkId
                           join ud1 in _context.UserDetails on ub.AssignTo equals ud1.UserId
                           join s in _context.Shops on ud.ShopId equals s.ShopId
                           where ud.IsTestData == isTestData && ud.IsDelete == false && ub.Status == 4 && ub.AssignTo == AssignTo
                           select new LowerTask
                           {
                               WorkId = w.WorkId,
                               Name = ud.Name,
                               DeliveryDate = w.DeliveryDate,
                               ShopName = s.ShopName,
                               BranchName = s.BranchName,
                               ClothId = ub.ClothId,
                               ClothName = ub.ClothName,
                               Height = ub.Height,
                               Knee = ub.Knee,
                               Bottom = ub.Bottom,
                               Waist = ub.Waist,
                               Seat = ub.Seat,
                               Thigh = ub.Thigh,
                               Underline = ub.Underline,
                               Note = ub.Note,
                               Status = UIFrendlyStatus(ub.Status),
                               Price = ub.Price,
                               AssignTo = ud1.Name,
                               PaidTo = ub.PaidTo,
                               FeedbackId = ub.FeedbackId

                           }).ToList();

                return all;
            }

        }

        // GET: pritam/masterdata/workOutDetails
        [HttpGet("workOutDetails")]
        public ActionResult<IEnumerable<WorkOutDetails>> workOutDetails(DateTime startDate, DateTime endDate, Boolean isTestData)
        {
            var all = (from ud in _context.UserDetails
                       join w in _context.Work on ud.UserId equals w.UserId
                       join s in _context.Shops on ud.ShopId equals s.ShopId
                       join wo in _context.WorkOut on w.WorkId equals wo.WorkId
                       join ud1 in _context.UserDetails on wo.CreatedBy equals ud1.UserId
                       where ud.IsTestData == isTestData && ud.IsDelete == false && wo.DeliveredOn>= startDate && wo.DeliveredOn<= endDate
                           select new WorkOutDetails
                           {
                               WorkId = w.WorkId,
                               Name = ud.Name,
                               ShopName= s.ShopName,
                               BranchName= s.BranchName,
                               Pay= wo.Pay,
                               addedBy= ud1.Name,
                               DeliveredOn= wo.DeliveredOn

                           }).ToList();

                return all;
        }

        // PUT: pritam/masterdata/chnagePasssword
        [HttpGet("chnagePasssword")]
        public async Task<ActionResult<Boolean>> chnagePasssword(string userName, string password)
        {
            var currentUser = _context.Users.Where(w => w.Username == userName).FirstOrDefault();

            if (currentUser != null)
            {
                currentUser.Password = password;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return false;
                }

                return true;
            }
            else
            {
                return NotFound();
            }
        }

        private double UIFrendlyStatus(byte? status)
        {
            if(status== 1)
            {
                return (double)1;
            }

            return (double)(status - 1) * 20;
        }
    }
}
