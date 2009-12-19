namespace Tigraine.Logging.ObjectRenderers
{
    public class DefaultObjectRenderer : IObjectRenderer
    {
        public string Render(object subject)
        {
            return subject.ToString();
        }
    }
}