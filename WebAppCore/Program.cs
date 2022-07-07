using Fleck;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppCore.Common;

namespace WebAppCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

            FleckLog.Level = Fleck.LogLevel.Debug;

            var allSockets = new List<IWebSocketConnection>();

            var server = new WebSocketServer("ws://0.0.0.0:7181");
            server.Start(socket =>
            {
                socket.OnOpen = () => //当建立Socket链接时执行此方法
            {
                    var data = socket.ConnectionInfo; //通过data可以获得这个链接传递过来的Cookie信息，用来区分各个链接和用户之间的关系（如果需要后台主动推送信息到某个客户的时候，可以使用Cookie）
                    Console.WriteLine("Open!");
                    allSockets.Add(socket);
                };

                socket.OnClose = () =>// 当关闭Socket链接十执行此方法
                {

                    Console.WriteLine("Close!");
                    allSockets.Remove(socket);
                };

                socket.OnMessage = message =>// 接收客户端发送过来的信息
                {
                    Console.WriteLine(message);
                    //将数据反序列化出来
                    //存入缓存redis
                    new RedisHelpers().SetRedisQueue(message);                 
                   // socket.Send("Echo: " + message);
                };
            });

            var input = Console.ReadLine();
            while (input != "exit")
            {
                foreach (var socket in allSockets.ToList())
                {
                    socket.Send(input);
                }
                input = Console.ReadLine();
            }


        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
