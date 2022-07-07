using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppCore.Common;
using WebAppCore.Interface;

namespace WebAppCore.Service
{
    public class MessageOperation : IMessageOperation
    {
        //public int SaveMessage(Message message);
        //{
        //   return 1;
        //}
        public int SaveMessage(Message message)
        {
            //保存数据库 考虑使用事务

           
            var sql2 = "INSERT INTO payload (machine_id,id,timestamp,status)VALUES('{0}', '{1}', '{2}', '{3}');";
            string sqlstr2 = string.Format(sql2, message.topic, message.refstr, message.eventstr);
            var result2 = DbContext.Execute(sqlstr2);

            var sql1 = "INSERT INTO Message (topic,refstr,eventstr,payloadid)VALUES('{0}', '{1}', '{2}', '{3}');";
            string sqlstr1 = string.Format(sql1, message.topic, message.refstr, message.eventstr, result2);
            var result1 = DbContext.Execute(sqlstr1);

            var result = result1 + result2;
            return result;
        }

        public List<Message> GetMessage()
        {
            //读取数据库
            var sql = "select m.topic,m.refstr,m.eventstr,m.payloadid,p.machine_id,p.id,p.timestamp,p.status from Message as m join payload as p on m.msgid = p.id";
            var result = DbContext.Query<Message>(sql).ToList();
            return result;

        }
    }
}
