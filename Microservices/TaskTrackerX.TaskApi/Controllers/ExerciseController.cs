using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Security.Claims;
using TaskTrackerX.TaskApi.DTOs.Incoming;
using TaskTrackerX.TaskApi.DTOs.Outgoing;
using TaskTrackerX.TaskApi.Managers.ExerciseManager;
using TaskTrackerX.TaskApi.Models;
using TaskTrackerX.TaskApi.Services;
using TaskTrackerX.TaskApi.Services.UserService;

namespace TaskTrackerX.TaskApi.Controllers
{
    [ApiController]
    [Route("api/exercise")]
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseManager _exerciseManager;
        private readonly IMapper _mapper;

        public ExerciseController(
            IExerciseManager exerciseManager,
            IMapper mapper)
        {
            _exerciseManager = exerciseManager;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<ActionResult<ApiResponse<IEnumerable<ExerciseDto>>>> GetListAsync()
        {
            var listResult = await _exerciseManager.GetListAsync();

            return new ApiResponse<IEnumerable<ExerciseDto>>(
                _mapper.Map<IEnumerable<ExerciseDto>>(listResult));
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "Admin, Moderator, User")]
        public async Task<ActionResult<ApiResponse<ExerciseDto>>> GetByIdAsync(Guid id)
        {
            var result = await _exerciseManager.FindByIdAsync(id);
            if (result == null)
                return new ApiResponse<ExerciseDto>(
                    errors: ErrorDescriber.InvalidExercise());

            if (User.IsInRole("User"))
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!result.AssignedToUserId.Equals(userId))
                    return new ApiResponse<ExerciseDto>(
                        errors: ErrorDescriber.DefaultError());
            }

            return new ApiResponse<ExerciseDto>(
                _mapper.Map<ExerciseDto>(result));
        }

        [HttpGet]
        [Route("current-exercise")]
        [Authorize(Roles = "Admin, Moderator, User")]
        public async Task<ActionResult<ApiResponse<IEnumerable<ExerciseDto>>>> GetByTokenAsync()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var convert = Guid.Parse(userId);
            var listResult = await _exerciseManager.GetByAssignedUserIdAsync(convert);

            return new ApiResponse<IEnumerable<ExerciseDto>>(
                _mapper.Map<IEnumerable<ExerciseDto>>(listResult));
        }

        [HttpPut]
        [Route("{exerciseId}/change-status")]
        [Authorize(Roles = "Admin, Moderator, User")]
        public async Task<ActionResult<ApiResponse<ExerciseDto>>> UpdateStatusAsync(Guid exerciseId, [FromBody] ChangeExercireStatusDto exercireStatusDto)
        {
            if (exercireStatusDto == null)
                throw new ArgumentNullException(nameof(exercireStatusDto));
            
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var convert = Guid.Parse(userId);

            var exerciseUpdate = await _exerciseManager.FindByIdAsync(exerciseId);
            if (exerciseUpdate == null)
                return new ApiResponse<ExerciseDto>(
                    errors: ErrorDescriber.InvalidExercise());

            if (!exerciseUpdate.AssignedToUserId.Equals(convert))
                return new ApiResponse<ExerciseDto>(
                    errors: ErrorDescriber.DefaultError());

            exerciseUpdate.ExerciseStatusId = exercireStatusDto.StatusId;

            var updateResult = await _exerciseManager.UpdateAsync(exerciseUpdate);
            if (!updateResult.Succeeded)
                return new ApiResponse<ExerciseDto>(updateResult.Errors);

            return new ApiResponse<ExerciseDto>(
                _mapper.Map<ExerciseDto>(exerciseUpdate));
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<ActionResult<ApiResponse<ExerciseDto>>> CreateAsync([FromBody] ExerciseCreateDto createDTO)
        {
            if (createDTO == null)
                throw new ArgumentNullException(nameof(createDTO));
            
            var statusCreated = _mapper.Map<Exercise>(createDTO);

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var convert = Guid.Parse(userId);

            statusCreated.CreatedByUserId = convert;

            var result = await _exerciseManager.CreateAsync(statusCreated);
            if (!result.Succeeded)
                return new ApiResponse<ExerciseDto>(result.Errors);

            return new ApiResponse<ExerciseDto>(
                _mapper.Map<ExerciseDto>(statusCreated));
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<ActionResult<ApiResponse<ExerciseDto>>> UpdateAsync(Guid id, [FromBody] ExerciseUpdateDto updateDTO)
        {
            if (updateDTO == null)
                throw new ArgumentNullException(nameof(updateDTO));

            var exerciseUpdate = await _exerciseManager.FindByIdAsync(id);
            if (exerciseUpdate == null)
                return new ApiResponse<ExerciseDto>(
                    errors: ErrorDescriber.InvalidExercise());

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
            if (!updateResult.Succeeded)
                return new ApiResponse<ExerciseDto>(updateResult.Errors);

            return new ApiResponse<ExerciseDto>(
                _mapper.Map<ExerciseDto>(exerciseUpdate));
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin, Moderator")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteAsync(Guid id)
        {
            var exercise = await _exerciseManager.FindByIdAsync(id);
            if (exercise == null)
                return new ApiResponse<bool>(errors: ErrorDescriber.InvalidExercise());

            var deleteExercise = await _exerciseManager.DeleteAsync(exercise);
            if (!deleteExercise.Succeeded)
                return new ApiResponse<bool>(deleteExercise.Errors);

            return new ApiResponse<bool>(true);
        }
    }
}
