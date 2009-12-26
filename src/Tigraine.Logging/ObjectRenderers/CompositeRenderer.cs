namespace Tigraine.Logging.ObjectRenderers
{
    using System;

    public class CompositeRenderer<T> : IObjectRenderer
    {
        private readonly Func<T, string> renderString;

        public CompositeRenderer(Func<T, string> renderString)
        {
            this.renderString = renderString;
        }

        public string Render(object subject)
        {
            var tSubject = (T)subject;
            return renderString(tSubject);
        }
    }
}