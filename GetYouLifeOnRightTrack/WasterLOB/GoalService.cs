using System;
using System.Linq;
using WasterDAL.Model;
using WasterDAL.Repositories;

namespace WasterLOB
{
    public class GoalService : IGoalService
    {
        private readonly IGoalRepository _repository;
        private readonly IIdentityRepository _identityRepository;
        private readonly IUserContextProvider _userContextProvider;
        private  string _userName;

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


        public GoalService(IGoalRepository goalRepository, IIdentityRepository identityRepository, IUserContextProvider userContextProvider)
        {
            if (goalRepository == null) throw new ArgumentNullException("goalRepository");
            if (identityRepository == null) throw new ArgumentNullException("identityRepository");
            if (userContextProvider == null) throw new ArgumentNullException("userContextProvider");
            _repository = goalRepository;
            _identityRepository = identityRepository;
            _userContextProvider = userContextProvider;
        }

        public Guid Create(Goal entity)
        {
            var user = _identityRepository.Query(identity => identity.Login == UserName).FirstOrDefault(i => true);//.Id;

            entity.IdentityId = user.Id;

            return _repository.Create(entity);

        }

        public void Update(Goal entity)
        {
            //TODO: sprawdziæ jakieœ waruneczki
            _repository.Update(entity);
        }

        public Goal Get(Guid id)
        {
            return _repository.Get(id);
        }

        public Goal[] GetAll()
        {
            return _repository.Query(goal => goal.Identity.Login == UserName).OrderByDescending(goal => goal.StartDate).ToArray();
        }

        public Goal[] GetLast(int number)
        {
            return _repository.Query(goal => goal.Identity.Login == UserName).OrderByDescending(g => g.StartDate).Take(number).ToArray();
        }

        public void Delete(Guid id)
        {
            //TODO: Need to decide between soft and hard delete
            throw new NotImplementedException();
        }

        public void Finish(Guid id)
        {
            var goal = _repository.Get(id);

            goal.Progress = 100;
            goal.EndDate = DateTime.Today;

            _repository.Update(goal);
        }
    }
}