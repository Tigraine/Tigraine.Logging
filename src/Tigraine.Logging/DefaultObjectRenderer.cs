namespace Tigraine.Logging
{
    public class DefaultObjectRenderer : IObjectRenderer
    {
        public string Render(object subject)
        {
            return subject.ToString();
        }
    }
}