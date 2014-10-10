#pragma once

#ifdef TP2_EXTERN
#define TP2_API __declspec(dllexport)
#else
#define TP2_API __declspec(dllimport)
#endif

#include <string>

namespace TeamPlatform { 
	namespace TP2_WINCPP_SDK {
		class PrinterAPI
		{
		public:
			PrinterAPI() {}
			virtual ~PrinterAPI() {}

			virtual std::wstring authenticate(std::wstring Email, std::wstring Password) = 0;
		};

		TP2_API TeamPlatform::TP2_WINCPP_SDK::PrinterAPI* createPrinterAPI(const std::wstring& Host);
	}
}


