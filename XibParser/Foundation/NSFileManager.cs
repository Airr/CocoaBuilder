﻿/*
* XibParser.
* Copyright (C) 2013 Smartmobili SARL
* 
* This library is free software; you can redistribute it and/or
* modify it under the terms of the GNU Library General Public
* License as published by the Free Software Foundation; either
* version 2 of the License, or (at your option) any later version.
* 
* This library is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
* Library General Public License for more details.
* 
* You should have received a copy of the GNU Library General Public
* License along with this library; if not, write to the
* Free Software Foundation, Inc., 51 Franklin St, Fifth Floor,
* Boston, MA  02110-1301, USA. 
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Smartmobili.Cocoa
{
    public class NSDirectoryEnumerator : NSEnumerator
    {
        new public static Class Class = new Class(typeof(NSDirectoryEnumerator));
        new public static NSDirectoryEnumerator Alloc() { return new NSDirectoryEnumerator(); }

        public virtual id InitWithPath(NSString aPath)
        {
            id self = this;

            return self;
        }

        public override NSArray AllObjects()
        {
            throw new NotImplementedException();            
            return null;
        }

        public override id NextObject()
        {
            throw new NotImplementedException();

            return null;
        }

    }

    public class NSFileManager : NSObject
    {
        new public static Class Class = new Class(typeof(NSFileManager));
        new public static NSFileManager Alloc() { return new NSFileManager(); }

        private static NSFileManager _defaultFileMgr;


        public static NSFileManager DefaultManager
        {
            get
            {
                if (_defaultFileMgr == null)
                {
                    _defaultFileMgr = (NSFileManager)Alloc().Init();
                }

                return _defaultFileMgr;
            }
        }



        public virtual string FileSystemRepresentationWithPath(NSString path)
        {
            string fsPath;

            if (string.IsNullOrWhiteSpace(path))
                throw new Exception();

            fsPath = System.IO.Path.GetFullPath(path);

            return fsPath;
        }



        public virtual NSDictionary FileAttributesAtPath(NSString path, bool flag)
        {
            NSMutableDictionary attrs;
            NSString fileTypeKey = (NSString)"NSFileType";

            try
            {
                FileInfo info = new FileInfo(path);
                FileAttributes fileAttrs = info.Attributes;

                attrs = (NSMutableDictionary)NSMutableDictionary.Alloc().Init();
                
                    
                bool isDirectory = ((fileAttrs & FileAttributes.Directory) == FileAttributes.Directory);
                if (!isDirectory)
                {
                    bool isSymbolicLink = ((fileAttrs & FileAttributes.ReparsePoint) == FileAttributes.ReparsePoint);
                    if (isSymbolicLink)
                        attrs.SetObjectForKey((NSString)"NSFileTypeSymbolicLink", fileTypeKey);
                    else
                        attrs.SetObjectForKey((NSString)"NSFileTypeRegular", fileTypeKey);
                }
                else
                {
                    attrs.SetObjectForKey((NSString)"NSFileTypeDirectory", fileTypeKey);
                }

                attrs.SetObjectForKey((NSString)"NSFileSize", new NSNumber(info.Length));
            }
            catch (Exception)
            {
                attrs = null;
            }
            return attrs;
        }

        public virtual NSArray DirectoryContentsAtPath(NSString path)
        {
            NSArray items = null;

            try
            {
                DirectoryInfo diTop = new DirectoryInfo(path);
                items = (NSArray)NSArray.Alloc().Init();

                foreach (var fi in diTop.EnumerateFiles())
                {
                    items.AddObject((NSString)fi.Name);
                }
                foreach (var fi in diTop.EnumerateDirectories("*"))
                {
                    items.AddObject((NSString)fi.Name);
                }
            }
            catch (Exception)
            {
                items = null;
            }


            return items;
        }



        public virtual bool ChangeFileAttributes(NSDictionary attributes, NSString path)
        {
            return false;
        }

        public virtual NSString PathContentOfSymbolicLinkAtPath(NSString path)
        {
            return null;
        }


        public virtual bool IsWritableFileAtPath(NSString aPath)
        {
            bool isWrite = false;

            FileAttributes fileAttributes = File.GetAttributes(aPath);
            if ((fileAttributes & FileAttributes.ReadOnly) != FileAttributes.ReadOnly)
            {
                try
                {
                    FileStream currentWriteableFile = File.OpenWrite(aPath);
                    isWrite = true;
                }
                catch
                {
                    isWrite = false;
                }
            }

            return isWrite;
        }

        public virtual bool FileExistsAtPath(NSString aPath)
        {
            bool isDir = false;
            return FileExistsAtPath(aPath, ref isDir);
        }

        public virtual bool FileExistsAtPath(NSString aPath, ref bool isDirectory)
        {
            bool exists = false;

            if (System.IO.File.Exists(aPath))
            {
                exists = true;
                isDirectory = false;
            }
            else if (System.IO.Directory.Exists(aPath))
            {
                exists = true;
                isDirectory = true;
            }
            
            return exists;
        }


        public virtual bool CreateDirectoryAtPath(NSString path, bool createIntermediates, NSDictionary attributes, ref NSError error)
        {
            return false;
        }


        public virtual NSDirectoryEnumerator EnumeratorAtPath(NSString aPath)
        {
            return (NSDirectoryEnumerator)NSDirectoryEnumerator.Alloc().InitWithPath(aPath);
        }

    }
}
