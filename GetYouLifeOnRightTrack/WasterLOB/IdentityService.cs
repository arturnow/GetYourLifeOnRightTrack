using System;
using System.Linq;
using System.Threading.Tasks;
using Common;
using WasterDAL.Model;
using WasterDAL.Repositories;

namespace WasterLOB
{
    public class IdentityService : IIdentityService
    {
        private readonly IIdentityRepository _repository;
        private readonly IPasswordHashProvider _passwordHashProvider;

        public IdentityService(IIdentityRepository repository, IPasswordHashProvider passwordHashProvider)
        {
            if (repository == null) throw new ArgumentNullException("repository");
            if (passwordHashProvider == null) throw new ArgumentNullException("passwordHashProvider");
            _repository = repository;
            _passwordHashProvider = passwordHashProvider;
        }

        public int Register(Identity identity)
        {
            if (_repository.Query(i => i.Login == identity.Login).Any())
            {
                throw new ApplicationException(string.Format("User with name {0} exists", identity.Login));
            }
            if (_repository.Query(i => i.Email == identity.Email).Any())
            {
                throw new ApplicationException(string.Format("E-mail {0} already registerd", identity.Email));
            }

            identity.Password = _passwordHashProvider.CreateHash(identity.Password);

            return _repository.Create(identity);
        }

        //public Task<int> RegisterAsync(Identity identity)
        //{
        //    identity.Password = _passwordHashProvider.CreateHash(identity.Password);

        //    if (_repository.Query(i => i.Login == identity.Login).Any())
        //    {
        //        throw new ApplicationException(string.Format("User with name {0} exists", identity.Login));
        //    }
        //    if (_repository.Query(i => i.Email == identity.Email).Any())
        //    {
        //        throw new ApplicationException(string.Format("E-mail {0} already registerd", identity.Email));
        //    }

        //    return _repository.Create(identity);
        //}

        public void Update(Identity entity)
        {
            _repository.Update(entity);
        }

        public Identity Get(int id)
        {
            return _repository.Get(id);
        }

        public Identity Get(string login)
        {
            var existingIdentity = _repository.Query(identity => identity.Login == login).FirstOrDefault();

            return existingIdentity;
        }

        public Identity[] GetAll()
        {
            return _repository.Query(identity => true).OrderBy(identity => identity.CreateDate).ToArray();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool IsValid(string userName, string password)
        {
            bool isValid = false;

            //Temporary solution, as I have unhashed password in DB
            var hashedPass = _repository.Query(identity => identity.Login == userName)
                .Select(identity => new { identity.Password, identity.Salt }).FirstOrDefault();


            if (string.IsNullOrWhiteSpace(hashedPass.Password))
                return false;

#if DEBUG
            if (hashedPass.Salt != null)
            {
                isValid = _passwordHashProvider.ValidatePassword(password, hashedPass.Password);
            }
            else
            {
                isValid = hashedPass.Password.Equals(password);
            }
#endif
            return isValid;
        }
    }
}