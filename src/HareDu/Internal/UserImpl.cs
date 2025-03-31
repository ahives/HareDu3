namespace HareDu.Internal;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Core.Extensions;
using Extensions;
using Model;

class UserImpl :
    BaseBrokerImpl,
    User
{
    public UserImpl(IHttpClientFactory clientFactory)
        : base(clientFactory)
    {
    }

    public async Task<Results<UserInfo>> GetAll(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
            
        return await GetAllRequest<UserInfo>("api/users", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Results<UserInfo>> GetAllWithoutPermissions(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await GetAllRequest<UserInfo>("api/users/without-permissions", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Create(string username, string password, string passwordHash = null,
        Action<UserConfigurator> configurator = null, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (configurator == null)
            return Faulted.Result("api/users/{username}", [new Error {Reason = "The tags are missing."}]);
        
        var impl = new UserConfiguratorImpl();
        configurator(impl);

        string Normalize(string value) => string.IsNullOrWhiteSpace(value) ? null : value;

        UserRequest request =
            new ()
            {
                Password = Normalize(password),
                PasswordHash = !string.IsNullOrWhiteSpace(Normalize(password)) ? null : passwordHash,
                Tags = impl.Tags
            };

        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(username))
            errors.Add(new (){Reason = "The username is missing."});

        if (string.IsNullOrWhiteSpace(password) && string.IsNullOrWhiteSpace(passwordHash))
            errors.Add(new() {Reason = "The password/hash is missing."});

        string url = $"api/users/{username}";

        if (errors.Count > 0)
            return Faulted.Result(url, errors, request.ToJsonString());

        return await PutRequest(url, request, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Delete(string username, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(username))
            errors.Add(new (){Reason = "The username is missing."});

        string url = $"api/users/{username}";

        if (errors.Count > 0)
            return Faulted.Result(url, errors);

        return await DeleteRequest(url, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> BulkDelete(IList<string> usernames, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        string url = "api/users/bulk-delete";

        if (usernames.IsEmpty())
            return Faulted.Result(url, [new() {Reason = "Valid usernames is missing."}]);

        var errors = new List<Error>();

        for (int i = 0; i < usernames.Count; i++)
        {
            if (string.IsNullOrWhiteSpace(usernames[i]))
                errors.Add(new() {Reason = $"The username at index {i} is missing."});
        }

        if (errors.Count > 0)
            return Faulted.Result(url, errors);

        BulkUserDeleteRequest request = new() {Users = usernames};

        return await PostRequest(url, request, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Results<UserLimitsInfo>> GetLimitsByUser(string username, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(username))
            errors.Add(new (){Reason = "The username is missing."});

        if (errors.Count > 0)
            return Faulted.Results<UserLimitsInfo>("api/user-limits/{username}", errors);

        return await GetAllRequest<UserLimitsInfo>($"api/user-limits/{username}", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Results<UserLimitsInfo>> GetAllUserLimits(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await GetAllRequest<UserLimitsInfo>("api/user-limits", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> DefineLimit(string username, Action<UserLimitConfigurator> configurator = null, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();

        if (configurator == null)
            return Faulted.Result("api/user-limits/{username}/{limit}", [new() {Reason = "No user limit was defined."}]);

        var impl = new UserLimitConfiguratorImpl();
        configurator(impl);

        errors.AddRange(impl.Validate());

        if (string.IsNullOrWhiteSpace(username))
            errors.Add(new (){Reason = "The username is missing."});

        var request = new UserLimitRequest{Value = impl.LimitValue};

        if (errors.Count > 0)
            return Faulted.Result("api/user-limits/{username}/{limit}", errors);

        // /api/user-limits/user/name
        return await PutRequest($"api/user-limits/{username}/{impl.Limit}", request, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> DeleteLimit(string username, UserLimit limit, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();

        if (string.IsNullOrWhiteSpace(username))
            errors.Add(new (){Reason = "The username is missing."});

        if (errors.Count > 0)
            return Faulted.Result("api/user-limits/{username}/{limit}", errors);

        return await DeleteRequest($"api/user-limits/{username}/{limit.Convert()}", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Results<UserPermissionsInfo>> GetAllPermissions(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await GetAllRequest<UserPermissionsInfo>("api/permissions", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> ApplyPermissions(string username, string vhost,
        Action<UserPermissionsConfigurator> configurator, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var impl = new UserPermissionsConfiguratorImpl();
        configurator?.Invoke(impl);

        var request = impl.Request.Value;

        var errors = new List<Error>();

        string sanitizedVHost = vhost.ToSanitizedName();

        if (string.IsNullOrWhiteSpace(username))
            errors.Add(new (){Reason = "The username and/or password is missing."});

        if (string.IsNullOrWhiteSpace(sanitizedVHost))
            errors.Add(new (){Reason = "The name of the virtual host is missing."});

        if (errors.Count > 0)
            return Faulted.Result("api/permissions/{vhost}/{username}", errors, request.ToJsonString());

        return await PutRequest($"api/permissions/{sanitizedVHost}/{username}", request, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> DeletePermissions(string username, string vhost, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();

        string sanitizedVHost = vhost.ToSanitizedName();

        if (string.IsNullOrWhiteSpace(username))
            errors.Add(new (){Reason = "The username and/or password is missing."});

        if (string.IsNullOrWhiteSpace(sanitizedVHost))
            errors.Add(new (){Reason = "The name of the virtual host is missing."});

        if (errors.Count > 0)
            return Faulted.Result("api/permissions/{vhost}/{username}", errors);

        return await DeleteRequest($"api/permissions/{sanitizedVHost}/{username}", cancellationToken).ConfigureAwait(false);
    }


    
    class UserLimitConfiguratorImpl :
        UserLimitConfigurator
    {
        List<Error> Errors { get; } = new();
 
        public ulong LimitValue { get; private set; }

        public string Limit { get; private set; }

        public void SetLimit(UserLimit limit, ulong value)
        {
            Limit = limit.Convert();
            LimitValue = value;

            if (value < 1)
                Errors.Add(new (){Reason = "Max connection limit value is missing."});
        }

        public List<Error> Validate()
        {
            if (string.IsNullOrWhiteSpace(Limit))
                Errors.Add(new (){Reason = "No limits were defined."});

            return Errors;
        }
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


    class UserConfiguratorImpl :
        UserConfigurator
    {
        string _tags;

        public string Tags => _tags;

        public void WithTags(Action<UserAccessOptions> tags)
        {
            var impl = new UserAccessOptionsImpl();
            tags?.Invoke(impl);

            if (string.IsNullOrWhiteSpace(_tags))
                _tags = impl.ToString();
            else
            {
                var sb = new StringBuilder();
                sb.Append($"{_tags},{impl.ToString()}");

                _tags = sb.ToString();
            }
        }

            
        class UserAccessOptionsImpl :
            UserAccessOptions
        {
            List<string> Tags { get; }
                
            public UserAccessOptionsImpl()
            {
                Tags = new List<string>();
            }

            public void None() => Tags.Add(UserAccessTag.None);

            public void Administrator() => Tags.Add(UserAccessTag.Administrator);

            public void Monitoring() => Tags.Add(UserAccessTag.Monitoring);

            public void Management() => Tags.Add(UserAccessTag.Management);
                
            public void PolicyMaker() => Tags.Add(UserAccessTag.PolicyMaker);

            public void Impersonator() => Tags.Add(UserAccessTag.Impersonator);

            public override string ToString()
            {
                if (Tags.Contains(UserAccessTag.None) || !Tags.Any())
                    return UserAccessTag.None;

                return string.Join(",", Tags);
            }
        }
    }
}