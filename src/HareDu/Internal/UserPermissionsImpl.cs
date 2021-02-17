namespace HareDu.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Core.Extensions;
    using Model;

    class UserPermissionsImpl :
        BaseBrokerObject,
        UserPermissions
    {
        public UserPermissionsImpl(HttpClient client)
            : base(client)
        {
        }

        public async Task<ResultList<UserPermissionsInfo>> GetAll(CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            string url = "api/permissions";
            
            return await GetAll<UserPermissionsInfo>(url, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result> Create(Action<NewUserPermissionsConfiguration> configuration, CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var impl = new NewUserPermissionsConfigurationImpl();
            configuration?.Invoke(impl);

            impl.Validate();

            UserPermissionsDefinition definition = impl.Definition.Value;

            Debug.Assert(definition != null);

            string url = $"api/permissions/{impl.VirtualHost.Value.ToSanitizedName()}/{impl.Username.Value}";
            
            if (impl.Errors.Value.Any())
                return new FaultedResult{Errors = impl.Errors.Value, DebugInfo = new (){URL = url, Request = definition.ToJsonString()}};

            return await Put(url, definition, cancellationToken).ConfigureAwait(false);
        }

        public async Task<Result> Delete(Action<DeleteUserPermissionsConfiguration> configuration, CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var impl = new DeleteUserPermissionsConfigurationImpl();
            configuration?.Invoke(impl);

            impl.Validate();

            string url = $"api/permissions/{impl.VirtualHost.Value.ToSanitizedName()}/{impl.Username.Value}";
            
            if (impl.Errors.Value.Any())
                return new FaultedResult{Errors = impl.Errors.Value, DebugInfo = new (){URL = url, Request = null}};

            return await Delete(url, cancellationToken).ConfigureAwait(false);
        }

        
        class DeleteUserPermissionsConfigurationImpl :
            DeleteUserPermissionsConfiguration
        {
            string _vhost;
            string _user;
            bool _userCalled;
            bool _targetingCalled;
            
            readonly List<Error> _errors;

            public Lazy<string> Username { get; }
            public Lazy<string> VirtualHost { get; }
            public Lazy<List<Error>> Errors { get; }

            public DeleteUserPermissionsConfigurationImpl()
            {
                _errors = new List<Error>();
                
                Errors = new Lazy<List<Error>>(() => _errors, LazyThreadSafetyMode.PublicationOnly);
                Username = new Lazy<string>(() => _user, LazyThreadSafetyMode.PublicationOnly);
                VirtualHost = new Lazy<string>(() => _vhost, LazyThreadSafetyMode.PublicationOnly);
            }

            public void Targeting(Action<UserPermissionsTarget> target)
            {
                _targetingCalled = true;
                
                var impl = new UserPermissionsTargetImpl();
                target?.Invoke(impl);

                _vhost = impl.VirtualHostName;
                
                if (string.IsNullOrWhiteSpace(_vhost))
                    _errors.Add(new() {Reason = "The name of the virtual host is missing."});
            }

            public void User(string name)
            {
                _userCalled = true;
                
                _user = name;

                if (string.IsNullOrWhiteSpace(_user))
                    _errors.Add(new() {Reason = "The username and/or password is missing."});
            }

            public void Validate()
            {
                if (!_targetingCalled)
                    _errors.Add(new() {Reason = "The name of the virtual host is missing."});

                if (!_userCalled)
                    _errors.Add(new() {Reason = "The username and/or password is missing."});
            }

            
            class UserPermissionsTargetImpl :
                UserPermissionsTarget
            {
                public string VirtualHostName { get; private set; }

                public void VirtualHost(string name) => VirtualHostName = name;
            }
        }

        
        class NewUserPermissionsConfigurationImpl :
            NewUserPermissionsConfiguration
        {
            string _configurePattern;
            string _writePattern;
            string _readPattern;
            string _vhost;
            string _user;
            bool _targetingCalled;
            bool _userCalled;
            readonly List<Error> _errors;

            public Lazy<UserPermissionsDefinition> Definition { get; }
            public Lazy<string> VirtualHost { get; }
            public Lazy<string> Username { get; }
            public Lazy<List<Error>> Errors { get; }

            public NewUserPermissionsConfigurationImpl()
            {
                _errors = new List<Error>();
                
                Errors = new Lazy<List<Error>>(() => _errors, LazyThreadSafetyMode.PublicationOnly);
                Definition = new Lazy<UserPermissionsDefinition>(
                    () => new UserPermissionsDefinition
                    {
                        Configure = _configurePattern,
                        Write = _writePattern,
                        Read = _readPattern
                    }, LazyThreadSafetyMode.PublicationOnly);
                VirtualHost = new Lazy<string>(() => _vhost, LazyThreadSafetyMode.PublicationOnly);
                Username = new Lazy<string>(() => _user, LazyThreadSafetyMode.PublicationOnly);
            }

            public void User(string username)
            {
                _userCalled = true;
                
                _user = username;
            
                if (string.IsNullOrWhiteSpace(_user))
                    _errors.Add(new (){Reason = "The username and/or password is missing."});
            }

            public void Configure(Action<NewUserPermissionsConfigurator> configurator)
            {
                var impl = new NewUserPermissionsConfiguratorImpl();
                configurator?.Invoke(impl);

                _configurePattern = impl.ConfigurePattern;
                _writePattern = impl.WritePattern;
                _readPattern = impl.ReadPattern;
            }

            public void Targeting(Action<UserPermissionsTarget> target)
            {
                _targetingCalled = true;
                
                var impl = new UserPermissionsTargetImpl();
                target?.Invoke(impl);

                _vhost = impl.VirtualHostName;

                if (string.IsNullOrWhiteSpace(_vhost))
                    _errors.Add(new() {Reason = "The name of the virtual host is missing."});
            }

            public void Validate()
            {
                if (!_targetingCalled)
                    _errors.Add(new() {Reason = "The name of the virtual host is missing."});

                if (!_userCalled)
                    _errors.Add(new() {Reason = "The username and/or password is missing."});
            }

            
            class UserPermissionsTargetImpl :
                UserPermissionsTarget
            {
                public string VirtualHostName { get; private set; }

                public void VirtualHost(string name) => VirtualHostName = name;
            }

            
            class NewUserPermissionsConfiguratorImpl :
                NewUserPermissionsConfigurator
            {
                public string ConfigurePattern { get; private set; }
                public string ReadPattern { get; private set; }
                public string WritePattern { get; private set; }
                
                public void UsingConfigurePattern(string pattern) => ConfigurePattern = pattern;

                public void UsingWritePattern(string pattern) => WritePattern = pattern;

                public void UsingReadPattern(string pattern) => ReadPattern = pattern;
            }
        }
    }
}