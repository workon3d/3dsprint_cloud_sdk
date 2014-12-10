// This is the main DLL file.

#include "stdafx.h"

#include "TSCloud_WINCPP_SDK.h"
#include "json.h"


namespace TDSPRINT{
	namespace Cloud{ 
		namespace WINCPP_SDK {

			BaseAPICore::BaseAPICore()
			{
				m_TSCloud = gcnew TDSPRINT::Cloud::SDK::TSCloud();
			}

			BaseAPICore::BaseAPICore(const std::wstring& host)
			{
				m_TSCloud = gcnew TDSPRINT::Cloud::SDK::TSCloud();
				if (!host.empty())
					m_TSCloud->Hostname = StringConverter::nativeToManaged(host);
			}

			std::wstring BaseAPICore::authenticate(std::wstring Email, std::wstring Password)
			{
				msclr::auto_gcroot<TDSPRINT::Cloud::SDK::Datas::User^> user = m_TSCloud->authenticate(
					StringConverter::nativeToManaged(Email),
					StringConverter::nativeToManaged(Password));

				if (user->ApiToken && user->ApiToken->Length != 0) {
					Json::Value result;
					result["id"] = user->Id;
					result["api_token"] = StringConverter::ws2s(StringConverter::managedToNative(user->ApiToken));
					result["email"] = StringConverter::ws2s(StringConverter::managedToNative(user->Email));

					Json::FastWriter writer;
					std::string s = writer.write(result);
					return StringConverter::s2ws(s);
				}
	
				return std::wstring(L"{}");
			}

			/////////////////////
			PrinterAPICore::PrinterAPICore()
			{
				m_TSCloud = gcnew TDSPRINT::Cloud::SDK::PrinterClient();
			}

			PrinterAPICore::PrinterAPICore(const std::wstring& host)
			{
				m_TSCloud = gcnew TDSPRINT::Cloud::SDK::PrinterClient();
				if (!host.empty())
					m_TSCloud->Hostname = StringConverter::nativeToManaged(host);
			}

			std::wstring PrinterAPICore::authenticate(std::wstring Email, std::wstring Password)
			{
				msclr::auto_gcroot<TDSPRINT::Cloud::SDK::Datas::User^> user = m_TSCloud->authenticate(
					StringConverter::nativeToManaged(Email),
					StringConverter::nativeToManaged(Password));

				if (user->ApiToken && user->ApiToken->Length != 0) {
					Json::Value result;
					result["id"] = user->Id;
					result["api_token"] = StringConverter::ws2s(StringConverter::managedToNative(user->ApiToken));
					result["email"] = StringConverter::ws2s(StringConverter::managedToNative(user->Email));

					Json::FastWriter writer;
					std::string s = writer.write(result);
					return StringConverter::s2ws(s);
				}
	
				return std::wstring(L"{}");
			}

			std::wstring PrinterAPICore::authenticate(std::wstring ApiToken)
			{
				msclr::auto_gcroot<TDSPRINT::Cloud::SDK::Datas::User^> user = m_TSCloud->authenticate(StringConverter::nativeToManaged(ApiToken));

				if (user->ApiToken && user->ApiToken->Length != 0) {
					Json::Value result;
					result["id"] = user->Id;
					result["api_token"] = StringConverter::ws2s(StringConverter::managedToNative(user->ApiToken));
					result["email"] = StringConverter::ws2s(StringConverter::managedToNative(user->Email));

					Json::FastWriter writer;
					std::string s = writer.write(result);
					return StringConverter::s2ws(s);
				}
	
				return std::wstring(L"{}");
			}

			bool PrinterAPICore::Create(std::wstring PrinterName, std::wstring MetaJson)
			{
				msclr::auto_gcroot<TDSPRINT::Cloud::SDK::Datas::Printer^> printer = m_TSCloud->Create(
					StringConverter::nativeToManaged(PrinterName), 
					StringConverter::nativeToManaged(MetaJson));
				return ((int)(printer->StatusCode) == 200/*HttpStatusCode::OK*/) ? true : false;
			}

			void PrinterAPICore::CreateAsync(std::wstring PrinterName, std::wstring MetaJson)
			{
				m_TSCloud->CreateAsync(
					StringConverter::nativeToManaged(PrinterName), 
					StringConverter::nativeToManaged(MetaJson));
			}

			void PrinterAPICore::BatchUpdate(std::wstring DataJson)
			{
				m_TSCloud->BatchUpdate(StringConverter::nativeToManaged(DataJson));
			}
		}
	}
}