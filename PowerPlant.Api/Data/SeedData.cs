using PowerPlant.Api.Entities;

namespace PowerPlant.Api.Data;

public static class SeedData
{
    public static readonly DateTime SeedNow = new(2025, 8, 4, 0, 0, 0, DateTimeKind.Utc);
    
    private const int AdminRoleId = 1;
    private const int UserRoleId = 2;
    
    public const string AdminEmail = "tanchikipro7777777@gmail.com";
    public const string AdminPassword = "MainAdmin341";
    // вычисляем Hash пароля заранее, чтобы не ловить ошибку PendingModelChangesWarning
    // ничего не генерим "на лету", всё - константы
    public const string AdminPasswordHash =
        "$2a$11$NSQdN6n7l1b0q6wOyJ/zDO0sVuED8h2VD8wJEDTSX51ryd8l7OKTS"; // MainAdmin341

    public const string UserEmail = "cheburashka@gmail.com";
    public const string UserPassword = "SimpleCheburashka732";
    public const string UserPasswordHash =
        "$2a$11$s22XKQRuh81CSJPkU9TY6eVCR8GtcuAGeCHeWvFVfs9fNTTl6n8Xe"; // SimpleCheburashka732

    public static readonly Role[] Roles =
    [
        new() { Id = AdminRoleId, Name = "Admin" },
        new() { Id = UserRoleId, Name = "User" }
    ];

    public static readonly User[] AdminUsers =
    [
        new User
        {
            Id = 1,
            Surname = "Главный",
            FirstName = "Админ",
            Email = AdminEmail,
            PasswordHash = AdminPasswordHash,
            RoleId = AdminRoleId
        }
    ];

    public static readonly User[] SimpleUsers =
    [
        new User
        {
            Id = 2,
            Surname = "Простой",
            FirstName = "Пользователь",
            Email = UserEmail,
            PasswordHash = UserPasswordHash,
            RoleId = UserRoleId
        }
    ];
}