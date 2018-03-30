using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Loader;
using System.IO;
using System.Linq;
using Subble.Core.Plugin;

using static Subble.Core.Func.Option;
using Subble.Core.Func;

namespace Subble.Service
{
    internal class AssemblyLoader
    {
        public DirectoryInfo Directory { get; }
        public List<FileInfo> DependenciesDll { get; private set; }
        public FileInfo PluginDll { get; private set; }

        private AssemblyLoader(DirectoryInfo directory)
        {
            Directory = directory;
            AssemblyLoadContext.Default.Resolving += ResolveDependencies;
        }

        /// <summary>
        /// Attemps to resolve dependencies
        /// </summary>
        /// <param name="arg1"></param>
        /// <param name="arg2"></param>
        /// <returns></returns>
        private Assembly ResolveDependencies(AssemblyLoadContext arg1, AssemblyName arg2)
        {
            //In case multiple folders of dependencies this allows to have dupplicate dll names
            foreach(var dllFile in DependenciesDll?.Where(x => x.Name.IndexOf(arg2.Name) == 0))
            {
                var instance = LoadAssemblyFromFile(dllFile);
                if (instance is null)
                    continue;

                return instance;
            }

            return null;
        }

        /// <summary>
        /// Load an assembly from a DLL file
        /// </summary>
        /// <param name="info"></param>
        /// <returns>Return null if file not found</returns>
        private Assembly LoadAssemblyFromFile(FileInfo info)
        {
            if (info == null || !info.Exists)
                return null;

            return AssemblyLoadContext.Default.LoadFromAssemblyPath(info.FullName);
        }

        /// <summary>
        /// Loads the Plugin.dll
        /// </summary>
        /// <param name="files">Collection of files to search for the plugin</param>
        /// <returns></returns>
        private bool LoadPluginDll(IEnumerable<FileInfo> files)
        {
            var pluginFile = files.FirstOrDefault(f =>
                string.Equals(f.Name, "plugin.dll", StringComparison.OrdinalIgnoreCase));

            if (pluginFile is null)
                return false;

            PluginDll = pluginFile;
            return true;
        }

        private bool LoadDependencies(IEnumerable<FileInfo> files)
        {
            //If no plugin dll was found, just skip this
            if (PluginDll is null)
                return false;

            DependenciesDll = new List<FileInfo>();

            foreach(var f in files)
            {
                if (f.FullName == PluginDll.FullName)
                    continue;

                DependenciesDll.Add(f);
            }

            return true;
        }

        /// <summary>
        /// Enumerates dependencies and the Plugin dll
        /// </summary>
        /// <returns></returns>
        private bool LoadFiles()
        {
            var files = Directory.EnumerateFiles("*.dll", SearchOption.AllDirectories);
            return LoadPluginDll(files) && LoadDependencies(files);
        }

        /// <summary>
        /// Creates an intance of the plugin
        /// </summary>
        /// <returns></returns>
        public Option<ISubblePlugin> CreatePluginInstance()
        {
            if (PluginDll is null)
                return None<ISubblePlugin>();

            var pluginAssembly = LoadAssemblyFromFile(PluginDll);
            foreach (var t in pluginAssembly?.GetExportedTypes() ?? new Type[0])
            {
                if (!t.GetInterfaces().Contains(typeof(ISubblePlugin)))
                    continue;

                var instance = Activator.CreateInstance(t) as ISubblePlugin;
                return Some<ISubblePlugin>(instance);
            }

            return None<ISubblePlugin>();
        }

        public static (bool valid, AssemblyLoader loader) TryToInitFolder(DirectoryInfo folder)
        {
            var loader = new AssemblyLoader(folder);
            var isValid = loader.LoadFiles();

            return (isValid, loader);
        }
    }
}
