using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SmartLibrary.Models;
using SmartLibrary.Models.EntityModels;
using AutoMapper;
using SmartLibrary.Models.ViewModels.AuditLog;

namespace SmartLibrary.Controllers
{
    public class AuditLogController : BaseController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: AuditLog
        public async Task<ActionResult> Index()
        {
            var auditLogs = await db.AuditLogs.ToListAsync();
            return View(Mapper.Map<List<AuditLogViewModel>>(auditLogs));
        }

        // GET: AuditLog/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AuditLog auditLog = await db.AuditLogs.FindAsync(id);
            if (auditLog == null)
            {
                return HttpNotFound();
            }
            return View(Mapper.Map<AuditLogViewModel>(auditLog));
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
