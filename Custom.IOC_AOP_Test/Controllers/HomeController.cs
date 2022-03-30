using Custom.IBLL;
using Custom.IOC_AOP_Test.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Custom.IOC_AOP_Test.Controllers
{
    public class HomeController : Controller
    {
        //asp.net core 中默认的IOC容器只支持构造函数注入
        //如果有多个构造函数，会匹配到所有构造函数参数类型的合集的构造函数，如果没有会报错InvalidOperationException
        private readonly ILogger<HomeController> _logger;
        private IUserBLL userBLL;

        public HomeController(ILogger<HomeController> logger,IUserBLL userBll)
        {
            _logger = logger;
            userBLL = userBll;
        }

        public IActionResult Index()
        {
            var userModel = userBLL.Find(1);

            return View(userModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
