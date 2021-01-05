namespace SpecFlowProject1.Personas
{
    public abstract class Persona
    {
        public abstract string FriendlyName { get; }

        internal void CreateAndAddUser(string alias)
        {
            UserManager.UsersInternal.Add(alias, CreateUser(alias));
        }

        protected abstract IUser CreateUser(string alias);
    }
}
