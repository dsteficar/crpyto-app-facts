using Domain.Entity.Trading.Graphs;
using Radzen;
using Radzen.Blazor;
using System.Linq.Dynamic.Core;
using WebAdminUI.Models.ParallelChannels;

namespace WebAdminUI.Components.Pages.ParallelChannels
{
    public partial class ParallelChannelManagement
    {
        private RadzenDataGrid<ParallelChannelDataGridAdminModel> parallelChannelGrid = new();
        private List<ParallelChannelDataGridAdminModel> parallelChannels = new();

        List<ParallelChannelDataGridAdminModel> parallelChannelsToInsert = new();
        List<ParallelChannelDataGridAdminModel> parallelChannelsToUpdate = new();

        private DataGridEditMode editMode = DataGridEditMode.Single;

        protected override async Task OnInitializedAsync()
        {
            var getResult = await ParallelChannelService.GetAllAsync();

            if (!getResult.IsSuccess) return;

            var parallelChannelEntityList = getResult.Value;
            parallelChannels = Mapper.Map<List<ParallelChannelDataGridAdminModel>>(parallelChannelEntityList);
        }

        void Reset()
        {
            parallelChannelsToInsert.Clear();
            parallelChannelsToUpdate.Clear();
        }

        void Reset(ParallelChannelDataGridAdminModel parallelChannel)
        {
            parallelChannelsToInsert.Remove(parallelChannel);
            parallelChannelsToUpdate.Remove(parallelChannel);
        }

        async Task EditRow(ParallelChannelDataGridAdminModel parallelChannel)
        {
            if (editMode == DataGridEditMode.Single && parallelChannelsToInsert.Count() > 0)
            {
                Reset();
            }

            parallelChannelsToUpdate.Add(parallelChannel);
            await parallelChannelGrid.EditRow(parallelChannel);
        }

        async Task OnUpdateRow(ParallelChannelDataGridAdminModel parallelChannel)
        {
            Reset(parallelChannel);

            var getByIdResult = await ParallelChannelService.GetByIdAsync(parallelChannel.Id);

            if (!getByIdResult.IsSuccess) return;

            var entityInDb = getByIdResult.Value;

            Mapper.Map(parallelChannel, entityInDb);

            var updateResult = await ParallelChannelService.UpdateAsync(entityInDb);

            if (!updateResult.IsSuccess) return;

            var updatedParallelChannel = Mapper.Map<ParallelChannelDataGridAdminModel>(entityInDb);

            var index = parallelChannels.FindIndex(u => u.Id == parallelChannel.Id);

            if (index != -1)
            {
                parallelChannels[index] = updatedParallelChannel;
                await parallelChannelGrid.Reload();
            }
        }

        async Task SaveRow(ParallelChannelDataGridAdminModel parallelChannel)
        {
            await parallelChannelGrid.UpdateRow(parallelChannel);
        }

        void CancelEdit(ParallelChannelDataGridAdminModel parallelChannel)
        {
            Reset(parallelChannel);

            parallelChannelGrid.CancelEditRow(parallelChannel);
        }

        async Task DeleteRow(ParallelChannelDataGridAdminModel parallelChannel)
        {
            Reset(parallelChannel);

            if (parallelChannels.Contains(parallelChannel))
            {
                var getByIdResult = await ParallelChannelService.GetByIdAsync(parallelChannel.Id);

                if (!getByIdResult.IsSuccess) return;

                var entityToDelete = getByIdResult.Value;

                var deleteResult = await ParallelChannelService.DeleteAsync(entityToDelete);

                if (!deleteResult.IsSuccess) return;

                parallelChannels.Remove(parallelChannel);
                await parallelChannelGrid.Reload();
            }
            else
            {
                parallelChannelGrid.CancelEditRow(parallelChannel);
                await parallelChannelGrid.Reload();
            }
        }

        async Task InsertRow()
        {
            if (editMode == DataGridEditMode.Single)
            {
                Reset();
            }

            var parallelChannel = new ParallelChannelDataGridAdminModel();
            parallelChannelsToInsert.Add(parallelChannel);
            await parallelChannelGrid.InsertRow(parallelChannel);
        }

        async Task OnCreateRow(ParallelChannelDataGridAdminModel parallelChannel)
        {
            var entityToDb = new ParallelChannel();

            Mapper.Map(parallelChannel, entityToDb);

            var addResult = await ParallelChannelService.AddAsync(entityToDb);

            if (!addResult.IsSuccess) return;

            var newParallelChannel = new ParallelChannelDataGridAdminModel();

            Mapper.Map(entityToDb, newParallelChannel);

            parallelChannelsToInsert.Remove(parallelChannel);
            parallelChannels.Add(newParallelChannel);

            await parallelChannelGrid.Reload();
        }
    }
}
