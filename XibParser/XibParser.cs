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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Xml.XPath;
using System.IO;
using System.Xml;

namespace Smartmobili.Cocoa
{
    public class XibParser
    {
        string _nibPath = string.Empty;

        public XibParser(string nibPath)
        {
            _nibPath = nibPath;
           
        }

        public IBArchive Deserialize()
        {
            NSData data = NSData.Alloc().InitWithContentsOfFile(_nibPath);
            if (data != null)
            {
                var xibUna = WSXibKeyedUnarchiver.Alloc().InitForReadingWithData(data);
            }



            IBArchive ibArchive = new IBArchive();

            var reader = new StringReader(_nibPath);
            XDocument xDoc = XDocument.Load(_nibPath);
            if (xDoc.Root.Name == "archive")
            {
                ibArchive = IBArchive.Parse(xDoc);
                
            }

            return ibArchive;
        }

    }
}
