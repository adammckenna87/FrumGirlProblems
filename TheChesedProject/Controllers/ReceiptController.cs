﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TheChesedProject.Controllers
{
    public class ReceiptController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}