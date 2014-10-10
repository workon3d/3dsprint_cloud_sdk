#pragma once

#include <vcclr.h>
#include <string>
#include <vector>

using namespace System;
using namespace System::Collections;
using namespace System::Diagnostics;
using namespace System::Runtime::InteropServices;

namespace GSWinApp 
{
	public ref class ManagedStringArray : public System::Collections::IEnumerable
	{
	public:
		virtual System::Collections::IEnumerator^ GetEnumerator()
		{
			return strings_.GetEnumerator();
		}

	public:
		ArrayList strings_;
	};

	public ref class StringConverter
	{
	public:
		static String^ nativeToManaged(const std::wstring& nativeString)
		{
			IntPtr ip((void *)nativeString.c_str());
			return Marshal::PtrToStringUni(ip);
		}

		static ManagedStringArray^ nativeToManaged(const std::vector<std::wstring>& nativeUtf8Strings)
		{
			ManagedStringArray^ managedStrings = gcnew ManagedStringArray();
			for (auto it = nativeUtf8Strings.begin(); it != nativeUtf8Strings.end(); ++it)
			{
				managedStrings->strings_.Add(nativeToManaged(*it));
			}
			return managedStrings;
		}

		// Caller should free returned native string
		static std::wstring managedToNative(String^ managedString)
		{
			wchar_t* wc = static_cast<wchar_t*>(Marshal::StringToHGlobalUni(managedString).ToPointer());
			std::wstring wstr(wc);
			IntPtr ip((void*)wc);
			Marshal::FreeHGlobal(ip);
			return wstr;
		}

		static std::vector<std::wstring> managedToNative(ManagedStringArray^ managedStrings)
		{
			std::vector<std::wstring> strings;
			System::Collections::IEnumerator^ ienum = managedStrings->GetEnumerator();

			while (ienum->MoveNext())
			{
				String^ obj = safe_cast<String^>(ienum->Current);
				strings.push_back(managedToNative(obj));
			}
			return strings;
		}

	};
}
