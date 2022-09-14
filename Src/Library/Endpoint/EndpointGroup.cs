﻿namespace FastEndpoints;

/// <summary>
/// common configuration for a group of endpoints can be specified by implementing this abstract class and calling <see cref="Configure(string, Action{EndpointDefinition})"/> in the constructor.
/// </summary>
public abstract class EndpointGroup
{
    internal Action<EndpointDefinition> Action { get; set; }

    /// <summary>
    /// call this method in the constructor in order to configure the endpoint group.
    /// </summary>
    /// <param name="routePrefix">the route prefix for the group</param>
    /// <param name="ep">the configuration action to be performed on the <see cref="EndpointDefinition"/></param>
    protected void Configure(string routePrefix, Action<EndpointDefinition> ep)
        => Action = RouteModifier(routePrefix) + ep;

    private static Action<EndpointDefinition> RouteModifier(string routePrefix) => e =>
    {
        if (e.Routes?.Length > 0)
        {
            for (var i = 0; i < e.Routes.Length; i++)
            {
                var route = e.Routes[i];
                var slash = !routePrefix.EndsWith("/") && !route.StartsWith("/") ? "/" : "";
                e.Routes[i] = routePrefix + slash + route;
            }
        }
    };
}