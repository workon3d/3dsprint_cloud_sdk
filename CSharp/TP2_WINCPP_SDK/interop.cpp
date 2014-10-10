#include "Stdafx.h"
#include "interop.h"
#include "TP2_WINCPP_SDK.h"

using namespace TeamPlatform::TP2_WINCPP_SDK;

namespace TeamPlatform {
	namespace TP2_WINCPP_SDK {
		PrinterAPI* createPrinterAPI(const std::wstring& Host)
		{
			return new PrinterAPICore(Host);
		}
	}
}
