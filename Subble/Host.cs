using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Subble.Core;
using Subble.Core.Events;
using Subble.Core.Plugin;
using Subble.Core.ServiceContainer;
using Subble.Service;

using static Subble.Core.Events.EventsType.Core;
using static Subble.Core.Logger.SubbleLog;

namespace Subble
{
    public class Host : ISubbleHost
    {
        public Host()
        {
            Events = InitEvents();
            ServiceContainer = new ServiceContainer(this);
        }

        public SemVersion Version
            => new SemVersion(0, 0, 1);

        private IObserver<ISubbleEvent> EventSource { get; set; }
        public IObservable<ISubbleEvent> Events { get; }

        public IServiceContainer ServiceContainer { get; }

        private IObservable<ISubbleEvent> InitEvents()
        {
            return Observable.Create(
                (IObserver<ISubbleEvent> source) => {
                    EventSource = source;
                    return Disposable.Empty;
                });
        }

        public SubbleEmitResponse EmitEvent(string type, string source)
        {
            return EmitEvent<string>(type, source, null);
        }

        public SubbleEmitResponse EmitEvent<T>(string type, string source, T payload)
        {
            var e = new SubbleEvent<T>(type, source, payload);
            Task.Run(() => EventSource.OnNext(e));
            return new SubbleEmitResponse(false, "Emit", e.Id);
        }

        public void Start()
        {
            LoadInternalPlugins();
            EmitEvent(INIT, "HOST");
            EmitEvent(LOG, "HOST", GetInfoLog("Subble initialized"));
        }

        //Load plugins built internly
        private void LoadInternalPlugins()
        {
            ConfigManager.RegisterService(this);
        }
    }
}
