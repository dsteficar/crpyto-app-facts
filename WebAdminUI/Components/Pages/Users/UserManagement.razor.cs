using Domain.Entity.Authentication;
using Radzen;
using Radzen.Blazor;
using WebAdminUI.Models.Users;

namespace WebAdminUI.Components.Pages.Users
{
    public partial class UserManagement
    {
        private RadzenDataGrid<UserDataGridAdminModel> usersGrid = new();
        private List<UserDataGridAdminModel> users = new();

        List<UserDataGridAdminModel> usersToInsert = new();
        List<UserDataGridAdminModel> usersToUpdate = new();

        private DataGridEditMode editMode = DataGridEditMode.Single;

        protected override async Task OnInitializedAsync()
        {
            var getResult = await UserService.GetAllAsync();

            if (!getResult.IsSuccess) return;

            var userEntityList = getResult.Value;
            users = Mapper.Map<List<UserDataGridAdminModel>>(userEntityList);
        }

        void Reset()
        {
            usersToInsert.Clear();
            usersToUpdate.Clear();
        }

        void Reset(UserDataGridAdminModel user)
        {
            usersToInsert.Remove(user);
            usersToUpdate.Remove(user);
        }

        async Task EditRow(UserDataGridAdminModel user)
        {
            if (editMode == DataGridEditMode.Single && usersToInsert.Count() > 0)
            {
                Reset();
            }

            usersToUpdate.Add(user);
            await usersGrid.EditRow(user);
        }

        async Task OnUpdateRow(UserDataGridAdminModel user)
        {
            Reset(user);

            var getByIdResult = await UserService.GetByIdAsync(user.Id);

            if (!getByIdResult.IsSuccess) return;

            var entityInDb = getByIdResult.Value;

            Mapper.Map(user, entityInDb);

            var updateResult = await UserService.UpdateAsync(entityInDb, entityInDb.PasswordHash);

            if (!updateResult.IsSuccess) return;

            var updatedUser = Mapper.Map<UserDataGridAdminModel>(entityInDb);

            var index = users.FindIndex(u => u.Id == user.Id);

            if (index != -1)
            {
                users[index] = updatedUser;
                await usersGrid.Reload();
            }
        }


        async Task SaveRow(UserDataGridAdminModel user)
        {
            await usersGrid.UpdateRow(user);
        }

        void CancelEdit(UserDataGridAdminModel user)
        {
            Reset(user);

            usersGrid.CancelEditRow(user);
        }

        async Task DeleteRow(UserDataGridAdminModel user)
        {
            Reset(user);

            if (users.Contains(user))
            {
                var getByIdResult = await UserService.GetByIdAsync(user.Id);

                if (!getByIdResult.IsSuccess) return;

                var entityToDelete = getByIdResult.Value;

                var deleteResult = await UserService.DeleteAsync(entityToDelete);

                if (!deleteResult.IsSuccess) return;

                users.Remove(user);
                await usersGrid.Reload();
            }
            else
            {
                usersGrid.CancelEditRow(user);
                await usersGrid.Reload();
            }
        }

        async Task InsertRow()
        {
            if (editMode == DataGridEditMode.Single)
            {
                Reset();
            }

            var user = new UserDataGridAdminModel();
            usersToInsert.Add(user);
            await usersGrid.InsertRow(user);
        }

        async Task OnCreateRow(UserDataGridAdminModel user)
        {
            var entityToDb = new ApplicationUser();

            Mapper.Map(user, entityToDb);

            var addResult = await UserService.AddAsync(entityToDb, entityToDb.PasswordHash);

            if (!addResult.IsSuccess) return;

            var newUser = new UserDataGridAdminModel();

            Mapper.Map(entityToDb, newUser);

            usersToInsert.Remove(user);
            users.Add(newUser);
            await usersGrid.Reload();
        }
    }
}


