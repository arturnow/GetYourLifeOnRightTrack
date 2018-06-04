using System;
using WasterDAL.Model;

namespace WasterLOB
{

    public interface IMessageService 
    {
        Guid Create(Message message);
        void Delete(Guid id);
        Message Get(Guid id);

        Message[] GetAll();
        Message[] GetLast(int number);
    }
}