namespace GServer.Archestra.Exceptions
{
    public class ObjectIsCheckedOutException : GalaxyException
    {
        public ObjectIsCheckedOutException(string message) : base(message)
        {
        }
    }
}