#pragma once

#include <QWidget>
#include "ui_Tab_Creator.h"

class Tab_Creator : public QWidget
{
	Q_OBJECT

public:
	Tab_Creator(QWidget *parent = Q_NULLPTR);
	~Tab_Creator();

private:
	Ui::Tab_Creator ui;
};
