#pragma once

#include <QtWidgets/QMainWindow>
#include "ui_Frontend.h"

class Frontend : public QMainWindow
{
	Q_OBJECT

public:
	Frontend(QWidget *parent = Q_NULLPTR);

private:
	Ui::FrontendClass ui;

private slots:
	void closeTab_(int);
};
