#pragma once

#ifndef __AFXWIN_H__
	#error "include 'stdafx.h' before including this file for PCH"
#endif

#include "resource.h"		// main symbols

class CCreaDiccionarioTeclasApp : public CWinApp
{
public:
	CCreaDiccionarioTeclasApp();

	BOOL InitInstance() override;

	DECLARE_MESSAGE_MAP()
};

extern CCreaDiccionarioTeclasApp theApp;
