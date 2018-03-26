///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

const string SubblePath = "Subble/Subble.csproj";
const string PluginsPath = "Plugins";
const string Output = "./Build";
const string OutputPlugins = Output + "/Plugins";

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(ctx =>
{
   // Executed BEFORE the first task.
   Information("Running tasks...");
});

Teardown(ctx =>
{
   // Executed AFTER the last task.
   Information("Finished running tasks.");
});

///////////////////////////////////////////////////////////////////////////////
// TASKS
///////////////////////////////////////////////////////////////////////////////

IEnumerable<DirectoryInfo> LoadPlugins(string path){
    var info = new System.IO.DirectoryInfo(path);
    if(!info.Exists) throw new IOException($"Can't find directory: {path}");

    return info.GetDirectories();
}

void PublishPlugin(DirectoryInfo info){
    var name = info.Name;
    var project = info.GetFiles("*.csproj", SearchOption.AllDirectories).FirstOrDefault();

    if(project is null || !project.Exists)
    {
        Information("Can't find project in: " + info.FullName);
        return;
    }

    Information("Found project in: " + project.FullName);

    var projOutput = System.IO.Path.Combine(OutputPlugins, name);
    DotNetCorePublish(project.FullName, new DotNetCorePublishSettings{OutputDirectory = projOutput});
}

Task("Clean")
.Does(() => {
    var dirInfo = new System.IO.DirectoryInfo("Build");
    if(dirInfo.Exists) dirInfo.Delete(true);
});

Task("Subble")
.IsDependentOn("Clean")
.Does(() => {
    DotNetCorePublish(SubblePath, new DotNetCorePublishSettings{OutputDirectory = "Build"});
});

Task("Plugins")
.IsDependentOn("Subble")
.Does(() => {
    foreach(var d in LoadPlugins(PluginsPath))
        PublishPlugin(d);
});

Task("Default")
.IsDependentOn("Plugins")
.Does(() => {
    Information("Completed!");
});

RunTarget(target);