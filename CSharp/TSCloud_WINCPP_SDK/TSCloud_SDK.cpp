#include "Stdafx.h"
#include "include/TSCloud_SDK.h"
#include "TSCloud_WINCPP_SDK.h"

using namespace TDSPRINT::Cloud::WINCPP_SDK;

namespace TDSPRINT{
	namespace Cloud{
		namespace WINCPP_SDK {
			PrinterAPI* createPrinterAPI(const std::wstring& Host)
			{
				return new PrinterAPICore(Host);
			}
		}
	}
}
