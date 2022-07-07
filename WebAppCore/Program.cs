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
                socket.OnOpen = () => //������Socket����ʱִ�д˷���
            {
                    var data = socket.ConnectionInfo; //ͨ��data���Ի��������Ӵ��ݹ�����Cookie��Ϣ���������ָ������Ӻ��û�֮��Ĺ�ϵ�������Ҫ��̨����������Ϣ��ĳ���ͻ���ʱ�򣬿���ʹ��Cookie��
                    Console.WriteLine("Open!");
                    allSockets.Add(socket);
                };

                socket.OnClose = () =>// ���ر�Socket����ʮִ�д˷���
                {

                    Console.WriteLine("Close!");
                    allSockets.Remove(socket);
                };

                socket.OnMessage = message =>// ���տͻ��˷��͹�������Ϣ
                {
                    Console.WriteLine(message);
                    //�����ݷ����л�����
                    //���뻺��redis
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
