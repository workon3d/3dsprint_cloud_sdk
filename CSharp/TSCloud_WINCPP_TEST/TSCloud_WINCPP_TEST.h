
// TP2_WINCPP_TEST.h : main header file for the PROJECT_NAME application
//

#pragma once

#ifndef __AFXWIN_H__
	#error "include 'stdafx.h' before including this file for PCH"
#endif

#include "resource.h"		// main symbols


// CTP2_WINCPP_TESTApp:
// See TP2_WINCPP_TEST.cpp for the implementation of this class
//

class CTP2_WINCPP_TESTApp : public CWinApp
{
public:
	CTP2_WINCPP_TESTApp();

// Overrides
public:
	virtual BOOL InitInstance();

// Implementation

	DECLARE_MESSAGE_MAP()
};

extern CTP2_WINCPP_TESTApp theApp;