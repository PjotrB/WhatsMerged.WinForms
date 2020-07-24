using GitGetter.GitExe;
using GitGetter.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using WhatsMerged.Base;
using WhatsMerged.Base.Helpers;
using WhatsMerged.Base.Models;

namespace WhatsMerged.WinForms.Forms
{
    public partial class MainForm : Form, IErrorReporter
    {
        public static readonly Regex PathCheck = new Regex(@"^[A-Za-z]:\\");

        public List<string> ProjectFolders = null;
        public const string UserSettingsFilename = "WhatsMerged.UserSettings.json";

        public const string DefaultFolderWithExclusion = @"D:\Web^Application";
        public const string DefaultExclusions = "^bin^obj^packages^.vs^node_modules";

        private const int FrozenDividerSize = 3;
        private bool FormLoad_Ready = false;

        private Dictionary<Control, ToolTip> ToolTips = new Dictionary<Control, ToolTip>();

        public UserSettings UserSettings { get; private set; }

        private readonly Engine WMEngine;
        private readonly ContextMenuStrip Menu_LstWorkBranches;
        private readonly ContextMenuStrip Menu_LstMergeBranches;
        private readonly ContextMenuStrip Menu_LstIgnoreBranches;

        /// <summary>
        /// Dictionary for easy mapping from HeaderCellType values to DataGridView Header styles
        /// </summary>
        private readonly Dictionary<HeaderCellType, DataGridViewCellStyle> HeaderStyles;

        /// <summary>
        /// Dictionary for easy mapping from CellType values to DataGridView Cell styles
        /// </summary>
        private readonly Dictionary<CellType, DataGridViewCellStyle> CellStyles;

        #region IErrorReporter implementation

        public void ClearError()
        {
            ShowError(null);
            Utils.PaintNowWhileDiscardingOtherEvents();
        }

        public bool HasError() => txtError.Text.HasValue();

        public void ShowError(string msg)
        {
            if (msg == null)
                txtError.Text = "";
            else if (msg.Length > 0)
                txtError.Text = txtError.Text.HasValue() ? txtError.Text + "\r\n" + msg : msg;

            txtError.Visible = txtError.Text.HasValue();
            Utils.PaintNowWhileDiscardingOtherEvents();
        }

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            WMEngine = new Engine(new Git(), reporter: this);
            WMEngine.SaveProjectSettings = SaveUserSettings;

            ClearError();
            try
            {
                HeaderStyles = GetHeaderStyles(new Font(grid.Font, FontStyle.Bold));
                CellStyles = GetCellStyles(btnMergeSortByDate.Font);

                grid.AutoGenerateColumns = false;
                grid.EnableHeadersVisualStyles = false;
                grid.AllowUserToAddRows = false;
                grid.AllowUserToResizeRows = false;
                grid.ReadOnly = true;
                grid.RowHeadersVisible = false;
                grid.ColumnHeadersVisible = true;
                SetDoubleBuffering(grid, true);

                Menu_LstWorkBranches = Create_Menu_LstWorkBranches();
                Menu_LstMergeBranches = Create_Menu_LstMergeBranches();
                Menu_LstIgnoreBranches = Create_Menu_LstIgnoreBranches();
            }
            catch (Exception ex)
            {
                ShowError("Exception in MainForm Constructor:");
                ShowError(ex.Message);
            }
        }

        private void LoadProjectFolders()
        {
            ProjectFolders = new List<string>();
            if (UserSettings.Folders.Length == 0)
            {
                StatusAdd("No folders configured");
                return;
            }

            SetEnabled(busy: true);
            try
            {
                for (int i = 0; i < UserSettings.Folders.Length; i++)
                {
                    var parts = UserSettings.Folders[i].Split('^');
                    var basePath = parts[0];
                    var foldersToExclude = parts.Skip(1).ToArray();
                    try
                    {
                        Status("Scanning " + basePath + "...");
                        var foundDirs = Disk.LoadSubDirs(basePath, ".git", foldersToExclude).ToArray();
                        ProjectFolders.AddRange(foundDirs);
                    }
                    catch
                    {
                        ShowError("Skipping \"" + basePath + "\": path is invalid or not accessible.");
                    }
                }
                InitProjects();
            }
            finally
            {
                SetEnabled(busy: false);
            }
            Status("Ready");
        }

        private IEnumerable<string> GetDefaultFolders()
        {
            var path = DefaultFolderWithExclusion.Split('^')[0];
            if (Directory.Exists(path))
                yield return DefaultFolderWithExclusion + DefaultExclusions;

            path = Path.Combine(UserHomePath, "source");
            if (Directory.Exists(path))
                yield return path + DefaultExclusions;
        }

        private string UserHomePath => Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");

        private void SetDoubleBuffering(Control ctrl, bool value)
        {
            // Double buffering can make DataGridView slower instead of faster when using Remote Desktop, so in that case -> don't:
            if (System.Windows.Forms.SystemInformation.TerminalServerSession)
                return;

            // In class DataGridView, the property 'DoubleBuffered' is not public for unknown reasons. So reflection to the help:
            PropertyInfo pi = ctrl.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            pi.SetValue(ctrl, value, null);
        }

        private static Dictionary<HeaderCellType, DataGridViewCellStyle> GetHeaderStyles(Font fontForTitle)
        {
            var dict = new Dictionary<HeaderCellType, DataGridViewCellStyle>()
            {
                { HeaderCellType.Regular, new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, WrapMode = DataGridViewTriState.False } },
                { HeaderCellType.Title, new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft, Font = fontForTitle, WrapMode = DataGridViewTriState.False } },
                { HeaderCellType.BranchName, new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, BackColor = Color.LightGreen, WrapMode = DataGridViewTriState.False } }
            };
            CheckDict(dict);
            return dict;
        }

        private static Dictionary<CellType, DataGridViewCellStyle> GetCellStyles(Font fontForDate)
        {
            var dict = new Dictionary<CellType, DataGridViewCellStyle>()
            {
                { CellType.SubTitle, new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft, BackColor = Color.LemonChiffon, WrapMode = DataGridViewTriState.False } },
                { CellType.BranchName, new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleLeft, WrapMode = DataGridViewTriState.False } },
                { CellType.BranchDate, new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, Font = fontForDate, WrapMode = DataGridViewTriState.False } },
                { CellType.Merged, new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, ForeColor = Color.Green, WrapMode = DataGridViewTriState.False } },
                { CellType.NotMerged, new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, ForeColor = Color.Red, WrapMode = DataGridViewTriState.False } },
                { CellType.SelfJoin, new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleCenter, BackColor = Color.LightGray, WrapMode = DataGridViewTriState.False } }
            };
            CheckDict(dict);
            return dict;
        }

        private static void CheckDict<T>(Dictionary<T, DataGridViewCellStyle> dict) where T : struct
        {
            var values = Enum.GetValues(typeof(T));
            foreach (T v in values)
                if (!dict.ContainsKey(v))
                    throw new Exception("Value '" + typeof(T).Name + "." + v.ToString() + "' was not added to Style Dictionary.\r\nTO DO: fix Dictionary initialization code to match the definition of the enum.");
        }

        // Form_Load handler

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Set various tooltips. We use a Helper method because (1) tooltips are horrible to set using the Forms Designer, and (2) this allows
                // to store all tooltips in a dictionary which is used later on to make them work better.
                SetToolTip(btnReloadProject, "Refresh branches from Git");
                SetToolTip(btnUserSettings, "Configure folders");

                SetToolTip(btnWorkVsMerge, "Show which Work branches have been merged into which Merge branches");
                SetToolTip(btnMergeVsWork, "Show if Work branches contain everyting from the latest master, or from other production/release branches");
                SetToolTip(btnWorkAndMergeVsMerge, "Show which Work+Merge branches have been merged into which Merge branches");

                SetToolTip(btnMergeVsItself, "Show which Merge branches have been merged into other Merge branches");
                SetToolTip(btnWorkVsItself, "Show which Work branches have been merged into other Work branches");
                SetToolTip(btnWorkAndMergeVsItself, "Show which Work+Merge branches have been merged into other Work+Merge branches");

                SetToolTip(lstWorkBranches, "Branches can be moved to other lists by using the Right-Click menu, or using Ctrl-Left / Ctrl-Right hotkeys, or Double Click (move into Merge list)");
                SetToolTip(lstMergeBranches, "Branches can be moved to other lists by using the Right-Click menu, or using Ctrl-Left / Ctrl-Right hotkeys, or Double Click (move into Ignore list)");
                SetToolTip(lstIgnoreBranches, "Branches can be moved to other lists by using the Right-Click menu, or using Ctrl-Left / Ctrl-Right hotkeys, or Double Click (move into Merge list)");
            }
            finally
            {
                SetEnabled(busy: false);    // Allow controls to become enabled (taking into account various other dependencies).
                FormLoad_Ready = true;      // Set a marker that the Form has finised loading. This helps prevent update/refresh code from acting 'too soon'.
            }
        }

        private void MainForm_MouseClick(object sender, MouseEventArgs e)
        {
            ActiveControl = null; // This makes it possible to un-set the active control by a click outside of all controls.
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            // After the mainform has become visible, we do 3 things:
            // (1) Load UserSettings from disk.
            // (2) If (1) didn't succeed, then generate default UserSettings + open the Settings Edit box for folder review and/or customization.
            // (3) Scan project folders based on the UserSettings.

            UserSettings = JsonFileHelper.Load<UserSettings>(UserHomePath, UserSettingsFilename, reporter: this);
            if (UserSettings == null)
            {
                UserSettings = new UserSettings(GetDefaultFolders().ToArray());
                EditUserSettings("Default folder list was created, please review or customize", hideCancelButton: true); // This will call LoadProjectFolders() as a result of only being able to click OK
            }
            else
            {
                LoadProjectFolders();
            }

            // If we want to force the user to fix their folders until LoadProjectFolders() produces no more errors, then we can do this:
            //while (HasError())
            //{
            //    EditUserSettings("One or more folders are incorrect, please fix", hideCancel: true); // This will call LoadProjectFolders() as a result of only being able to click OK
            //}
        }

        private void SetToolTip(Control ctrl, string text)
        {
            var tt = new ToolTip { IsBalloon = true, InitialDelay = 500, AutoPopDelay = 30000, ReshowDelay = 500, AutomaticDelay = 500, ShowAlways = true, Active = true };
            tt.SetToolTip(ctrl, text);
            ToolTips.Add(ctrl, tt);
        }

        private void InitProjects()
        {
            cbxProject.DataSource = ProjectFolders;
            cbxProject.SelectedIndex = -1;

            LoadSelectedProject(force: false);
        }

        private void LoadSelectedProject(bool force)
        {
            if (WMEngine == null)
                return;

            var selectedProject = cbxProject.SelectedValue as string;
            if (force || selectedProject != WMEngine.ProjectPath)
            {
                ClearGrid();
                ClearLists();

                if (selectedProject.HasValue())
                {
                    LoadSettingsAndGetGitData(); // Load ProjectSettings from the settings JSON-file + from git.

                    // ProjectSettings.WorkBranches is loaded fresh every time from the repo using Git. This means that initially
                    // it is always filled in alphabetical order (the default order from Git). For us that is not very useful,
                    // so we sort it by Last Commit Date immediately.
                    WMEngine.SortByCommitDate(WorkBranches);
                }
            }
        }

        private void ClearLists()
        {
            lstWorkBranches.DataSource = null;
            lstMergeBranches.DataSource = null;
            lstIgnoreBranches.DataSource = null;
        }

        private void ClearGrid()
        {
            grid.Rows.Clear();
            grid.Columns.Clear();
            Utils.PaintNowWhileDiscardingOtherEvents(); // Allow the grid to refresh & show that it is empty before we start any other processing.
        }

        private void LoadSettingsAndGetGitData()
        {
            if (!FormLoad_Ready) return;
            SetEnabled(busy: true);
            try
            {
                Status("Loading settings...");
                ClearError();

                var projectPath = cbxProject.SelectedValue as string;
                var projectSettings = GetProjectSettings(projectPath);

                WMEngine.ApplyProjectSettings(projectPath, projectSettings);
                if (HasError()) return;

                StatusAdd("Refreshing Git...");
                while (true)
                {
                    ClearError();
                    WMEngine.GitRefresh();
                    if (HasError())
                    {
                        if (txtError.Text.Contains("[deleted]") || txtError.Text.Contains("[new branch]"))
                        {
                            ClearError();
                        }
                        else
                        {
                            var response = Utils.ShowMessage("Please see Git error output.\r\nDo you want to Abort, Retry or Ignore (= proceed without retrying)?", MessageBoxButtons.AbortRetryIgnore);
                            if (response == DialogResult.Abort) return;
                            if (response == DialogResult.Retry) continue;
                        }
                    }
                    break;
                }

                StatusAdd("Loading branches from git...");
                WMEngine.GitLoadBranchesAndUpdateProjectSettings();
                if (HasError()) return;

                Status("Ready");

                lstWorkBranches.DataSource = WorkBranches;
                lstMergeBranches.DataSource = MergeBranches;
                lstIgnoreBranches.DataSource = IgnoreBranches;
                ClearSelectedItems();

                ShowMergeBranchHelper(fromMenu: false);
            }
            finally
            {
                SetEnabled(busy: false);
            }
        }

        private static readonly string[] TypicalMergeBranches = { "develop", "test", "demo", "release", "main", "master" };
        private static readonly string[] TypicalMergeBranchesWithOrigin = TypicalMergeBranches.Select(x => "origin/" + x).ToArray();

        private void ShowMergeBranchHelper(bool fromMenu)
        {
            var branchesList = TypicalMergeBranchesWithOrigin
                .Where(branch => WorkBranches.Any(b => b.StartsWith(branch, StringComparison.OrdinalIgnoreCase)))
                .Select(branch => branch + "*")
                .ToList();

            if (branchesList.Count == 0)
            {
                if (fromMenu)
                    Utils.ShowMessage("There are no branches in the Work list that we would consider as belonging in the Merge list.\r\nIf you want to make changes, you can use the Right-Click Popup menu from each list or selecting a branch + Ctrl/Arrow keys to move branches between lists.");
            }
            else
            {
                var introText = WorkBranches.Count > 0 && MergeBranches.Count == 0
                    ? "This project is new for WhatsMerged. Please select how you want to treat the branches that it contains.\r\n" +
                      "We recognized some popular branch names, please tell us if these are Merge branches or not.\r\n\r\n"
                    : "";

                introText +=
                    "What we call Work branches (the left list) is where you implement your changes and fixes.\r\n" +
                    "What we call Merge branches (the middle list) is where those changes are then merged, typically using PRs.\r\n\r\n" +
                    "Note: these settings are stored in '" + Path.Combine(UserHomePath, UserSettingsFilename) + "'.\r\n\r\n" +
                    "Please check/uncheck which groups of branches to set as Merge branches, with * meaning a wildcard that matches all text:";

                var frm = new CheckboxListForm
                {
                    Intro = introText,
                    Items = branchesList,
                    StartPosition = FormStartPosition.CenterParent
                };
                frm.ShowDialog(this);

                if (frm.Items.Count > 0)
                {
                    branchesList.Clear();
                    for (var i = 0; i < frm.Items.Count; i++)
                    {
                        var branch = frm.Items[i].RemoveAtEnd("*");
                        branchesList.AddRange(WorkBranches.Where(b => b.StartsWith(branch)));
                    }

                    WorkBranches.RemoveRange(branchesList);
                    MergeBranches.AddRange(branchesList);

                    WMEngine.SortByCommitDate(MergeBranches); // Workbranches always gets sorted on load, so no need to do that again.
                    lstMergeBranches.SelectedIndex = -1;

                    SaveUserSettings();
                }
            }
        }

        private Settings GetProjectSettings(string projectPath)
        {
            UserSettings.SettingsPerProject.TryGetValue(projectPath, out Settings projectSettings);
            if (projectSettings == null)
            {
                projectSettings = new Settings();
                UserSettings.SettingsPerProject.Add(projectPath, projectSettings);
            }
            return projectSettings;
        }

        private void Status(string s)
        {
            lblStatus.Text = s;
            Utils.PaintNowWhileDiscardingOtherEvents();
        }

        private void StatusAdd(string s)
        {
            Status(lblStatus.Text + " " + s);
        }

        private void SetEnabled(bool busy = false)
        {
            Cursor = busy ? Cursors.WaitCursor : Cursors.Default;
            if (busy)
            {
                Focus(); // It is important that we set Focus to the Form. Reason: if Focus stays on a Button that becomes disabled, then Focus can't be moved with the Tab-key later (only by clicking with the mouse).
                ActiveControl = null;
            }

            // Controls that can be used even if no project has been loaded:
            cbxProject.Enabled = !busy;
            btnUserSettings.Enabled = !busy;

            var hasProject = cbxProject.SelectedIndex > -1;
            var enabled = !busy && hasProject;

            // Controls that can only be used if a project has been loaded:
            btnReloadProject.Enabled = enabled;

            btnWorkVsMerge.Enabled = enabled;
            btnMergeVsWork.Enabled = enabled;
            btnWorkAndMergeVsMerge.Enabled = enabled;

            btnWorkVsItself.Enabled = enabled;
            btnMergeVsItself.Enabled = enabled;
            btnWorkAndMergeVsItself.Enabled = enabled;

            lstWorkBranches.Enabled = enabled;
            lstMergeBranches.Enabled = enabled;
            lstIgnoreBranches.Enabled = enabled;

            btnWorkSortByDate.Enabled = enabled;
            btnMergeSortByDate.Enabled = enabled;
            btnIgnoreSortByDate.Enabled = enabled;

            if (!busy)
            {
                // Tooltips on Buttons somehow get deactivated by a click on the Button (meaning they no longer appear after that). There seems to be no property that we can set to prevent this.
                // Discussion of this issue + possible solutions:
                // https://social.msdn.microsoft.com/Forums/windows/en-US/92c71938-b1e4-4dc2-bcd4-6036cfef3cea/tooltip-disappear-after-clicking-the-control?forum=winforms
                // A variation of the solutions from the link above has been used below: we deactivate each tooltip, hide it, and then reactivate it.
                foreach (Control ctrl in ToolTips.Keys)
                {
                    var tt = ToolTips[ctrl];
                    tt.Active = false;
                    tt.Hide(ctrl);
                    tt.Active = true;
                }
            }

            // Process Windows Paint events, and throw away all other events:
            Utils.PaintNowWhileDiscardingOtherEvents();
        }

        private void OnEnterSelectNextControl(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                SelectNextControl((Control)sender, forward: true, tabStopOnly: true, nested: false, wrap: false);
                e.Handled = true;
            }
        }

        // Handling code for the various What-Has-Been-Merged-Where buttons

        private void BtnWorkVsMerge_Click(object sender, EventArgs e) => ShowMergeStatusInGrid(GetButtonText(sender), fromWork: true, toMerge: true);

        private void BtnMergeVsWork_Click(object sender, EventArgs e) => ShowMergeStatusInGrid(GetButtonText(sender), fromMerge: true, toWork: true);

        private void BtnWorkAndMergeVsMerge_Click(object sender, EventArgs e) => ShowMergeStatusInGrid(GetButtonText(sender), fromWork: true, fromMerge: true, toMerge: true);

        private void BtnWorkVsItself_Click(object sender, EventArgs e) => ShowMergeStatusInGrid(GetButtonText(sender), fromWork: true, toWork: true);

        private void BtnMergeVsItself_Click(object sender, EventArgs e) => ShowMergeStatusInGrid(GetButtonText(sender), fromMerge: true, toMerge: true);

        private void BtnWorkAndMergeVsItself_Click(object sender, EventArgs e) => ShowMergeStatusInGrid(GetButtonText(sender), fromWork: true, fromMerge: true, toWork: true, toMerge: true);

        private string GetButtonText(object sender) => (sender as Button)?.Text;

        private void ShowMergeStatusInGrid(string title, bool fromWork = false, bool fromMerge = false, bool toWork = false, bool toMerge = false)
        {
            Status($"Gathering data for {title} overview...");
            SetEnabled(busy: true);
            try
            {
                ClearError();
                ClearGrid();

                if ((fromWork || toWork) && WorkBranches.IsNullOrEmpty())
                {
                    ShowError($"{title}: {lblWorkBranches.Text} list is empty.");
                    return;
                }
                if ((fromMerge || toMerge) && MergeBranches.IsNullOrEmpty())
                {
                    ShowError($"{title}: {lblMergeBranches.Text} list is empty.");
                    return;
                }

                var mergeTable = WMEngine.GetMergeTable(title, fromWork, fromMerge, toWork, toMerge);
                if (HasError() || mergeTable == null) return;
                LoadGrid(mergeTable);
                StatusAdd("Done.");
            }
            catch (Exception ex)
            {
                ShowError("Exception: " + ex.Message);
                StatusAdd("Error occurred.");
            }
            finally
            {
                SetEnabled(busy: false);
            }
        }

        private void LoadGrid(MergeTable table)
        {
            var lastFrozenColIndex = table.ColFrozenCount - 1;
            var lastFrozenRowIndex = table.RowFrozenCount - 1;

            for (var col = 0; col < table.ColCount; col++)
            {
                var colHeader = table.ColHeaders?[col];
                var colHeaderCell = colHeader == null ? null : new DataGridViewColumnHeaderCell { Style = HeaderStyles[colHeader.Type], Value = colHeader.Text };
                var column = new DataGridViewColumn
                {
                    Frozen = col <= lastFrozenColIndex,
                    DividerWidth = col == lastFrozenColIndex ? FrozenDividerSize : 0,
                    HeaderCell = colHeaderCell,
                    CellTemplate = new DataGridViewTextBoxCell(),
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells
                };
                grid.Columns.Add(column);
            }

            for (var row = 0; row < table.RowCount; row++)
            {
                var rowHeader = table.RowHeaders?[row];
                var rowHeaderCell = rowHeader == null ? null : new DataGridViewRowHeaderCell { Style = HeaderStyles[rowHeader.Type], Value = rowHeader.Text };
                var gridRow = new DataGridViewRow
                {
                    Frozen = row <= lastFrozenRowIndex,
                    DividerHeight = row == lastFrozenRowIndex ? FrozenDividerSize : 0,
                    HeaderCell = rowHeaderCell
                };
                for (var col = 0; col < table.ColCount; col++)
                {
                    var cell = table.Cells[row, col];
                    var gridCell = new DataGridViewTextBoxCell { Value = cell.Text, Style = CellStyles[cell.Type] };
                    // We already set ReadOnly at grid level, so there is no need to do it here again. Also: ReadOnly of an individual grid CELL can only be set after adding it to a grid ROW.
                    gridRow.Cells.Add(gridCell);
                }
                grid.Rows.Add(gridRow);
            }
        }

        // Shortcuts for accessing the BindingLists from the Engine.ProjectSettings object

        private BindingList<string> WorkBranches => WMEngine.ProjectSettings.WorkBranches;
        private BindingList<string> MergeBranches => WMEngine.ProjectSettings.MergeBranches;
        private BindingList<string> IgnoreBranches => WMEngine.ProjectSettings.IgnoreBranches;

        // MouseDown handlers for all 3 ListBoxes. They handle both left-click (deselect item in other ListBoxes) and right-click (show the proper menu at the mouse-click location).

        private void LstWorkBranches_MouseDown(object sender, MouseEventArgs e) => BranchListMouseDown(e, lstWorkBranches, Menu_LstWorkBranches);

        private void LstMergeBranches_MouseDown(object sender, MouseEventArgs e) => BranchListMouseDown(e, lstMergeBranches, Menu_LstMergeBranches);

        private void LstIgnoreBranches_MouseDown(object sender, MouseEventArgs e) => BranchListMouseDown(e, lstIgnoreBranches, Menu_LstIgnoreBranches);

        private void BranchListMouseDown(MouseEventArgs e, ListBox listbox, ContextMenuStrip menu)
        {
            ClearSelectedItems(exceptFor: listbox);
            Status("Ready");

            if (e.Button != MouseButtons.Right) return;
            var index = listbox.IndexFromPoint(e.Location);
            if (index != ListBox.NoMatches)
            {
                listbox.SelectedIndex = index;
                menu.Show(Cursor.Position);
                menu.Visible = true;
            }
        }

        private void ClearSelectedItems(ListBox exceptFor = null)
        {
            if (lstWorkBranches != exceptFor) lstWorkBranches.SelectedIndex = -1;
            if (lstMergeBranches != exceptFor) lstMergeBranches.SelectedIndex = -1;
            if (lstIgnoreBranches != exceptFor) lstIgnoreBranches.SelectedIndex = -1;
        }

        /// <summary>
        /// Move the selected item up or down inside listbox (if possible) while keeping it selected. Then save the changed list if needed.
        /// </summary>
        /// <param name="listBox"></param>
        /// <param name="delta"></param>
        private void MoveSelectedItem(ListBox listBox, int delta)
        {
            int index = listBox.SelectedIndex;
            if (index < 0 || delta == 0) return;

            int newIndex = index + delta;
            if (newIndex < 0 || newIndex > listBox.Items.Count - 1) return;

            var list = (BindingList<string>)listBox.DataSource;             // Get the BindingList from the ListBox
            (list[index], list[newIndex]) = (list[newIndex], list[index]);  // Swap items using Tuple assignment (it will sync to the ListBox)
            listBox.SelectedIndex = newIndex;

            if (WMEngine.IsListForSave(list)) SaveUserSettings();
        }

        /// <summary>
        /// If listBoxTarget is not null, then remove the selected item from the source ListBox and append it to the target ListBox, where we also make it selected and focused. Then save the changed lists if needed.
        /// </summary>
        /// <param name="listBoxSource"></param>
        /// <param name="listBoxTarget"></param>
        private void MoveSelectedItem(ListBox listBoxSource, ListBox listBoxTarget)
        {
            if (listBoxTarget == null) return;

            var listSource = (BindingList<string>)listBoxSource.DataSource; // Get the BindingLists from source + target ListBoxes
            var listTarget = (BindingList<string>)listBoxTarget.DataSource;

            var index = listBoxSource.SelectedIndex;    // Get the source item
            if (index < 0) return; // Happens e.g. if user "tabs" into Listbox without making an item selected.

            var itemToMove = listSource[index];

            var topIndex = listBoxSource.TopIndex;      // Source ListBox: Get scroll position, remove item from BindingList (it will sync to the ListBox), make no item selected, restore scroll position
            listSource.RemoveAt(index);
            listBoxSource.SelectedIndex = -1;
            listBoxSource.TopIndex = Math.Min(listSource.Count, topIndex);

            listTarget.Add(itemToMove);                 // Destination ListBox: add item to BindingList (it will sync to the ListBox), make the item selected, give focus to ListBox
            listBoxTarget.SelectedIndex = listTarget.Count - 1;
            listBoxTarget.Focus();

            if (WMEngine.IsListForSave(listSource, listTarget)) SaveUserSettings();
        }

        // KeyDown event handlers for all 3 ListBoxes

        private void LstWorkBranches_KeyDown(object sender, KeyEventArgs e) => BranchListKeyDown(e, lstWorkBranches, null, lstMergeBranches);

        private void LstMergeBranches_KeyDown(object sender, KeyEventArgs e) => BranchListKeyDown(e, lstMergeBranches, lstWorkBranches, lstIgnoreBranches);

        private void LstIgnoreBranches_KeyDown(object sender, KeyEventArgs e) => BranchListKeyDown(e, lstIgnoreBranches, lstMergeBranches, null);

        private void BranchListKeyDown(KeyEventArgs e, ListBox activeListBox, ListBox listBoxLeft, ListBox listBoxRight)
        {
            if (e.Control)
            {
                e.Handled = true; // We mark all Ctrl-key-combos as Handled, so no 'default' behaviour remains, regardless of if we really act on a Ctrl-key-combo or not.

                switch (e.KeyCode)
                {
                    case Keys.Up:
                        MoveSelectedItem(activeListBox, -1);
                        break;
                    case Keys.Down:
                        MoveSelectedItem(activeListBox, +1);
                        break;
                    case Keys.Left:
                        MoveSelectedItem(activeListBox, listBoxLeft);
                        break;
                    case Keys.Right:
                        MoveSelectedItem(activeListBox, listBoxRight);
                        break;
                }
            }
        }

        // Handling of DoubleClick for the 3 ListBoxes

        private void LstWorkBranches_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var keyEvent = new KeyEventArgs(Keys.Control | Keys.Right); // simulate a "Ctrl-Right" event
            LstWorkBranches_KeyDown(sender, keyEvent);
        }

        private void LstMergeBranches_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var keyEvent = new KeyEventArgs(Keys.Control | Keys.Right); // simulate a "Ctrl-Right" event
            LstMergeBranches_KeyDown(sender, keyEvent);
        }

        private void LstIgnoreBranches_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            var keyEvent = new KeyEventArgs(Keys.Control | Keys.Left);  // simulate a "Ctrl-Left" event
            LstIgnoreBranches_KeyDown(sender, keyEvent);
        }

        // Handling of Click for the 3 sort-by-date Buttons

        private void BtnWorkSortByDate_Click(object sender, EventArgs e) => SortByCommitDate(lstWorkBranches);

        private void BtnMergeSortByDate_Click(object sender, EventArgs e) => SortByCommitDate(lstMergeBranches);

        private void BtnIgnoreSortByDate_Click(object sender, EventArgs e) => SortByCommitDate(lstIgnoreBranches);

        private void SortByCommitDate(ListBox lbx)
        {
            Status("Sorting...");
            SetEnabled(busy: true);
            try
            {
                var list = (BindingList<string>)lbx.DataSource;
                WMEngine.SortByCommitDate(list);
                lbx.SelectedIndex = -1;
            }
            finally
            {
                Status("Ready");
                SetEnabled(busy: false);
            }
        }

        private void BtnReloadProject_Click(object sender, EventArgs e) => LoadSelectedProject(force: true);

        // Having a higlighted selected cell in the grid serves no purpose. There is nothing the user can do with it, so
        // we un-select it as soon as one is made. Every click in the grid will cause a selection.
        private void Grid_SelectionChanged(object sender, EventArgs e) => grid.ClearSelection();

        private ContextMenuStrip Create_Menu_LstWorkBranches()
        {
            return new ContextMenuStrip()
                .WithItem("Set as Merge branch | Ctrl ->", () => MoveSelectedItem(lstWorkBranches, lstMergeBranches))
                .WithItem("Set as Ignored branch | Ctrl -> ->", () => MoveSelectedItem(lstWorkBranches, lstIgnoreBranches))
                .WithSeparator()
                .WithItem("Recommend me items to be moved from the Work list to the Merge list", () => ShowMergeBranchHelper(fromMenu: true));
        }

        private ContextMenuStrip Create_Menu_LstMergeBranches()
        {
            return new ContextMenuStrip()
                .WithItem("Set as Work branch | Ctrl <-", () => MoveSelectedItem(lstMergeBranches, lstWorkBranches))
                .WithItem("Set as Ignored branch | Ctrl ->", () => MoveSelectedItem(lstMergeBranches, lstIgnoreBranches));
        }

        private ContextMenuStrip Create_Menu_LstIgnoreBranches()
        {
            return new ContextMenuStrip()
                .WithItem("Set as Work branch | Ctrl <- <-", () => MoveSelectedItem(lstIgnoreBranches, lstWorkBranches))
                .WithItem("Set as Merge branch | Ctrl <-", () => MoveSelectedItem(lstIgnoreBranches, lstMergeBranches));
        }

        private void CbxProject_KeyDown(object sender, KeyEventArgs e)
        {
            if (!cbxProject.DroppedDown)
            {
                // If the ComboBox is not expanded, then we prevent the Up/Down/Home/End/PgUp/PgDn keys from changing the selection because
                // allowing that to happen would trigger undesired immediate switch to a different Git Project.
                // Instead of that, we tell the ComboBox to expand. Later, on collapse of the Combox, the then-active Selected Item is applied.
                if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Home || e.KeyCode == Keys.End || e.KeyCode == Keys.PageUp || e.KeyCode == Keys.PageDown)
                {
                    e.Handled = true;               // Mark the event as Handled, so default behaviour to change the selection item is suppressed.
                    cbxProject.DroppedDown = true;  // Make the dropdown-part appear. This triggers the associated event which sets the value of CbxProject_IsExpanded.
                }
            }
        }

        // SelectedIndexChanged for all 3 ListBoxes. These handle the situation where person uses TAB key followed by Up or Down, which causes the focused ListBox to get a Selected Item. And then we de-select the other ListBoxes.

        private void LstWorkBranches_SelectedIndexChanged(object sender, EventArgs e) => DeSelectTheOthersIfSelected(lstWorkBranches);

        private void LstMergeBranches_SelectedIndexChanged(object sender, EventArgs e) => DeSelectTheOthersIfSelected(lstMergeBranches);

        private void LstIgnoreBranches_SelectedIndexChanged(object sender, EventArgs e) => DeSelectTheOthersIfSelected(lstIgnoreBranches);

        private void DeSelectTheOthersIfSelected(ListBox listbox)
        {
            if (listbox.SelectedIndex > -1)
                ClearSelectedItems(exceptFor: listbox);
        }

        private void CbxProject_DropDownClosed(object sender, EventArgs e) => LoadSelectedProject(force: false);

        private void BtnUserSettings_Click(object sender, EventArgs e) => EditUserSettings();

        private void EditUserSettings(string titleText = null, bool hideCancelButton = false)
        {
            ClearError();
            SetEnabled(busy: true);
            try
            {
                var frm = new UserSettingsForm
                {
                    FolderText = string.Join("\r\n", UserSettings.Folders),
                    StartPosition = FormStartPosition.CenterParent,
                    TitleText = titleText,
                    HideCancelButton = hideCancelButton
                };

                // Show the Settings form, and keep doing that until either (a) the user presses Cancel, or (b) the user clicks OK and the entered
                // folders pass validation. In case of (b), the entered folders are applied and scanned for projects, and the loaded project is cleared.
                while (true)
                {
                    var result = frm.ShowDialog(this);
                    if (result == DialogResult.Cancel)
                        break;

                    var folders = frm.FolderText
                        .Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(p => p.Trim())
                        .Where(p => p.HasValue())
                        .ToArray();

                    var error = ValidateFolders(folders);
                    if (error == null)
                    {
                        UserSettings.Folders = folders;
                        SaveUserSettings();
                        LoadProjectFolders();
                        WMEngine.ApplyProjectSettings(null, null);
                        break;
                    }
                    ShowMessage(error);
                }
            }
            finally
            {
                SetEnabled(busy: false);
                ActiveControl = cbxProject;
                cbxProject.Focus();
            }
        }

        private void SaveUserSettings() => JsonFileHelper.Save(UserSettings, UserHomePath, UserSettingsFilename, reporter: this);

        private void ShowMessage(string msg) => new MessageForm { Message = msg, StartPosition = FormStartPosition.CenterParent }.ShowDialog(this);

        private string ValidateFolders(string[] folders)
        {
            var paths = new List<string>();
            string error = null;
            for (int i = 0; i < folders.Length; i++)
            {
                var parts = folders[i]
                    .Split('^')
                    .Select(p => p.Trim())
                    .ToArray();

                if (!Path.IsPathRooted(parts[0]) || !PathCheck.IsMatch(parts[0]) || parts[0].IndexOf(':', 2) > -1)
                    error = $"\"{parts[0]}\" is invalid or not fully qualified.";

                if (error != null)
                {
                    try
                    {
                        parts[0] = Path.GetFullPath(parts[0]); // Throws exception if the path is invalid.
                    }
                    catch
                    {
                        error = $"\"{parts[0]}\" is not a valid path.\r\nPlease fix.";
                    }
                }

                // Some simple rules for the exclude parts:
                if (error == null && parts.Skip(1).Any(part => part.Contains(":")))
                    error = "The exclude-parts can not contain drive letters";

                if (error == null && parts.Skip(1).Any(part => part.Contains("\\")))
                    error = "The exclude-parts can not contain backslashes";

                if (error == null && paths.Contains(parts[0].ToLowerInvariant()))
                    error = $"\"{parts[0]}\" is present multiple times.";

                if (error == null)
                {
                    paths.Add(parts[0].ToLowerInvariant());
                    folders[i] = string.Join("^", parts.Where(p => p.HasValue()));
                }
                else
                {
                    break;
                }
            }
            return error;
        }
    }
}