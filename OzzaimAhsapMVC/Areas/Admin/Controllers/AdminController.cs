﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OzzaimAhsapMVC.Areas.Admin.Controllers
{
    [Area("Admin"),Authorize()]
    public class AdminController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }
    }
}
