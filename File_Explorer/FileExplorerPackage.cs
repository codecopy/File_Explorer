using System;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.Win32;

namespace File_Explorer
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]       
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(FileExplorerPackage.PackageGuidString)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "pkgdef, VS and vsixmanifest are valid VS terms")]
    public sealed class FileExplorerPackage : Package
    {
        public const string PackageGuidString = "255228a6-0f7a-4614-baae-a236cbb14390";

        public FileExplorerPackage()
        {
        }

        #region Package Members

        protected override void Initialize()
        {
            FileExplorer.Initialize(this);
            base.Initialize();
        }

        #endregion
    }
}
