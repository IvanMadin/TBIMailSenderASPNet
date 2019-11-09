using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using EmailManager.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmailManager.Web.Controllers
{
    public class SerilogController : Controller
    {
        ILogger<SerilogController> logger;

        public SerilogController(ILogger<SerilogController> logger)
        {
            this.logger = logger;
        }

        //public IActionResult Index()
        //{
        //    this.logger.LogDebug("Index was called");
        //    return View();
        //}

        public IActionResult Index()
        {
            this.logger.LogDebug("Index was called");
            return View();
        }

    }
}
   