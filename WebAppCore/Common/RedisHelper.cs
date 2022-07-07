using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppCore.Common
{
    public class RedisHelpers
    {
        public  string MESSAGE_KEY = "message:queue";
        
        /// <summary>
        /// 将消息存入redis
        /// </summary>
        /// <param name="message"></param>
        public void SetRedisQueue(string message)
        {
            //创建链接
            var csredis = new CSRedis.CSRedisClient("127.0.0.1:6379,password=123,defaultDatabase=1,poolsize=50,ssl=false,writeBuffer=10240");
            //初始化 RedisHelper
            RedisHelper.Initialization(csredis);
            //赋值
            RedisHelper.LPush(MESSAGE_KEY, message);
           // Console.WriteLine(RedisHelper.Get<String>("k1"));
        }


        /// <summary>
        /// 将消息取出redis
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Message GetRedisQueue()
        {
            //创建链接
            var csredis = new CSRedis.CSRedisClient("127.0.0.1:6379,password=123,defaultDatabase=1,poolsize=50,ssl=false,writeBuffer=10240");
            //初始化 RedisHelper
            RedisHelper.Initialization(csredis);
            //赋值
            var result = RedisHelper.LPop(MESSAGE_KEY);
            Message model = JsonConvert.DeserializeObject<Message>(result);
            return model;
        }


    }
}
