#pragma once

#include <QWidget>
#include "ui_Front2.h"

class Front2 : public QWidget
{
	Q_OBJECT

public:
	Front2(QWidget *parent = Q_NULLPTR);
	~Front2();

private:
	Ui::Front2 ui;
};
