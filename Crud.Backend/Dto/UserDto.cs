namespace Crud.Backend.Dto;

public record CreateUserDto
{
    public string Name { get; set; } = string.Empty;
}

public record PutUserDto
{
    public string Name { get; set; } = string.Empty;
}