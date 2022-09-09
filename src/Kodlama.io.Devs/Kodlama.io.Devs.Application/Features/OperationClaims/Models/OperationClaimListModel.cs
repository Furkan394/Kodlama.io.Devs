using Core.Persistence.Paging;
using Kodlama.io.Devs.Application.Features.OperationClaims.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.OperationClaims.Models
{
    public class OperationClaimListModel : BasePageableModel
    {
        public List<OperationClaimListDto>? Items  { get; set; }
    }
}
