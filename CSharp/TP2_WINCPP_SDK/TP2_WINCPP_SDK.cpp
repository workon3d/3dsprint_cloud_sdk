// This is the main DLL file.

#include "stdafx.h"

#include "TP2_WINCPP_SDK.h"

namespace TeamPlatform { 
	namespace TP2_WINCPP_SDK {
		BaseAPICore::BaseAPICore()
		{
			m_tp2 = gcnew TeamPlatform::TP2_SDK::TP2();
		}

		BaseAPICore::BaseAPICore(const std::wstring& host)
		{
			m_tp2 = gcnew TeamPlatform::TP2_SDK::TP2();
			if (!host.empty())
				m_tp2->Tp2Host = StringConverter::nativeToManaged(host);
		}

		std::wstring BaseAPICore::authenticate(std::wstring Email, std::wstring Password)
		{
			msclr::auto_gcroot<TeamPlatform::TP2_SDK::Datas::User^> user = m_tp2->authenticate(
				StringConverter::nativeToManaged(Email),
				StringConverter::nativeToManaged(Password));

			if (user->api_token && user->api_token->Length != 0)
				return StringConverter::managedToNative(user->api_token);

			return std::wstring(L"");
		}

		/////////////////////
		PrinterAPICore::PrinterAPICore()
		{
			m_tp2 = gcnew TeamPlatform::TP2_SDK::TpPrinter();
		}

		PrinterAPICore::PrinterAPICore(const std::wstring& host)
		{
			m_tp2 = gcnew TeamPlatform::TP2_SDK::TpPrinter();
			if (!host.empty())
				m_tp2->Tp2Host = StringConverter::nativeToManaged(host);
		}

		std::wstring PrinterAPICore::authenticate(std::wstring Email, std::wstring Password)
		{
			return BaseAPICore::authenticate(Email, Password);
		}
	}
}