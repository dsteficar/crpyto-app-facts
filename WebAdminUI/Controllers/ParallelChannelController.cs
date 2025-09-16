using Microsoft.AspNetCore.Mvc;

namespace WebAdminUI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ParallelChannelController : ControllerBase
    {
        //private readonly IMapper _mapper;
        //private readonly IParallelChannelService _parallelChannelService;

        //public ParallelChannelController(
        //    IMapper mapper,
        //    IParallelChannelService parallelChannelService)
        //{
        //    _mapper = mapper;
        //    _parallelChannelService = parallelChannelService;
        //}


        //[HttpGet("all")]
        //[Authorize]
        //public async Task<ActionResult<List<ParallelChannelTableAdminDTO>>> GetAll()
        //{
        //    var getResult = await _parallelChannelService.GetAllAsync();

        //    if (!getResult.IsSuccess) return NotFound(getResult.ErrorMessage);

        //    var response = _mapper.Map<List<ParallelChannelTableAdminDTO>>(getResult.Value.ToList());

        //    return Ok(response);
        //}

        //[HttpPost("add")]
        //[Authorize]
        //public async Task<ActionResult<ParallelChannelTableAdminDTO>> AddParallelChannel([FromBody] ParallelChannelTableAdminDTO request)
        //{
        //    var parallelChannel = _mapper.Map<ParallelChannel>(request);

        //    await _parallelChannelService.AddAsync(parallelChannel);

        //    var response = _mapper.Map<ParallelChannelTableAdminDTO>(parallelChannel);

        //    return Ok(response);
        //}

        //[HttpPost("update")]
        //[Authorize]
        //public async Task<ActionResult<ParallelChannelTableAdminDTO>> UpdateParallelChannel([FromBody] ParallelChannelTableAdminDTO request)
        //{
        //    var getResult = await _parallelChannelService.GetByIdAsync(request.Id);

        //    if (!getResult.IsSuccess) return NotFound(getResult.ErrorMessage);

        //    var parallelChannel = getResult.Value;

        //    _mapper.Map(request, parallelChannel);

        //    await _parallelChannelService.UpdateAsync(parallelChannel);

        //    return Ok(parallelChannel);
        //}

        //[HttpDelete("delete/{parallelChannelId}")]
        //[Authorize]
        //public async Task<ActionResult> DeleteParallelChannel(int id)
        //{
        //    var getResult = await _parallelChannelService.GetByIdAsync(id);

        //    if (!getResult.IsSuccess) return NotFound(getResult.ErrorMessage);

        //    var parallelChannel = getResult.Value;

        //    await _parallelChannelService.DeleteAsync(parallelChannel);

        //    return Ok("Parallel channel deleted.");
        //}


        //[HttpGet("all")]
        //[Authorize]
        //public async Task<ActionResult<List<ParallelChannelDTO>>> GetAll()
        //{
        //    var parallelChannelEntites = await _parallelChannelService.GetAllAsync();

        //    var responseDtos = _mapper.Map<List<ParallelChannelDTO>>(parallelChannelEntites);

        //    return Ok(responseDtos);
        //}

        //[HttpPost("create")]
        //[Authorize]
        //public async Task<ActionResult<UserDTO>> CreateAsync(ParallelChannelDTO requestDto)
        //{
        //    var entityToDb = _mapper.Map<ParallelChannel>(requestDto);

        //    await _parallelChannelService.AddAsync(entityToDb);

        //    var responseDto = _mapper.Map<ParallelChannelDTO>(requestDto);

        //    return Ok(responseDto);
        //}

        //[HttpPost("update")]
        //[Authorize]
        //public async Task<ActionResult<List<UserDTO>>> UpdateAsync(ParallelChannelDTO requestDto)
        //{
        //    var entityToDb = _mapper.Map<ParallelChannel>(requestDto);

        //    await _parallelChannelService.UpdateAsync(entityToDb);

        //    var responseDto = _mapper.Map<ParallelChannelDTO>(requestDto);

        //    return Ok(responseDto);
        //}

        //[HttpPost("delete")]
        //[Authorize]
        //public async Task<ActionResult<bool>> DeleteAsync(ParallelChannelDTO requestDto)
        //{
        //    var entity = await _parallelChannelService.GetByIdAsync(requestDto.Id);

        //    await _parallelChannelService.DeleteAsync(entity);

        //    return Ok(true);
        //}
    }
}

