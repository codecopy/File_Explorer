using System;
using System.ComponentModel.Design;
using System.Globalization;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using EnvDTE;
using File_Explorer.Utilities;
using EnvDTE80;
using System.Collections.Generic;
using System.Linq;

namespace File_Explorer
{
    internal sealed class FileExplorer
    {
        public const int CommandId = 0x0100;

        public static readonly Guid CommandSet = new Guid("2e0c0069-03a0-4e85-a36c-d4129765e929");

        private readonly Package package;

        private FileExplorer(Package package)
        {
            if (package == null)
            {
                throw new ArgumentNullException("package");
            }

            this.package = package;

            OleMenuCommandService commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                var menuCommandID = new CommandID(CommandSet, CommandId);
                var menuItem = new MenuCommand(this.MenuItemCallback, menuCommandID);
                commandService.AddCommand(menuItem);
            }
        }

        public static FileExplorer Instance
        {
            get;
            private set;
        }

        private IServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        public static void Initialize(Package package)
        {
            Instance = new FileExplorer(package);
        }

        private void MenuItemCallback(object sender, EventArgs e)
        {
            var selectedItems = ((UIHierarchy)((DTE2)this.ServiceProvider.GetService(typeof(DTE))).Windows.Item("{3AE79031-E1BC-11D0-8F78-00A0C9110057}").Object).SelectedItems as object[];
            if (selectedItems != null)
            {
                LocateFile.FilesOrFolders((IEnumerable<string>)(from t in selectedItems
                                                                where (t as UIHierarchyItem)?.Object is ProjectItem
                                                                select ((ProjectItem)((UIHierarchyItem)t).Object).FileNames[1]));
            }
        }
    }
}
