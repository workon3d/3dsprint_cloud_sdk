
// TP2_WINCPP_TESTDlg.h : header file
//

#pragma once


// CTP2_WINCPP_TESTDlg dialog
class CTP2_WINCPP_TESTDlg : public CDialogEx
{
// Construction
public:
	CTP2_WINCPP_TESTDlg(CWnd* pParent = NULL);	// standard constructor

// Dialog Data
	enum { IDD = IDD_TP2_WINCPP_TEST_DIALOG };

	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support


// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	virtual BOOL OnInitDialog();
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	DECLARE_MESSAGE_MAP()
};
