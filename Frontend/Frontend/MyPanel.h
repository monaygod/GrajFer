#pragma once
#include <QWidget>
#include "ui_MyPanel.h"

class MyPanel : public QWidget
{
	Q_OBJECT
		QPoint offset_;

public:
	MyPanel(QWidget *parent = Q_NULLPTR);
	~MyPanel();
	void mousePressEvent(QMouseEvent* event);

	void mouseMoveEvent(QMouseEvent * event);

private:
	Ui::MyPanel ui;
};
