#pragma once

#include <QWidget>
#include "ui_Tab_Welcome.h"

class Tab_Welcome : public QWidget
{
	Q_OBJECT

public:
	Tab_Welcome(QWidget *parent = Q_NULLPTR);
	~Tab_Welcome();

private:
	Ui::Tab_Welcome ui;
};
