using KSProject.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace KSProject.Presentation.Services;
public class PermissionDiscoveryService : IPermissionDiscoveryService
{
	private readonly Dictionary<string, List<string>> _groupedPermissions;

	public PermissionDiscoveryService()
	{
		var assembly = AssemblyReference.Assembly;
		if (assembly == null) throw new Exception("Presentation assembly not found");

		var controllerTypes = assembly.GetTypes()
			.Where(t => typeof(ControllerBase).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);

		_groupedPermissions = new Dictionary<string, List<string>>();

		foreach (var controllerType in controllerTypes)
		{
			var controllerName = controllerType.Name.Replace("Controller", string.Empty);
			var actions = controllerType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
				.Where(m => m.IsDefined(typeof(Presentation.Attributes.PermissionAttribute), true));

			var permissions = actions.SelectMany(m =>
			{
				var attr = m.GetCustomAttribute<Presentation.Attributes.PermissionAttribute>();
				return attr.Permissions;
			}).Distinct().ToList();

			if (permissions.Any())
			{
				_groupedPermissions[controllerName] = permissions;
			}
		}
	}

	public Dictionary<string, List<string>> GetGroupedPermissions()
	{
		return new Dictionary<string, List<string>>(_groupedPermissions);
	}
}
