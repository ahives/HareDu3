namespace HareDu.Internal;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Core;
using Core.Extensions;
using Extensions;
using Model;

class TopicPermissionsImpl :
    BaseBrokerImpl,
    TopicPermissions
{
    public TopicPermissionsImpl(HttpClient client)
        : base(client)
    {
    }

    public async Task<Results<TopicPermissionsInfo>> GetAll(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return await GetAllRequest<TopicPermissionsInfo>("api/topic-permissions", cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Create(string username, string vhost, Action<TopicPermissionsConfigurator> configurator,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (configurator is null)
            return Response.Panic("api/topic-permissions/{vhost}/{username}", [new() {Reason = "No topic permissions was defined."}]);

        var impl = new TopicPermissionsConfiguratorImpl();
        configurator(impl);

        string sanitizedVHost = vhost.ToSanitizedName();
        var errors = impl.Validate();

        if (string.IsNullOrWhiteSpace(username))
            errors.Add("The username and/or password is missing.");

        if (string.IsNullOrWhiteSpace(sanitizedVHost))
            errors.Add("The name of the virtual host is missing.");

        var request = impl.Request.Value;

        if (errors.Count > 0)
            return Response.Panic("api/topic-permissions/{vhost}/{username}", errors, request.ToJsonString());

        return await PutRequest($"api/topic-permissions/{sanitizedVHost}/{username}", request, cancellationToken).ConfigureAwait(false);
    }

    public async Task<Result> Delete(string username, string vhost, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var errors = new List<Error>();
        string sanitizedVHost = vhost.ToSanitizedName();

        if (string.IsNullOrWhiteSpace(username))
            errors.Add("The username and/or password is missing.");

        if (string.IsNullOrWhiteSpace(sanitizedVHost))
            errors.Add("The name of the virtual host is missing.");

        if (errors.Count > 0)
            return Response.Panic("api/topic-permissions/{vhost}/{username}", errors);

        return await DeleteRequest($"api/topic-permissions/{sanitizedVHost}/{username}", cancellationToken).ConfigureAwait(false);
    }


    class TopicPermissionsConfiguratorImpl :
        TopicPermissionsConfigurator
    {
        string _writePattern;
        string _readPattern;
        bool _usingWritePatternCalled;
        bool _usingReadPatternCalled;
        string _exchange;

        List<Error> Errors { get; } = new();

        public Lazy<TopicPermissionsRequest> Request { get; }

        public TopicPermissionsConfiguratorImpl()
        {
            Errors = new List<Error>();
            Request = new Lazy<TopicPermissionsRequest>(
                () => new ()
                {
                    Exchange = _exchange,
                    Read = _readPattern,
                    Write = _writePattern
                }, LazyThreadSafetyMode.PublicationOnly);
        }

        public void Exchange(string exchange) => _exchange = exchange;

        public void UsingWritePattern(string pattern)
        {
            _usingWritePatternCalled = true;
            _writePattern = pattern;

            if (string.IsNullOrWhiteSpace(pattern))
                Errors.Add("The write pattern is missing.");
        }

        public void UsingReadPattern(string pattern)
        {
            _usingReadPatternCalled = true;
            _readPattern = pattern;

            if (string.IsNullOrWhiteSpace(_readPattern))
                Errors.Add("The read pattern is missing.");
        }

        public List<Error> Validate()
        {
            if (!_usingWritePatternCalled)
                Errors.Add("The write pattern is missing.");

            if (!_usingReadPatternCalled)
                Errors.Add("The read pattern is missing.");

            if (string.IsNullOrWhiteSpace(_exchange))
                Errors.Add("Then name of the exchange is missing.");

            return Errors;
        }
    }
}