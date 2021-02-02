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

    class ScopedParameterImpl :
        BaseBrokerObject,
        ScopedParameter
    {
        public ScopedParameterImpl(HttpClient client)
            : base(client)
        {
        }

        public Task<ResultList<ScopedParameterInfo>> GetAll(CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            string url = "api/parameters";
            
            return GetAll<ScopedParameterInfo>(url, cancellationToken);
        }

        public Task<Result> Create<T>(Action<ScopedParameterCreateAction<T>> action,
            CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();
            
            var impl = new ScopedParameterCreateActionImpl<T>();
            action(impl);
            
            impl.Validate();

            ScopedParameterDefinition<T> definition = impl.Definition.Value;

            Debug.Assert(definition != null);
                    
            string url = $"api/parameters/{definition.Component}/{definition.VirtualHost.ToSanitizedName()}/{definition.ParameterName}";

            if (impl.Errors.Value.Any())
                return Task.FromResult<Result>(new FaultedResult{Errors = impl.Errors.Value, DebugInfo = new (){URL = url, Request = definition.ToJsonString()}});

            return Put(url, definition, cancellationToken);
        }

        public Task<Result> Delete(Action<ScopedParameterDeleteAction> action, CancellationToken cancellationToken = default)
        {
            cancellationToken.RequestCanceled();

            var impl = new ScopedParameterDeleteActionImpl();
            action(impl);
            
            impl.Validate();

            string url = $"api/parameters/{impl.Component.Value}/{impl.VirtualHost.Value}/{impl.ScopedParameter.Value}";

            if (impl.Errors.Value.Any())
                return Task.FromResult<Result>(new FaultedResult {Errors = impl.Errors.Value, DebugInfo = new() {URL = url, Request = null}});

            return Delete(url, cancellationToken);
        }

        
        class ScopedParameterDeleteActionImpl :
            ScopedParameterDeleteAction
        {
            string _vhost;
            string _component;
            string _scopedParamName;
            readonly List<Error> _errors;

            public Lazy<string> ScopedParameter { get; }
            public Lazy<string> Component { get; }
            public Lazy<string> VirtualHost { get; }
            public Lazy<List<Error>> Errors { get; }

            public ScopedParameterDeleteActionImpl()
            {
                _errors = new List<Error>();
                
                Errors = new Lazy<List<Error>>(() => _errors, LazyThreadSafetyMode.PublicationOnly);
                ScopedParameter = new Lazy<string>(() => _scopedParamName, LazyThreadSafetyMode.PublicationOnly);
                Component = new Lazy<string>(() => _component, LazyThreadSafetyMode.PublicationOnly);
                VirtualHost = new Lazy<string>(() => _vhost, LazyThreadSafetyMode.PublicationOnly);
            }

            public void Parameter(string name) => _scopedParamName = name;

            public void Targeting(Action<ScopedParameterTarget> target)
            {
                var impl = new ScopedParameterTargetImpl();
                target(impl);

                _vhost = impl.VirtualHostName;
                _component = impl.ComponentName;
            }

            public void Validate()
            {
                if (string.IsNullOrWhiteSpace(_scopedParamName))
                    _errors.Add(new() {Reason = "The name of the parameter is missing."});

                if (string.IsNullOrWhiteSpace(_vhost))
                    _errors.Add(new() {Reason = "The name of the virtual host is missing."});

                if (string.IsNullOrWhiteSpace(_component))
                    _errors.Add(new() {Reason = "The component name is missing."});
            }

            
            class ScopedParameterTargetImpl :
                ScopedParameterTarget
            {
                public string ComponentName { get; private set; }
                public string VirtualHostName { get; private set; }

                public void Component(string component) => ComponentName = component;

                public void VirtualHost(string name) => VirtualHostName = name;
            }
        }

        
        class ScopedParameterCreateActionImpl<T> :
            ScopedParameterCreateAction<T>
        {
            string _component;
            string _vhost;
            T _scopedParamValue;
            string _scopedParamName;
            readonly List<Error> _errors;

            public Lazy<ScopedParameterDefinition<T>> Definition { get; }
            public Lazy<List<Error>> Errors { get; }

            public ScopedParameterCreateActionImpl()
            {
                _errors = new List<Error>();
                
                Errors = new Lazy<List<Error>>(() => _errors, LazyThreadSafetyMode.PublicationOnly);
                Definition = new Lazy<ScopedParameterDefinition<T>>(
                    () => new ScopedParameterDefinition<T>
                    {
                        VirtualHost = _vhost,
                        Component = _component,
                        ParameterName = _scopedParamName,
                        ParameterValue = _scopedParamValue
                    }, LazyThreadSafetyMode.PublicationOnly);
            }

            public void Parameter(string name, T value)
            {
                _scopedParamName = name;
                _scopedParamValue = value;
            }

            public void Targeting(Action<ScopedParameterTarget> target)
            {
                var impl = new ScopedParameterTargetImpl();
                target(impl);

                _vhost = impl.VirtualHostName;
                _component = impl.ComponentName;
            }

            public void Validate()
            {
                if (string.IsNullOrWhiteSpace(_scopedParamName))
                    _errors.Add(new() {Reason = "The name of the parameter is missing."});

                if (string.IsNullOrWhiteSpace(_vhost))
                    _errors.Add(new() {Reason = "The name of the virtual host is missing."});

                if (string.IsNullOrWhiteSpace(_component))
                    _errors.Add(new() {Reason = "The component name is missing."});
            }

            
            class ScopedParameterTargetImpl :
                ScopedParameterTarget
            {
                public string ComponentName { get; private set; }
                public string VirtualHostName { get; private set; }

                public void Component(string component) => ComponentName = component;

                public void VirtualHost(string name) => VirtualHostName = name;
            }
        }
    }
}