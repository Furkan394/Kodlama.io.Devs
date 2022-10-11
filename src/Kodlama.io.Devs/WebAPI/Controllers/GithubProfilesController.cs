using Core.Application.Requests;
using Kodlama.io.Devs.Application.Features.GithubProfiles.Commands.CreateGithubProfile;
using Kodlama.io.Devs.Application.Features.GithubProfiles.Commands.DeleteGithubProfile;
using Kodlama.io.Devs.Application.Features.GithubProfiles.Commands.UpdateGithubProfile;
using Kodlama.io.Devs.Application.Features.GithubProfiles.Dtos;
using Kodlama.io.Devs.Application.Features.GithubProfiles.Models;
using Kodlama.io.Devs.Application.Features.GithubProfiles.Queries.GetByIdGithubProfile;
using Kodlama.io.Devs.Application.Features.GithubProfiles.Queries.GetListGithubProfile;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Commands.DeleteProgrammingLanguage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GithubProfilesController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListGithubProfileQuery getListGithubProfileQuery = new() { PageRequest = pageRequest };
            GithubProfileListModel result = await Mediator.Send(getListGithubProfileQuery);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Get([FromRoute] GetByIdGithubProfileQuery getByIdGithubProfileQuery)
        {
            GithubProfileGetByIdDto result = await Mediator.Send(getByIdGithubProfileQuery);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateGithubProfileCommand createGithubProfileCommand)
        {
            CreatedGithubProfileDto result = await Mediator.Send(createGithubProfileCommand);
            return Created("", result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] UpdateGithubProfileCommand updateGithubProfileCommand)
        {
            UpdatedGithubProfileDto result = await Mediator.Send(updateGithubProfileCommand);
            return Ok(result);

        }

        [HttpPost("delete/{Id}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteGithubProfileCommand deleteGithubProfileCommand)
        {
            await Mediator.Send(deleteGithubProfileCommand);
            return Ok();
        }
    }
}
