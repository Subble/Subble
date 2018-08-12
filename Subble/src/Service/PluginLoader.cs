using System.Collections.Generic;
using System.IO;
using System.Linq;
using Subble.Core;
using Subble.Core.Plugin;

using static Subble.Core.Events.EventsType.Core;
using static Subble.Core.Logger.SubbleLog;

namespace Subble.Service {
    internal static class PluginLoader {
        /// <summary>
        /// Load plugins from folders, each plugin is loaded in is own task
        /// </summary>
        /// <param name="pluginsFolder">Collection of folders to search for plugins</param>
        /// <param name="host"></param>
        /// <returns>False, if any error occurs during loading</returns>
        public static void LoadPlugins (IEnumerable<string> pluginsFolder, ISubbleHost host)
        {
            //Loads top directories in folders
            var directories = LoadTopFolders(pluginsFolder, host);

            //Instaciates plugins, list contains only valid intances
            var plugins = LoadPluginInstances(directories, host);

            InitialisePlugins(plugins, host);
        }

        private static void InitialisePlugins(IEnumerable<ISubblePlugin> plugins, ISubbleHost host, bool ignoreDependencies = false)
        {
            var pluginQueue = new Queue<ISubblePlugin>(plugins.OrderBy(p => p.LoadPriority));
            var skippedPlugins = new List<ISubblePlugin>();
            var initPluginCount = 0;

            while(pluginQueue.Count > 0)
            {
                var plugin = pluginQueue.Dequeue();

                if(!ignoreDependencies && !CheckDependencies(host, plugin))
                {
                    skippedPlugins.Add(plugin);
                    continue;
                }

                if (!plugin.Initialize(host))
                {
                    EmitError(host,
                        $"Error loading plugin '{plugin.Info.Name}' with guid: {plugin.Info.GUID}");
                    continue;
                }

                host.EmitEvent(NEW_PLUGIN, "HOST", plugin.Info);
                initPluginCount++;
            }

            if (skippedPlugins.Count > 0 && initPluginCount > 0)
            {
                InitialisePlugins(skippedPlugins, host);
            }
            else if(skippedPlugins.Count > 0)
            {
                EmitWarning(host, "Failed to load dependencies for some plugins");
                InitialisePlugins(skippedPlugins, host, true);
            }
        }

        /// <summary>
        /// Checks if all dependencies are valid
        /// </summary>
        /// <param name="host"></param>
        /// <param name="plugin"></param>
        /// <returns></returns>
        private static bool CheckDependencies(ISubbleHost host, ISubblePlugin plugin)
        {
            if (!plugin.Dependencies.Any())
                return true;

            foreach (var d in plugin.Dependencies)
            {
                if (!CheckDependency(host, d))
                    return false;
            }

            return true;
        }

        private static bool CheckDependency(ISubbleHost host, Dependency dependency)
        {
            var matchResult = host.ServiceContainer.ValidateDependency(dependency);
            var name = dependency.DependencyType.Name;

            switch (matchResult)
            {
                case 0:
                    EmitWarning(host, "Can't find dependency: " + name);
                    return false;

                case 1:
                    return true;

                case 2:
                    EmitWarning(host, "Invalid dependency version for: " + name);
                    return false;

                default:
                    EmitError(host, "Unkown result on validating dependency: " + name);
                    return false;
            }
        }

        /// <summary>
        /// Instanciate plugins dll
        /// </summary>
        /// <param name="directories"></param>
        /// <param name="host"></param>
        /// <returns></returns>
        private static IEnumerable<ISubblePlugin> LoadPluginInstances(
            IEnumerable<DirectoryInfo> directories, ISubbleHost host)
        {
            //Emits an warning
            void OnError(string path)
                => EmitWarning(host, "Error creating instance for plugin: " + path);

            var list = new List<ISubblePlugin>();

            foreach(var d in directories)
            {
                var (valid, loader) = AssemblyLoader.TryToInitFolder(d);
                if (!valid)
                {
                    EmitWarning(host, "Can't find plugin dll in: " + d.FullName);
                    continue;
                }

                var instance = loader.CreatePluginInstance();
                instance
                    .Some(p => list.Add(p))
                    .None(() => OnError(d.FullName));
            }

            return list;
        }

        /// <summary>
        /// List top folders from plugin folders
        /// </summary>
        /// <param name="folders"></param>
        /// <param name="host"></param>
        /// <returns></returns>
        private static IEnumerable<DirectoryInfo> LoadTopFolders (
            IEnumerable<string> folders, ISubbleHost host)
        {
            foreach (var f in folders)
            {
                if (Directory.Exists(f))
                {
                    foreach (var d in Directory.GetDirectories(f))
                        yield return new DirectoryInfo(d);
                }
                else
                {
                    EmitWarning(host, "Can't find plugin folder: " + f);
                }
            }
        }

        private static void EmitWarning(ISubbleHost host, string message)
            => host.EmitEvent(LOG, "HOST", GetWarningLog(message));

        private static void EmitError(ISubbleHost host, string message)
            => host.EmitEvent(LOG, "HOST", GetErrorLog(message));
    }
}