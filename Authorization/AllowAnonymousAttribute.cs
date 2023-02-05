namespace GuacAPI.Authorization;

[AttributeUsage(AttributeTargets.Method)]
public abstract class AllowAnonymousAttribute : Attribute
{ }