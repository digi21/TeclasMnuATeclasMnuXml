#pragma once

class CCreaDiccionarioTeclasDlg : public CDialogEx
{
	HICON m_hIcon;
	CListCtrl m_lista;
	std::map<UINT, std::vector<std::tuple<CString, bool, bool, bool>>> m_teclas;
public:
	CCreaDiccionarioTeclasDlg(CWnd* pParent = nullptr);	// standard constructor

#ifdef AFX_DESIGN_TIME
	enum { IDD = IDD_CREADICCIONARIOTECLAS_DIALOG };
#endif

	protected:
	void DoDataExchange(CDataExchange* pDX) override;	// DDX/DDV support

	// Generated message map functions
	BOOL OnInitDialog() override;
	BOOL PreTranslateMessage(MSG* pMsg) override;

	afx_msg void OnBnClickedOk();
	DECLARE_MESSAGE_MAP()
};
