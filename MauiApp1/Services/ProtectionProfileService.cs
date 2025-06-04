using MauiApp1.Models;
using MauiApp1.Repositories;

namespace MauiApp1.Services;


public interface IProtectionProfileService
{
    event EventHandler ProfilesUpdated;
    List<ProtectionProfileModel> GetProfiles();
    Task AddProfile(ProtectionProfileModel profile);
    
    ProtectionProfileModel? GetById(int id);
    
    Task DeleteProfile(ProtectionProfileModel profile);
}

public class ProtectionProfileService : IProtectionProfileService
{
    private readonly IDatabaseRepository _repository;
    private List<ProtectionProfileModel> _profiles = new();
    
    public event EventHandler ProfilesUpdated = null!;
    
    public ProtectionProfileService(IDatabaseRepository repository)
    {
        _repository = repository;
        _ = LoadProfiles();
    }

    private async Task LoadProfiles()
    {
        try
        {
            _profiles = await _repository.GetEntitiesAsync<ProtectionProfileModel>();
            ProfilesUpdated?.Invoke(this, EventArgs.Empty);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public async Task AddProfile(ProtectionProfileModel profile)
    {
        bool titleExists = _profiles.Any(p =>
            p.ProfileName.Equals(profile.ProfileName, StringComparison.OrdinalIgnoreCase) &&
            p.Id != profile.Id 
        );

        if (titleExists)
            throw new InvalidOperationException($"Profile with title '{profile.ProfileName}' already exists.");
        
        await _repository.SaveNewEntityAsync(profile);
        await LoadProfiles();
    }
    
    public List<ProtectionProfileModel> GetProfiles()
    {
        return _profiles;
    }
    
    public ProtectionProfileModel? GetById(int id)
    {
        return _profiles.FirstOrDefault(c => c.Id == id);
    }

    public async Task DeleteProfile(ProtectionProfileModel profile)
    {
        profile.UnlinkAssociations();
        await _repository.DeleteEntityAsync(profile);
        await LoadProfiles();
    }
}