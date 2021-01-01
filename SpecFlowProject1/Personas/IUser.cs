namespace SpecFlowProject1.Personas
{
    public interface IUser
    {
        string Alias { get; }

        Persona Persona { get; }
    }
}
