namespace FP.Core.Api.ApiDto;

public class UserDto
{
    public string Name { get; set; } = "";
    public string Passwordhash { get; set; } = "";
    public string Email { get; set; } = "";

    public override string ToString()
    {
        return $"ObjectId - {Email} | Type - UserDto";
    }
}
