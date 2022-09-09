using Core.Persistence.Repositories;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Domain.Entities
{
    public class GithubProfile : Entity
    {
        public int UserId { get; set; }
        public string GithubUrl { get; set; }
        public User User { get; set; }
        public GithubProfile()
        {

        }

        public GithubProfile(int id, int userId, string githubUrl) : this()
        {
            Id = id;
            UserId = userId;
            GithubUrl = githubUrl;
        }
    }
}
