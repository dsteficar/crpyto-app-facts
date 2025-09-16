using Application.Contracts.Services;
using Application.DTOs.TradeBots.Requests;
using Application.DTOs.TradeBots.Responses;
using AutoMapper;
using Domain.Entity.Trading.Bots;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/trading-bots")]
    [ApiController]
    public class TradingBotController : ControllerBase
    {
        private ITradingBotTaskService _tradeBotTaskService;
        private ITradingBotSettingsService _tradeBotSettingsService;
        private readonly IMapper _mapper;

        public TradingBotController(ITradingBotTaskService tradeBotTaskService, ITradingBotSettingsService tradeBotSettingsService, IMapper mapper)
        {
            _tradeBotTaskService = tradeBotTaskService;
            _tradeBotSettingsService = tradeBotSettingsService;
            _mapper = mapper;
        }

        [HttpGet("user/{userId}/{skip}/{top}")]
        [Authorize]
        public async Task<ActionResult<GetPagedUserTradingBotsDTO>> GetUserTradingBots(int userId, int skip, int top)
        {

            var getResult = await _tradeBotTaskService.GetPagedByUserIdAsync(userId, skip, top);

            if (!getResult.IsSuccess) return NotFound(getResult.ErrorMessage);

            var userTradeBotTasks = getResult.Value;

            var botList = _mapper.Map<IEnumerable<GetUserTradeBotResponseDTO>>(userTradeBotTasks);

            var response = new GetPagedUserTradingBotsDTO();

            var getCountResult = await _tradeBotTaskService.GetCountByUserIdAsync(userId);

            if (!getCountResult.IsSuccess) return NotFound(getResult.ErrorMessage);

            response.Bots = botList;
            response.TotalCount = getCountResult.Value;

            return Ok(response);
        }

        [HttpGet("settings/{tradingBotId}")]
        [Authorize]
        public async Task<ActionResult<GetUserTradingBotSettingsResponseDTO>> GetUserTradingBotSettings(int tradingBotId)
        {
            var getResult = await _tradeBotTaskService.GetByIdEagerAsync(tradingBotId);

            if (!getResult.IsSuccess) return NotFound(getResult.ErrorMessage);

            var userTradeBot = getResult.Value;

            var response = _mapper.Map<GetUserTradingBotSettingsResponseDTO>(userTradeBot);

            return Ok(response);
        }

        [HttpPost("add")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<AddUserTradeBotResponseDTO>>> AddUserTradingBots([FromBody] AddUserTradeBotRequestDTO request)
        {

            var tradeBotTask = _mapper.Map<TradingBotTask>(request);
            var tradeBotSettings = _mapper.Map<TradingBotSettings>(request);

            // Save the TradeBotTask
            var saveTaskResult = await _tradeBotTaskService.AddAsync(tradeBotTask);

            if (!saveTaskResult.IsSuccess)
                return BadRequest(saveTaskResult.ErrorMessage);

            // Assign the saved Task ID to the TradeBotSettings
            tradeBotSettings.TradingBotTaskId = tradeBotTask.Id;

            // Save the TradeBotSettings
            var saveSettingsResult = await _tradeBotSettingsService.AddAsync(tradeBotSettings);

            if (!saveSettingsResult.IsSuccess)
                return BadRequest(saveSettingsResult.ErrorMessage);

            // Map the saved entities to the response DTO
            var response = _mapper.Map<AddUserTradeBotResponseDTO>(tradeBotTask);
            _mapper.Map(tradeBotSettings, response);

            return Ok(response);
        }

        [HttpPost("update")]
        [Authorize]
        public async Task<ActionResult<UpdateUserTradeBotResponseDTO>> UpdateTradeBot([FromBody] UpdateUserTradeBotRequestDTO request)
        {
            // Fetch the existing TradeBotTask
            var getTaskResult = await _tradeBotTaskService.GetByIdAsync(request.Id);

            if (!getTaskResult.IsSuccess)
                return NotFound(getTaskResult.ErrorMessage);

            var tradeBotTask = getTaskResult.Value;

            // Fetch the corresponding TradeBotSettings
            var getSettingsResult = await _tradeBotSettingsService.GetByTradeBotTaskIdAsync(request.Id);

            if (!getSettingsResult.IsSuccess)
                return NotFound(getSettingsResult.ErrorMessage);

            var tradeBotSettings = getSettingsResult.Value;

            // Map the updates from the request DTO to the entities
            _mapper.Map(request, tradeBotTask);
            _mapper.Map(request, tradeBotSettings);

            // Update TradeBotTask
            var updateTaskResult = await _tradeBotTaskService.UpdateAsync(tradeBotTask);

            if (!updateTaskResult.IsSuccess)
                return BadRequest(updateTaskResult.ErrorMessage);

            // Update TradeBotSettings
            var updateSettingsResult = await _tradeBotSettingsService.UpdateAsync(tradeBotSettings);

            if (!updateSettingsResult.IsSuccess)
                return BadRequest(updateSettingsResult.ErrorMessage);

            // Map the updated entities to the response DTO
            var response = _mapper.Map<UpdateUserTradeBotResponseDTO>(tradeBotTask);
            _mapper.Map(tradeBotSettings, response);

            return Ok(response);
        }


        [HttpDelete("delete/{id}")]
        [Authorize]
        public async Task<ActionResult> DeleteTradeBot(int id)
        {
            // Fetch the TradeBotTask by ID
            var getTaskResult = await _tradeBotTaskService.GetByIdAsync(id);

            if (!getTaskResult.IsSuccess)
                return NotFound(getTaskResult.ErrorMessage);

            var tradeBotTask = getTaskResult.Value;

            // Fetch the associated TradeBotSettings
            var getSettingsResult = await _tradeBotSettingsService.GetByTradeBotTaskIdAsync(id);

            if (!getSettingsResult.IsSuccess)
                return NotFound(getSettingsResult.ErrorMessage);

            var tradeBotSettings = getSettingsResult.Value;

            // Delete the TradeBotSettings
            var deleteSettingsResult = await _tradeBotSettingsService.DeleteAsync(tradeBotSettings);

            if (!deleteSettingsResult.IsSuccess)
                return BadRequest(deleteSettingsResult.ErrorMessage);

            // Delete the TradeBotTask
            var deleteTaskResult = await _tradeBotTaskService.DeleteAsync(tradeBotTask);

            if (!deleteTaskResult.IsSuccess)
                return BadRequest(deleteTaskResult.ErrorMessage);

            return Ok($"Trade bot with ID {id} and its configuration have been deleted.");
        }
    }
}
