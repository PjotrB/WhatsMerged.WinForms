# WhatsMerged.WinForms
A Windows Forms (.NET) front-end for showing a branch-by-branch "What's been merged" overview of any GIT project that you have access to.

**WhatsMerged.WinForms** properties and features:

- Built using the current latest .NET Core 3.0 preview (the first to have Windows Forms support). See below for a detailed list.
- When running, it uses your locally installed **git.exe** (which must be available through your **`PATH`**) to access the repos that you are already using.
- You can tell it where to look for repos.
- It works exclusively on *remote branches*. It is *fine* to be connected with any branch locally, or even to have work in progress - it's all irrelevant, because this tool only uses *remote branches* (prefixed with **`origin/`**).
- It works best with projects for which work typically is done in `feature/xyz` branches + (optionally) e.g. `bugfix/xyz` and/or `hotfix/xyz` branches, which then get merged into branches with common names such as `develop`, `test`, `release` (or `release/1.1`, `release/1.2`, etc) and finally `master`.

Output from **`dotnet list package`**, filtered for "preview":

    Top-level Package                                   Requested                    Resolved
    > Microsoft.NETCore.Platforms                       [3.0.0-preview3.19128.7, )   3.0.0-preview3.19128.7
    > Microsoft.WindowsDesktop.App                      [3.0.0-preview3-27504-2, )   3.0.0-preview3-27504-2
    > runtime.win-x64.Microsoft.NETCore.DotNetAppHost   [3.0.0-preview3-27503-5, )   3.0.0-preview3-27503-5
