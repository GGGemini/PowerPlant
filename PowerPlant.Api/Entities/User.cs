namespace PowerPlant.Api.Entities;

public class User
{
    public int Id { get; set; }

    public string Surname { get; set; } = default!;

    public string FirstName { get; set; } = default!;

    public string Patronymic { get; set; } = default!;

    public string Email { get; set; } = default!;

    public string PasswordHash { get; set; } = default!;

    public int RoleId { get; set; }
    public Role Role { get; set; } = default!;
}