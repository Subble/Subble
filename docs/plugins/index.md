# Plugins
---

## Installation

To install a Plugin just copy it folder to the `Plugins` folder in the Subble directory.
Each plugin should have is own folder and the entry plugin file should be named as `Plugin.[GUID].dll`.

So the directory structure should look like:

    
    Subble
      Plugins
        MyPlugin
            Plugin.d204b7a4-a73b-4d2b-8ca2-4ba45a888c59.dll
        OtherPlugin
            Plugin.59025e0f-438d-4334-a0f6-ff9c68794439.dll

## Plugin types

We can divide the plugins in 2 main groups:

 - Function Plugins:
    This are the plugins that extend functionality of the core application

- Service Plugins:
    This are the plugins that create and register new services to be used by other plugins

Function plugins usually depends on other services plugins, its the user responsibility to make sure that all required dependencies are present

## Plugin development

Plugins are developed using [.NET Core](https://docs.microsoft.com/en-us/dotnet/core/index) and referencing the `Subble.Core.dll` library present in the Subble directory, check the [quick-start guide](quick-start.md) to see a basic example of a working plugin

The development can be done in any OS supported by .net core, [click here](https://www.microsoft.com/net/learn/get-started) to see a list of supported OS