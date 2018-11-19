#include "stdafx.h"
#include "Tab_Creator.h"
#include <qevent.h>
#include "MyPanel.h"
#include "ResizableFrame.h"
#include <iostream>

Tab_Creator::Tab_Creator(QWidget *parent)
	: QWidget(parent)
{
	ui.setupUi(this);
	//QKeyEvent::accept();
	QWidget::setFocusPolicy(Qt::ClickFocus);

	
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

void Tab_Creator::keyPressEvent(QKeyEvent * event)
{
	//qDebug() << "klik a";
	if (event->key() == Qt::Key_A)
	{
		
		QObjectList pList = ui.frame->children();
		for (auto p : pList.toVector())
		{
			p->setObjectName("myObject");
			this->setStyleSheet("#myObject { border: 1px solid crimson; }");
			qDebug() << "Obslugujemy takze klawiature";
			//(ResizableFrame)p -> setStyleSheet("#myObject { border: 5px solid black; }");
		}
		/*
		QListIterator<float> i(ui.frame->children());
		while (i.hasNext())
		{
			qDebug() << i.next();
		}
			*/

	}
}


void Tab_Creator::mousePressEvent(QMouseEvent *event)
{
	offset_ = event->pos();
	if (event->buttons() & Qt::RightButton)
	{
		qDebug() << "noelo";
		this->move(mapToParent(event->pos() - offset_));
		ResizableFrame *r2 = new ResizableFrame(ui.frame);
		r2->setGeometry(QRect(event->pos().x(), event->pos().y(), 120, 80));
		r2->setStyleSheet(QLatin1String("color: rgb(255, 255, 255);\n"
			"background-color: rgb(250, 255, 255);")); 
		r2->show();
	}
}

void Tab_Creator::mouseMoveEvent(QMouseEvent *event)
{
	if (event->buttons() & Qt::LeftButton)
	{
		this->move(mapToParent(event->pos() - offset_));
	}
}