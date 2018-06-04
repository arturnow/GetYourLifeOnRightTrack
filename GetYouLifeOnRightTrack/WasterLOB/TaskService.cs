using System;
using System.Linq;
using WasterDAL.Model;
using WasterDAL.Repositories;

namespace WasterLOB
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repository;
        private readonly IIdentityRepository _identityRepository;
        private readonly IGoalRepository _goalRepository;
        private readonly IUserContextProvider _userContextProvider;
        private string _userName;
        public string UserName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_userName))
                {
                    _userName = _userContextProvider.GetContextUserName();
                }
                return _userName;
            }
        }

        public TaskService(ITaskRepository repository, IIdentityRepository identityRepository, IGoalRepository goalRepository, IUserContextProvider userContextProvider)
        {
            _repository = repository;
            _identityRepository = identityRepository;
            _goalRepository = goalRepository;
            _userContextProvider = userContextProvider;
        }

        public Guid Create(Guid goalId, Task entity)
        {
            //TODO: sprawdziæ czy goalId nale¿y do u¿ytkownika
            var user =  _identityRepository.Query(identity => identity.Login == UserName).FirstOrDefault();

            if (_goalRepository.Query(goal => goal.Id == goalId && goal.Identity.Login == UserName).Any())
            {
                entity.GoalId = goalId;
                return _repository.Create(entity);
            }
            else
            {
                throw  new ApplicationException("Parent Goal doesn't exist or you have no privelages to use it");
            }


        }

        public void Update(Task entity)
        {
            _repository.Update(entity);
        }

        public Task Get(Guid id)
        {
            return _repository.Get(id);
        }

        public Task[] GetAll(Guid goalId)
        {
            return _repository.Query(goal => goal.GoalId == goalId).OrderByDescending(goal => goal.StartDate).ToArray();
        }

        public Task[] GetLast(Guid goalId, int number)
        {
            return _repository.Query(goal => goal.GoalId == goalId).Take(number).OrderByDescending(goal => goal.StartDate).ToArray();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Finish(Guid id)
        {
            var task = _repository.Get(id);
            task.Progress = 100;

            _repository.Update(task);
        }
    }
}