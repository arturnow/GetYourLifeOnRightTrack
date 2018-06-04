using System;
using System.Linq;
using WasterDAL.Model;
using WasterDAL.Repositories;

namespace WasterLOB
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _repository;
        private readonly IIdentityRepository _identityRepository;
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

        private int messagePackCapacity = 3;

        public MessageService(IMessageRepository repository, IIdentityRepository identityRepository, IUserContextProvider userContextProvider)
        {
            _repository = repository;
            _identityRepository = identityRepository;
            _userContextProvider = userContextProvider;
        }

        public Guid Create(Message message)
        {
            var user =_identityRepository.Query(identity => identity.Login == UserName).FirstOrDefault();
            message.Identity = user;

            return _repository.Create(message);
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Message Get(Guid id)
        {
            return _repository.Get(id);
        }

        public Message[] GetAll()
        {
            return _repository.Query(message => message.Identity.Login == UserName).ToArray();
        }

        public Message[] GetLast(int number)
        {
            return _repository.Query(message => message.Identity.Login == UserName).Take(number).OrderByDescending(message => message.DueDate).ToArray();
        }
    }
}