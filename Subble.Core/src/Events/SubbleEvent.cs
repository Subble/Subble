using System;
using System.Reactive.Linq;
using Subble.Core.Func;

namespace Subble.Core.Events
{
    /// <summary>
    /// ISubbleEvent implementation
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SubbleEvent<T> : ISubbleEvent<T>
    {
        public SubbleEvent()
            => InitNewEvent("System", "Unkown", Option.None<T>());

        public SubbleEvent(string type, string source)
            => InitNewEvent(source, type, Option.None<T>());

        public SubbleEvent(string type, string source, T payload)
            => InitNewEvent(source, type, Option.Some(payload));

        public SubbleEvent(ISubbleEvent source)
        {
            Id = source.Id;
            Timestamp = source.Timestamp;
            Type = source.Type;
            Payload = source.Payload.ToTyped<T>();
        }

        private void InitNewEvent(string source, string type, Option<T> payload)
        {
            Id = Guid.NewGuid();
            Timestamp = DateTime.Now;
            Type = type;
            Payload = payload;
            Source = source;
        }

        public Guid Id { get; private set; }
        public string Type { get; private set; }
        public Option<T> Payload { get; private set; }
        public DateTime Timestamp { get; private set; }
        public string Source { get; private set; }

        Guid ISubbleEvent.Id { get { return Id; } }
        string ISubbleEvent.Type { get { return Type; } }
        Option ISubbleEvent.Payload { get { return Payload; } }
        DateTime ISubbleEvent.Timestamp { get { return Timestamp; } }
        string ISubbleEvent.Source { get { return Source; } }

        public override string ToString()
        {
            return $"{Source}:{Type}";
        }
    }
}
