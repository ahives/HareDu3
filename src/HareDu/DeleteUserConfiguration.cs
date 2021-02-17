namespace HareDu
{
    public interface DeleteUserConfiguration
    {
        /// <summary>
        /// Specify the user targeted for deletion.
        /// </summary>
        /// <param name="name"></param>
        void User(string name);
    }
}