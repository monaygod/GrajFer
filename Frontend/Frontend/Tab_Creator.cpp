#include "stdafx.h"
#include "Tab_Creator.h"
#include <qevent.h>
#include "MyPanel.h"
#include "ResizableFrame.h"

Tab_Creator::Tab_Creator(QWidget *parent)
	: QWidget(parent)
{
	ui.setupUi(this);

	MyPanel *p = new MyPanel(ui.frame);

	p->setObjectName(QStringLiteral("frame_3"));
	p->setGeometry(QRect(320, 90, 120, 80));
	p->setStyleSheet(QLatin1String("color: rgb(0, 255, 255);\n"
		"background-color: rgb(0, 255, 255);"));

	ResizableFrame *r = new ResizableFrame(ui.frame);
	r->setGeometry(QRect(320, 90, 120, 80));
	r->setStyleSheet(QLatin1String("color: rgb(0, 255, 255);\n"
		"background-color: rgb(0, 255, 255);"));

	ResizableFrame *r2 = new ResizableFrame(ui.frame);
	r2->setGeometry(QRect(320, 90, 120, 80));
	r2->setStyleSheet(QLatin1String("color: rgb(255, 255, 255);\n"
		"background-color: rgb(250, 255, 255);"));
}

Tab_Creator::~Tab_Creator()
{
}


void Tab_Creator::mousePressEvent(QMouseEvent *event)
{
	offset_ = event->pos();
}

void Tab_Creator::mouseMoveEvent(QMouseEvent *event)
{
	if (event->buttons() & Qt::LeftButton)
	{
		this->move(mapToParent(event->pos() - offset_));
	}
}