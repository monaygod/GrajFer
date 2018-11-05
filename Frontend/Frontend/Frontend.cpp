#include "stdafx.h"
#include "Frontend.h"

Frontend::Frontend(QWidget *parent)
	: QMainWindow(parent)
{
	ui.setupUi(this);
}

void Frontend::on_Plus_clicked()
{
	int value = ui.progressBar->value();
	value = value < 100 ? value + 1 : value;
	ui.progressBar->setValue(value);
}

void Frontend::on_Minus_clicked()
{
	int value = ui.progressBar->value();
	value = value > 0 ? value - 1 : value;
	ui.progressBar->setValue(value);
}

void Frontend::on_pushButton_clicked()
{
	
}