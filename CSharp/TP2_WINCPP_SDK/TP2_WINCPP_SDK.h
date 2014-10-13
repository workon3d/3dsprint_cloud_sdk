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

		class PrinterAPICore : public PrinterAPI
		{
		public:
			PrinterAPICore();
			PrinterAPICore(const std::wstring& host);
		
		protected:
			msclr::auto_gcroot<TeamPlatform::TP2_SDK::TpPrinter^> m_tp2;
		
		public:
			std::wstring authenticate(std::wstring Email, std::wstring Password);
			bool Create(std::wstring PrinterName, std::wstring MetaJson);
			void CreateAsync(std::wstring PrinterName, std::wstring MetaJson);
			void BatchUpdate(std::wstring DataJson);
		};
	}
}
