using System;
using System.IO;

using AppKit;
using Foundation;

namespace ForensicUSB
{
    public partial class ViewController : NSViewController
    {

        private string url;

        public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
           
            // Do any additional setup after loading the view.
        }

        public override NSObject RepresentedObject
        {
            get
            {
                return base.RepresentedObject;
            }
            set
            {
                base.RepresentedObject = value;
                // Update the view, if already loaded.
                
            }
        }

        partial void openDialogBtn(NSButton sender)
        {
            var dlg = NSOpenPanel.OpenPanel;
            dlg.CanChooseFiles = false;
            dlg.CanChooseDirectories = true;
            dlg.AllowsMultipleSelection = false;

            if (dlg.RunModal() == 1)
            {
                // Nab the first file
                var url = dlg.Urls[0];

                locationText.StringValue = "Location: " + url.Path;
                this.url = url.Path;
                disabledCollect.Enabled = true;
            }
        }

        partial void collectFiles(NSButton sender)
        {
            
            try
            {
                string user = Environment.UserName;
                string flashPath = this.url;
                string[] copyPaths = { flashPath + "/copiedFiles/",
                                    flashPath + "/copiedFiles/Safari/",
                                    flashPath + "/copiedFiles/Firefox/",
                                    flashPath + "/copiedFiles/System/",
                                    flashPath + "/copiedFiles/Photos/"};
                //File Paths
                string safariLastSession = "/Users/" + user + "/Library/Safari";
                string firefoxFiles = "/Users/" + user + "/Library/Application Support/Firefox/Profiles/";
                string chromeFiles = "/Users/" + user + "";
                string photosLocation = "/Users/" + user + "/Pictures";
                string fileSharing = "/Library/Preferences/SystemConfiguration/";

                for (int i = 0; i < copyPaths.Length; i++)
                {
                    if (System.IO.Directory.Exists(copyPaths[i]))
                    {
                        System.IO.Directory.Delete(copyPaths[i], true);
                    }

                    System.IO.Directory.CreateDirectory(copyPaths[i]);
                }

                System.IO.File.Copy(fileSharing + "com.apple.smb.server.plist", copyPaths[3] + "com.apple.smb.server.plist", true);
                System.IO.File.Copy(fileSharing + "preferences.plist", copyPaths[3] + "preferences.plist", true);
                System.IO.File.Copy(fileSharing + "com.apple.wifi.message-tracer.plist", copyPaths[3] + "com.apple.wifi.message-tracer.plist", true);
                System.IO.File.Copy(fileSharing + "NetworkInterfaces.plist", copyPaths[3] + "NetworkInterfaces.plist", true);
                System.IO.File.Copy(fileSharing + "com.apple.accounts.exists.plist", copyPaths[3] + "com.apple.accounts.exists.plist", true);

                string[] safariFiles = System.IO.Directory.GetFiles(safariLastSession);
                foreach (string s in safariFiles)
                {
                    if (s.Contains(".plist"))
                    {
                        string fileName = System.IO.Path.GetFileName(s);
                        string destFile = System.IO.Path.Combine(copyPaths[1], fileName);
                        System.IO.File.Copy(s, destFile, true);
                    }
                }

                string[] photoFiles = System.IO.Directory.GetFiles(photosLocation);

                foreach (string p in photoFiles)
                {
                    if (p.Contains(".jpg") || p.Contains(".jpeg") || p.Contains(".png") || p.Contains(".gif") || p.Contains(".tif"))
                    {
                        string fileName = System.IO.Path.GetFileName(p);
                        string destFile = System.IO.Path.Combine(copyPaths[4], fileName);
                        System.IO.File.Copy(p, destFile, true);
                    }
                }

                string[] firefoxProfile = System.IO.Directory.GetDirectories(firefoxFiles);
                foreach(string f in firefoxProfile)
                {
                    string[] firefoxData = System.IO.Directory.GetFiles(f);
                    foreach(string data in firefoxData)
                    {
                        if(data.Contains(".sqlite") || data.Contains(".db"))
                        {
                            string fileName = System.IO.Path.GetFileName(data);
                            string destFile = System.IO.Path.Combine(copyPaths[2], fileName);
                            System.IO.File.Copy(data, destFile, true);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            completedText.StringValue = "Files have been transferred!";
        }
    }
}
