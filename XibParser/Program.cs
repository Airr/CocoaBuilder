﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartmobili.Cocoa
{
    class Program
    {
        static void Main(string[] args)
        {
#if TEST
            string xibPath = @"C:/Developer/cygwin/home/v.richomme/CocoaBuilder/Tests/Button/ButtonTextAlign/ButtonTextAlign/en.lproj/NSButton.xib";
            //string xibPath = @"C:\cygwin\home\Vincent\projects\CocoaBuilder\Tests\Button\ButtonTextAlign\ButtonTextAlign\en.lproj\ButtonTextAlign.xib";
            XibParser xibParser = new XibParser(xibPath);
            var ibArchive = xibParser.Deserialize();


            NSWindowTemplate nsWindow = (NSWindowTemplate)ibArchive.Data.RootObjects.Where(o =>
                (o != null) && (o.GetType() == typeof(NSWindowTemplate))).FirstOrDefault();

            if (nsWindow != null)
            {
                if (nsWindow.WindowView != null)
                {
                    // We want to see differences between buttons starting from the upper button to the lower one
                    // Note tha origin in cocoa is the left bottom side
                    var btnArray = nsWindow.WindowView.SubViews.Where(o => (o != null) && (o.GetType() == typeof(NSButton))).OrderByDescending(x => ((NSButton)x).Frame.Origin.Y).ToArray();
                    if (btnArray != null && btnArray.Length > 0)
                    {
                        KellermanSoftware.CompareNetObjects.CompareObjects compareObjects = new KellermanSoftware.CompareNetObjects.CompareObjects();
                        if (!compareObjects.Compare(btnArray[0], btnArray[1]))
                        {
                            System.Diagnostics.Trace.WriteLine(compareObjects.DifferencesString);
                            Console.WriteLine(compareObjects.DifferencesString);
                        }
                        if (!compareObjects.Compare(btnArray[0], btnArray[2]))
                        {
                            System.Diagnostics.Trace.WriteLine(compareObjects.DifferencesString);
                            Console.WriteLine(compareObjects.DifferencesString);
                        }
                        if (!compareObjects.Compare(btnArray[0], btnArray[3]))
                        {
                            System.Diagnostics.Trace.WriteLine(compareObjects.DifferencesString);
                            Console.WriteLine(compareObjects.DifferencesString);
                        }
                        if (!compareObjects.Compare(btnArray[0], btnArray[4]))
                        {
                            System.Diagnostics.Trace.WriteLine(compareObjects.DifferencesString);
                            Console.WriteLine(compareObjects.DifferencesString);
                        }
                    }
                }
            }
#endif
        }
    }
}
