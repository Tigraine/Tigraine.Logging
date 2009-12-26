namespace Tigraine.Logging
{
    using System;
    using System.Collections.Generic;
    using ObjectRenderers;

    public abstract class AbstractLogger : ILogger
    {
        private readonly LogLevel logLevel;

        protected AbstractLogger(LogLevel logLevel)
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
            for (var i = 0; i < parameters.Length; i++)
            {
                var renderer = FindSuitableObjectRenderer(parameters[i].GetType());
                if (renderer == null) continue;
                renderedParameters[i] = renderer.Render(parameters[i]);
            }
            return string.Format(message, renderedParameters);
        }

        private readonly object lockObject = new object();

        private IObjectRenderer FindSuitableObjectRenderer(Type type)
        {
            if (renderers.ContainsKey(type))
                return renderers[type];

            var baseType = type.BaseType;
            if (baseType == null) return null;
            if (renderers.ContainsKey(baseType))
            {
                var renderer = renderers[baseType];
                lock (lockObject)
                {
                    renderers.Add(type, renderer);
                }
            }
            return FindSuitableObjectRenderer(baseType);
        }

        public void AddObjectRenderer(Type targetType, IObjectRenderer renderer)
        {
            lock (lockObject)
            {
                renderers.Add(targetType, renderer);
            }
        }

        public void AddObjectRenderer<T>(IObjectRenderer renderer)
        {
            AddObjectRenderer(typeof(T), renderer);
        }

        public void RemoveObjectRendererForType(Type targetType)
        {
            lock (lockObject)
            {
                renderers.Remove(targetType);
            }
        }

        public void ClearObjectRenderers()
        {
            lock (lockObject)
            {
                renderers.Clear();
            }
        }
    }
}