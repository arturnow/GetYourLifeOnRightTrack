using System;
using System.Linq;
using WasterDAL.Model;
using WasterDAL.Repositories;

namespace WasterLOB
{
    public class TrackService : ITrackService
    {
        private readonly ITrackRecordRepository _repository;

        [Obsolete("Chyba nie bêdzie potrzebne")]
        private readonly IPatternRepository _patternRepository;

        private readonly IIdentityRepository _identityRepository;

        public TrackService(ITrackRecordRepository repository, IPatternRepository patternRepository, IIdentityRepository identityRepository, IUserContextProvider userContextProvider)
        {
            if (repository == null) throw new ArgumentNullException("repository");
            if (patternRepository == null) throw new ArgumentNullException("patternRepository");
            if (identityRepository == null) throw new ArgumentNullException("identityRepository");
            if (userContextProvider == null) throw new ArgumentNullException("userContextProvider");
            _repository = repository;
            _patternRepository = patternRepository;
            _identityRepository = identityRepository;
        }

        public Guid Create(TrackRecord entity, int userId)
        {
            var existingIdentity = this._identityRepository.Query(identity => identity.Id == userId).FirstOrDefault();
            var existingPattern = this._patternRepository.Query(pattern => pattern.Value == entity.PatternValue.ToLower()).FirstOrDefault();
            
            if(existingIdentity == null)
            {
                throw new ApplicationException("Parent Identity no found");
            }
            if (existingPattern == null)
            {
                throw new ApplicationException("Parent Pattern not found");
            }
            
            entity.Id = Guid.NewGuid();
            entity.CreateDate = DateTime.Now;

            entity.LoginId = existingIdentity.Id;
            entity.PatternId = existingPattern.Id;

            return this._repository.Create(entity);

        }

        public void StopTrack(Guid id, DateTime? endTime)
        {
            var trackRecord = _repository.Get(id);
            trackRecord.EndDate = endTime ?? DateTime.Now;
            _repository.Update(trackRecord);
        }

        public void CancelTrack(Guid id)
        {
            var trackRecord = _repository.Get(id);
            trackRecord.IsCancelled = true;
            _repository.Update(trackRecord);
        }

        public TrackRecord Get(Guid id)
        {
            return _repository.Get(id);
        }

        public TrackRecord[] GetAll(int userId)
        {
            return _repository.Query(track => track.Identity.Id == userId).OrderByDescending(track => track.CreateDate).ToArray();
        }

        public TrackRecord[] GetLast(int number, int userId)
        {
            return _repository.Query(track => track.Identity.Id == userId).Take(number).OrderByDescending(track => track.CreateDate).ToArray();
        }

        public TrackRecord[] GetByDate(DateTime? strtDateTime, DateTime? endDateTime, int userId)
        {
            return _repository.Query(track => track.Identity.Id == userId
                                              && track.StartDate >= strtDateTime && track.EndDate <= endDateTime)
                .ToArray();
        }
    }
}