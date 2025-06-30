namespace HareDu.Internal;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Core.Extensions;
using Extensions;
using Model;

class VirtualHostImpl :
    BaseBrokerImpl,
    VirtualHost
{
    public VirtualHostImpl(HttpClient client)
        : base(client)
    {
    }

    public async Task<Results<VirtualHostInfo>> GetAll(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await GetAllRequest<VirtualHostInfo>("api/vhosts", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result<VirtualHostInfo>> Get(string vhost, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();
        string sanitizedVHost = vhost.ToSanitizedName();

        errors.AddIfTrue(sanitizedVHost, string.IsNullOrWhiteSpace, Errors.Create("The name of the virtual host is missing."));

        if (errors.HaveBeenFound())
            return Response.Panic<VirtualHostInfo>("/api/vhosts/{vhost}", errors);

        return await GetRequest<VirtualHostInfo>($"/api/vhosts/{sanitizedVHost}", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Create(string vhost, Action<VirtualHostConfigurator> configurator = null, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();
        string sanitizedVHost = vhost.ToSanitizedName();

        errors.AddIfTrue(sanitizedVHost, string.IsNullOrWhiteSpace, Errors.Create("The name of the virtual host is missing."));

        VirtualHostRequest request = null;

        if (configurator is not null)
        {
            var impl = new VirtualHostConfiguratorImpl();
            configurator(impl);

            request = impl.Request.Value;
        }

        if (errors.HaveBeenFound())
            return Response.Panic("api/vhosts/{vhost}", errors, request.ToJsonString());

        return await PutRequest($"api/vhosts/{sanitizedVHost}", request, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Delete(string vhost, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();
        string sanitizedVHost = vhost.ToSanitizedName();

        errors.AddIfTrue(sanitizedVHost, string.IsNullOrWhiteSpace, Errors.Create("The name of the virtual host is missing."));
        errors.AddIfTrue(sanitizedVHost, x => x.Equals("2%f"), Errors.Create("Cannot delete the default virtual host."));

        if (errors.HaveBeenFound())
            return Response.Panic("api/vhosts/{vhost}", errors);

        return await DeleteRequest($"api/vhosts/{sanitizedVHost}", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Startup(string vhost, string node, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();
        string sanitizedVHost = vhost.ToSanitizedName();

        errors.AddIfTrue(node, string.IsNullOrWhiteSpace, Errors.Create("RabbitMQ node is missing."));
        errors.AddIfTrue(sanitizedVHost, string.IsNullOrWhiteSpace, Errors.Create("The name of the virtual host is missing."));

        if (errors.HaveBeenFound())
            return Response.Panic("/api/vhosts/{vhost}/start/{node}", errors);

        return await PostEmptyRequest($"/api/vhosts/{sanitizedVHost}/start/{node}", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Results<VirtualHostLimitsInfo>> GetAllLimits(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await GetAllRequest<VirtualHostLimitsInfo>("api/vhost-limits", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> DefineLimit(string vhost, Action<VirtualHostLimitsConfigurator> configurator = null,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (configurator is null)
            return Response.Panic("api/vhost-limits/{vhost}/{limit}", [Errors.Create("The name of the virtual host is missing.")]);

        var impl = new VirtualHostLimitsConfiguratorImpl();
        configurator(impl);

        string sanitizedVHost = vhost.ToSanitizedName();
        var errors = impl.Validate();

        errors.AddIfTrue(sanitizedVHost, string.IsNullOrWhiteSpace, Errors.Create("The name of the virtual host is missing."));

        var request = new VirtualHostLimitsRequest{Value = impl.LimitValue};

        if (errors.HaveBeenFound())
            return Response.Panic("api/vhost-limits/{vhost}/{limit}", errors, request.ToJsonString());

        return await PutRequest($"api/vhost-limits/{sanitizedVHost}/{impl.Limit}", request, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> DeleteLimit(string vhost, VirtualHostLimit limit, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();
        string sanitizedVHost = vhost.ToSanitizedName();

        errors.AddIfTrue(sanitizedVHost, string.IsNullOrWhiteSpace, Errors.Create("The name of the virtual host is missing."));

        if (errors.HaveBeenFound())
            return Response.Panic("api/vhost-limits/{vhost}/{limit}", errors);

        return await DeleteRequest($"api/vhost-limits/{sanitizedVHost}/{limit.Convert()}", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Results<VirtualHostPermissionInfo>> GetAllPermissions(string vhost, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();
        string sanitizedVHost = vhost.ToSanitizedName();

        errors.AddIfTrue(sanitizedVHost, string.IsNullOrWhiteSpace, Errors.Create("The name of the virtual host is missing."));

        if (errors.HaveBeenFound())
            return Responses.Panic<VirtualHostPermissionInfo>("api/vhosts/{vhost}/permissions", errors);

        return await GetAllRequest<VirtualHostPermissionInfo>($"api/vhosts/{sanitizedVHost}/permissions", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result<VirtualHostPermissionInfo>> GetUserPermissions(string vhost, string username, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();
        string sanitizedVHost = vhost.ToSanitizedName();

        errors.AddIfTrue(username, string.IsNullOrWhiteSpace, Errors.Create("The name of the user is missing."));
        errors.AddIfTrue(sanitizedVHost, string.IsNullOrWhiteSpace, Errors.Create("The name of the virtual host is missing."));

        if (errors.HaveBeenFound())
            return Response.Panic<VirtualHostPermissionInfo>("api/permissions/vhost/user", errors);

        return await GetRequest<VirtualHostPermissionInfo>($"api/permissions/{sanitizedVHost}/{username}", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Results<VirtualHostTopicPermissionInfo>> GetTopicPermissions(string vhost, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();
        string sanitizedVHost = vhost.ToSanitizedName();

        errors.AddIfTrue(sanitizedVHost, string.IsNullOrWhiteSpace, Errors.Create("The name of the virtual host is missing."));

        if (errors.HaveBeenFound())
            return Responses.Panic<VirtualHostTopicPermissionInfo>("api/vhosts/{vhost}/topic-permissions", errors);

        return await GetAllRequest<VirtualHostTopicPermissionInfo>($"api/vhosts/{sanitizedVHost}/topic-permissions", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> ApplyPermissions(string username, string vhost,
        Action<UserPermissionsConfigurator> configurator, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (configurator is null)
            return Response.Panic("api/permissions/{vhost}/{username}", []);

        var impl = new UserPermissionsConfiguratorImpl();
        configurator(impl);

        var errors = new List<Error>();
        string sanitizedVHost = vhost.ToSanitizedName();

        errors.AddIfTrue(username, string.IsNullOrWhiteSpace, Errors.Create("The name of the user is missing."));
        errors.AddIfTrue(sanitizedVHost, string.IsNullOrWhiteSpace, Errors.Create("The name of the virtual host is missing."));

        if (errors.HaveBeenFound())
            return Response.Panic("api/permissions/{vhost}/{username}", errors, impl.Request.Value.ToJsonString());

        return await PutRequest($"api/permissions/{sanitizedVHost}/{username}", impl.Request.Value, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> DeletePermissions(string username, string vhost, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();
        string sanitizedVHost = vhost.ToSanitizedName();

        errors.AddIfTrue(username, string.IsNullOrWhiteSpace, Errors.Create("The name of the user is missing."));
        errors.AddIfTrue(sanitizedVHost, string.IsNullOrWhiteSpace, Errors.Create("The name of the virtual host is missing."));

        if (errors.HaveBeenFound())
            return Response.Panic("api/permissions/{vhost}/{username}", errors);

        return await DeleteRequest($"api/permissions/{sanitizedVHost}/{username}", cancellationToken).ConfigureAwait(false);
    }

    
    class UserPermissionsConfiguratorImpl :
        UserPermissionsConfigurator
    {
        string _configurePattern;
        string _readPattern;
        string _writePattern;

        public Lazy<UserPermissionsRequest> Request { get; }

        public UserPermissionsConfiguratorImpl()
        {
            Request = new Lazy<UserPermissionsRequest>(
                () => new ()
                {
                    Configure = _configurePattern,
                    Write = _writePattern,
                    Read = _readPattern
                }, LazyThreadSafetyMode.PublicationOnly);
        }

        public void UsingConfigurePattern(string pattern) => _configurePattern = pattern;

        public void UsingWritePattern(string pattern) => _writePattern = pattern;

        public void UsingReadPattern(string pattern) => _readPattern = pattern;
    }


    class VirtualHostLimitsConfiguratorImpl :
        VirtualHostLimitsConfigurator
    {
        List<Error> InternalErrors { get; } = new();
        
        public string Limit { get; private set; }
        public ulong LimitValue { get; private set; }

        public void SetMaxConnectionLimit(ulong value)
        {
            Limit = "max-connections";
            LimitValue = value;

            InternalErrors.Clear();
            InternalErrors.AddIfTrue(value, x => x < 1, Errors.Create("Max connection limit value is missing."));
        }

        public void SetMaxQueueLimit(ulong value)
        {
            Limit = "max-queues";
            LimitValue = value;

            InternalErrors.Clear();
            InternalErrors.AddIfTrue(value, x => x < 1, Errors.Create("Max queue limit value is missing."));
        }

        public List<Error> Validate()
        {
            InternalErrors.Clear();
            InternalErrors.AddIfTrue(Limit, string.IsNullOrWhiteSpace, Errors.Create("No limit was defined."));

            return InternalErrors;
        }
    }


    class VirtualHostConfiguratorImpl :
        VirtualHostConfigurator
    {
        bool _tracing;
        string _description;
        string _tags;

        public Lazy<VirtualHostRequest> Request { get; }

        public VirtualHostConfiguratorImpl()
        {
            Request = new Lazy<VirtualHostRequest>(
                () => new ()
                {
                    Tracing = _tracing,
                    Description = _description,
                    Tags = _tags
                }, LazyThreadSafetyMode.PublicationOnly);
        }

        public void WithTracingEnabled() => _tracing = true;

        public void Description(string description) => _description = description;

        public void Tags(Action<VirtualHostTagConfigurator> configurator)
        {
            var impl = new VirtualHostTagConfiguratorImpl();
            configurator?.Invoke(impl);

            if (!impl.Tags.IsNotEmpty())
                return;
            
            var builder = new StringBuilder();

            impl.Tags.ForEach(x => builder.AppendFormat("{0},", x));

            _tags = builder.ToString().TrimEnd(',');
        }


        class VirtualHostTagConfiguratorImpl :
            VirtualHostTagConfigurator
        {
            readonly List<string> _tags = new();

            public List<string> Tags => _tags;

            public void Add(string tag)
            {
                if (_tags.Contains(tag) || string.IsNullOrWhiteSpace(tag))
                    return;

                _tags.Add(tag);
            }
        }
    }
}