#include "stdafx.h"
#include "Frontend.h"
#include "Tab_Welcome.h"
#include "Tab_Creator.h"
#include "DockTest.h"

Frontend::Frontend(QWidget *parent)
	: QMainWindow(parent)
{
	ui.setupUi(this);

	auto *WelcomeTab = new Tab_Welcome();
	ui.tabWidget->addTab(WelcomeTab, QString("Welcome"));

	auto *CreatorTab = new Tab_Creator();
	ui.tabWidget->addTab(CreatorTab, QString("Creator"));

	auto *DockTest1 = new DockTest();
	ui.tabWidget->addTab(DockTest1, QString("DockTest"));


	ui.tabWidget->setTabsClosable(true);
	connect(ui.tabWidget, SIGNAL(tabCloseRequested(int)), this, SLOT(closeTab_(int)));
}

void Frontend::closeTab_(int index)
{
	ui.tabWidget->removeTab(index);
}

void Frontend::test_server(int)
{
}
