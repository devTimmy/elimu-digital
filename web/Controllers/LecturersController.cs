﻿using AutoMapper;
using DAL.Extensions;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web.Controllers
{
    [Authorize]
    [ResponseCache(Duration = 120, Location = ResponseCacheLocation.Any)]
    public class LecturersController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IDataManager _dataManager;
        private readonly IRepositoryFactory _repos;
        private readonly IMapper _mapper;

        public LecturersController(UserManager<AppUser> userManager,IDataManager dataManager,IRepositoryFactory factory, IMapper mapper)
        {
            _userManager = userManager;
            _dataManager = dataManager;
            _repos = factory;
            _mapper = mapper;
        }

        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Client)]
        public IActionResult Index()
        {
            IList<Lecturer> lecturers = new List<Lecturer>();

            if(User.Role() == "Administrator")
            {
                lecturers = _repos.Lecturers
                                  .ListWith("Profile", "Units", "Likes")
                                  .ToList();
            }
            else if(User.Role() == "Lecturer")
            {
                lecturers = _repos.Lecturers
                                  .ListWith("Profile", "Units", "Likes")
                                  .ToList();
            }
            else if(User.Role() == "Student")
            {
                lecturers = _dataManager.MyLecturers(this.GetAccountId())
                                        .ToList();
            }

            if(lecturers == null)
            {
                lecturers = new List<Lecturer>();
            }

            var model = lecturers.SkipWhile(x => x == null).ToList();

            ViewBag.Notifications = this.GetNotifications();

            return View(model);
        }

        [HttpGet]
        [Route("lecturers/{id}/{names}")]
        public IActionResult Details(int id, string names)
        {
            return View();
        }
    }
}
