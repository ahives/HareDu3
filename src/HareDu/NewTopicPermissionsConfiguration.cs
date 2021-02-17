namespace HareDu
{
    using System;

    public interface NewTopicPermissionsConfiguration
    {
        /// <summary>
        /// Specify the user for which the permission should be assigned to.
        /// </summary>
        /// <param name="username"></param>
        void User(string username);

        /// <summary>
        /// Define how the user permission is to be created.
        /// </summary>
        /// <param name="definition"></param>
        void Configure(Action<TopicPermissionsConfigurator> configurator);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        void Targeting(Action<TopicPermissionsTarget> target);
    }
}