# WhatsMerged.WinForms
A Windows Forms (.NET) front-end for showing a branch-by-branch "What's been merged" overview of any GIT project that you have access to.

**WhatsMerged.WinForms** uses your locally installed **git.exe** (which must be available through your **`PATH`**) to access the repos that you are already using. You can tell it where to look for repos.

**WhatsMerged.WinForms** works exclusively on *remote branches*. It is *fine* to be connected with any branch locally, or even to have work in progress - it's all irrelevant, because this tool only uses *remote branches* (prefixed with **`origin/`**).

**WhatsMerged.WinForms** works best with projects for which work typically is done in `feature/xyz` branches + (optionally) e.g. `bugfix/xyz` and/or `hotfix/xyz` branches, which then get merged into branches with common names such as `develop`, `test`, `release` (or `release/1.1`, `release/1.2`, etc) and finally `master`.
