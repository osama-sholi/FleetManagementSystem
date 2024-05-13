namespace FleetManagementAPI
{
    public class ResourseNotFoundException : System.Exception
    {
        public ResourseNotFoundException()
        {
        }

        public ResourseNotFoundException(string message)
            : base(message)
        {
        }

        public ResourseNotFoundException(string message, System.Exception inner)
            : base(message, inner)
        {
        }
    }
}
