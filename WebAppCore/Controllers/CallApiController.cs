using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppCore.Service;

namespace WebAppCore.Controllers
{
    public class CallApiController :ControllerBase
    {


        /// <summary>
        /// 获取消息数据
        /// </summary>
        /// <returns></returns>
        [Route("GetMessage")]
        [HttpGet]
        public async Task<List<Message>> GetMessage()
        {
            var list = new List<Message>();
            list = await Task.Run(() => {
                //读取数据库
                MessageOperation messagebll = new MessageOperation();
                return messagebll.GetMessage();
            });
            return list;
        }


    }
}
