# Subble

A broken music player

---

## Overview

Subble is developed with the aim to be a total modular music manager/player, this is accomplished by having a core library with a list of base interfaces that will be implemented by external plugins. This means that in its core Subble can only load and manage plugins.

## Install

TODO...

## Build

`git clone --recurse-submodules https://github.com/Subble/Subble.git`

- windows: `build.ps1`
- linux: `build.sh`

`dotnet Build/Subble.dll`