namespace Application.DTOs.Account.Requests
{
    public class UpdateUserRoleRequestDTO
    {

        public string EmailAddress { get; set; }

        public string RoleName { get; set; }

        public UpdateUserRoleRequestDTO(string emailAddress, string roleName)
        {
            EmailAddress = emailAddress;

            RoleName = roleName;
        }
    }
}
