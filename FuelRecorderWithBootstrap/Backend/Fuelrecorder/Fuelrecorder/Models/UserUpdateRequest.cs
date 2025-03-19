namespace Fuelrecorder.Models;

public class UserUpdateRequest
{
    public int Id { get; set; }
    public string NewUsername { get; set; }
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
}