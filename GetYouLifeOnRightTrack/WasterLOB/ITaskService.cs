using System;
using WasterDAL.Model;

namespace WasterLOB
{
    public interface ITaskService
    {
        Guid Create(Guid goalId, Task entity);

        void Update(Task entity);

        Task Get(Guid id);

        Task[] GetAll(Guid goalId);
        Task[] GetLast(Guid goalId, int number);

        void Delete(Guid id);

        void Finish(Guid id);
    }
}
