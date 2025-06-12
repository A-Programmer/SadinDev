using System.Reflection;

namespace KSProject.WebApi;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}