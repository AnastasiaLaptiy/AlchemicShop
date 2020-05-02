﻿using AlchemicShop.BLL.DTO;
using AlchemicShop.BLL.Interfaces;
using AlchemicShop.WEB.Models;
using AutoMapper;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Security;

namespace AlchemicShop.WEB.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AccountController(
            IAccountService accountService,
            IUserService userService,
            IMapper mapper
           )
        {
            _accountService = accountService;
            _userService = userService;
            _mapper = mapper;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel account)
        {
            if (ModelState.IsValid)
            {
                var userAccount = await _accountService.GetAccount(account.Login, account.Password);
                if (userAccount != null)
                {
                    FormsAuthentication.SetAuthCookie(userAccount.Login, true);
                    return RedirectToAction("GetProductList", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }
            return View(account);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //need to override
                var userAccount =
                    await _accountService.GetAccount(model.Login, model.Password);
                if (userAccount != null)
                {
                    ModelState.AddModelError("", "Пользователь с таким логином уже существует");
                }
                else
                {
                    await _userService.AddUser(_mapper.Map<UserDTO>(
    new UserViewModel { Login = model.Login, Name = model.Name, Password = model.Password, IsAdmin = false }));
                    FormsAuthentication.SetAuthCookie(model.Login, true);
                    return RedirectToAction("GetProductList", "Product");
                }
            }
            return View(model);
        }


        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("GetProductList", "Product");
        }
    }
}