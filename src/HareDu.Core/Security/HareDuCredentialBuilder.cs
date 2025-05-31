namespace HareDu.Core.Security;

using System;
using System.Threading;

/// <summary>
/// The <c>HareDuCredentialBuilder</c> class facilitates the creation and configuration of
/// broker credentials used within the HareDu library by providing a mechanism to define
/// credential details using a specified provider.
/// </summary>
public class HareDuCredentialBuilder :
    IHareDuCredentialBuilder
{
    public HareDuCredentials Build(Action<HareDuCredentialProvider> provider)
    {
        Throw.IfNull<HareDuCredentialProvider, HareDuSecurityException>(provider, "Invalid user credentials.");

        var impl = new HareDuCredentialProviderImpl();
        provider(impl);

        var credentials = impl.Credentials.Value;

        Throw.IfCredentialsInvalid(credentials);

        return credentials;
    }


    class HareDuCredentialProviderImpl :
        HareDuCredentialProvider
    {
        string _username;
        string _password;

        public Lazy<HareDuCredentials> Credentials { get; }

        public HareDuCredentialProviderImpl()
        {
            Credentials = new Lazy<HareDuCredentials>(
                () => new HareDuCredentials {Username = _username, Password = _password}, LazyThreadSafetyMode.PublicationOnly);
        }

        public void UsingCredentials(string username, string password)
        {
            _username = username;
            _password = password;
        }
    }
}