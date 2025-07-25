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
    public UserImpl(HttpClient client)
        : base(client)
    {
    }

    public async Task<Results<UserInfo>> GetAll(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await GetAllRequest<UserInfo>("api/users", RequestType.User, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Results<UserInfo>> GetAllWithoutPermissions(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await GetAllRequest<UserInfo>("api/users/without-permissions", RequestType.User, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Create(string username, string password, string passwordHash = null,
        Action<UserConfigurator> configurator = null, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (configurator is null)
            return Response.Panic("api/users/{username}", [Errors.Create("The tags are missing.")]);

        var impl = new UserConfiguratorImpl();
        configurator(impl);

        string pwd = string.IsNullOrWhiteSpace(password) ? null : password;

        UserRequest request =
            new ()
            {
                Password = pwd,
                PasswordHash = !string.IsNullOrWhiteSpace(pwd) ? null : passwordHash,
                Tags = impl.Tags
            };

        var errors = new List<Error>();

        errors.AddIfTrue(username, string.IsNullOrWhiteSpace, Errors.Create("The username is missing."));
        errors.AddIfTrue(password, passwordHash, (x, y) => string.IsNullOrWhiteSpace(x) && string.IsNullOrWhiteSpace(y), Errors.Create("The password/hash is missing."));

        if (errors.HaveBeenFound())
            return Response.Panic("api/users/{username}", errors, request.ToJsonString());

        return await PutRequest($"api/users/{username}", request, RequestType.User, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Delete(string username, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();

        errors.AddIfTrue(username, string.IsNullOrWhiteSpace, Errors.Create("The username is missing."));

        if (errors.HaveBeenFound())
            return Response.Panic("api/users/{username}", errors);

        return await DeleteRequest($"api/users/{username}", RequestType.User, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> BulkDelete(IList<string> usernames, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (usernames.IsEmpty())
            return Response.Panic("api/users/bulk-delete", [Errors.Create("Valid usernames is missing.")]);

        var errors = new List<Error>();

        for (int i = 0; i < usernames.Count; i++)
            errors.AddIfTrue(usernames[i], string.IsNullOrWhiteSpace, Errors.Create($"Username[{i}] is missing."));

        if (errors.HaveBeenFound())
            return Response.Panic("api/users/bulk-delete", errors);

        BulkUserDeleteRequest request = new() {Users = usernames};

        return await PostRequest("api/users/bulk-delete", request, RequestType.User, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Results<UserLimitsInfo>> GetLimitsByUser(string username, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();

        errors.AddIfTrue(username, string.IsNullOrWhiteSpace, Errors.Create("The username is missing."));

        if (errors.HaveBeenFound())
            return Responses.Panic<UserLimitsInfo>("api/user-limits/{username}", errors);

        return await GetAllRequest<UserLimitsInfo>($"api/user-limits/{username}", RequestType.User, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Results<UserLimitsInfo>> GetAllUserLimits(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await GetAllRequest<UserLimitsInfo>("api/user-limits", RequestType.User, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> DefineLimit(string username, Action<UserLimitConfigurator> configurator = null, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (configurator is null)
            return Response.Panic("api/user-limits/{username}/{limit}", [Errors.Create("No user limit was defined.")]);

        var impl = new UserLimitConfiguratorImpl();
        configurator(impl);

        var errors = impl.Validate();

        errors.AddIfTrue(username, string.IsNullOrWhiteSpace, Errors.Create("The username is missing."));

        if (errors.HaveBeenFound())
            return Response.Panic("api/user-limits/{username}/{limit}", errors);

        return await PutRequest($"api/user-limits/{username}/{impl.Limit}",
            new UserLimitRequest {Value = impl.LimitValue}, RequestType.User, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> DeleteLimit(string username, UserLimit limit, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();

        errors.AddIfTrue(username, string.IsNullOrWhiteSpace, Errors.Create("The username is missing."));

        if (errors.HaveBeenFound())
            return Response.Panic("api/user-limits/{username}/{limit}", errors);

        return await DeleteRequest($"api/user-limits/{username}/{limit.Convert()}", RequestType.User, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Results<UserPermissionsInfo>> GetAllPermissions(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await GetAllRequest<UserPermissionsInfo>("api/permissions", RequestType.User, cancellationToken).ConfigureAwait(false);
    }

    
    class UserLimitConfiguratorImpl :
        UserLimitConfigurator
    {
        List<Error> InternalErrors { get; } = new();
 
        public ulong LimitValue { get; private set; }

        public string Limit { get; private set; }

        public void SetLimit(UserLimit limit, ulong value)
        {
            Limit = limit.Convert();
            LimitValue = value;

            InternalErrors.AddIfTrue(value, x => x > 1, Errors.Create("Max connection limit value is missing."));
        }

        public List<Error> Validate()
        {
            InternalErrors.AddIfTrue(Limit, string.IsNullOrWhiteSpace, Errors.Create("No limits were defined."));

            return InternalErrors;
        }
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

            public void AddTag(UserAccessTag tag)
            {
                Tags.Add(tag.Value);
            }

            public override string ToString()
            {
                if (Tags.Contains(UserAccessTag.None.Value) || !Tags.Any())
                    return UserAccessTag.None.Value;

                return string.Join(",", Tags);
            }
        }
    }
}