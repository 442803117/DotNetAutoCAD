﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace RegistryTools {
public class RegistryHelper {

    public static RegistryKey GetRegistryKey() {
        return GetRegistryKey(null);
    }

    public static RegistryKey GetRegistryKey(string keyPath) {
        RegistryKey localMachineRegistry
            = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine,
                                      Environment.Is64BitOperatingSystem
                                      ? RegistryView.Registry64
                                      : RegistryView.Registry32);

        return string.IsNullOrEmpty(keyPath)
               ? localMachineRegistry
               : localMachineRegistry.OpenSubKey(keyPath);
    }

    public static object GetRegistryValue(string keyPath, string keyName) {
        RegistryKey registry = GetRegistryKey(keyPath);
        return registry.GetValue(keyName);
    }
}
}
