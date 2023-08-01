using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.ComponentModel;
using System.IO;
using System.Security.Claims;
using TaskTrackerX.TaskApi.DTOs.Incoming;
using TaskTrackerX.TaskApi.DTOs.Outgoing;
using TaskTrackerX.TaskApi.Extensions;
using TaskTrackerX.TaskApi.Managers.ExerciseManager;
using TaskTrackerX.TaskApi.Models;
using TaskTrackerX.TaskApi.Models.Options;
using TaskTrackerX.TaskApi.Models.Query;
using TaskTrackerX.TaskApi.Services;
using TaskTrackerX.TaskApi.Services.UserService;

namespace TaskTrackerX.TaskApi.Controllers
{
    [ApiController]
    [Route("api/exercise")]
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseManager _exerciseManager;
        private readonly IOptions<SettingOptions> _options;
        private readonly IMapper _mapper;

        public ExerciseController(
            IExerciseManager exerciseManager,
            IOptions<SettingOptions> options,
            IMapper mapper)
        {
            _exerciseManager = exerciseManager;
            this._options = options;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> GetListAsync([FromQuery] ExerciseParametersDto filterOptions)
        {
            var listResult = await _exerciseManager.GetListAsync(
                filterOptions.ConvertToFilterOptions(_options));

            return this.ToApiResponse(_mapper.Map<PagedResult<ExerciseDto>>(listResult));
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Admin, Moderator, User")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var result = await _exerciseManager.FindByIdAsync(id);
            if (result == null)
                return this.ToApiResponseError(errors: ErrorDescriber.InvalidExercise());

            if (User.IsInRole("User"))
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!result.AssignedToUserId.Equals(userId))
                    return this.ToApiResponseError(errors: ErrorDescriber.DefaultError());
            }

            return this.ToApiResponse(_mapper.Map<ExerciseDto>(result));
        }

        [HttpGet]
        [Route("current-exercise")]
        [Authorize(Roles = "Admin, Moderator, User")]
        public async Task<IActionResult> GetByTokenAsync([FromQuery] QueryParametersDto queryParameters)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                throw new ArgumentNullException(nameof(userId));

            var convert = Guid.Parse(userId);

            return await GetListAsync(new ExerciseParametersDto()
            {
                PageNumber = queryParameters.PageNumber,
                PageSize = queryParameters.PageSize,
                AssignedToUserId = convert
            });
        }

        [HttpPut]
        [Route("{exerciseId}/change-status")]
        [Authorize(Roles = "Admin, Moderator, User")]
        public async Task<IActionResult> UpdateStatusAsync(Guid exerciseId, [FromBody] ChangeExercireStatusDto exercireStatusDto)
        {
            if (exercireStatusDto == null)
                throw new ArgumentNullException(nameof(exercireStatusDto));

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                throw new ArgumentNullException(nameof(userId));

            var convert = Guid.Parse(userId);

            var exerciseUpdate = await _exerciseManager.FindByIdAsync(exerciseId);
            if (exerciseUpdate == null)
                return this.ToApiResponseError(errors: ErrorDescriber.InvalidExercise());

            if (!exerciseUpdate.AssignedToUserId.Equals(convert))
                return this.ToApiResponseError(errors: ErrorDescriber.DefaultError());

            exerciseUpdate.ExerciseStatusId = exercireStatusDto.StatusId;

            var updateResult = await _exerciseManager.UpdateAsync(exerciseUpdate);

            return updateResult.Succeeded ? 
                this.ToApiResponse(_mapper.Map<ExerciseDto>(exerciseUpdate)) :
                this.ToApiResponseError(errors: updateResult.Errors.ToArray());
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> CreateAsync([FromBody] ExerciseCreateDto createDTO)
        {
            if (createDTO == null)
                throw new ArgumentNullException(nameof(createDTO));

            var statusCreated = _mapper.Map<Exercise>(createDTO);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                throw new ArgumentNullException(nameof(userId));

            var convert = Guid.Parse(userId);

            statusCreated.CreatedByUserId = convert;

            var createdResult = await _exerciseManager.CreateAsync(statusCreated);

            return createdResult.Succeeded ? 
                this.ToApiResponse(_mapper.Map<ExerciseDto>(statusCreated)) :
                this.ToApiResponseError(errors: createdResult.Errors.ToArray());
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] ExerciseUpdateDto updateDTO)
        {
            if (updateDTO == null)
                throw new ArgumentNullException(nameof(updateDTO));

            var exerciseUpdate = await _exerciseManager.FindByIdAsync(id);
            if (exerciseUpdate == null)
                return this.ToApiResponseError(errors: ErrorDescriber.InvalidExercise());

            if (updateDTO.Description != null)
                exerciseUpdate.Description = updateDTO.Description;

            if (updateDTO.Subject != null)
                exerciseUpdate.Subject = updateDTO.Subject;

            if (updateDTO.ReceivedDate != null)
                exerciseUpdate.ReceivedDate = updateDTO.ReceivedDate.Value;

            if (updateDTO.Deadline != null)
                exerciseUpdate.Deadline = updateDTO.Deadline.Value;

            if (updateDTO.ExerciseStatusId != null)
                exerciseUpdate.ExerciseStatusId = updateDTO.ExerciseStatusId.Value;

            var updateResult = await _exerciseManager.UpdateAsync(exerciseUpdate);

            return updateResult.Succeeded ? 
                this.ToApiResponse(_mapper.Map<ExerciseDto>(exerciseUpdate)) :
                this.ToApiResponseError(errors: updateResult.Errors.ToArray());
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var exercise = await _exerciseManager.FindByIdAsync(id);
            if (exercise == null)
                return this.ToApiResponseError(errors: ErrorDescriber.InvalidExercise());

            var deleteResult = await _exerciseManager.DeleteAsync(exercise);

            return deleteResult.Succeeded ?
                this.ToApiResponse(true) :
                this.ToApiResponseError(errors: deleteResult.Errors.ToArray());
        }
    }
}
