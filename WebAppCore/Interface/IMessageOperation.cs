using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppCore.Interface
{
    public interface IMessageOperation
    {
        public  int SaveMessage(Message message);
        public List<Message> GetMessage();
    }
}
