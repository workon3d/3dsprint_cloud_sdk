#pragma once

#ifdef TP2_EXTERN
#define TP2_API __declspec(dllexport)
#else
#define TP2_API __declspec(dllimport)
#endif

#include <string>

namespace TDSPRINT{
	namespace Cloud{
		namespace WINCPP_SDK {
			class PrinterAPI
			{
			public:
				PrinterAPI() {}
				virtual ~PrinterAPI() {}

				virtual std::wstring authenticate(std::wstring Email, std::wstring Password) = 0;
				virtual std::wstring authenticate(std::wstring ApiToken) = 0;
				virtual bool Create(std::wstring PrinterName, std::wstring MetaJson) = 0;
				virtual void CreateAsync(std::wstring PrinterName, std::wstring MetaJson) = 0;
				virtual void BatchUpdate(std::wstring DataJson) = 0;
			};

			TP2_API TDSPRINT::Cloud::WINCPP_SDK::PrinterAPI* createPrinterAPI(const std::wstring& Host);
		}
	}
}


