using Core.Persistence.Paging;
using Kodlama.io.Devs.Application.Features.GithubProfiles.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.GithubProfiles.Models
{
    public class GithubProfileListModel : BasePageableModel
    {
        public List<GithubProfileListDto> Items { get; set; }
    }
}
