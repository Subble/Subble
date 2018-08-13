using Subble.Core;
using Subble.Core.Events;
using Subble.Core.Logger;
using Subble.Core.Plugin;
using System;
using System.IO;
using System.Reactive.Linq;
using System.Reflection;

namespace Subble
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var path = CreatePluginsFolder();

            var host = new Host();
            SubscribeLogs(host);
            host.Start(path);
            AwaitUserInput(host);
        }

        private static string GetRunningDirectory()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        private static string CreatePluginsFolder()
        {
            var path = GetRunningDirectory() + "/Plugins";
            Directory.CreateDirectory(path);
            return path;
        }

        private static void SubscribeLogs(ISubbleHost host)
        {
            host.Events
                .Where(e => e.Type == EventsType.Core.LOG)
                .Subscribe(LogEvents);

            host.Events
                .Where(e => e.Type == EventsType.Core.NEW_PLUGIN)
                .Subscribe(LogNewPlugin);
        }

        private static void LogEvents(ISubbleEvent e)
        {
            var info = e.Payload.ToTyped<ILog>();
            info.Some(i =>
            {
                Console.WriteLine($"${e.Source}:{i}");
            });
        }

        private static void LogNewPlugin(ISubbleEvent e)
        {
            var name = e.Payload.ToTyped<IPluginInfo>();
            name.Some(n => Console.WriteLine("Loaded plugin: " + n.Name));
        }

        private static void AwaitUserInput(ISubbleHost host)
        {
            while (true)
            {
                var input = Console.ReadLine();
                
                switch (input)
                {
                    case "exit":
                        return;

                    case "clear":
                        Console.Clear();
                        break;

                    case "":
                        break;

                    default:
                        host.EmitEvent(EventsType.Core.INPUT, "USER", input);
                        break;
                }
            }
        }
    }
}
