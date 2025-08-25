namespace KSProject.Application.Contracts;
public interface IPermissionDiscoveryService
{
	Dictionary<string, List<string>> GetGroupedPermissions();
}
