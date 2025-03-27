namespace Fuelrecorder.Models;

public class UserUpdateRequest
{
    public int Id { get; set; }
    public string OldUserName { get; set; }
    public string NewUserName { get; set; }
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
}