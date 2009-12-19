namespace Tigraine.Logging
{
    using System;
    using System.Collections.Generic;

    public abstract class AbstractLogger
    {
        private readonly LogLevel logLevel;

        public AbstractLogger(LogLevel logLevel)
        {
            this.logLevel = logLevel;
        }

        protected abstract void WriteDebug(string message);
        protected abstract void WriteInformation(string message);
        protected abstract void WriteWarning(string message);
        protected abstract void WriteError(string message);

        public void Debug(string message, params object[] parameters)
        {
            if (logLevel > LogLevel.Debug) return;
            var msg = CreateMessage(message, parameters);
            WriteDebug(msg);
        }
        public void Information(string message, params object[] parameters)
        {
            if (logLevel > LogLevel.Information) return;
            var msg = CreateMessage(message, parameters);
            WriteInformation(msg);
        }
        public void Warning(string message, params object[] parameters)
        {
            if (logLevel > LogLevel.Warning) return;
            var msg = CreateMessage(message, parameters);
            WriteWarning(msg);
        }
        public void Error(string message, params object[] parameters)
        {
            if (logLevel > LogLevel.Error) return;
            var msg = CreateMessage(message, parameters);
            WriteError(msg);
        }

        private readonly IDictionary<Type, IObjectRenderer> renderers = new Dictionary<Type, IObjectRenderer>();
        private readonly IObjectRenderer defaultObjectRenderer = new DefaultObjectRenderer();
        private string CreateMessage(string message, object[] parameters)
        {
            var renderedParameters = new string[parameters.Length];
            for(var i = 0; i < parameters.Length; i++)
            {
                var renderer = FindSuitableObjectRenderer(parameters[i].GetType());
                renderedParameters[i] = renderer.Render(parameters[i]);
            }
            return string.Format(message, renderedParameters);
        }

        private IObjectRenderer FindSuitableObjectRenderer(Type type)
        {
            if (renderers.ContainsKey(type))
                return renderers[type];

            var baseType = type.BaseType;
            if (baseType == null) return defaultObjectRenderer;
            if (renderers.ContainsKey(baseType))
            {
                var renderer = renderers[baseType];
                renderers.Add(type, renderer);
            }
            return FindSuitableObjectRenderer(baseType);
        }
    }
}