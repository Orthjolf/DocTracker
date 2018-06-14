using System;
using System.Runtime.InteropServices;

namespace WpfApp.Scanning
{
	/// <summary>
	/// Класс, перехватывающий на низком уровне события клавиатуры.
	/// Перехват происходит независимо от того, какое приложение в данный момент находится в фокусе
	/// </summary>
	internal class KeyboardHookHelper
	{
		private const int WhKeyboardLl = 13;

		private LowLevelKeyboardProcDelegate _keyboardProcDelegate;

		private IntPtr _mHHook;

		[DllImport("user32.dll", SetLastError = true)]
		private static extern IntPtr SetWindowsHookEx(
			int idHook,
			LowLevelKeyboardProcDelegate lpfn,
			IntPtr hMod, int dwThreadId);

		[DllImport("user32.dll", SetLastError = true)]
		private static extern bool UnhookWindowsHookEx(IntPtr hhk);

		[DllImport("Kernel32.dll", SetLastError = true)]
		private static extern IntPtr GetModuleHandle(IntPtr lpModuleName);

		[DllImport("user32.dll", SetLastError = true)]
		private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

		private IntPtr LowLevelKeyboardHookProc(int nCode, IntPtr wParam, IntPtr lParam)
		{
			if (nCode < 0)
			{
				return CallNextHookEx(_mHHook, nCode, wParam, lParam);
			}

			var keyboardHookStruct = (KeyboardHookStruct) Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));
			BarCodeListener.KeyCodeEntered(keyboardHookStruct.VirtualKeyCode);
			return CallNextHookEx(_mHHook, nCode, wParam, lParam);
		}


		[StructLayout(LayoutKind.Sequential)]
		private struct KeyboardHookStruct
		{
			public readonly int VirtualKeyCode;
		}


		private delegate IntPtr LowLevelKeyboardProcDelegate(
			int nCode, IntPtr wParam, IntPtr lParam);


		public void SetHook()
		{
			_keyboardProcDelegate = LowLevelKeyboardHookProc;

			_mHHook = SetWindowsHookEx(WhKeyboardLl,
				_keyboardProcDelegate,
				GetModuleHandle(IntPtr.Zero), 0);
		}


		public void Unhook()
		{
			UnhookWindowsHookEx(_mHHook);
		}
	}
}