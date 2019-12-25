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
    public class WorksController : ControllerBase
    {
        private readonly tailoreShopContext DBContext;

        public WorksController(tailoreShopContext context)
        {
            DBContext = context;
        }

        // GET: api/Works
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Work>>> GetWork()
        {
            return await DBContext.Work.ToListAsync();
        }

        // GET: api/Works/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Work>> GetWork(int id)
        {
            var work = await DBContext.Work.FindAsync(id);

            if (work == null)
            {
                return NotFound();
            }

            return work;
        }

        // POST: pritam/works/createWork
        [HttpPost("createWork")]
        public async Task<ActionResult<int>> createWork([FromBody] WorkInsertable work)
        {

            if (work.UserId== null)
            {
                return BadRequest();
            }

            var newWork = new Work()
            {
                UserId = work.UserId,
                CreatedOn = work.CreatedOn,
                CreatedBy = work.CreatedBy,
                UpdatedOn = null,
                UpdatedBy = null,
                DeliveryDate = work.DeliveryDate.Value.AddDays(1),
                DeliveredOn = null,
                Advance = work.Advance
            };

            DBContext.Work.Add(newWork);

            try
            {
                await DBContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkExists(newWork.WorkId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return newWork.WorkId;
        }

        // POST: pritam/works/createLowerBody
        [HttpPost("createLowerBody")]
        public async Task<ActionResult<Boolean>> createLowerBody([FromBody] LowerBody lowerBody)
        {

            var newLowerBody = new LowerBody()
            {
                WorkId= lowerBody.WorkId,
                ClothId= lowerBody.ClothId,
                ClothName= lowerBody.ClothName,
                Height= lowerBody.Height,
                Knee= lowerBody.Knee,
                Waist= lowerBody.Waist,
                Seat= lowerBody.Seat,
                Thigh= lowerBody.Thigh,
                Bottom= lowerBody.Bottom,
                Underline= lowerBody.Underline,
                Note= lowerBody.Note,
                Status= 2,
                Price= lowerBody.Price,
                AssignTo= null,
                PaidTo= null,
                FeedbackId= null
            };

            DBContext.LowerBody.Add(newLowerBody);

            try
            {
                await DBContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

            return true;
        }

        // POST: pritam/works/createUpperBody
        [HttpPost("createUpperBody")]
        public async Task<ActionResult<Boolean>> createUpperBody([FromBody] UpperBody upperBody)
        {

            var newUpperBody = new UpperBody()
            {
                WorkId = upperBody.WorkId,
                ClothId = upperBody.ClothId,
                ClothName = upperBody.ClothName,
                Height = upperBody.Height,
                Front = upperBody.Front,
                Collar = upperBody.Collar,
                Shoulder = upperBody.Shoulder,
                Sleeve = upperBody.Sleeve,
                SleeveWidth = upperBody.SleeveWidth,
                Cuff = upperBody.Cuff,
                Note = upperBody.Note,
                Status = 2,
                Price = upperBody.Price,
                AssignTo = null,
                PaidTo = null,
                FeedbackId = null
            };

            DBContext.UpperBody.Add(newUpperBody);

            try
            {
                await DBContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

            return true;
        }

        // DELETE: pritam/Works/deleteWork
        [HttpDelete("deleteWork")]
        public async Task<ActionResult<Boolean>> DeleteWork(int id)
        {
            //Delete work
            var work = await DBContext.Work.FindAsync(id);
            if (work != null)
            {
                DBContext.Work.Remove(work);
                await DBContext.SaveChangesAsync();
            }

            //Delete lowerBody
            List<LowerBody> lowerBody= await DBContext.LowerBody.Where(w => w.WorkId == id).ToListAsync();
            if(lowerBody!= null)
            {
                for (int n = 0; n < lowerBody.Count(); n++)
                {
                    DBContext.LowerBody.Remove(lowerBody[n]);
                    await DBContext.SaveChangesAsync();
                }
            }

            //Delete lowerBody
            List<UpperBody> upperBody = await DBContext.UpperBody.Where(w => w.WorkId == id).ToListAsync();
            if (lowerBody != null)
            {
                for (int n = 0; n < upperBody.Count(); n++)
                {
                    DBContext.UpperBody.Remove(upperBody[n]);
                    await DBContext.SaveChangesAsync();
                }
            }

            return true;
        }

        // PUT: pritam/works/changeStatus
        [HttpPut("changeStatus")]
        public async Task<ActionResult<Boolean>> chnageStatus([FromBody] StatusChnage status)
        {
            if (status.ClothType)
            {
                var currentCloth = await DBContext.UpperBody.Where(w => w.WorkId == status.WorkId && w.ClothId == status.ClothId).FirstOrDefaultAsync();

                if (currentCloth != null)
                {
                    if(status.AssignTo> 0)
                    {
                        currentCloth.AssignTo = status.AssignTo;
                        currentCloth.Status = status.Status;
                        currentCloth.PaidTo = status.PaidTo;
                    }
                    else
                    {
                        currentCloth.Status = status.Status;
                    }

                    try
                    {
                        await DBContext.SaveChangesAsync();
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
            else
            {
                var currentCloth = await DBContext.LowerBody.Where(w => w.WorkId == status.WorkId && w.ClothId == status.ClothId).FirstOrDefaultAsync();

                if (currentCloth != null)
                {
                    if (status.AssignTo > 0)
                    {
                        currentCloth.AssignTo = status.AssignTo;
                        currentCloth.Status = status.Status;
                        currentCloth.PaidTo = status.PaidTo;
                    }
                    else
                    {
                        currentCloth.Status = status.Status;
                    }

                    try
                    {
                        await DBContext.SaveChangesAsync();
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

        }

        // POST: pritam/works/createWorkOut
        [HttpPost("createWorkOut")]
        public async Task<ActionResult<Boolean>> createWorkOut([FromBody] WorkOutInsertable workOut)
        {
            var work = await DBContext.Work.Where(w => w.WorkId == workOut.WorkId).FirstOrDefaultAsync();
            var test = await DBContext.UserDetails.Where(w => w.UserId == work.UserId).FirstOrDefaultAsync();

            if(test.IsTestData== workOut.IsTestData)
            {
                var newWorkOut = new WorkOut()
                {
                    WorkId = workOut.WorkId,
                    DeliveredOn = workOut.DeliveredOn,
                    CreatedBy = workOut.CreatedBy,
                    Pay = workOut.Pay,
                    UpdatedOn = workOut.UpdatedOn,
                    UpdatedBy = workOut.UpdatedBy
                };

                DBContext.WorkOut.Add(newWorkOut);

                try
                {
                    await DBContext.SaveChangesAsync();

                    List<UpperBody> upperCloths = await DBContext.UpperBody.Where(w => w.WorkId == workOut.WorkId).ToListAsync();
                    List<LowerBody> lowerCloths = await DBContext.LowerBody.Where(w => w.WorkId == workOut.WorkId).ToListAsync();

                    if (upperCloths != null)
                    {
                        foreach (UpperBody u in upperCloths)
                        {
                            u.Status = 6;

                            try
                            {
                                await DBContext.SaveChangesAsync();
                            }
                            catch (DbUpdateConcurrencyException)
                            {
                                return false;
                            }
                        }
                    }

                    if (lowerCloths != null)
                    {
                        foreach (LowerBody l in lowerCloths)
                        {
                            l.Status = 6;

                            try
                            {
                                await DBContext.SaveChangesAsync();
                            }
                            catch (DbUpdateConcurrencyException)
                            {
                                return false;
                            }
                        }
                    }

                    work.DeliveredOn = workOut.DeliveredOn;

                    try
                    {
                        await DBContext.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        return false;
                    }

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

        // POST: pritam/works/createShop
        [HttpPost("createShop")]
        public async Task<ActionResult<Boolean>> createShop([FromBody] ShopsInsertable shop)
        {
            var newShop = new Shops()
            {
                ShopName= shop.ShopName,
                BranchName= shop.BranchName
            };

            DBContext.Shops.Add(newShop);

            try
            {
                await DBContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

        }

        // PUT: pritam/works/changeFeedback
        [HttpPut("changeFeedback")]
        public async Task<ActionResult<Boolean>> changeFeedback([FromBody] Feedback feedback)
        {
            var currentFeedback = await DBContext.Feedback.Where( w => w.FeedbackId == feedback.FeedbackId ).FirstOrDefaultAsync();

            if (currentFeedback != null)
            {
                if(feedback.Feedback1.Length> 500)
                {
                    feedback.Feedback1= feedback.Feedback1.Substring(0, 500);
                }
                currentFeedback.Fit = feedback.Fit;
                currentFeedback.Style = feedback.Style;
                currentFeedback.Quality = feedback.Quality;
                currentFeedback.Feedback1 = feedback.Feedback1;

                try
                {
                    await DBContext.SaveChangesAsync();
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

        // POST: pritam/works/createFeedback
        [HttpPost("createFeedback")]
        public async Task<ActionResult<Boolean>> createFeedback([FromBody] FeedbackInsertable feedback)
        {
            if (feedback.Feedback1.Length > 500)
            {
                feedback.Feedback1 = feedback.Feedback1.Substring(0, 500);
            }

            var newFeedback = new Feedback()
            {
                Fit= feedback.Fit,
                Style= feedback.Style,
                Quality= feedback.Quality,
                Feedback1= feedback.Feedback1
            };

            DBContext.Feedback.Add(newFeedback);

            try
            {
                await DBContext.SaveChangesAsync();
                
                if(feedback.ClothType)
                {
                    var currentUpperCloth = await DBContext.UpperBody.Where(w => w.WorkId == feedback.WorkId && w.ClothId== feedback.ClothId).FirstOrDefaultAsync();

                    if (currentUpperCloth != null)
                    {
                        currentUpperCloth.FeedbackId = newFeedback.FeedbackId;

                        try
                        {
                            await DBContext.SaveChangesAsync();
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
                else
                {
                    var currentLowerCloth = await DBContext.LowerBody.Where(w => w.WorkId == feedback.WorkId && w.ClothId == feedback.ClothId).FirstOrDefaultAsync();

                    if (currentLowerCloth != null)
                    {
                        currentLowerCloth.FeedbackId = newFeedback.FeedbackId;

                        try
                        {
                            await DBContext.SaveChangesAsync();
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

            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        private bool WorkExists(int id)
        {
            return DBContext.Work.Any(e => e.WorkId == id);
        }
    }
}
