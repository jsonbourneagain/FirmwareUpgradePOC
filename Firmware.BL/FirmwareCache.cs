using System;
using System.Runtime.Caching;

namespace Firmware.BL
{
    public sealed class FirmwareCache
    {
        private static readonly FirmwareCache firmwareCache = new FirmwareCache();

        private FirmwareCache() { }


        public static object AddOrGetFirmware(string key, PackageFile packageFile)
        {
            object fw = MemoryCache.Default.AddOrGetExisting(key, packageFile, DateTime.Now.AddMinutes(30));
            return fw;
        }
        public static void DeleteFromMemoryCache(string key)
        {
            MemoryCache.Default.Remove(key);
        }
    }
}
