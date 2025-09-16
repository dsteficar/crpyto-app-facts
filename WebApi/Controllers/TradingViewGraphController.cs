using Application.Contracts.Services;
using Application.DTOs.ParallelChannel.Requests;
using Application.DTOs.ParallelChannel.Responses;
using AutoMapper;
using Domain.Entity.Trading.Graphs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TradingViewGraphController : ControllerBase
    {
        private readonly ITradingDataService _tradingDataService;
        private readonly IParallelChannelService _parallelChannelService;
        private readonly IMapper _mapper;

        public TradingViewGraphController(ITradingDataService tradingDataService, IParallelChannelService parallelChannelService, IMapper mapper)
        {
            _tradingDataService = tradingDataService;
            _parallelChannelService = parallelChannelService;
            _mapper = mapper;
        }

        [HttpGet("futures/symbols/all")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<string>>> GetAllFuturesPairSymbols()
        {

            var getResult = await _tradingDataService.GetAllFuturesPairsAsync();

            if (!getResult.IsSuccess) return NotFound(getResult.ErrorMessage);

            var futurePairSymbols = getResult.Value;

            return Ok(futurePairSymbols);
        }

        [HttpPost("channels/parallels/add")]
        [Authorize]
        public async Task<ActionResult<AddParallelChannelResponseDTO>> AddParallelChannel([FromBody] AddParallelChannelRequestDTO request)
        {
            var parallelChannel = _mapper.Map<ParallelChannel>(request); 

            await _parallelChannelService.AddAsync(parallelChannel);

            var response = _mapper.Map<AddParallelChannelResponseDTO>(parallelChannel);

            return Ok(response);
        }


        [HttpDelete("channels/parallels/delete/{parallelChannelId}")]
        [Authorize]
        public async Task<ActionResult> DeleteParallelChannel(int parallelChannelId)
        {
            var getResult = await _parallelChannelService.GetByIdAsync(parallelChannelId);

            if (!getResult.IsSuccess) return NotFound(getResult.ErrorMessage);

            var parallelChannel = getResult.Value;

            await _parallelChannelService.DeleteAsync(parallelChannel);

            return Ok("Parallel channel deleted.");
        }

        [HttpPost("channels/parallels/update")]
        [Authorize]
        public async Task<ActionResult<AddParallelChannelResponseDTO>> UpdateParallelChannel([FromBody] UpdateParallelChannelRequestDTO request)
        {
            var getResult = await _parallelChannelService.GetByIdAsync(request.Id);

            if (!getResult.IsSuccess) return NotFound(getResult.ErrorMessage); 

            var parallelChannel = getResult.Value;

            _mapper.Map(request, parallelChannel);

            await _parallelChannelService.UpdateAsync(parallelChannel);

            return Ok(parallelChannel);
        }


        [HttpGet("users/{userId}/channels/parallels")]
        [Authorize]
        public async Task<ActionResult<List<GetUserParallelChannelResponseDTO>>> GetUserParallelChannels(int userId, [FromQuery] decimal startTimestamp)
        {
            var getResult = await _parallelChannelService.GetUserParallelChannels(userId, startTimestamp);

            if (!getResult.IsSuccess) return NotFound(getResult.ErrorMessage);

            var response = _mapper.Map<List<GetUserParallelChannelResponseDTO>>(getResult.Value);

            return Ok(response);
        }
    }
}
