using WebAdminUI.Models.ParallelChannels;

namespace WebAdminUI.Services.ParallelChannels
{
    public interface IParallelChannelsClientService
    {
        Task<List<ParallelChannelDataGridAdminModel>> GetAllAsync();

        Task<ParallelChannelDataGridAdminModel> AddAsync(ParallelChannelDataGridAdminModel requestDto);

        Task<ParallelChannelDataGridAdminModel> UpdateAsync(ParallelChannelDataGridAdminModel requestDto);

        Task<bool> DeleteAsync(int id);
    }
}
