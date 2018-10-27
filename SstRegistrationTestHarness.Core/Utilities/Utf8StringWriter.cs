﻿using System.IO;
using System.Text;

namespace SstRegistrationTestHarness.Core.Utilities
{
    internal class Utf8StringWriter : StringWriter
    {
        public override Encoding Encoding => Encoding.UTF8;
    }
}