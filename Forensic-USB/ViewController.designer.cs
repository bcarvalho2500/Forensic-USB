// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace ForensicUSB
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSTextFieldCell completedText { get; set; }

		[Outlet]
		AppKit.NSButton disabledCollect { get; set; }

		[Outlet]
		AppKit.NSTextField locationText { get; set; }

		[Action ("collectFiles:")]
		partial void collectFiles (AppKit.NSButton sender);

		[Action ("openDialogBtn:")]
		partial void openDialogBtn (AppKit.NSButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (completedText != null) {
				completedText.Dispose ();
				completedText = null;
			}

			if (locationText != null) {
				locationText.Dispose ();
				locationText = null;
			}

			if (disabledCollect != null) {
				disabledCollect.Dispose ();
				disabledCollect = null;
			}
		}
	}
}
