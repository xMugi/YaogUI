using System;
using System.IO;
using System.Reflection;
using HarmonyLib;

namespace YaogUI
{
    public static class Main
	{
		private static bool Patched = false;
		public static void Patch()
		{
			try
			{
				Assembly.LoadFrom(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "0Harmony.dll"));
				Harmony harmony = new Harmony("YaogUI!");
				if (!Patched)
                {
					harmony.PatchAll();
					Patched = true;
				}
				YaogUIBinder.BindAll();
				Debug("Loaded!");
			}
			catch (Exception e)
			{
				Debug(e.ToString());
			}
		}
		public static void Debug(string message)
		{
			// The original code has an issue where an empty message is logged
			// if there is an exception but a bad format string
			if (string.IsNullOrEmpty(message))
			{
				KLog.Dbg("[YaogUI]An unknown error occurred.", new object[0]);
			}
			else
			{
				KLog.Dbg(string.Format("[YaogUI]{0}", message), new object[0]);
			}
		}
	}
}
