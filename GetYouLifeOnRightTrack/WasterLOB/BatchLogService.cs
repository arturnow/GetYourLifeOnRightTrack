using WasterDAL.Repositories;
namespace WasterLOB
{
    using System;
    using System.Linq;

    using WasterDAL.Model;

    public class BatchLogService : IBatchLogService
    {
        private readonly IBatchLogRepository _repository;

        public BatchLogService(IBatchLogRepository repository)
        {
            if (repository == null)
            {
                throw new ArgumentNullException("repository");
            }
            this._repository = repository;
        }

        public long StartBatch(string name)
        {
            var batchLog = new BatchLog
                           {
                               Name = name,
                               StartDateTime = DateTime.Now,
                               InProgress = true
                           };

            return _repository.Create(batchLog);
        }

        public void StopBatch(string name)
        {
            var batchToStop =_repository.Query(log => log.Name == name && log.InProgress)
                .OrderByDescending(log => log.StartDateTime)
                .FirstOrDefault();

            batchToStop.EndDateTime = DateTime.Now;
            batchToStop.InProgress = false;

            _repository.Update(batchToStop);
        }

        public void StopBatch(long id)
        {
            var batchToStop = _repository.Query(log => log.Id == id  && log.InProgress)
                .OrderByDescending(log => log.StartDateTime)
                .FirstOrDefault();

            batchToStop.EndDateTime = DateTime.Now;
            batchToStop.InProgress = false;

            _repository.Update(batchToStop);
        }

        public DateTime? GetLastRun(string name)
        {
            return _repository.Query(log => log.Name.ToLower() == name.ToLower() && log.EndDateTime != null)
                .OrderByDescending(log => log.EndDateTime)
                .Select(log => log.EndDateTime).FirstOrDefault();
        }
    }
}