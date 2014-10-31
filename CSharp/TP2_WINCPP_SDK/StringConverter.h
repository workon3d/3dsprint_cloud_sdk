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

		static std::wstring s2ws(const std::string& s)
		{
			std::wstring ws(s.size(), L' '); // Overestimate number of code points.
			ws.resize(mbstowcs(&ws[0], s.c_str(), s.size())); // Shrink to fit.
			return ws;
		}

		static std::string ws2s(const std::wstring& wstr)
		{
			if (wstr.length() == 0)
				return std::string("");

			const std::locale locale("");
			typedef std::codecvt<wchar_t, char, std::mbstate_t> converter_type;
			const converter_type& converter = std::use_facet<converter_type>(locale);
			std::vector<char> to(wstr.length() * converter.max_length());
			std::mbstate_t state;
			const wchar_t* from_next;
			char* to_next;
			const converter_type::result result = converter.out(state, wstr.data(), wstr.data() + wstr.length(), from_next, &to[0], &to[0] + to.size(), to_next);
			if (result == converter_type::ok || result == converter_type::noconv) {
				const std::string s(&to[0], to_next);
				return s;
			}

			return std::string("");
		}

	};
}
