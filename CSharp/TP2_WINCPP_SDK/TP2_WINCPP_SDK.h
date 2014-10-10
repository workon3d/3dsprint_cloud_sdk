// TP2_WINCPP_SDK.h

#pragma once
#include <vcclr.h>
#include "interop.h"
#include "StringConverter.h"
#include <msclr/auto_gcroot.h>

using namespace TeamPlatform::TP2_SDK;
using namespace GSWinApp;

namespace TeamPlatform { 
	namespace TP2_WINCPP_SDK {

		class BaseAPICore
		{
		public:
			BaseAPICore();
			BaseAPICore(const std::wstring& host);

		public:
			virtual std::wstring authenticate(std::wstring Email, std::wstring Password);

		protected:
			msclr::auto_gcroot<TeamPlatform::TP2_SDK::TP2^> m_tp2;
		};

		class PrinterAPICore : public BaseAPICore, public PrinterAPI
		{
		using PrinterAPI::authenticate;

		public:
			PrinterAPICore();
			PrinterAPICore(const std::wstring& host);
		
		public:
			std::wstring authenticate(std::wstring Email, std::wstring Password);
		};
	}
}
