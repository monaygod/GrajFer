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
	void keyPressEvent(QKeyEvent * event) override;
	void mousePressEvent(QMouseEvent* event) override;
	void mouseMoveEvent(QMouseEvent * event) override;

private:
	Ui::Tab_Creator ui;
};
