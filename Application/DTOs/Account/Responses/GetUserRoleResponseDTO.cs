namespace Application.DTOs.Account.Responses
{
    public class GetUserRoleResponseDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string RoleName { get; set; } = string.Empty;
        public int RoleId { get; set; }

        public GetUserRoleResponseDTO(string name, string email, string roleName, int roleId)
        {
            Name = name;
            Email = email;
            RoleName = roleName;
            RoleId = roleId;
        }
    }
}
