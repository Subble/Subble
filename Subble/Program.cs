using Subble.Core;
using Subble.Core.Events;
using Subble.Core.Logger;
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
            CreatePluginsFolder();

            var host = new Host();
            SubscribeLogs(host);
            host.Start();

            Console.ReadKey();
        }

        private static string GetRunningDirectory()
        {
            return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        private static void CreatePluginsFolder()
        {
            var path = GetRunningDirectory() + "/Plugins";
            Directory.CreateDirectory(path);
        }

        private static void SubscribeLogs(ISubbleHost host)
        {
            host.Events
                .Where(e => e.Type == EventsType.Core.LOG)
                .Subscribe(LogEvents);
        }

        private static void LogEvents(ISubbleEvent e)
        {
            var info = e.Payload.ToTyped<ILog>();
            info.Some(i =>
            {
                Console.WriteLine($"${e.Source}");
                Console.WriteLine(i.ToString());
            });
        }
    }
}
