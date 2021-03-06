#include "stdafx.h"
#include "CreaDiccionarioTeclas.h"
#include "CreaDiccionarioTeclasDlg.h"
#include "afxdialogex.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#endif

CCreaDiccionarioTeclasDlg::CCreaDiccionarioTeclasDlg(CWnd* pParent /*=nullptr*/)
	: CDialogEx(IDD_CREADICCIONARIOTECLAS_DIALOG, pParent)
{
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
}

void CCreaDiccionarioTeclasDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_LISTA, m_lista);
}

BEGIN_MESSAGE_MAP(CCreaDiccionarioTeclasDlg, CDialogEx)
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDOK, &CCreaDiccionarioTeclasDlg::OnBnClickedOk)
END_MESSAGE_MAP()

BOOL CCreaDiccionarioTeclasDlg::OnInitDialog()
{
	CDialogEx::OnInitDialog();

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

	m_lista.InsertColumn(0, L"Código", 0, 50);
	m_lista.InsertColumn(1, L"Tecla", 0, 100);
	m_lista.InsertColumn(2, L"Control", 0, 50);
	m_lista.InsertColumn(3, L"Mayúsculas", 0, 50);
	m_lista.InsertColumn(4, L"Alt", 0, 50);

	return TRUE;  // return TRUE  unless you set the focus to a control
}

BOOL CCreaDiccionarioTeclasDlg::PreTranslateMessage(MSG* pMsg)
{
	if (pMsg->message != WM_KEYDOWN && pMsg->message != WM_SYSKEYDOWN)
		return __super::PreTranslateMessage(pMsg);

	const auto nTecla = pMsg->wParam;
	UINT nFlags = (pMsg->lParam & 0xFFFF0000) >> 16;

	// Algunas teclas no nos interesan
	switch (nTecla) 
	{
	case VK_SHIFT:
	case VK_CONTROL:
	case VK_MENU:
	case VK_PAUSE:
	case VK_CAPITAL:
		return __super::PreTranslateMessage(pMsg);
	default:
		break;
	}

	const auto bMayusculas = (0x80 & GetKeyState(VK_SHIFT)) ? true : false;
	const auto bControl = (0x80 & GetKeyState(VK_CONTROL)) ? true : false;
	const auto bAlt = (0x80 & GetKeyState(VK_MENU)) ? true : false;
	UINT nTeclaDigiDos = 0;
	BYTE nChar = 0;

	if (::TranslateMessage(pMsg)) {
		MSG msg;

		if (PeekMessage(&msg, pMsg->hwnd, WM_CHAR, WM_CHAR, PM_REMOVE)) {
			nChar = msg.wParam;
			nTeclaDigiDos = msg.wParam;
		}
		else {
			if ((nTecla >= VK_F1 && nTecla <= VK_F12) ||
				(nTecla >= VK_SPACE && nTecla <= VK_HELP)) {
				if (bMayusculas) nFlags += 25;
				else if (bControl) nFlags += 35;
				else if (bAlt) nFlags += 45;
			}
			nTeclaDigiDos = 0;
		}
	}

	const int nScanCode = (nFlags & 0xFF) << 8;
	nTeclaDigiDos |= nScanCode;

	TCHAR nombreTecla[256];
	GetKeyNameText(pMsg->lParam, nombreTecla, 256);

	CString cadena;
	cadena.Format(L"%d", nTeclaDigiDos);

	const auto nItem = m_lista.InsertItem(0, cadena);
	m_lista.SetItemText(nItem, 1, nombreTecla);

	m_lista.SetItemText(nItem, 2, bControl ? L"Sí" : L"No");
	m_lista.SetItemText(nItem, 3, bMayusculas ? L"Sí" : L"No");
	m_lista.SetItemText(nItem, 4, bAlt ? L"Sí" : L"No");

	if (m_teclas.count(nTeclaDigiDos) == 0)
		m_teclas[nTeclaDigiDos] = std::vector< std::tuple<CString, bool, bool, bool>>();

	const auto tupla = std::make_tuple(CString(nombreTecla), bControl, bMayusculas, bAlt);

	if( m_teclas[nTeclaDigiDos].end() == std::find(m_teclas[nTeclaDigiDos].begin(), m_teclas[nTeclaDigiDos].end(), tupla))
		m_teclas[nTeclaDigiDos].push_back(tupla);

	return TRUE;
}

void CCreaDiccionarioTeclasDlg::OnBnClickedOk()
{
	CFileDialog dlg(FALSE, L".cs", nullptr, OFN_ENABLESIZING, L"Archivos de C#|*.cs||", this);
	if(IDOK != dlg.DoModal()) {
		return;
	}

	CStdioFile os;
	if( !os.Open(dlg.GetPathName(), CFile::modeCreate | CFile::modeWrite))
	{
		AfxMessageBox(L"No se pudo crear el archivo");
		return;
	}

	CString cadena;
	os.WriteString(L"private static readonly Dictionary<int, List<DatosTecla>> Diccionario = new Dictionary<int, List<DatosTecla>>	{");
	for(const auto teclaDos : m_teclas)
	{
		cadena.Format(L"{\n\t%d, new List<DatosTecla> {\n",
			teclaDos.first);
		os.WriteString(cadena);

		for (auto tecla : teclaDos.second) {
			cadena.Format(L"\t\tnew DatosTecla(\"%s\", %s, %s, %s),\n",
				std::get<0>(tecla),
				std::get<1>(tecla) ? L"true" : L"false",
				std::get<2>(tecla) ? L"true" : L"false",
				std::get<3>(tecla) ? L"true" : L"false");
			os.WriteString(cadena);
		}

		os.WriteString(L"\t}},\n");
	}
	os.WriteString(L"};");

	CDialogEx::OnOK();
}
