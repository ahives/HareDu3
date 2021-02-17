namespace HareDu
{
    using System;

    public interface DeleteTopicPermissionsConfiguration
    {
        /// <summary>
        /// Specify the user for which the permission should be assigned to.
        /// </summary>
        /// <param name="username"></param>
        void User(string username);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="target"></param>
        void Targeting(Action<TopicPermissionsTarget> target);
    }
}