
// TP2_WINCPP_TESTDlg.cpp : implementation file
//

#include "stdafx.h"
#include "TSCloud_WINCPP_TEST.h"
#include "TSCloud_WINCPP_TESTDlg.h"
#include "afxdialogex.h"

#include <string>
#include "../TSCloud_WINCPP_SDK/include/TSCloud_SDK.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

using namespace TDSPRINT::Cloud::WINCPP_SDK;

// CTP2_WINCPP_TESTDlg dialog




CTP2_WINCPP_TESTDlg::CTP2_WINCPP_TESTDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(CTP2_WINCPP_TESTDlg::IDD, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CTP2_WINCPP_TESTDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}

BEGIN_MESSAGE_MAP(CTP2_WINCPP_TESTDlg, CDialogEx)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
END_MESSAGE_MAP()


// CTP2_WINCPP_TESTDlg message handlers

BOOL CTP2_WINCPP_TESTDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

	// TODO: Add extra initialization here
	TDSPRINT::Cloud::WINCPP_SDK::PrinterAPI* test = TDSPRINT::Cloud::WINCPP_SDK::createPrinterAPI(std::wstring(L"http://192.168.1.163:3000"));
//	std::wstring result = test->authenticate();
	std::wstring email = test->authenticate(std::wstring(L"be595d04ae9a1222c6b031ad9eb36132"));
	test->CreateAsync(std::wstring(L"Test_PC"), std::wstring(L"{\"id\": \"00-26-B9-E2-FA-67\", \"name\": \"test\"}"));
//	test->BatchUpdate(std::wstring(L"{\"printers\":[{\"complete_percentage\":100,\"device_type\":2005,\"engine_type\":3,\"id\":\"00-26-B9-E2-FA-68\",\"ip\":\"192.168.2.3\",\"material\":\"\",\"model_name\":\"Projet 360\",\"name\":\"VISPOWER-PC\",\"queue\":[{\"create_time\":\"2014-10-07T23:30:04Z\",\"filename\":\"\",\"id\":\"00-26-B9-E2-FA-68-cylinder_20141007162943\",\"material\":\"\",\"name\":\"cylinder_20141007162943\",\"remaining_time\":0,\"status\":0},{\"create_time\":\"2014-10-07T23:27:56Z\",\"filename\":\"\",\"id\":\"00-26-B9-E2-FA-68-cylinder_20141007152000\",\"material\":\"\",\"name\":\"cylinder_20141007152000\",\"remaining_time\":0,\"status\":2}],\"remaining_time\":0,\"status\":1}],\"update_time\":\"2014-10-08T22:24:15Z\"}"));

	return TRUE;  // return TRUE  unless you set the focus to a control
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CTP2_WINCPP_TESTDlg::OnPaint()
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, reinterpret_cast<WPARAM>(dc.GetSafeHdc()), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialogEx::OnPaint();
	}
}

// The system calls this function to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CTP2_WINCPP_TESTDlg::OnQueryDragIcon()
{
	return static_cast<HCURSOR>(m_hIcon);
}

