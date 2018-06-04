using System;
using System.Linq;
using WasterDAL.Model;
using WasterDAL.Repositories;

namespace WasterLOB
{
    public class PatternService : IPatternService
    {
        private string _userName = "";

        private readonly IPatternRepository _repository;

        private readonly IIdentityRepository _identityRepository;
        private readonly IUserContextProvider _userContextProvider;

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

        public PatternService(IPatternRepository repository, IIdentityRepository identityRepository, IUserContextProvider userContextProvider )
        {
            if (repository == null) throw new ArgumentNullException("repository");
            if (identityRepository == null) throw new ArgumentNullException("identityRepository");
            if (userContextProvider == null) throw new ArgumentNullException("userContextProvider");
            _repository = repository;
            _identityRepository = identityRepository;
            _userContextProvider = userContextProvider;
        }

        public Guid Create(Pattern entity)
        {
            var user = this._identityRepository.Query(identity => identity.Login == this.UserName).FirstOrDefault(i => true);//.Id;

            if (user == null)
            {
                throw new ApplicationException("Identity not found");
            }

            var existing =
                _repository.Query(pattern => pattern.IdentityId == user.Id && pattern.Value.ToLower() == entity.Value.ToLower()).FirstOrDefault();

            if (existing != null)
            {
                //Log this
                return existing.Id;
            }

            entity.IdentityId = user.Id;

            return _repository.Create(entity);
        }

        public void Disable(Guid id)
        {
            var entity = this._repository.Get(id);
            entity.IsEnabled = false;

            this._repository.Update(entity);
        }

        public void Disable(string patternName)
        {
            var entity = this._repository.Query(p => p.Value == patternName.Trim().ToLower()).FirstOrDefault();
            if (entity != null)
                this.Disable(entity.Id);
        }

        public Pattern Get(Guid id)
        {
            return _repository.Get(id);
        }

        public Pattern[] GetPatterns()
        {
            return _repository.Query(pattern => pattern.Identity.Login == UserName).ToArray();
        }
    }
}