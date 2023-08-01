using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Net;
using TaskTrackerX.TaskApi.Extensions;
using TaskTrackerX.TaskApi.DTOs.Incoming;
using TaskTrackerX.TaskApi.DTOs.Outgoing;
using TaskTrackerX.TaskApi.Managers.StatusManager;
using TaskTrackerX.TaskApi.Models;
using TaskTrackerX.TaskApi.Services;

namespace TaskTrackerX.TaskApi.Controllers
{
    [ApiController]
    [Route("api/status")]
    public class StatusController : ControllerBase
    {
        private readonly IStatusManager _statusManager;
        private readonly IMapper _mapper;

        public StatusController(
            IStatusManager statusManager,
            IMapper mapper)
        {
            _statusManager = statusManager;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetListAsync()
        {
            var listResult = await _statusManager.GetListAsync();

            return this.ToApiResponse(_mapper.Map<IEnumerable<StatusDto>>(listResult));
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _statusManager.FindByIdAsync(id);
            if (result == null)
                return this.ToApiResponseError(errors: ErrorDescriber.InvalidStatus());

            return this.ToApiResponse(_mapper.Map<StatusDto>(result));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAsync([FromBody] StatusCreateUpdateDTO createDTO)
        {
            if (createDTO == null)
                throw new ArgumentNullException(nameof(createDTO));

            var statusCreated = _mapper.Map<Status>(createDTO);

            var result = await _statusManager.CreateAsync(statusCreated);
            if (!result.Succeeded)
                return this.ToApiResponseError(errors: result.Errors.ToArray());

            return this.ToApiResponse(_mapper.Map<StatusDto>(statusCreated));
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] StatusCreateUpdateDTO updateDTO)
        {
            if (updateDTO == null)
                throw new ArgumentNullException(nameof(updateDTO));

            var statusUpdate = await _statusManager.FindByIdAsync(id);
            if (statusUpdate == null)
                return this.ToApiResponseError(errors: ErrorDescriber.InvalidStatus());

            statusUpdate.Name = updateDTO.Name;

            var updateResult = await _statusManager.UpdateAsync(statusUpdate);
            if (!updateResult.Succeeded)
                return this.ToApiResponseError(errors: updateResult.Errors.ToArray());

            return this.ToApiResponse(_mapper.Map<StatusDto>(statusUpdate));
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var status = await _statusManager.FindByIdAsync(id);
            if (status == null)
                return this.ToApiResponseError(errors: ErrorDescriber.InvalidStatus());

            var deleteStatus = await _statusManager.DeleteAsync(status);
            if (!deleteStatus.Succeeded)
                return this.ToApiResponseError(errors: deleteStatus.Errors.ToArray());

            return this.ToApiResponse(true);
        }
    }
}
