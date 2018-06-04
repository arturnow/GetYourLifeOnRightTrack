using System;
using WasterDAL.Model;

namespace WasterLOB
{
    public interface IPatternService
    {
        Guid Create(Pattern entity);

        void Disable(Guid id);

        void Disable(string patternName);

        Pattern Get(Guid id);

        Pattern[] GetPatterns();

    }
}