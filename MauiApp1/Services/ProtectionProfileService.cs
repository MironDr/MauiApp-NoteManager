using MauiApp1.Models;

namespace MauiApp1.Services;


public interface IProtectionProfileService
{
    event EventHandler ProfilesUpdated;
    List<ProtectionProfileModel> GetProfiles();
    void AddProfile(ProtectionProfileModel profile);
    
    ProtectionProfileModel? GetById(int id);
}

public class ProtectionProfileService : IProtectionProfileService
{
    private readonly List<ProtectionProfileModel> _profiles = new();
    
    public event EventHandler ProfilesUpdated = null!;
    
    public ProtectionProfileService()
    {
        LoadProfiles();
    }

    private void LoadProfiles()
    {
        
    }

    public void AddProfile(ProtectionProfileModel profile)
    {
        _profiles.Add(profile);
        ProfilesUpdated?.Invoke(this, EventArgs.Empty);
    }
    
    public List<ProtectionProfileModel> GetProfiles()
    {
        return _profiles;
    }
    
    public ProtectionProfileModel? GetById(int id)
    {
        return _profiles.FirstOrDefault(c => c.Id == id);
    }

}