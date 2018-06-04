using System;
using WasterDAL.Model;

namespace WasterLOB
{
    public interface IGoalService
    {
        Guid Create(Goal entity);

        void Update(Goal entity);

        Goal Get(Guid id);

        Goal[] GetAll();
        Goal[] GetLast(int number);

        void Delete(Guid id);

        void Finish(Guid id);

    }
}
