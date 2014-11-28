// TP2_WINCPP_SDK.h

#pragma once
#include <vcclr.h>
#include "include/TSCloud_SDK.h"
#include "StringConverter.h"
#include <msclr/auto_gcroot.h>

using namespace TDSPRINT::Cloud::SDK;
using namespace GSWinApp;

namespace TDSPRINT{ 
	namespace Cloud{
		namespace WINCPP_SDK {

			class BaseAPICore
			{
			public:
				BaseAPICore();
				BaseAPICore(const std::wstring& host);

			public:
				virtual std::wstring authenticate(std::wstring Email, std::wstring Password);

			protected:
				msclr::auto_gcroot<TDSPRINT::Cloud::SDK::TSCloud^> m_TSCloud;
			};

			class PrinterAPICore : public PrinterAPI
			{
			public:
				PrinterAPICore();
				PrinterAPICore(const std::wstring& host);
		
			protected:
				msclr::auto_gcroot<TDSPRINT::Cloud::SDK::PrinterClient^> m_tp2;
		
			public:
				std::wstring authenticate(std::wstring Email, std::wstring Password);
				std::wstring authenticate(std::wstring ApiToken);
				bool Create(std::wstring PrinterName, std::wstring MetaJson);
				void CreateAsync(std::wstring PrinterName, std::wstring MetaJson);
				void BatchUpdate(std::wstring DataJson);
			};
		}
	}
}
