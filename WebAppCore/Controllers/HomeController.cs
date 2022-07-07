using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebAppCore.Common;
using WebAppCore.Models;
using WebAppCore.Service;

namespace WebAppCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //页面初始化，启动多线程保存数据
            StartThread();
            return View();
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


        /// <summary>
        /// 保存数据
        /// </summary>
        public static void SaveData(Message c)
        {
            //读取数据库
            MessageOperation messagebll = new MessageOperation();
            messagebll.SaveMessage(c);
        }

        public void StartThread()
        {
            //创建无参的线程
            Thread thread1 = new Thread(new ThreadStart(Thread1));
            //调用Start方法执行线程
            thread1.Start();  
            Console.ReadKey();
        }

         /// <summary>
         /// 创建无参的方法
         /// </summary>
         static void Thread1()
         {
             var model = new RedisHelpers().GetRedisQueue();
             SaveData(model);
         }

    }
}
