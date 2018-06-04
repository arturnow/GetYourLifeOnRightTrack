using System;
using System.Data;
using WasterDAL.Model;

namespace WasterLOB
{
    public interface ITrackService
    {
        Guid Create(TrackRecord entity, int userId);

        void StopTrack(Guid id, DateTime? endTime);

        void CancelTrack(Guid id);


        TrackRecord Get(Guid id);

        TrackRecord[] GetAll( int userId);
        TrackRecord[] GetLast(int number, int userId);

        TrackRecord[] GetByDate(DateTime? startDateTime, DateTime? endDateTime, int userId);

    }
}