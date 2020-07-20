# WhatsMerged.WinForms
**Q**: *What is WhatsMerged?*<br>
**A**: **WhatsMerged.WinForms** is an application for showing a branch-by-branch "What has been merged where"-overview of your GIT-based projects.

**Q**: *Hoe does WhatsMerged know about my project folders? I sure hope it doesn't scan all my drives entirely...*<br>
**A**: At first start, you are asked to specify which folders to scan. The default is your Windows Home Dir, but you can change it to whatever applies to you. Multiple folders are supported, just put each on a separate line, and folder exclusions are also supported (examples are in the folder setup screen). You can change this any time using the [🗲] (lightning bolt) button at top left.

**Q**: *Hoe does WhatsMerged get access to my repos?*<br>
**A**: WhatsMerged works by running your locally installed **git.exe** (which must be available through your **`PATH`**). Depending on how your system is configured, it may never ask for access because access is granted using a token that was created by **git.exe** using previously entered credentials.

**Q**: *How does WhatsMerged look?*<br>
**A**: Screenshot:<br>
![WhatsMerged screenshot](WhatsMerged-01.png)

**Q**: *Why does WhatsMerged only work with branches in my remote repos, and not locally?*<br>
**A**: To have a good overview of what is merged where, we can not rely on your local system **always** having **fully up-to-date copies** of **all branches**. There is just too big a a chance that you would get the wrong view if we were to use local branches.

---

Overview of **WhatsMerged.WinForms** properties and features:

- Built using **Visual Studio 2019** and **.NET Core 3.1**.
- The **WhatsMerged.WinForms** project is dual targeted for .NET Core 3.1 and .NET Framework 4.7.2. The reason is that the VS 2019 Forms Designer only works when targeting .NET Framework; offering the same support for .NET Core-only projects is still a [Work-In-Progress](https://devblogs.microsoft.com/dotnet/updates-to-net-core-windows-forms-designer-in-visual-studio-16-5-preview-1/).
- When running, WhatsMerged uses your locally installed **git.exe** to access the repos that you are already using, typically using a previously created access token.
- You can tell WhatsMerged where to look for repo folders, which it will then find by doing a disk scan.
- WhatsMerged works exclusively on *remote branches*. WhatsMerged does not care about your local branches, or whether they have have work-in-progress or local commits or whatnot, because it only examines *remote branches* (prefixed with **`origin/`**).
- WhatsMerged works best with projects for which work typically happens in branches like **`feature/xyz`**, **`bugfix/xyz`**, **`hotfix/xyz`** etc. which then get merged into branches with common names such as **`develop`**, **`test`**, **`demo`**, **`release`** (or **`release/1.1`**, **`release/1.2`**, etc) and finally **`main`** (or **`master`**).
