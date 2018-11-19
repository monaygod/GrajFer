#pragma once

#include <QMainWindow>
#include "ui_DockTest.h"

class DockTest : public QMainWindow
{
	Q_OBJECT

public:
	DockTest(QWidget *parent = Q_NULLPTR);
	~DockTest();

private:
	Ui::DockTest ui;
};
