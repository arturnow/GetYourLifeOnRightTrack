using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WasterDAL.Model;
using WasterDAL.Repositories;

namespace WasterLOB
{
    public interface ISocialProfileService
    {
        SocialProfile[] GetAll();
    }

    public class SocialProfileService : ISocialProfileService
    {
        private readonly ISocialProfileRepository _socialProfileRepository;

        public SocialProfileService(ISocialProfileRepository socialProfileRepository)
        {
            if (socialProfileRepository == null) throw new ArgumentNullException("socialProfileRepository");
            _socialProfileRepository = socialProfileRepository;
        }

        public SocialProfile[] GetAll()
        {
            return _socialProfileRepository.Query(profile => true).ToArray();
        }
    }
    public class FakeSocialProfileService : ISocialProfileService
    {
        public SocialProfile[] GetAll()
        {
            return new[]
            {
                new SocialProfile(){Age = 45, Nickname = "Alfredo", Type = ProfileType.Public},
                new SocialProfile(){Age = 35, Nickname = "Giowani", Type = ProfileType.Public},
                new SocialProfile(){Age = 25, Nickname = "Anastaci", Type = ProfileType.Public},
                new SocialProfile(){ Age = 15, Nickname = "Stefano", Type = ProfileType.Public}
            };
        }
    }
}
