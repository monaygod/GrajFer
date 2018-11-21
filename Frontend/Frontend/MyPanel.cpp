#include "stdafx.h"
#include "MyPanel.h"
#include <qevent.h>

MyPanel::MyPanel(QWidget *parent)
	: QWidget(parent)
{
	ui.setupUi(this);

}

MyPanel::~MyPanel()
{
}



void MyPanel::mousePressEvent(QMouseEvent *event)
{
	offset_ = event->pos();
}

void MyPanel::mouseMoveEvent(QMouseEvent *event)
{
	if (event->buttons() & Qt::LeftButton)
	{
		this->move(mapToParent(event->pos() - offset_));
	}
}