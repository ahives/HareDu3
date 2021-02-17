namespace HareDu
{
    public interface TopicPermissionsTarget
    {
        /// <summary>
        /// Specify the target for which the user permission will be created.
        /// </summary>
        /// <param name="target">Define which user is associated with the permissions that are being created.</param>
        void VirtualHost(string name);
    }
}