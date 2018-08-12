using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reflection;
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
            EventSource = new List<IObserver<ISubbleEvent>>();
            Events = InitEvents();
            ServiceContainer = new ServiceContainer(this);

            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            WorkingDirectory = new DirectoryInfo(path);
        }

        public SemVersion Version
            => new SemVersion(0, 0, 1);

        private List<IObserver<ISubbleEvent>> EventSource { get; }
        public IObservable<ISubbleEvent> Events { get; }

        public IServiceContainer ServiceContainer { get; }

        public DirectoryInfo WorkingDirectory { get; }

        private IObservable<ISubbleEvent> InitEvents()
        {
            return Observable.Create(
                (IObserver<ISubbleEvent> source) =>
                {
                    EventSource.Add(source);
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
            
            foreach(var client  in EventSource)
                Task.Run(() => client.OnNext(e));

            return new SubbleEmitResponse(false, "Emit", e.Id);
        }


        public void Start(string path)
        {
            PluginLoader.LoadPlugins(new string[1] { path }, this);
            EmitEvent(INIT, "HOST");
            EmitEvent(LOG, "HOST", GetInfoLog("Subble initialized"));
        }
    }
}
