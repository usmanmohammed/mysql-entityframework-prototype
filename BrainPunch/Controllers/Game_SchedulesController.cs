using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BrainPunch.Models;

namespace BrainPunch.Controllers
{
    public class Game_SchedulesController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Game_Schedules
        public async Task<ActionResult> Index()
        {
            return View(await db.GameSchedules.ToListAsync());
        }

        // GET: Game_Schedules/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game_Schedule game_Schedule = await db.GameSchedules.FindAsync(id);
            if (game_Schedule == null)
            {
                return HttpNotFound();
            }
            return View(game_Schedule);
        }

        // GET: Game_Schedules/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Game_Schedules/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ScheduleID,FirstOpponentID,SecondOpponentID,ScheduleDate,WinnerID")] Game_Schedule game_Schedule)
        {
            if (ModelState.IsValid)
            {
                db.GameSchedules.Add(game_Schedule);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(game_Schedule);
        }

        // GET: Game_Schedules/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game_Schedule game_Schedule = await db.GameSchedules.FindAsync(id);
            if (game_Schedule == null)
            {
                return HttpNotFound();
            }
            return View(game_Schedule);
        }

        // POST: Game_Schedules/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ScheduleID,FirstOpponentID,SecondOpponentID,ScheduleDate,WinnerID")] Game_Schedule game_Schedule)
        {
            if (ModelState.IsValid)
            {
                db.Entry(game_Schedule).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(game_Schedule);
        }

        // GET: Game_Schedules/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game_Schedule game_Schedule = await db.GameSchedules.FindAsync(id);
            if (game_Schedule == null)
            {
                return HttpNotFound();
            }
            return View(game_Schedule);
        }

        // POST: Game_Schedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Game_Schedule game_Schedule = await db.GameSchedules.FindAsync(id);
            db.GameSchedules.Remove(game_Schedule);
            await db.SaveChangesAsync();
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
    }
}
