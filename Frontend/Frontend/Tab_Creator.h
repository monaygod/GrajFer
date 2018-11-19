#pragma once

#include <QWidget>
#include "ui_Tab_Creator.h"

class Tab_Creator : public QWidget
{
	Q_OBJECT
	QPoint offset_;

public:
	Tab_Creator(QWidget *parent = Q_NULLPTR);
	~Tab_Creator();
	void mousePressEvent(QMouseEvent* event);

	void mouseMoveEvent(QMouseEvent * event);

private:
	Ui::Tab_Creator ui;
};
