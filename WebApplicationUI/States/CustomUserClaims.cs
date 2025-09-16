namespace WebApplicationUI.States
{
    public record CustomUserClaims(string NameIdentifier = null!, string Name = null!, string Email = null!, DateTime Expiration = default);
}
