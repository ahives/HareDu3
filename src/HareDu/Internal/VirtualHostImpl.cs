namespace HareDu.Internal;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Core.Extensions;
using Core.Serialization;
using Extensions;
using Microsoft.Extensions.DependencyInjection;
using Model;

class VirtualHostImpl :
    BaseHareDuImpl,
    VirtualHost
{
    public VirtualHostImpl(HttpClient client, [FromKeyedServices("broker")] IHareDuDeserializer deserializer)
        : base(client, deserializer)
    {
    }

    public async Task<Results<VirtualHostInfo>> GetAll(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await GetAllRequest<VirtualHostInfo>("api/vhosts", RequestType.VirtualHost, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result<VirtualHostInfo>> Get(string vhost, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();
        string sanitizedVHost = vhost.ToSanitizedName();

        errors.AddIfTrue(sanitizedVHost, string.IsNullOrWhiteSpace, Errors.Create("The name of the virtual host is missing."));

        return errors.HaveBeenFound()
            ? Response.Panic<VirtualHostInfo>(Debug.Info("/api/vhosts/{vhost}", errors))
            : await GetRequest<VirtualHostInfo>($"/api/vhosts/{sanitizedVHost}", RequestType.VirtualHost, cancellationToken).ConfigureAwait(false);
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

        return errors.HaveBeenFound()
            ? Response.Panic(Debug.Info("api/vhosts/{vhost}", errors, Deserializer.ToJsonString(request)))
            : await PutRequest($"api/vhosts/{sanitizedVHost}", request, RequestType.VirtualHost, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Delete(string vhost, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();
        string sanitizedVHost = vhost.ToSanitizedName();

        errors.AddIfTrue(sanitizedVHost, string.IsNullOrWhiteSpace, Errors.Create("The name of the virtual host is missing."));
        errors.AddIfTrue(sanitizedVHost, x => x.Equals("2%f"), Errors.Create("Cannot delete the default virtual host."));

        return errors.HaveBeenFound()
            ? Response.Panic(Debug.Info("api/vhosts/{vhost}", errors))
            : await DeleteRequest($"api/vhosts/{sanitizedVHost}", RequestType.VirtualHost, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Startup(string vhost, string node, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();
        string sanitizedVHost = vhost.ToSanitizedName();

        errors.AddIfTrue(node, string.IsNullOrWhiteSpace, Errors.Create("RabbitMQ node is missing."));
        errors.AddIfTrue(sanitizedVHost, string.IsNullOrWhiteSpace, Errors.Create("The name of the virtual host is missing."));

        return errors.HaveBeenFound()
            ? Response.Panic(Debug.Info("/api/vhosts/{vhost}/start/{node}", errors))
            : await PostEmptyRequest($"/api/vhosts/{sanitizedVHost}/start/{node}", RequestType.VirtualHost, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Results<VirtualHostLimitsInfo>> GetAllLimits(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await GetAllRequest<VirtualHostLimitsInfo>("api/vhost-limits", RequestType.VirtualHost, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> DefineLimit(string vhost, Action<VirtualHostLimitsConfigurator> configurator = null, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (configurator is null)
            return Response.Panic(Debug.Info("api/vhost-limits/{vhost}/{limit}",
                Errors.Create(e => { e.Add("The name of the virtual host is missing."); })));

        var impl = new VirtualHostLimitsConfiguratorImpl();
        configurator(impl);

        string sanitizedVHost = vhost.ToSanitizedName();
        var errors = impl.Validate();

        errors.AddIfTrue(sanitizedVHost, string.IsNullOrWhiteSpace, Errors.Create("The name of the virtual host is missing."));

        var request = new VirtualHostLimitsRequest{Value = impl.LimitValue};

        return errors.HaveBeenFound()
            ? Response.Panic(Debug.Info("api/vhost-limits/{vhost}/{limit}", errors, Deserializer.ToJsonString(request)))
            : await PutRequest($"api/vhost-limits/{sanitizedVHost}/{impl.Limit}", request, RequestType.VirtualHost,
                cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> DeleteLimit(string vhost, VirtualHostLimit limit, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();
        string sanitizedVHost = vhost.ToSanitizedName();

        errors.AddIfTrue(sanitizedVHost, string.IsNullOrWhiteSpace, Errors.Create("The name of the virtual host is missing."));

        return errors.HaveBeenFound()
            ? Response.Panic(Debug.Info("api/vhost-limits/{vhost}/{limit}", errors))
            : await DeleteRequest($"api/vhost-limits/{sanitizedVHost}/{limit.Convert()}", RequestType.VirtualHost,
                cancellationToken).ConfigureAwait(false);
    }

    public async Task<Results<VirtualHostPermissionInfo>> GetAllPermissions(string vhost, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();
        string sanitizedVHost = vhost.ToSanitizedName();

        errors.AddIfTrue(sanitizedVHost, string.IsNullOrWhiteSpace, Errors.Create("The name of the virtual host is missing."));

        return errors.HaveBeenFound()
            ? Responses.Panic<VirtualHostPermissionInfo>(Debug.Info("api/vhosts/{vhost}/permissions", errors))
            : await GetAllRequest<VirtualHostPermissionInfo>($"api/vhosts/{sanitizedVHost}/permissions", RequestType.VirtualHost, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result<VirtualHostPermissionInfo>> GetUserPermissions(string vhost, string username, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();
        string sanitizedVHost = vhost.ToSanitizedName();

        errors.AddIfTrue(username, string.IsNullOrWhiteSpace, Errors.Create("The name of the user is missing."));
        errors.AddIfTrue(sanitizedVHost, string.IsNullOrWhiteSpace, Errors.Create("The name of the virtual host is missing."));

        return errors.HaveBeenFound()
            ? Response.Panic<VirtualHostPermissionInfo>(Debug.Info("api/permissions/vhost/user", errors))
            : await GetRequest<VirtualHostPermissionInfo>($"api/permissions/{sanitizedVHost}/{username}",
                RequestType.VirtualHost, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Results<VirtualHostTopicPermissionInfo>> GetTopicPermissions(string vhost, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();
        string sanitizedVHost = vhost.ToSanitizedName();

        errors.AddIfTrue(sanitizedVHost, string.IsNullOrWhiteSpace, Errors.Create("The name of the virtual host is missing."));

        return errors.HaveBeenFound()
            ? Responses.Panic<VirtualHostTopicPermissionInfo>(Debug.Info("api/vhosts/{vhost}/topic-permissions", errors))
            : await GetAllRequest<VirtualHostTopicPermissionInfo>($"api/vhosts/{sanitizedVHost}/topic-permissions",
                RequestType.VirtualHost, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> ApplyPermissions(string username, string vhost,
        Action<UserPermissionsConfigurator> configurator, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (configurator is null)
            return Response.Panic(Debug.Info("api/permissions/{vhost}/{username}", []));

        var impl = new UserPermissionsConfiguratorImpl();
        configurator(impl);

        var errors = new List<Error>();
        string sanitizedVHost = vhost.ToSanitizedName();

        errors.AddIfTrue(username, string.IsNullOrWhiteSpace, Errors.Create("The name of the user is missing."));
        errors.AddIfTrue(sanitizedVHost, string.IsNullOrWhiteSpace, Errors.Create("The name of the virtual host is missing."));

        return errors.HaveBeenFound()
            ? Response.Panic(Debug.Info("api/permissions/{vhost}/{username}", errors, Deserializer.ToJsonString(impl.Request.Value)))
            : await PutRequest($"api/permissions/{sanitizedVHost}/{username}", impl.Request.Value,
                RequestType.VirtualHost, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> DeletePermissions(string username, string vhost, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();
        string sanitizedVHost = vhost.ToSanitizedName();

        errors.AddIfTrue(username, string.IsNullOrWhiteSpace, Errors.Create("The name of the user is missing."));
        errors.AddIfTrue(sanitizedVHost, string.IsNullOrWhiteSpace, Errors.Create("The name of the virtual host is missing."));

        return errors.HaveBeenFound()
            ? Response.Panic(Debug.Info("api/permissions/{vhost}/{username}", errors))
            : await DeleteRequest($"api/permissions/{sanitizedVHost}/{username}", RequestType.VirtualHost, cancellationToken).ConfigureAwait(false);
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