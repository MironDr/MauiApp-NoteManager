using MauiApp1.Base;

namespace MauiApp1.DTOs;

public class ProtectionProfileDto : BaseCommon
{
    public string ProfileName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    
    public string PasswordRepeat { get; set; } = string.Empty;
}